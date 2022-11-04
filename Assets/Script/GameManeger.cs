using UnityEngine;

public class GameManeger : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�Q�[���̎��s���Ԃł��B�@���̎��Ԃ����ƃV�[�����J�ڂ��܂��B")]
    public float GameTime;
    private float ExecutionTime = 0.0f;
    public int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        //�Q�[���̎��s���Ԃ̏�����
        ExecutionTime = 0.0f;
        //Score�̏�����
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //ExecutionTime��GameTime�ɂȂ�܂�ExecutionTime���X�V����
        if (ExecutionTime <= GameTime)
        {
            //�o�ߎ��Ԃ̍X�V
            ExecutionTime += Time.deltaTime;
        }
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