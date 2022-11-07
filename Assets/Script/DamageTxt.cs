using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTxt : MonoBehaviour
{
	//[SerializeField]
	private Text damageText;
	//�@�t�F�[�h�A�E�g����X�s�[�h
	private float fadeOutSpeed = 1f;
	//�@�ړ��l
	[SerializeField]
	private float moveSpeed = 0.4f;
	//�A���t�@�l
	float alpha = 1.0f;
	void Start()
	{
		damageText = GetComponentInChildren<Text>();
	}

	void LateUpdate()
	{
		transform.rotation = Camera.main.transform.rotation;
		transform.position += Vector3.up * moveSpeed * Time.deltaTime;
		//�@�����Â����ɂ��Ă���
		alpha -= fadeOutSpeed * Time.deltaTime;
		damageText.color = Color.Lerp(damageText.color, new Color(1f, 1f, 1f, alpha), fadeOutSpeed * Time.deltaTime);

		if (damageText.color.a <= 0.1f)
		{
			Destroy(gameObject);
		}
	}
}
