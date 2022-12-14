using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceanManager_Result : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI ScoreTxt;
    [SerializeField]
    private int ResultScore;

    private bool GetScore;

    //キーが押されたかの判定用変数
    private bool GetKey;
    //シーン遷移のメソッドが呼ばれたか判定用変数
    private bool NextSceane;

    [SerializeField]
    [Tooltip("Resultに移動したとき最初操作を受け付けない時間")]
    private float ResultTime;
    private float TempTime;
    private bool Result;
    private void Start()
    {
        //ResultScoreの初期化
        ResultScore = 0;

        //GetScoreの初期化
        GetScore = false;

        //変数の初期化
        GetKey = false;
        NextSceane = false;
    }
    public void Update()
    {
        if(GetScore == false)
        {
            GetScore = true;
            ResultScore = Score();
        }

        TempTime += Time.deltaTime;
        if (TempTime >= ResultTime)
        {
            Result = true;
        }
        //スコアの表示
        ScoreTxt.text = ResultScore.ToString();

        //mouseが押されたとき
        if ((Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) == true) && (Result == true))
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
            SceneManager.LoadScene("StartSceane");
        }
    }
    //Scoreを取得する
    public int Score()
    {
        return GameManeger.GetResultScore();
    }
}
