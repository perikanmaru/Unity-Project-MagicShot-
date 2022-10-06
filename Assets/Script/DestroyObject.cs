using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アニメーションから呼び出す　Dieアニメーション再生後指定秒数後にオブジェクトを削除
public class DestroyObject : MonoBehaviour
{
    [Tooltip(" オブジェクトが死んだあとシーン内にオブジェクトが留まる時間です。")]
    public float ObjectHoldingTime;

    //すべてのオブジェクトを破壊するとき用変数 これでシーン上に存在する敵を管理する
    private GameObject[] targets;
    private bool AllDestroyActive = false;

    //プレイヤーが敵を倒したとき用の破壊メソッド
    private void ObjectHoldingTimeAtDestroy()
    {

        Destroy(gameObject, ObjectHoldingTime);
    }

    //シーン上のすべての敵破壊用メソッド
    public void AllDestroy()
    {

        if (AllDestroyActive == false)
        {
            targets = GameObject.FindGameObjectsWithTag("Enemy");

            //すべてのオブジェクトを破壊をtrueにする
            AllDestroyActive = true;

            foreach (GameObject i in targets)
            {
                Destroy(gameObject, ObjectHoldingTime);
            }

        }
    }
}
