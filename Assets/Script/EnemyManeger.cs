using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵の数を管理用スクリプト

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
    //敵を倒した数を足す
    public void EnemyDestroySum()
    {
        EnemyDestroyNum++;
    }

    public int EnemyDestroyNumber()
    {
        return EnemyDestroyNum;
    }



}
