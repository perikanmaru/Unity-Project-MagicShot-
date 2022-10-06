using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Animator animator;  //�L�����N�^�[�̃A�j���[�^�[

    [SerializeField]
    CharacterController controller;  //�L�����N�^�[�̃R���g���[���[

    [SerializeField]
    Transform endPos;  //�I�_���W
    [SerializeField]
    float flightTime = 2;  //�؋󎞊�
    [SerializeField]
    float speedRate = 1;   //�؋󎞊Ԃ���Ƃ����ړ����x�{��

    [SerializeField]
    GameObject camera1;
    [SerializeField]
    GameObject camera2;


    [Tooltip(" �L�����N�^�[�̈ړ��J�����̉�]���x�ł��B")]
    public float rotateSpeed = 2.0f;

    [Tooltip(" �L�����N�^�[�̈ړ����x�ł��B")]
    public float speed;

    [Tooltip(" �L�����N�^�[�̈ړ����x���Z�b�g�p�ϐ��@speed�Ɠ����l�ɂ��邱��")]
    public float ResetSpeed;

    [Tooltip(" �L�����N�^�[�̃_�b�V���ړ����x�ł��B")]
    public float DashSpeed;

    [Tooltip(" �L�����N�^�[�̍��E�_�b�V���ړ����x�ł��B")]
    public float LR_DashSpeed;

    [Tooltip(" �L�����N�^�[�̃W�����v���x�ł��B")]
    public float JumpSpeed = 3;

    [Tooltip(" �L�����N�^�[�̃��[�e�[�V�������x�ł��B")]
    public float rotas;

    [Tooltip(" �L�����N�^�[�̎󂯂�d�͂ł��B")]
    public float gravity = 1;

    [Tooltip(" �L�����N�^�[�̈ړ��̊�ƂȂ�J�����ł��B")]
    public Transform MainCamera;

    private Vector3 moveDirection = Vector3.zero;
    private float second;   //�A�j���[�V�����؂�ւ������Ԃɂ���čs�����߂̕ϐ�

    private int GetMoveKey = -1;    //�L�[�������ꂽ���̔���p�ϐ�
    Quaternion targetRotation;

    private void Awake()
    {
        TryGetComponent(out animator);
        targetRotation = transform.rotation; //������
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
    //�A�j���[�V�����ݒ胁�\�b�h
    private void AnimationSetting(float second, int MoveFlag)
    {
        //------- �A�j���[�V�����Đ��t���O�̐ݒ�---------

        if (Input.GetKeyDown("space")) //Space�L�[�������ꂽ�Ƃ�
        {
            second = 0; //second�̏�����

            animator.SetBool("jumpFlag", true);      //Jump�t���O��ture��
            animator.SetBool("walkFlag", false);    //walk�t���O��false��
            animator.SetBool("idleFlag", false);    //Idle�t���O��false��

            animator.SetBool("idleBFlag", false);   //IdleB�t���O��false��
        }//Space�ȊO�̈ړ�����L�[�������ꂽ�Ƃ��@�c���̈ړ�
        else if (MoveFlag >= 1)
        {
            second = 0; //second�̏�����

            animator.SetBool("jumpFlag", false);    //Jump�t���O��false��
            animator.SetBool("walkFlag", true);    //walk�t���O��true��
            animator.SetBool("idleFlag", false);    //Idle�t���O��false��

            animator.SetBool("idleBFlag", false);   //IdleB�t���O��false��
        }//���E�̈ړ�
        else if (MoveFlag >= 1)
        {
            second = 0; //second�̏�����
            animator.SetBool("jumpFlag", false);    //Jump�t���O��false��
            animator.SetBool("walkFlag", true);    //walk�t���O��true��
            animator.SetBool("idleFlag", false);    //Idle�t���O��false��

            animator.SetBool("idleBFlag", false);   //IdleB�t���O��false��
        }
        else if (second < 15) //�������삪�Ȃ������ꍇ
        {
            animator.SetBool("jumpFlag", false);    //Jump�t���O��false��
            animator.SetBool("walkFlag", false);    //walk�t���O��false��
            animator.SetBool("idleFlag", true);      //Idle�t���O��true��

            animator.SetBool("idleBFlag", false);   //IdleB�t���O��false��
        }
        else//15�b�����Ă��Ȃɂ����삪�Ȃ������ꍇ ���̏ꍇ�̂�IdleB�̍Đ�
        {
            animator.SetBool("jumpFlag", false);    //Jump�t���O��false��
            animator.SetBool("walkFlag", false);    //walk�t���O��false��
            animator.SetBool("idleFlag", false);    //Idle�t���O��false��

            animator.SetBool("idleBFlag", true);   //IdleB�t���O��ture��
        }
    }

    //�L�����N�^�[���_�b�V�����邽�߂̃X�s�[�h�ݒ胁�\�b�h
    private void DashMode(int MoveFlag)
    {
        if (Input.GetKey("left shift") && MoveFlag == 1) //Shift�Ɓ@�㉺�̈ړ��L�[�������ɉ�����ꂢ�邩�H�@������Ă���Ȃ�_�b�V������
        {
            second = 0; //����ҋ@���Ԃ�������
            speed = DashSpeed; //�_�b�V���X�s�[�h�Ɉړ����x��ύX

            animator.SetBool("running", true);  //running��true

            animator.SetBool("jumpFlag", false);      //Jump�t���O��ture��
            animator.SetBool("walkFlag", false);    //walk�t���O��false��
            animator.SetBool("idleFlag", false);    //Idle�t���O��false��
            animator.SetBool("idleBFlag", false);   //IdleB�t���O��false��
        }
        else if (Input.GetKey("left shift") && MoveFlag == 2) //Shift�� ���E�̈ړ��L�[�������ɉ�����ꂢ�邩�H�@������Ă���Ȃ炷�������x�𗎂Ƃ��ă_�b�V������
        {
            second = 0; //����ҋ@���Ԃ�������
            speed = LR_DashSpeed; //�_�b�V���X�s�[�h�Ɉړ����x��ύX

            animator.SetBool("running", true);  //running��true

            animator.SetBool("jumpFlag", false);      //Jump�t���O��ture��
            animator.SetBool("walkFlag", false);    //walk�t���O��false��
            animator.SetBool("idleFlag", false);    //Idle�t���O��false��
            animator.SetBool("idleBFlag", false);   //IdleB�t���O��false��
        }
        else if (Input.GetKey("left shift") == true && MoveFlag < 1)//Shift�͉�����Ă邯�ǈړ��L�[��������Ă��Ȃ��Ƃ�
        {
            animator.SetBool("running", false);  //running��false

            animator.SetBool("jumpFlag", false);      //Jump�t���O��ture��
            animator.SetBool("walkFlag", false);    //walk�t���O��false��
            animator.SetBool("idleFlag", true);    //Idle�t���O��false��
            animator.SetBool("idleBFlag", false);   //IdleB�t���O��false��
        }
        else if (Input.GetKey("left shift") == false && MoveFlag >= 1)//Shift�͉�����ĂȂ����ǈړ��L�[��������Ă���Ƃ�
        {
            animator.SetBool("running", false);  //running��false

            animator.SetBool("jumpFlag", false);      //Jump�t���O��ture��
            animator.SetBool("walkFlag", true);    //walk�t���O��false��
            animator.SetBool("idleFlag", false);    //Idle�t���O��false��
            animator.SetBool("idleBFlag", false);   //IdleB�t���O��false��
        }

    }

    private int MoveKey()
    {
        if (Input.GetKeyDown("space")) //Space�L�[�������ꂽ�Ƃ�
        {
            return 0;
        }//space�ȊO�̈ړ��L�[�������ꂽ�Ƃ� �c���̈ړ�
        else if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("w") || Input.GetKey("s"))
        {
            return GetMoveKey = 1;
        }//���E�̈ړ�
        else if (Input.GetKey("left") || Input.GetKey("right") || Input.GetKey("a") || Input.GetKey("d"))
        {
            return GetMoveKey = 2;
        }
        else //�ړ��L�[�ł͂Ȃ����������ꂽ�Ƃ�
            return GetMoveKey = -1;
    }

    Rigidbody rb;
    float inputHorizontal;
    float inputVertical;

    void Update()
    {

        second += Time.deltaTime;   //Idle��؂�ւ��邽�߂̕ϐ� 15�b�o�߂�IdleB�ɐ؂�ւ���

        int MoveFlag = MoveKey(); //�ړ��L�[�������ꂽ���̔���

        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");


        //�n�ʂƂ̓����蔻��
        if (controller.isGrounded)
        {
            //�A�j���[�V�����ݒ�
            AnimationSetting(second, MoveFlag);

            speed = ResetSpeed; //�ړ��X�s�[�h�̃��Z�b�g

            //�ړ����_�b�V�����邩����
            DashMode(MoveFlag);

        }

    }
}
