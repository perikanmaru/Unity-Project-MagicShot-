using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceanManager_Result : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI ScoreTxt;
    [SerializeField]
    private int ResultScore;

    private bool GetScore;

    private void Start()
    {
        //ResultScoreの初期化
        ResultScore = 0;

        //GetScoreの初期化
        GetScore = false;
    }
    public void Update()
    {
        if(GetScore == false)
        {
            GetScore = true;
            ResultScore = Score();
        }
        //スコアの表示
        ScoreTxt.text = "Score　: "  + ResultScore.ToString();
    }
    //Scoreを取得する
    public int Score()
    {
        return GameManeger.GetResultScore();
    }
}
