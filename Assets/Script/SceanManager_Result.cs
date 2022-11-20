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

    //�L�[�������ꂽ���̔���p�ϐ�
    private bool GetKey;
    //�V�[���J�ڂ̃��\�b�h���Ă΂ꂽ������p�ϐ�
    private bool NextSceane;

    [SerializeField]
    [Tooltip("Result�Ɉړ������Ƃ��ŏ�������󂯕t���Ȃ�����")]
    private float ResultTime;
    private float TempTime;
    private bool Result;
    private void Start()
    {
        //ResultScore�̏�����
        ResultScore = 0;

        //GetScore�̏�����
        GetScore = false;

        //�ϐ��̏�����
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
        //�X�R�A�̕\��
        ScoreTxt.text = ResultScore.ToString();

        //mouse�������ꂽ�Ƃ�
        if ((Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) == true) && (Result == true))
        {
            //�V�[���J�ڂ�ture�ɂ���
            GetKey = true;
        }

        //�L�[��������Ă���NextSceaneLoad���܂��Ă΂�Ă��Ȃ��Ƃ�
        if (GetKey == true && NextSceane == false)
        {
            //�V�[���J�ڂ��Ă΂�邱�Ƃ�ture�ɂ���
            NextSceane = true;

            //�Q�[���V�[�������[�h����
            SceneManager.LoadScene("StartSceane");
        }
    }
    //Score���擾����
    public int Score()
    {
        return GameManeger.GetResultScore();
    }
}
