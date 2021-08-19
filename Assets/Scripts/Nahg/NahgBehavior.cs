using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NahgBehavior : MonoBehaviour {

	private Sprite[] TexturesNahgs;

    private Animator animator;
	public static float Gravity = 0.05f;
    private int indexTexture;

	private string name;

	void Awake()
	{
		this.indexTexture = Random.Range(1, 9);
        //this.TexturesNahgs = Resources.LoadAll<Sprite> ("CharsGame/Nahgs/NahgsAlive/" + this.indexTexture);
        this.animator = gameObject.GetComponent<Animator>();
        this.animator.runtimeAnimatorController = Resources.Load("CharsGame/Nahgs/NahgsAlive/" + this.indexTexture + "/Controller/Controller") as RuntimeAnimatorController;
		gameObject.GetComponent<Rigidbody2D> ().gravityScale = NahgBehavior.Gravity;

		gameObject.transform.position = new Vector3 (0, 6, -3);
        this.name = this.indexTexture.ToString();

        gameObject.name = this.name;

//		Debug.Log (this.TexturesNahgs.Length);
	}

	void Start() 
	{
		//Sprite s = gameObject.GetComponent<SpriteRenderer> ().sprite = this.TexturesNahgs[Random.Range(0, this.TexturesNahgs.Length - 1)];

        //this.name = s.name;

	}

	public void addVelocity()
	{
		NahgBehavior.Gravity += 0.0017f;
		gameObject.GetComponent<Rigidbody2D> ().gravityScale = NahgBehavior.Gravity;
		Debug.Log ("Run + Gravity: " + NahgBehavior.Gravity);
	}

	public void setGravity(float scale)
	{
		NahgBehavior.Gravity = scale;
	}

	public void stopNahg()
	{
		this.gameObject.GetComponent<Rigidbody2D> ().simulated = false;
		this.gameObject.transform.position = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
	}

	public void playNahg()
	{
		this.gameObject.GetComponent<Rigidbody2D>().simulated = true;;
	}

	public string getName()
	{
		return this.name;	
	}

	public IEnumerator reverseNahg()
	{
		this.gameObject.transform.position = new Vector3 (0, this.gameObject.transform.position.y + 6 * Time.deltaTime, 0);
		yield return 0;
	}

	public void animationTheWinnerNahg(float position)
	{
        StartCoroutine (this.theWinnerNahg ());

        Debug.Log(position);
	}

    public IEnumerator theWinnerNahg()
	{
        this.gameObject.transform.position = new Vector3 (0, this.gameObject.transform.position.y - 1 * Time.deltaTime, 0);

		yield return 0;
	}

}
