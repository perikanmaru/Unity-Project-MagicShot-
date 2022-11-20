using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorHPgauge : MonoBehaviour
{
    public float maxHP = 100;
    [Range(0, 100)] public float hp = 100;
    [Range(0, 1)] public float color_h, color_s, color_v;
    private Image image_HPgauge;
    private float hp_ratio;
    //プレイヤーのHP
    [SerializeField]
    private GameObject PlayerHP;
    // Start is called before the first frame update
    void Start()
    {
        image_HPgauge = gameObject.GetComponent<Image>();
        color_s = 1.0f;
        color_v = 1.0f;
     //   PlayerHP = GameObject.Find("Player"); //Playerっていうオブジェクトを探す
       
    }

    Color HSVtoRGB(float h, float s, float v)
    {
        Color col = new Color(v, v, v, 1f);

        float c = v * s;
        float hd = h * 6.0f;
        float x = 1f - Mathf.Abs(hd % 2 - 1f);

        int i = (int)Mathf.Floor(hd);

        switch (i)
        {
            case 0:
                col.g *= 1f - s * (1f - x);
                col.b *= 1f - s;
                break;
            case 1:
                col.r *= 1f - s * (1f - x);
                col.b *= 1f - s;
                break;
            case 2:
                col.r *= 1f - s;
                col.b *= 1f - s * (1f - x);
                break;
            case 3:
                col.r *= 1f - s;
                col.g *= 1f - s * (1f - x);
                break;
            case 4:
                col.r *= 1f - s * (1f - x);
                col.g *= 1f - s;
                break;
            case 5:
                col.g *= 1f - s;
                col.b *= 1f - s * (1f - x);
                break;
            default:
                col.r = 1f - s;
                col.g = 1f - s;
                col.b = 1f - s;
                break;
        }
        return col;
    }

    // Update is called once per frame
    void Update()
    {

        hp = PlayerHP.GetComponent<Health>().GetHPAmount();　//付いているスクリプトを取得 残りHPの所得
        hp_ratio = hp / maxHP;

        image_HPgauge.fillAmount = hp_ratio;
        color_h = Mathf.Lerp(0f, 0.4f, hp_ratio);

        image_HPgauge.color = HSVtoRGB(color_h, color_s, color_v);
    }
}
