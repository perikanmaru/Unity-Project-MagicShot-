using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyDamageReaction : MonoBehaviour
{
    /// <summary> �}�e���A���̐F�p�����[�^��ID </summary>
    private static readonly int PROPERTY_COLOR = Shader.PropertyToID("_Color");

    /// <summary> ���f����Renderer </summary>
    [SerializeField]
    private Renderer _renderer;

    /// <summary> ���f���̃}�e���A���̕��� </summary>
    private Material _material;

    private Sequence _seq;

    private void Awake()
    {
        // material�ɃA�N�Z�X���Ď������������}�e���A����ێ�
        _material = _renderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        HitFadeBlink(Color.red);
    }

    /// <summary> �J���[��Z�ɂ��_���[�W���o�Đ� </summary>
    private void HitFadeBlink(Color color)
    {
        _seq?.Kill();
        _seq = DOTween.Sequence();
        _seq.Append(DOTween.To(() => Color.white, c => _material.SetColor(PROPERTY_COLOR, c), color, 0.1f));
        _seq.Append(DOTween.To(() => color, c => _material.SetColor(PROPERTY_COLOR, c), Color.white, 0.1f));
        _seq.Play();
    }
}
