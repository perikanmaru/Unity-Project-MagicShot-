using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTxt : MonoBehaviour
{
	//[SerializeField]
	private Text damageText;
	//　フェードアウトするスピード
	private float fadeOutSpeed = 1f;
	//　移動値
	[SerializeField]
	private float moveSpeed = 0.4f;
	//アルファ値
	float alpha = 1.0f;
	void Start()
	{
		damageText = GetComponentInChildren<Text>();
	}

	void LateUpdate()
	{
		transform.rotation = Camera.main.transform.rotation;
		transform.position += Vector3.up * moveSpeed * Time.deltaTime;
		//　少しづつ透明にしていく
		alpha -= fadeOutSpeed * Time.deltaTime;
		damageText.color = Color.Lerp(damageText.color, new Color(1f, 1f, 1f, alpha), fadeOutSpeed * Time.deltaTime);

		if (damageText.color.a <= 0.1f)
		{
			Destroy(gameObject);
		}
	}
}
