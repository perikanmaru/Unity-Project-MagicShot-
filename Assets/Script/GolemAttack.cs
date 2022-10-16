using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttack : MonoBehaviour
{
    //EnemyMoveScript
    private EnemyMove EnemyMoveScript;
    [SerializeField]
    [Tooltip("ゴーレムがプレイヤーに与えるダメージです。　マイナスの値を入れること！")]
    float DamageⅤalue = 0f;
    [SerializeField][Tooltip("Golemの攻撃判定用コライダーです。")]
    private SphereCollider SphereCollider;
    void Start()
    {
        //EnemyMoveScriptのアタッチ
        EnemyMoveScript = GetComponent<EnemyMove>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("当たり");
            col.GetComponent<Health>().ChangeHealth(DamageⅤalue);
        }
    }
    
}
