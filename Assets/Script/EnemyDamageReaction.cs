using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyDamageReaction : MonoBehaviour
{
    /// <summary> マテリアルの色パラメータのID </summary>
    private static readonly int PROPERTY_COLOR = Shader.PropertyToID("_Color");

    /// <summary> モデルのRenderer </summary>
    [SerializeField]
    private Renderer _renderer;

    /// <summary> モデルのマテリアルの複製 </summary>
    private Material _material;

    private Sequence _seq;

    private void Awake()
    {
        // materialにアクセスして自動生成されるマテリアルを保持
        _material = _renderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        HitFadeBlink(Color.red);
    }

    /// <summary> カラー乗算によるダメージ演出再生 </summary>
    private void HitFadeBlink(Color color)
    {
        _seq?.Kill();
        _seq = DOTween.Sequence();
        _seq.Append(DOTween.To(() => Color.white, c => _material.SetColor(PROPERTY_COLOR, c), color, 0.1f));
        _seq.Append(DOTween.To(() => color, c => _material.SetColor(PROPERTY_COLOR, c), Color.white, 0.1f));
        _seq.Play();
    }
}
