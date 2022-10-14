using UnityEngine;

public class EnemySpowner : MonoBehaviour
{
    //　出現させる敵を入れておく
    [SerializeField] GameObject[] enemys;
    [Tooltip("次に敵が出現するまでの時間です")]
    [SerializeField] float appearNextTime;
    [Tooltip("この場所から出現する敵の数")]
    [SerializeField] int maxNumOfEnemys;
    //　今何人の敵を出現させたか（総数）
    private int numberOfEnemys;
    //　待ち時間計測フィールド
    private float elapsedTime;

    //AllDestroyが呼ばれたか判定用変数
    [Tooltip("Inspecter上からチェックを入れてtrueにしないこと！")]
    [SerializeField]
    public bool AlldestroyActive = false;
    // Use this for initialization
    void Start()
    {
        numberOfEnemys = 0;
        elapsedTime = 0f;
    }

    void Update()
    {

        //　この場所から出現する最大数を超えてたら何もしない  またはAllDestroyが実行されたら何もしない(スポーンの停止)
        if (numberOfEnemys >= maxNumOfEnemys || AlldestroyActive == true)
        {
            return;
        }

        //　経過時間を足す
        elapsedTime += Time.deltaTime;

        //　経過時間が経ったら
        if (elapsedTime > appearNextTime)
        {
            elapsedTime = 0f;

            AppearEnemy();
        }
    }

    //　敵出現メソッド
    void AppearEnemy()
    {
        //　出現させる敵をランダムに選ぶ
        var randomValue = Random.Range(0, enemys.Length);
        //　敵の向きをランダムに決定
        var randomRotationY = Random.value * 360f;
        //   //敵の出現位置をランダムに設定
        //   var enemyPos = this.transform.position;
        //   float x = Random.Range(-10.0f, 10.0f) + enemyPos.x;
        //   float y = Random.Range(-10.0f, 10.0f) + enemyPos.y;
        //   Vector3 pos = new Vector3(x, y, 0);


        GameObject.Instantiate(enemys[randomValue], transform.position, Quaternion.Euler(0f, randomRotationY, 0f));

        numberOfEnemys++;
        elapsedTime = 0f;
    }
}

