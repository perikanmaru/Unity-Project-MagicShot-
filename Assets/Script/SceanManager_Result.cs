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
        //ResultScore�̏�����
        ResultScore = 0;

        //GetScore�̏�����
        GetScore = false;
    }
    public void Update()
    {
        if(GetScore == false)
        {
            GetScore = true;
            ResultScore = Score();
        }
        //�X�R�A�̕\��
        ScoreTxt.text = "Score�@: "  + ResultScore.ToString();
    }
    //Score���擾����
    public int Score()
    {
        return GameManeger.GetResultScore();
    }
}
