using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//“G‚Ì”‚ğŠÇ——pƒXƒNƒŠƒvƒg

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
    //“G‚ğ“|‚µ‚½”‚ğ‘«‚·
    public void EnemyDestroySum()
    {
        EnemyDestroyNum++;
    }

    public int EnemyDestroyNumber()
    {
        return EnemyDestroyNum;
    }



}
