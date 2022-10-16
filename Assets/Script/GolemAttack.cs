using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttack : MonoBehaviour
{
    //EnemyMoveScript
    private EnemyMove EnemyMoveScript;
    [SerializeField]
    [Tooltip("�S�[�������v���C���[�ɗ^����_���[�W�ł��B�@�}�C�i�X�̒l�����邱�ƁI")]
    float Damage�Xalue = 0f;
    [SerializeField][Tooltip("Golem�̍U������p�R���C�_�[�ł��B")]
    private SphereCollider SphereCollider;
    void Start()
    {
        //EnemyMoveScript�̃A�^�b�`
        EnemyMoveScript = GetComponent<EnemyMove>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("������");
            col.GetComponent<Health>().ChangeHealth(Damage�Xalue);
        }
    }
    
}
