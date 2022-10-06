using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent navMeshAgent;
    GolemAnimation GolemAnimation;
    private EnemyState state;

    //�@�U��������̃t���[�Y����
    [SerializeField]
    private float freezeTime = 0.5f;

    private float waitTime;
    private float ElapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        // NavMeshAgent��ێ����Ă���
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player"); //Player���Ă����I�u�W�F�N�g��T��

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
        //�m�b�N�o�b�N�ȂǂŃi�r���b�V���͈̔͊O�ɂȂ��Ă��܂����ꍇ�␳����
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            // �ʒu��NavMesh���ɕ␳
            transform.position = hit.position;
        }

        //�L�����N�^�[��ǂ���������
        if (state == EnemyState.Walk)
        {
            ElapsedTime = 0;
            GolemAnimation.Walk();

            // ���ɖڎw���ׂ��ʒu���擾
            var nextPoint = navMeshAgent.steeringTarget;
            Vector3 targetDir = nextPoint - transform.position;

            //��]�ʂ��[���ł͂Ȃ��Ƃ���]����
            if (targetDir != Vector3.zero)
            {
                // ���̕����Ɍ����Đ��񂷂�(120�x/�b)
                Quaternion targetRotation = Quaternion.LookRotation(targetDir);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);

                // �����̌����Ǝ��̈ʒu�̊p�x����30�x�ȏ�̏ꍇ�A���̏�Ő���
                float angle = Vector3.Angle(targetDir, transform.forward);
                if (angle < 30f)
                {
                    transform.position += transform.forward * 5.0f * Time.deltaTime;
                    // �������̏ꍇ�̕␳
                    if (Vector3.Distance(nextPoint, transform.position) < 0.5f) transform.position = nextPoint;
                }
            }

            // player�Ɍ������Ĉړ�
            navMeshAgent.SetDestination(player.transform.position);
            navMeshAgent.nextPosition = transform.position;
        }

        //�v���C���[�ƓG�̋���
        var distance = Vector3.Distance(transform.position, player.transform.position);

        //  �G���v���C���[�ƂԂ����ău���u�����Ȃ��悤�ɐݒ�
        if (distance < 1.0f)
        {
            //state��Wait�ɕύX
            state = EnemyState.Wait;
            //wait�A�j���[�V�����̍Đ�
            GolemAnimation.Wait();
            navMeshAgent.isStopped = true;
        }//�U���͈͂�������U��
        else if (distance < 3.0f)
        {
            navMeshAgent.isStopped = false;
            state = EnemyState.Attack;
            GolemAnimation.StartAttack();
            state = EnemyState.Freeze;
        }
        //�@�U����̃t���[�Y��� 
        else if (state == EnemyState.Freeze)
        {
            ElapsedTime += Time.deltaTime;

            if (ElapsedTime > freezeTime)
            {
                state = EnemyState.Walk;
            }
        }
    }

    //�U�����[�V�������I��������state��walk�Ɉڍs
    private void EnemyMoveResume()
{
    state = EnemyState.Walk;
}

}
