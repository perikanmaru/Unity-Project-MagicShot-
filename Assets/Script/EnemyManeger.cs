using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�G�̐����Ǘ��p�X�N���v�g

public class EnemyManeger : MonoBehaviour
{
    public int EnemyDestroyNum = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //�G��|�������𑫂�
    public void EnemyDestroySum()
    {
        EnemyDestroyNum++;
    }

    public int EnemyDestroyNumber()
    {
        return EnemyDestroyNum;
    }



}
