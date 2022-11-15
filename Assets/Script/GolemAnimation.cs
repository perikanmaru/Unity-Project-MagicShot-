using UnityEngine;

public class GolemAnimation : MonoBehaviour
{
    [SerializeField]
    Animator animator;  //オブジェクトのアニメーター
    private GameObject Golem;

    //EnemyMoveScript
    private EnemyMove EnemyMoveScript;
    [SerializeField]
    [Tooltip("Golemの攻撃判定用コライダーです。")]
    private SphereCollider SphereCollider;

    //ゴーレムの残りHP
    private float HP;
    //最大HP
    private float MaxHP;
    //のこりHP
    private float HPAmount;
    Collider collider;
    //死んだかの判定用変数
    private bool Die = false;
    //敵の撃破数参照用変数
    public int EnemyDestroyNum = 0;
    //敵をすべて破壊するメソッドを実行したかの判定用変数
    public bool AllDestroyActive = false;
    //倒すべき敵の数
    [Tooltip("倒すべき敵の数です。 出現させる敵より大きくしないこと！")]
    [SerializeField]
    public int MaxEnemy;
    //エネミーマネージャー格納用変数
    private GameObject EnemyManeger;
    private EnemyManeger enemyManeger;

    //EnemySpowner格納用変数
    private GameObject EnemySpowner;
    private EnemySpowner enemySpowner;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        // Golem = GameObject.Find("Golem"); //Playerっていうオブジェクトを探す
        MaxHP = GetComponent<Health>().MaxHP(); //付いているスクリプトを取得 最大HPの所得
        //EnemyManegerの取得
        EnemyManeger = GameObject.Find("EnemyManeger");
        enemyManeger = EnemyManeger.GetComponent<EnemyManeger>();

        EnemySpowner = GameObject.Find("EnemySpowner/SponeEnemy1");
        enemySpowner = EnemySpowner.GetComponent<EnemySpowner>();
        //当たり判定用コライダーをFalseにしておく
        SphereCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //残りHPを所得する
        HP = GetComponent<Health>().GetHPAmount();　//付いているスクリプトを取得 残りHPの所得

        //プレイヤーが倒した数を更新する
        EnemyDestroyNum = enemyManeger.EnemyDestroyNumber();

        //敵の撃破数が所定の数に達したか？
        if (AllDestroyActive == false && EnemyDestroyNum >= MaxEnemy)
        {
            //colliderを無効化して死んだらすり抜けるようにする 
            collider.enabled = false;
            //Attack判定用コライダーをfalseにする。
            SphereCollider.enabled = false;

            AllDestroyActive = true;
            animator.Play("Die");

            //AllDestroyを実行するので敵の出現を停止する
           // enemySpowner.AlldestroyActive = true;

            //シーン上にに存在する敵をすべて破壊する
            GetComponent<DestroyObject>().AllDestroy();
        }

        //HPは残っているか？
        HPAmount = CanDie();

        if (HPAmount <= 0 && Die == false)
        {
            //倒した敵をカウント
            enemyManeger.EnemyDestroySum();
            DieObject();
            Die = true;

        }
    }
    //残りHPの計算
    private float CanDie()
    {
        HPAmount = HP / MaxHP;
        return HPAmount;
    }

    private void DieObject()
    {
        //colliderを無効化して死んだらすり抜けるようにする 
        collider.enabled = false;
        //
        animator.Play("Die");

    }
    //Attackの再生
    public void StartAttack()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Attack", true);
        animator.SetBool("Idle", false);

        SphereCollider.enabled = true;

    }
    //Attackの終了 Golemアニメーションから呼ぶ
    public void AttackEnd()
    {
        SphereCollider.enabled = false;
    }

    public void Walk()
    {
        animator.SetBool("Walk", true);
        animator.SetBool("Attack", false);
        animator.SetBool("Idle", false);
    }

    public void Wait()
    {
        animator.SetBool("Idle", true);
        animator.SetBool("Walk", false);
        animator.SetBool("Attack", false);
    }
}
