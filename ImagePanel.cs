using UnityEngine;
using System.Collections;
using System.IO;


/// <summary>
/// 条件
/// * このGameObject以下に破片用の子GameObject(以下破片オブジェクト)がある
/// * 破片オブジェクトにRigidbodyとColiderが設定されている
/// </summary>
public class ImagePanel : MonoBehaviour {

	public Texture tex = null;
	private bool isFinish = false;

	void Start()
	{
		//エラー処理
		if (tex == null)
		{
			Debug.LogError("Texture is null");
			isFinish = true;
			return;
		}
		
		//設定
		foreach (Transform child in this.transform)
		{
			var render = child.gameObject.GetComponent<Renderer>();
			render.material.mainTexture = tex;

			var rbody = child.gameObject.GetComponent<Rigidbody>();
			rbody.constraints = RigidbodyConstraints.FreezeAll;
			rbody.useGravity = false;

			var colider = child.gameObject.GetComponent<BoxCollider>();
			colider.enabled = false;
		} 

	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
			crash();
	}


	public void crash()
	{
		if (isFinish)
			return;

		//拘束を解除していく
		foreach (Transform child in this.transform)
		{
			var rbody = child.gameObject.GetComponent<Rigidbody>();
			rbody.constraints = RigidbodyConstraints.None;
			rbody.useGravity = true;

			var colider = child.gameObject.GetComponent<BoxCollider>();
			colider.enabled = true;
		}

		isFinish = true;

	}
 }
