using UnityEngine;
using System.Collections;
using System.IO;


/// <summary>
/// 条件
/// * このGameObject以下に破片用の子GameObject(以下破片オブジェクト)がある
/// * 破片オブジェクトにRigidbodyとColiderが設定されている
/// </summary>
public class ImagePanel : MonoBehaviour {

	[Tooltip("表示させる画像")]
	public Texture texure = null;

	[Tooltip("破壊前に見える板")]
	public GameObject panel;

	[Tooltip("破壊後の破片を纏めているオブジェクト")]
	public GameObject debris;

	private bool isFinish = false; //debug

	void Start()
	{
		//エラー処理
		if (texure == null)
		{
			Debug.LogError("Texture is null");
			isFinish = true;
			return;
		}

		debris.SetActive(false); //飛び散らないように非アクティブにしておく

		//Texture設定
		panel.GetComponent<Renderer>().material.mainTexture = texure;
		foreach (Transform child in debris.transform)
		{
			child.gameObject.GetComponent<Renderer>().material.mainTexture = texure; //破片すべてにTexture設定
		} 
	}

	void Update () {
		//debug スペースキーを押すと爆散する
		if (Input.GetKeyDown(KeyCode.Space) && !isFinish)
		{
			crash();
			isFinish = true;
		}
	}


	public void crash()
	{
		debris.SetActive(true); //破片を表示
		debris.transform.parent = null; //Destroy(this.gameObject)に巻き込まれないように
		Destroy(debris, 10.0f);


		Destroy(this.gameObject);
	}
 }
