using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScore_Polyart : MonoBehaviour
{
    //エネミーマネージャー格納用変数
    private GameObject GameManeger;
    private GameManeger gameManeger;
    [SerializeField]
    [Tooltip("ゴーレムを倒した時に得られるスコアです。")]
    private int Score = 0;
    // Start is called before the first frame update
    void Start()
    {
        //GameManegerの取得
        GameManeger = GameObject.Find("GameManeger");
        //GameManegerScriptのアタッチ
        gameManeger = GameManeger.GetComponent<GameManeger>();

    }

    //倒した時にスコアを加算するメソッド　ゴーレムがデストロイされたとき呼び出す
    private void OnDestroy()
    {
        //スコアを足す。
        gameManeger.SumGameSocre(Score);
    }
}
