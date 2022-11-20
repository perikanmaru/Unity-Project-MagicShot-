using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceanManager_GameOver : MonoBehaviour
{
    //�L�[�������ꂽ���̔���p�ϐ�
    private bool GetKey;
    //�V�[���J�ڂ̃��\�b�h���Ă΂ꂽ������p�ϐ�
    private bool NextSceane;
    [SerializeField][Tooltip("�Q�[���I�[�o�[scene�Ɉړ������Ƃ��ŏ�������󂯕t���Ȃ�����")]
    private float GameOverTime;

    private bool GameOver;
    //GameOverTime�̔���p�ϐ�
    private float TempTime;
    void Start()
    {
        //�ϐ��̏�����
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

        //�L�[�{�[�h�̃L�[�܂���mouse�������ꂽ�Ƃ�
        if ((Input.anyKey || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) == true) && (GameOver == true))
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
            SceneManager.LoadScene ("StartSceane");
        }

    }
}
