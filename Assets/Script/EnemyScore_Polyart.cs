using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScore_Polyart : MonoBehaviour
{
    //�G�l�~�[�}�l�[�W���[�i�[�p�ϐ�
    private GameObject GameManeger;
    private GameManeger gameManeger;
    [SerializeField]
    [Tooltip("�S�[������|�������ɓ�����X�R�A�ł��B")]
    private int Score = 0;
    // Start is called before the first frame update
    void Start()
    {
        //GameManeger�̎擾
        GameManeger = GameObject.Find("GameManeger");
        //GameManegerScript�̃A�^�b�`
        gameManeger = GameManeger.GetComponent<GameManeger>();

    }

    //�|�������ɃX�R�A�����Z���郁�\�b�h�@�S�[�������f�X�g���C���ꂽ�Ƃ��Ăяo��
    private void OnDestroy()
    {
        //�X�R�A�𑫂��B
        gameManeger.SumGameSocre(Score);
    }
}
