using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceanManager_GameOver : MonoBehaviour
{
    //キーが押されたかの判定用変数
    private bool GetKey;
    //シーン遷移のメソッドが呼ばれたか判定用変数
    private bool NextSceane;
    [SerializeField][Tooltip("ゲームオーバーsceneに移動したとき最初操作を受け付けない時間")]
    private float GameOverTime;

    private bool GameOver;
    //GameOverTimeの判定用変数
    private float TempTime;
    void Start()
    {
        //変数の初期化
        GetKey = false;
        NextSceane = false;
        GameOver = false;
    }

    void Update()
    {
        TempTime += Time.deltaTime;
        if(TempTime >= GameOverTime)
        {
            GameOver = true;
        }

        //キーボードのキーまたはmouseが押されたとき
        if ((Input.anyKey || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) == true) && (GameOver == true))
        {
            //シーン遷移をtureにする
            GetKey = true;
        }

        //キーが押されてかつNextSceaneLoadがまだ呼ばれていないとき
        if (GetKey == true && NextSceane == false)
        {
            //シーン遷移が呼ばれることをtureにする
            NextSceane = true;
            //ゲームシーンをロードする
            SceneManager.LoadScene ("StartSceane");
        }

    }
}
