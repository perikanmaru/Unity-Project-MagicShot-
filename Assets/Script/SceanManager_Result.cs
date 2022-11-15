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
        //�X�R�A�̕\��
        ScoreTxt.text = "Score�@: "  + ResultScore.ToString();

        //�L�[�{�[�h�̃L�[�������ꂽ�Ƃ�
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) == true)
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
