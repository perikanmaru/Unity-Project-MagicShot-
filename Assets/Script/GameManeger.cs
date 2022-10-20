using UnityEngine;

public class GameManeger : MonoBehaviour
{
    [SerializeField]
    [Tooltip("ゲームの実行時間です。　この時間がたつとシーンが遷移します。")]
    public float GameTime;
    private float ExecutionTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //ゲームの実行時間の初期化
        ExecutionTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //ExecutionTimeがGameTimeになるまでExecutionTimeを更新する
        if (ExecutionTime <= GameTime)
        {
            //経過時間の更新
            ExecutionTime += Time.deltaTime;
        }
    }

    //ゲームの経過時間を返すメソッド
    public float ReturnGameTime()
    {
        return GameTime;
    }

}
