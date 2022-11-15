using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [Tooltip("最初の画面が出現する時間です")]
    [SerializeField] float WaitTime;

    [SerializeField]
    GameObject EnemySpowner;

    [SerializeField]
    GameObject PlayerUI;

    [SerializeField]
    GameObject GameUI;
    //最初の画面に表示するロゴです。
    [SerializeField]
    GameObject StandbyLogo;
    [SerializeField]
    GameObject ReadyLogo;
    [SerializeField]
    GameObject GoLogo;
    //EnemyManeger格納用変数
    [SerializeField]
    GameObject SponeEnemy1;
    EnemySpowner spowner1;
    [SerializeField]
    GameObject SponeEnemy2;
    EnemySpowner spowner2;
    [SerializeField]
    GameObject SponeEnemy3;
    EnemySpowner spowner3;
    //最初に表示するロゴのゲージ
    [SerializeField]
    Image Left_Gauge;
    [SerializeField]
    Image Right_Gauge;

    private bool StartgaugeActive;
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
        //カメラ関連
        StartCamera.SetActive(true);
        player.SetActive(false);
        //エネミースポーン関連
        EnemySpowner.SetActive(false);
        //UI関連
        PlayerUI.SetActive(false);
        GameUI.SetActive(false);
        //最初の画面関連
        StandbyLogo.SetActive(false);
        ReadyLogo.SetActive(false);
        GoLogo.SetActive(false);
        //画像の非表示
        Left_Gauge.enabled = false;
        Right_Gauge.enabled = false;
        StartgaugeActive = false;
        //Enemypownerの取得
        spowner1 = SponeEnemy1.GetComponent<EnemySpowner>();
        spowner2 = SponeEnemy1.GetComponent<EnemySpowner>();
        spowner3 = SponeEnemy1.GetComponent<EnemySpowner>();

        //ゲーム時間計測開始用変数の初期化
        StartTimeCount = false;

        //リザルトスコアの初期化
        ResultScore = 0;
    }

    private IEnumerator StartGame()
    {
        //最初のロゴの表示
        StandbyLogo.SetActive(true);
        ReadyLogo.SetActive(true);

        //画像の表示
        Left_Gauge.enabled = true;
        Right_Gauge.enabled = true;
        StartgaugeActive = true;

        yield return new WaitForSeconds(1.5f); // 引数の秒数だけ待つ
        //表示するロゴを入れ替える
        StandbyLogo.SetActive(false);
        ReadyLogo.SetActive(false);
        GoLogo.SetActive(true);

        //画像の非表示
        Left_Gauge.enabled = false;
        Right_Gauge.enabled = false;
        StartgaugeActive = false;
        yield return new WaitForSeconds(0.5f);　//1秒待つ

        GoLogo.SetActive(false);
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

    private void EndGame()
    {

        //UIを非表示するようにする
        PlayerUI.SetActive(false);
        GameUI.SetActive(false);
        //AllDestroyを実行するので敵の出現を停止する
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

        //ExecutionTimeがGameTimeになるまでExecutionTimeを更新する
        if ((ExecutionTime <= GameTime) && StartTimeCount == true)
        {
            //経過時間の更新
            ExecutionTime += Time.deltaTime;
        }

        //ゲーム終了時間になった時シーンを遷移する
        if (ExecutionTime >= GameTime && (NextSceane == false))
        {
            NextSceane = true;
            //リザルト画面用変数に最終的なスコアを代入する。
            ResultScore = Score;
            EndGame();
            //1秒後にEndSeaneに移動する
            Invoke("GoToEndSeane", 1.0f);
        }
        //時間の小数点表示をなくす
        ceiledToIntValueToFloat = Mathf.CeilToInt(ExecutionTime);

        //経過時間の表示 小数点繰り上げで表示されてしまうため-1をする
        GameTimetxt.text = "経過時間　: " + (ceiledToIntValueToFloat - 1);
        //獲得スコアの表示
        GameScoretxt.text = "Score　: " + Score;
    }

    private void GoToEndSeane()
    {
        //エンドシーンをロードする
        SceneManager.LoadScene("Endsceane");
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
