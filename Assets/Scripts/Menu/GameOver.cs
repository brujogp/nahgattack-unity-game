using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using ClassDB;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	private GameObject menu;
	private GameObject LastScore;
//	private DB DataBase;

	void Awake()
	{
		this.menu = GameObject.FindGameObjectWithTag ("Menus");
	}

	// Use this for initialization
	void Start () {
//		this.DataBase = new DB ();
//		DataBase.Start("DB");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Actions(int index)
	{
		switch (index) {
		case 0:
			StartCoroutine (this.moveMenu ());
			break;
		}
	}

	private IEnumerator moveMenu()
	{
		while (menu.transform.position.y <= 5) {
			menu.transform.position = new Vector3 (0, menu.transform.position.y + 7 * Time.deltaTime, this.gameObject.transform.position.z);
			yield return null;
		}
	}

	void OnDisable()
	{
//		this.DataBase.Dispose ();
	}
}
