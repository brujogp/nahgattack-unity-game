using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehavior : MonoBehaviour {

	private Rigidbody2D body;
	private Sprite[] sprites;
	private SpriteRenderer Render;

	private Sprite JumpSprite, IdleSprite, Fall;


	AudioSource[] audioSources;

	private int evolutionNumber = 1;

	private BoxCollider2D collider;

	private Vector2 sizeSprite;

	private GameController gameController;

    private Animator animator;

	void Awake()
	{
//		this.sprites = Resources.LoadAll<Sprite>("CharsGame/Players/" + SelectChar.NameSelectedPlayer + "/" + this.evolutionNumber); //this.textures = Resources.LoadAll <Sprite>("CharsGame/Brako");
//		Debug.Log (this.sprites.Length);

		this.gameController = GameObject.Find ("GameController").GetComponent<GameController>();

		this.sizeSprite = new Vector2 ();

		this.audioSources = this.gameObject.GetComponents<AudioSource> ();

        this.Render = this.GetComponent<SpriteRenderer>();

		this.body = gameObject.GetComponent<Rigidbody2D>();
		this.collider = gameObject.GetComponent<BoxCollider2D> ();

		this.animator = gameObject.GetComponent<Animator>();
        this.changeTextures(1);

        this.changeBoxCollidersSize();
        //this.animator.runtimeAnimatorController = Resources.Load("CharsGame/Players/" + SelectChar.NameSelectedPlayer + "/" + this.evolutionNumber + "/Animations/Controller/Alex") as RuntimeAnimatorController;
	}


	public void changeTextures(int number)
	{
		this.evolutionNumber = number;
		this.sprites = Resources.LoadAll<Sprite>("CharsGame/Players/" + SelectChar.NameSelectedPlayer + "/" + this.evolutionNumber);

		this.assignSprites ();

		this.Render.sprite = this.JumpSprite;

        this.animator.runtimeAnimatorController = Resources.Load("CharsGame/Players/" + SelectChar.NameSelectedPlayer + "/" + this.evolutionNumber + "/Animations/Controller/" + SelectChar.NameSelectedPlayer) as RuntimeAnimatorController;

	}

	private void assignSprites()
	{
		foreach(Sprite s in this.sprites)
		{
			if (s.name.Equals ("1")) {
				this.IdleSprite = s;
			}else if (s.name.Equals ("2")) {
				this.JumpSprite = s;
			}else if (s.name.Equals ("3")) {
				this.Fall = s;
			}
		}
	}

	void Start()
	{
		body.freezeRotation = true;
	}

	void Update()
	{
		gameObject.transform.position = new Vector3(0, gameObject.transform.position.y, gameObject.transform.position.z);	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
//		if(col.gameObject.tag == "Enemy")
//			Destroy (col.gameObject);
		//col.rigidbody.AddForce(new Vector2(30, -20));

		int side = UnityEngine.Random.Range(0, 2);

		try{
		if(col.gameObject.CompareTag("Enemy")){
			//this.gameController.ChangeTextureLossOfNahg();

			this.SelectAudio ("Hit");
			if (side == 0) {
//				col.rigidbody.AddTorque (1); 
				col.rigidbody.AddForceAtPosition (new Vector2 (200, 10), new Vector2 (1, 1), ForceMode2D.Force);
				col.collider.enabled = false;
			} else {
//				col.rigidbody.AddTorque (-1); 
				col.rigidbody.AddForceAtPosition (new Vector2 (-200, 10), new Vector2 (-1, 1), ForceMode2D.Force);
				col.collider.enabled = false;
		}
		}
		}
		catch(NullReferenceException e)
		{
			
		}

	}

	public void Jump()
	{
        //StartCoroutine(this.AnimationJump());

        this.animator.SetTrigger("Jump");
		this.SelectAudio ("Jump");


		this.body.AddForce (new Vector2(0, 500));
	}

	IEnumerator AnimationJump()
	{
		this.Render.sprite = this.JumpSprite;
		yield return new WaitForSeconds (0.6f);
		this.Render.sprite = this.IdleSprite;
		yield return 0;
	}

	public void Loss()
	{

        this.animator.SetTrigger("Loss");
		StartCoroutine (AnimationLoss ());
	}

	public void up()
	{
//		if(!(this.gameObject.transform.position.y >= -3.8f))
//		{
//			StartCoroutine (this.Up ());
//		}
		this.Render.sprite = this.Fall;
	}

//	IEnumerator Up()
//	{
////		this.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f * Time.deltaTime, gameObject.transform.position.z);
//		this.Render.sprite = this.Fall;
//		yield return 0;
//	}   
    
	IEnumerator  AnimationLoss()
	{
		this.gameObject.transform.position = new Vector3 (0, this.gameObject.transform.position.y + 6 * Time.deltaTime, gameObject.transform.position.z);
//		this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 3.8f, this.transform.position.z);
		yield return 0;
	}

	private void SelectAudio(string name)
	{
		if (this.audioSources [0].clip.name.Equals (name))
			this.audioSources [0].Play ();
		else
			this.audioSources [1].Play ();
	}
		
	public void changeBoxCollidersSize()
	{
		Rect size =  this.IdleSprite.textureRect;
		this.sizeSprite.Set(size.width / 100, size.height/ 100);

		this.collider.size = this.sizeSprite;
	}

	public Vector2 getSpritesSize()
	{
		return this.sizeSprite;
	}
}
