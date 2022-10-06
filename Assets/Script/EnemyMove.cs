using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent navMeshAgent;
    GolemAnimation GolemAnimation;
    private EnemyState state;

    //　攻撃した後のフリーズ時間
    [SerializeField]
    private float freezeTime = 0.5f;

    private float waitTime;
    private float ElapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        // NavMeshAgentを保持しておく
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player"); //Playerっていうオブジェクトを探す

        GolemAnimation = GetComponent<GolemAnimation>();
    }

    public enum EnemyState
    {
        Walk,
        Wait,
        Chase,
        Attack,
        Freeze
    };
    // Update is called once per frame
    void Update()
    {
        //ノックバックなどでナビメッシュの範囲外になってしまった場合補正する
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            // 位置をNavMesh内に補正
            transform.position = hit.position;
        }

        //キャラクターを追いかける状態
        if (state == EnemyState.Walk)
        {
            ElapsedTime = 0;
            GolemAnimation.Walk();

            // 次に目指すべき位置を取得
            var nextPoint = navMeshAgent.steeringTarget;
            Vector3 targetDir = nextPoint - transform.position;

            //回転量がゼロではないとき回転する
            if (targetDir != Vector3.zero)
            {
                // その方向に向けて旋回する(120度/秒)
                Quaternion targetRotation = Quaternion.LookRotation(targetDir);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);

                // 自分の向きと次の位置の角度差が30度以上の場合、その場で旋回
                float angle = Vector3.Angle(targetDir, transform.forward);
                if (angle < 30f)
                {
                    transform.position += transform.forward * 5.0f * Time.deltaTime;
                    // もしもの場合の補正
                    if (Vector3.Distance(nextPoint, transform.position) < 0.5f) transform.position = nextPoint;
                }
            }

            // playerに向かって移動
            navMeshAgent.SetDestination(player.transform.position);
            navMeshAgent.nextPosition = transform.position;
        }

        //プレイヤーと敵の距離
        var distance = Vector3.Distance(transform.position, player.transform.position);

        //  敵がプレイヤーとぶつかってブルブルしないように設定
        if (distance < 1.0f)
        {
            //stateをWaitに変更
            state = EnemyState.Wait;
            //waitアニメーションの再生
            GolemAnimation.Wait();
            navMeshAgent.isStopped = true;
        }//攻撃範囲だったら攻撃
        else if (distance < 3.0f)
        {
            navMeshAgent.isStopped = false;
            state = EnemyState.Attack;
            GolemAnimation.StartAttack();
            state = EnemyState.Freeze;
        }
        //　攻撃後のフリーズ状態 
        else if (state == EnemyState.Freeze)
        {
            ElapsedTime += Time.deltaTime;

            if (ElapsedTime > freezeTime)
            {
                state = EnemyState.Walk;
            }
        }
    }

    //攻撃モーションが終了したらstateをwalkに移行
    private void EnemyMoveResume()
{
    state = EnemyState.Walk;
}

}
