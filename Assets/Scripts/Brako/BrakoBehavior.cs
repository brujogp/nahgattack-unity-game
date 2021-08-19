using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrakoBehavior : MonoBehaviour {

	public Text Score;
	private Sprite[] textures;
//
//	private Rigidbody2D rig;
//
	private SpriteRenderer render;

	public Vector3 positionObject;

	public float x = 0.23f;
	private GameController GC;

	void Awake()
	{
		this.GC = GameObject.Find("GameController").GetComponent<GameController>();
		this.textures = (SelectChar.NameSelectedPlayer != "Brako") ? Resources.LoadAll <Sprite>("CharsGame/Score/Brako") : Resources.LoadAll<Sprite>("CharsGame/Score/boy" + Random.Range(1, 4));
//
//		this.rig =  gameObject.GetComponent<Rigidbody2D>();
//		this.rig.freezeRotation = true;
//
		this.render = gameObject.GetComponent<SpriteRenderer> ();

		this.positionObject = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

		// 0.36 0.36 0.29 0.37

	}

	private void Start()
	{
		if (SelectChar.NameSelectedPlayer == "Brako")
		{
			this.Score.GetComponent<Transform>().position = new Vector3(this.Score.GetComponent<Transform>().position.x - 0.05f, this.Score.GetComponent<Transform>().position.y + 0.01f, this.Score.GetComponent<Transform>().position.z);//gameObject.transform.position.Set(this.gameObject.transform.position.x - 1, this.gameObject.transform.position.y + 0.01f, this.gameObject.transform.position.z);
		}
	}

	void Update ()
	{
		//this.ScoreMovement ();
	}

	public void AddToScore(int score)
	{
		this.Score.text = score.ToString();
		this.Jump ();
	}


	private void Jump()
	{
			StartCoroutine( this.Animation ());
	}

	IEnumerator Animation()
	{
		this.render.sprite = (this.textures [0].name == "2") ? this.textures [0] : this.textures [1];
		this.Score.enabled = true;

		StartCoroutine(this.UpDown(-4.469f));

		yield return this.GC.Score <= 20 ? new WaitForSeconds (1) : new WaitForSeconds(0.5f);

		this.Score.enabled = false;
		this.render.sprite = (this.textures [0].name == "1") ? this.textures [0] : this.textures [1];

		StartCoroutine(this.UpDown(-5.23f));
	}

	IEnumerator UpDown(float axis)
	{
		if (axis.Equals (-5.23f)) {
			while (this.transform.position.y >= axis/*-4.469*/) {
				this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y - 3 * Time.deltaTime, 1);
				yield return 0;
			}	
		} else{
			while (this.transform.position.y <= axis/*-4.469*/) {
				this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 3 * Time.deltaTime, 1);
				yield return 0;
			}
		}
	}
}
