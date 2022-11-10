using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceanManager_Start : MonoBehaviour
{
    //�L�[�������ꂽ���̔���p�ϐ�
    private bool GetKey;
    //�V�[���J�ڂ̃��\�b�h���Ă΂ꂽ������p�ϐ�
    private bool NextSceane;
    private bool NextSceaneComplate;

    [SerializeField]
    Image NextSceaneGo;
    // Start is called before the first frame update
    void Start()
    {
        //�ϐ��̏�����
        GetKey = false;
        NextSceane = false;
        NextSceaneComplate = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�L�[�{�[�h�̃L�[�������ꂽ�Ƃ�
        if(Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2) == true)
        {
            //�V�[���J�ڂ�ture�ɂ���
            GetKey = true;
        }

        //�L�[��������Ă���NextSceaneLoad���܂��Ă΂�Ă��Ȃ��Ƃ�
        if (GetKey == true && NextSceane == false)
        {
            //�V�[���J�ڂ��Ă΂�邱�Ƃ�ture�ɂ���
            NextSceane = true;

            StartCoroutine("GoNextSceane");
        }

    }

    IEnumerator GoNextSceane()
    {
        for (int i = 0; i < 126; i++)
        {
            NextSceaneGo.color = NextSceaneGo.color + new Color32(0, 0, 0, 2);
            yield return new WaitForSeconds(0.01f);
        }
        //�V�[�����[�h���\�b�h���Ă�
        NextSceaneLoad();
    }

    private void NextSceaneLoad()
    {
        //�Q�[���V�[�������[�h����
        SceneManager.LoadScene("GameSceane_ver2");
    }
}
