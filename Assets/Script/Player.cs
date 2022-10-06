using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Animator animator;  //キャラクターのアニメーター

    [SerializeField]
    CharacterController controller;  //キャラクターのコントローラー

    [SerializeField]
    Transform endPos;  //終点座標
    [SerializeField]
    float flightTime = 2;  //滞空時間
    [SerializeField]
    float speedRate = 1;   //滞空時間を基準とした移動速度倍率

    [SerializeField]
    GameObject camera1;
    [SerializeField]
    GameObject camera2;


    [Tooltip(" キャラクターの移動カメラの回転速度です。")]
    public float rotateSpeed = 2.0f;

    [Tooltip(" キャラクターの移動速度です。")]
    public float speed;

    [Tooltip(" キャラクターの移動速度リセット用変数　speedと同じ値にすること")]
    public float ResetSpeed;

    [Tooltip(" キャラクターのダッシュ移動速度です。")]
    public float DashSpeed;

    [Tooltip(" キャラクターの左右ダッシュ移動速度です。")]
    public float LR_DashSpeed;

    [Tooltip(" キャラクターのジャンプ速度です。")]
    public float JumpSpeed = 3;

    [Tooltip(" キャラクターのローテーション速度です。")]
    public float rotas;

    [Tooltip(" キャラクターの受ける重力です。")]
    public float gravity = 1;

    [Tooltip(" キャラクターの移動の基準となるカメラです。")]
    public Transform MainCamera;

    private Vector3 moveDirection = Vector3.zero;
    private float second;   //アニメーション切り替えを時間によって行うための変数

    private int GetMoveKey = -1;    //キーが押されたかの判定用変数
    Quaternion targetRotation;

    private void Awake()
    {
        TryGetComponent(out animator);
        targetRotation = transform.rotation; //初期化
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    float fwd, side, camY;
    Vector3 movementSpeed, rot;
    Quaternion rotation;

  // private void LateUpdate()
  // {
  //     camY = MainCamera.eulerAngles.y;
  //     rot = new Vector3(0, camY, 0);
  //     rotation = Quaternion.Euler(rot);
  //     transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 180);
  //
  // }
  //
    //アニメーション設定メソッド
    private void AnimationSetting(float second, int MoveFlag)
    {
        //------- アニメーション再生フラグの設定---------

        if (Input.GetKeyDown("space")) //Spaceキーが押されたとき
        {
            second = 0; //secondの初期化

            animator.SetBool("jumpFlag", true);      //Jumpフラグをtureに
            animator.SetBool("walkFlag", false);    //walkフラグをfalseに
            animator.SetBool("idleFlag", false);    //Idleフラグをfalseに

            animator.SetBool("idleBFlag", false);   //IdleBフラグをfalseに
        }//Space以外の移動するキーが押されたとき　縦下の移動
        else if (MoveFlag >= 1)
        {
            second = 0; //secondの初期化

            animator.SetBool("jumpFlag", false);    //Jumpフラグをfalseに
            animator.SetBool("walkFlag", true);    //walkフラグをtrueに
            animator.SetBool("idleFlag", false);    //Idleフラグをfalseに

            animator.SetBool("idleBFlag", false);   //IdleBフラグをfalseに
        }//左右の移動
        else if (MoveFlag >= 1)
        {
            second = 0; //secondの初期化
            animator.SetBool("jumpFlag", false);    //Jumpフラグをfalseに
            animator.SetBool("walkFlag", true);    //walkフラグをtrueに
            animator.SetBool("idleFlag", false);    //Idleフラグをfalseに

            animator.SetBool("idleBFlag", false);   //IdleBフラグをfalseに
        }
        else if (second < 15) //何も操作がなかった場合
        {
            animator.SetBool("jumpFlag", false);    //Jumpフラグをfalseに
            animator.SetBool("walkFlag", false);    //walkフラグをfalseに
            animator.SetBool("idleFlag", true);      //Idleフラグをtrueに

            animator.SetBool("idleBFlag", false);   //IdleBフラグをfalseに
        }
        else//15秒たってもなにも操作がなかった場合 この場合のみIdleBの再生
        {
            animator.SetBool("jumpFlag", false);    //Jumpフラグをfalseに
            animator.SetBool("walkFlag", false);    //walkフラグをfalseに
            animator.SetBool("idleFlag", false);    //Idleフラグをfalseに

            animator.SetBool("idleBFlag", true);   //IdleBフラグをtureに
        }
    }

    //キャラクターがダッシュするためのスピード設定メソッド
    private void DashMode(int MoveFlag)
    {
        if (Input.GetKey("left shift") && MoveFlag == 1) //Shiftと　上下の移動キーが同時に押されれいるか？　押されているならダッシュする
        {
            second = 0; //操作待機時間を初期化
            speed = DashSpeed; //ダッシュスピードに移動速度を変更

            animator.SetBool("running", true);  //runningをtrue

            animator.SetBool("jumpFlag", false);      //Jumpフラグをtureに
            animator.SetBool("walkFlag", false);    //walkフラグをfalseに
            animator.SetBool("idleFlag", false);    //Idleフラグをfalseに
            animator.SetBool("idleBFlag", false);   //IdleBフラグをfalseに
        }
        else if (Input.GetKey("left shift") && MoveFlag == 2) //Shiftと 左右の移動キーが同時に押されれいるか？　押されているならすこし速度を落としてダッシュする
        {
            second = 0; //操作待機時間を初期化
            speed = LR_DashSpeed; //ダッシュスピードに移動速度を変更

            animator.SetBool("running", true);  //runningをtrue

            animator.SetBool("jumpFlag", false);      //Jumpフラグをtureに
            animator.SetBool("walkFlag", false);    //walkフラグをfalseに
            animator.SetBool("idleFlag", false);    //Idleフラグをfalseに
            animator.SetBool("idleBFlag", false);   //IdleBフラグをfalseに
        }
        else if (Input.GetKey("left shift") == true && MoveFlag < 1)//Shiftは押されてるけど移動キーが押されていないとき
        {
            animator.SetBool("running", false);  //runningをfalse

            animator.SetBool("jumpFlag", false);      //Jumpフラグをtureに
            animator.SetBool("walkFlag", false);    //walkフラグをfalseに
            animator.SetBool("idleFlag", true);    //Idleフラグをfalseに
            animator.SetBool("idleBFlag", false);   //IdleBフラグをfalseに
        }
        else if (Input.GetKey("left shift") == false && MoveFlag >= 1)//Shiftは押されてないけど移動キーが押されているとき
        {
            animator.SetBool("running", false);  //runningをfalse

            animator.SetBool("jumpFlag", false);      //Jumpフラグをtureに
            animator.SetBool("walkFlag", true);    //walkフラグをfalseに
            animator.SetBool("idleFlag", false);    //Idleフラグをfalseに
            animator.SetBool("idleBFlag", false);   //IdleBフラグをfalseに
        }

    }

    private int MoveKey()
    {
        if (Input.GetKeyDown("space")) //Spaceキーが押されたとき
        {
            return 0;
        }//space以外の移動キーが押されたとき 縦下の移動
        else if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("w") || Input.GetKey("s"))
        {
            return GetMoveKey = 1;
        }//左右の移動
        else if (Input.GetKey("left") || Input.GetKey("right") || Input.GetKey("a") || Input.GetKey("d"))
        {
            return GetMoveKey = 2;
        }
        else //移動キーではない物が押されたとき
            return GetMoveKey = -1;
    }

    Rigidbody rb;
    float inputHorizontal;
    float inputVertical;

    void Update()
    {

        second += Time.deltaTime;   //Idleを切り替えるための変数 15秒経過でIdleBに切り替える

        int MoveFlag = MoveKey(); //移動キーが押されたかの判定

        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");


        //地面との当たり判定
        if (controller.isGrounded)
        {
            //アニメーション設定
            AnimationSetting(second, MoveFlag);

            speed = ResetSpeed; //移動スピードのリセット

            //移動時ダッシュするか判定
            DashMode(MoveFlag);

        }

    }
}
