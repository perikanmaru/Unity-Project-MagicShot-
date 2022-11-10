using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManeger : MonoBehaviour
{
    [SerializeField]
    [Tooltip("ゲームの実行時間です。　この時間がたつとシーンが遷移します。")]
    public float GameTime;
    private float ExecutionTime = 0.0f;
    public int Score = 0;
    //ゲーム時間計測開始用変数
    private bool StartTimeCount;
    //シーン遷移のメソッドが呼ばれたか判定用変数
    private bool NextSceane;
    //経過時間表示用変数
    [SerializeField]
    TextMeshProUGUI GameTimetxt;
    //経過時間表示用変数
    float ceiledToIntValueToFloat;
   //ゲームスコア表示用変数
   [SerializeField]
    TextMeshProUGUI GameScoretxt;

    [SerializeField]
    GameObject StartCamera;
    [SerializeField]
    GameObject player;
    private bool Colutine;

    [Tooltip("敵が最初に出現するまでの時間です")]
    [SerializeField] float WaitSporneTime;

    [SerializeField]
    GameObject EnemySpowner;

    [SerializeField]
    GameObject PlayerUI;

    [SerializeField]
    GameObject GameUI;
    //ゲームスコアを result画面で参照用変数
   public static int ResultScore;
    // Start is called before the first frame update
    void Start()
    {
        //ゲームの実行時間の初期化
        ExecutionTime = 0.0f;
        //Scoreの初期化
        Score = 0;
        //NextSceaneの初期化
        NextSceane = false;
        ceiledToIntValueToFloat = 0.0f;

        Colutine = false;
        //アクティブにするオブジェクトの初期設定化
        StartCamera.SetActive(true);
        player.SetActive(false);
        EnemySpowner.SetActive(false);
        PlayerUI.SetActive(false);
        GameUI.SetActive(false);

        //ゲーム時間計測開始用変数の初期化
        StartTimeCount = false;

        //リザルトスコアの初期化
        ResultScore = 0;
    }

    private IEnumerator StartGame()
    {

        yield return new WaitForSeconds(2); // 引数の秒数だけ待つ

        //アクティブと非アクティブを入れ替える
        player.SetActive(true);
        StartCamera.SetActive(false);

        //EnemyManagerをアクティブにする。
        EnemySpowner.SetActive(true);

        //UIを表示するようにする
        PlayerUI.SetActive(true);
        GameUI.SetActive(true);
        //ゲーム時間計測を開始する
        StartTimeCount = true; 
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

        //ExecutionTimeがGameTimeになるまでExecutionTimeを更新する
        if ((ExecutionTime <= GameTime) && StartTimeCount == true)
        {
            //経過時間の更新
            ExecutionTime += Time.deltaTime;
        }

        //ゲーム終了時間になった時シーンを遷移する
        if(ExecutionTime >= GameTime && (NextSceane == false))
        {
            NextSceane = true;
            //リザルト画面用変数に最終的なスコアを代入する。
            ResultScore = Score;
            //エンドシーンをロードする
            SceneManager.LoadScene("Endsceane");
        }
        //時間の小数点表示をなくす
       ceiledToIntValueToFloat = Mathf.CeilToInt(ExecutionTime);

        //経過時間の表示 小数点繰り上げで表示されてしまうため-1をする
        GameTimetxt.text = "経過時間　: " + (ceiledToIntValueToFloat - 1);
        //獲得スコアの表示
        GameScoretxt.text = "Score　: " + Score;
    }

    //ゲームの経過時間を返すメソッド
    public float ReturnGameTime()
    {
        return ExecutionTime;
    }
    //ゲームスコア参照用メソッド
    public int ReturnGameScore()
    {
        return Score;
    }

    public void SumGameSocre(int score)
    {
        Score = Score + score;
        Debug.Log(Score);
    }
    //ResultScoreをエンドシーンに返すメソッド
   public static int GetResultScore()
    {
        return ResultScore;
    }
}
