using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManeger : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�Q�[���̎��s���Ԃł��B�@���̎��Ԃ����ƃV�[�����J�ڂ��܂��B")]
    public float GameTime;
    private float ExecutionTime = 0.0f;
    public int Score = 0;
    //�Q�[�����Ԍv���J�n�p�ϐ�
    private bool StartTimeCount;
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

    [Tooltip("�ŏ��̉�ʂ��o�����鎞�Ԃł�")]
    [SerializeField] float WaitTime;

    [SerializeField]
    GameObject EnemySpowner;

    [SerializeField]
    GameObject PlayerUI;

    [SerializeField]
    GameObject GameUI;
    //�ŏ��̉�ʂɕ\�����郍�S�ł��B
    [SerializeField]
    GameObject StandbyLogo;
    [SerializeField]
    GameObject ReadyLogo;
    [SerializeField]
    GameObject GoLogo;
    //EnemyManeger�i�[�p�ϐ�
    [SerializeField]
    GameObject SponeEnemy1;
    EnemySpowner spowner1;
    [SerializeField]
    GameObject SponeEnemy2;
    EnemySpowner spowner2;
    [SerializeField]
    GameObject SponeEnemy3;
    EnemySpowner spowner3;
    //�ŏ��ɕ\�����郍�S�̃Q�[�W
    [SerializeField]
    Image Left_Gauge;
    [SerializeField]
    Image Right_Gauge;

    private bool StartgaugeActive;
    //�Q�[���X�R�A�� result��ʂŎQ�Ɨp�ϐ�
    public static int ResultScore;
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
        //�J�����֘A
        StartCamera.SetActive(true);
        player.SetActive(false);
        //�G�l�~�[�X�|�[���֘A
        EnemySpowner.SetActive(false);
        //UI�֘A
        PlayerUI.SetActive(false);
        GameUI.SetActive(false);
        //�ŏ��̉�ʊ֘A
        StandbyLogo.SetActive(false);
        ReadyLogo.SetActive(false);
        GoLogo.SetActive(false);
        //�摜�̔�\��
        Left_Gauge.enabled = false;
        Right_Gauge.enabled = false;
        StartgaugeActive = false;
        //Enemypowner�̎擾
        spowner1 = SponeEnemy1.GetComponent<EnemySpowner>();
        spowner2 = SponeEnemy1.GetComponent<EnemySpowner>();
        spowner3 = SponeEnemy1.GetComponent<EnemySpowner>();

        //�Q�[�����Ԍv���J�n�p�ϐ��̏�����
        StartTimeCount = false;

        //���U���g�X�R�A�̏�����
        ResultScore = 0;
    }

    private IEnumerator StartGame()
    {
        //�ŏ��̃��S�̕\��
        StandbyLogo.SetActive(true);
        ReadyLogo.SetActive(true);

        //�摜�̕\��
        Left_Gauge.enabled = true;
        Right_Gauge.enabled = true;
        StartgaugeActive = true;

        yield return new WaitForSeconds(1.5f); // �����̕b�������҂�
        //�\�����郍�S�����ւ���
        StandbyLogo.SetActive(false);
        ReadyLogo.SetActive(false);
        GoLogo.SetActive(true);

        //�摜�̔�\��
        Left_Gauge.enabled = false;
        Right_Gauge.enabled = false;
        StartgaugeActive = false;
        yield return new WaitForSeconds(0.5f);�@//1�b�҂�

        GoLogo.SetActive(false);
        //�A�N�e�B�u�Ɣ�A�N�e�B�u�����ւ���
        player.SetActive(true);
        StartCamera.SetActive(false);

        //EnemyManager���A�N�e�B�u�ɂ���B
        EnemySpowner.SetActive(true);

        //UI��\������悤�ɂ���
        PlayerUI.SetActive(true);
        GameUI.SetActive(true);
        //�Q�[�����Ԍv�����J�n����
        StartTimeCount = true;
        yield break;
    }

    private void EndGame()
    {

        //UI���\������悤�ɂ���
        PlayerUI.SetActive(false);
        GameUI.SetActive(false);
        //AllDestroy�����s����̂œG�̏o�����~����
        spowner1.AlldestroyActive = true;
        spowner2.AlldestroyActive = true;
        spowner3.AlldestroyActive = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Colutine == false)
        {
            Colutine = true;
            StartCoroutine("StartGame");
        }
        if (StartgaugeActive == true)
        {
            Left_Gauge.fillAmount -= (0.666f * Time.deltaTime);
            Right_Gauge.fillAmount -= (0.666f * Time.deltaTime);
        }

        //ExecutionTime��GameTime�ɂȂ�܂�ExecutionTime���X�V����
        if ((ExecutionTime <= GameTime) && StartTimeCount == true)
        {
            //�o�ߎ��Ԃ̍X�V
            ExecutionTime += Time.deltaTime;
        }

        //�Q�[���I�����ԂɂȂ������V�[����J�ڂ���
        if (ExecutionTime >= GameTime && (NextSceane == false))
        {
            NextSceane = true;
            //���U���g��ʗp�ϐ��ɍŏI�I�ȃX�R�A��������B
            ResultScore = Score;
            EndGame();
            //1�b���EndSeane�Ɉړ�����
            Invoke("GoToEndSeane", 1.0f);
        }
        //���Ԃ̏����_�\�����Ȃ���
        ceiledToIntValueToFloat = Mathf.CeilToInt(ExecutionTime);

        //�o�ߎ��Ԃ̕\�� �����_�J��グ�ŕ\������Ă��܂�����-1������
        GameTimetxt.text = "�o�ߎ��ԁ@: " + (ceiledToIntValueToFloat - 1);
        //�l���X�R�A�̕\��
        GameScoretxt.text = "Score�@: " + Score;
    }

    private void GoToEndSeane()
    {
        //�G���h�V�[�������[�h����
        SceneManager.LoadScene("Endsceane");
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
    //ResultScore���G���h�V�[���ɕԂ����\�b�h
    public static int GetResultScore()
    {
        return ResultScore;
    }
}
