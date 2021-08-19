using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterToPOC : MonoBehaviour {

	private GameObject POC;

	//Texture for POC
	private Sprite[] Textures;

	private GameObject Nahg;

	void Awake()
	{
		this.POC = GameObject.Find ("POC").gameObject;
		this.Textures = Resources.LoadAll<Sprite> ("POC");
		this.Nahg = null;
	}

	void OnTriggerStay2D(Collider2D colEnemy)
	{
		if (colEnemy.gameObject.tag.Equals ("Enemy")) {
			this.Nahg = colEnemy.gameObject;
			foreach (Sprite texture in this.Textures)
				if (texture.name.Equals ("MiraRoja")) {
					this.POC.GetComponent<SpriteRenderer> ().sprite = texture;
				}
		}else{
			this.Nahg = null;
            foreach (Sprite texture in this.Textures)
                if (texture.name.Equals("Mira"))
                {
                    this.POC.GetComponent<SpriteRenderer>().sprite = texture;
                }
		}
	}

	/*void OnTriggerExit2D(Collider2D nahg)
	{
		if (!nahg.gameObject.tag.Equals("Enemy")){
			this.Nahg = null;
			foreach (Sprite texture in this.Textures)
				if (texture.name.Equals("Mira"))
				{
					this.POC.GetComponent<SpriteRenderer>().sprite = texture;
				}
		}
		
	}  */ 
  

	//Whene the user is wrong
	public IEnumerator Wrong()
	{
		gameObject.GetComponent<AudioSource> ().Play ();

		foreach (Sprite texture in this.Textures)
			if (texture.name.Equals ("X")) {
				this.POC.GetComponent<SpriteRenderer> ().sprite = texture;
			}
		
		yield return new WaitForSeconds (0.1f);

		foreach (Sprite texture in this.Textures)
			if (texture.name.Equals ("Mira")) {
				this.POC.GetComponent<SpriteRenderer> ().sprite = texture;
			}
	}

//	public void ChangeToMira()
//	{
//		foreach (Sprite texture in this.Textures)
//			if (texture.name.Equals ("Mira")) {
//				this.POC.GetComponent<SpriteRenderer> ().sprite = texture;
//			}
//	}

	public GameObject getNahg()
	{
		return this.Nahg;
	}
}
