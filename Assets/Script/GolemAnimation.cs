using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAnimation : MonoBehaviour
{
    [SerializeField]
    Animator animator;  //�I�u�W�F�N�g�̃A�j���[�^�[
    private GameObject Golem;

    //�S�[�����̎c��HP
    private float HP;
    //�ő�HP
    private float MaxHP;
    //�̂���HP
   private float HPAmount;
    Collider collider;
    //���񂾂��̔���p�ϐ�
    private bool Die = false;
    //�G�̌��j���Q�Ɨp�ϐ�
    public int EnemyDestroyNum = 0;
    //�G�����ׂĔj�󂷂郁�\�b�h�����s�������̔���p�ϐ�
    public bool AllDestroyActive = false;
    //�|���ׂ��G�̐�
    [Tooltip("�|���ׂ��G�̐��ł��B �o��������G���傫�����Ȃ����ƁI")]
    [SerializeField]
    public int MaxEnemy ;
    //�G�l�~�[�}�l�[�W���[�i�[�p�ϐ�
    private GameObject EnemyManeger;
    private EnemyManeger enemyManeger;

    //EnemySpowner�i�[�p�ϐ�
    private GameObject EnemySpowner;
    private EnemySpowner enemySpowner;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
       // Golem = GameObject.Find("Golem"); //Player���Ă����I�u�W�F�N�g��T��
        MaxHP = GetComponent<Health>().MaxHP(); //�t���Ă���X�N���v�g���擾 �ő�HP�̏���
        //EnemyManeger�̎擾
        EnemyManeger = GameObject.Find("EnemyManeger");
        enemyManeger =EnemyManeger.GetComponent<EnemyManeger>();

        EnemySpowner = GameObject.Find("EnemySpowner/Enemy");
        enemySpowner = EnemySpowner.GetComponent<EnemySpowner>();

    }

    // Update is called once per frame
    void Update()
    {
        //�c��HP����������
        HP = GetComponent<Health>().GetHPAmount();�@//�t���Ă���X�N���v�g���擾 �c��HP�̏���

        //�v���C���[���|���������X�V����
        EnemyDestroyNum = enemyManeger.EnemyDestroyNumber();

        //�G�̌��j��������̐��ɒB�������H
        if( AllDestroyActive == false&&EnemyDestroyNum >= MaxEnemy)
        {
            AllDestroyActive = true;

            //collider�𖳌������Ď��񂾂炷�蔲����悤�ɂ��� 
            collider.enabled = false;
            animator.Play("Die");

            //AllDestroy�����s����̂œG�̏o�����~����
            enemySpowner.AlldestroyActive = true;
            //�V�[����ɂɑ��݂���G�����ׂĔj�󂷂�
            GetComponent<DestroyObject>().AllDestroy();
        }

        //HP�͎c���Ă��邩�H
        HPAmount = CanDie();

        if (HPAmount <= 0 && Die ==false )
        {
            //�|�����G���J�E���g
            enemyManeger.EnemyDestroySum();

            DieObject();
            Die = true;

            //�G�̌��j�����J�E���g����
            
        }
    }
    //�c��HP�̌v�Z
    private float CanDie()
    {
        HPAmount = HP / MaxHP;
        return HPAmount;
    }

    private void DieObject()
    {
        //collider�𖳌������Ď��񂾂炷�蔲����悤�ɂ��� 
        collider.enabled = false;
        animator.Play("Die"); 

    }
    //Attack�̍Đ�
    public void StartAttack()
    {
        animator.SetBool("Walk",false);
        animator.SetBool("Attack", true);
        animator.SetBool("Idle", false);

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
