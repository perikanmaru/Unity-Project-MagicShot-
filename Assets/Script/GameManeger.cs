using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManeger : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�Q�[���̎��s���Ԃł��B�@���̎��Ԃ����ƃV�[�����J�ڂ��܂��B")]
    public float GameTime;
    private float ExecutionTime = 0.0f;
    public int Score = 0;

    //�V�[���J�ڂ̃��\�b�h���Ă΂ꂽ������p�ϐ�
    private bool NextSceane;
    //�o�ߎ��ԕ\���p�ϐ�
    [SerializeField]
    TextMeshProUGUI GameTimetxt;
    //�o�ߎ��ԕ\���p�ϐ�
    float ceiledToIntValueToFloat;
   //�Q�[���X�R�A�\���p�ϐ�
   [SerializeField]
    TextMeshProUGUI GameScoretxt;

    [SerializeField]
    GameObject StartCamera;
    [SerializeField]
    GameObject player;
    private bool Colutine;

    [Tooltip("�G���ŏ��ɏo������܂ł̎��Ԃł�")]
    [SerializeField] float WaitSporneTime;

    [SerializeField]
    GameObject EnemySpowner;
    // Start is called before the first frame update
    void Start()
    {
        //�Q�[���̎��s���Ԃ̏�����
        ExecutionTime = 0.0f;
        //Score�̏�����
        Score = 0;
        //NextSceane�̏�����
        NextSceane = false;
        ceiledToIntValueToFloat = 0.0f;

        Colutine = false;
        //�A�N�e�B�u�ɂ���I�u�W�F�N�g�̏����ݒ艻
        StartCamera.SetActive(true);
        player.SetActive(false);
        EnemySpowner.SetActive(false);

    }

    private IEnumerator StartGame()
    {

        yield return new WaitForSeconds(2); // �����̕b�������҂�

        player.SetActive(true);
        StartCamera.SetActive(false);

        //EnemyManager���A�N�e�B�u�ɂ���B
        EnemySpowner.SetActive(true);
        yield break;
    }
    // Update is called once per frame
    void Update()
    {
        if (Colutine == false)
        {
            Colutine = true;
            StartCoroutine("StartGame");
        }

        //ExecutionTime��GameTime�ɂȂ�܂�ExecutionTime���X�V����
        if (ExecutionTime <= GameTime)
        {
            //�o�ߎ��Ԃ̍X�V
            ExecutionTime += Time.deltaTime;
        }
        //�Q�[���I�����ԂɂȂ������V�[����J�ڂ���
        if(ExecutionTime >= GameTime && (NextSceane == false))
        {
            NextSceane = true;
            //�G���h�V�[�������[�h����
            SceneManager.LoadScene("Endsceane");
        }
        ceiledToIntValueToFloat = Mathf.CeilToInt(ExecutionTime);

        //�o�ߎ��Ԃ̕\��
        GameTimetxt.text = "�o�ߎ��ԁ@: " + ceiledToIntValueToFloat;
        //�l���X�R�A�̕\��
        GameScoretxt.text = "Score�@: " + Score;
    }

    //�Q�[���̌o�ߎ��Ԃ�Ԃ����\�b�h
    public float ReturnGameTime()
    {
        return ExecutionTime;
    }
    //�Q�[���X�R�A�Q�Ɨp���\�b�h
    public int ReturnGameScore()
    {
        return Score;
    }

    public void SumGameSocre(int score)
    {
        Score = Score + score;
        Debug.Log(Score);
    }
}
