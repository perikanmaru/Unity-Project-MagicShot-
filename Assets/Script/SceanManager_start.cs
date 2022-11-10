using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceanManager_Start : MonoBehaviour
{
    //キーが押されたかの判定用変数
    private bool GetKey;
    //シーン遷移のメソッドが呼ばれたか判定用変数
    private bool NextSceane;
    private bool NextSceaneComplate;

    [SerializeField]
    Image NextSceaneGo;
    // Start is called before the first frame update
    void Start()
    {
        //変数の初期化
        GetKey = false;
        NextSceane = false;
        NextSceaneComplate = false;
    }

    // Update is called once per frame
    void Update()
    {
        //キーボードのキーが押されたとき
        if(Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2) == true)
        {
            //シーン遷移をtureにする
            GetKey = true;
        }

        //キーが押されてかつNextSceaneLoadがまだ呼ばれていないとき
        if (GetKey == true && NextSceane == false)
        {
            //シーン遷移が呼ばれることをtureにする
            NextSceane = true;

            StartCoroutine("GoNextSceane");
        }

    }

    IEnumerator GoNextSceane()
    {
        for (int i = 0; i < 126; i++)
        {
            NextSceaneGo.color = NextSceaneGo.color + new Color32(0, 0, 0, 2);
            yield return new WaitForSeconds(0.01f);
        }
        //シーンロードメソッドを呼ぶ
        NextSceaneLoad();
    }

    private void NextSceaneLoad()
    {
        //ゲームシーンをロードする
        SceneManager.LoadScene("GameSceane_ver2");
    }
}
