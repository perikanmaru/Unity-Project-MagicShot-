using UnityEngine;

public class EnemySpowner : MonoBehaviour
{
    //�@�o��������G�����Ă���
    [SerializeField] GameObject[] enemys;
    [Tooltip("���ɓG���o������܂ł̎��Ԃł�")]
    [SerializeField] float appearNextTime;
    [Tooltip("���̏ꏊ����o������G�̐�")]
    [SerializeField] int maxNumOfEnemys;
    //�@�����l�̓G���o�����������i�����j
    private int numberOfEnemys;
    //�@�҂����Ԍv���t�B�[���h
    private float elapsedTime;

    //AllDestroy���Ă΂ꂽ������p�ϐ�
    [Tooltip("Inspecter�ォ��`�F�b�N������true�ɂ��Ȃ����ƁI")]
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

        //�@���̏ꏊ����o������ő吔�𒴂��Ă��牽�����Ȃ�  �܂���AllDestroy�����s���ꂽ�牽�����Ȃ�(�X�|�[���̒�~)
        if (numberOfEnemys >= maxNumOfEnemys || AlldestroyActive == true)
        {
            return;
        }

        //�@�o�ߎ��Ԃ𑫂�
        elapsedTime += Time.deltaTime;

        //�@�o�ߎ��Ԃ��o������
        if (elapsedTime > appearNextTime)
        {
            elapsedTime = 0f;

            AppearEnemy();
        }
    }

    //�@�G�o�����\�b�h
    void AppearEnemy()
    {
        //�@�o��������G�������_���ɑI��
        var randomValue = Random.Range(0, enemys.Length);
        //�@�G�̌����������_���Ɍ���
        var randomRotationY = Random.value * 360f;
        //   //�G�̏o���ʒu�������_���ɐݒ�
        //   var enemyPos = this.transform.position;
        //   float x = Random.Range(-10.0f, 10.0f) + enemyPos.x;
        //   float y = Random.Range(-10.0f, 10.0f) + enemyPos.y;
        //   Vector3 pos = new Vector3(x, y, 0);


        GameObject.Instantiate(enemys[randomValue], transform.position, Quaternion.Euler(0f, randomRotationY, 0f));

        numberOfEnemys++;
        elapsedTime = 0f;
    }
}

