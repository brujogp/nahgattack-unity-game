using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SaveLoad;

public class SelectChar : MonoBehaviour {

	private Sprite[] Chars;
	private GameObject showChars, BestCharScore;
	private Text NameChar;
	private SaveLoadManager sLManager;

	public static string NameSelectedPlayer = "Alex";


	int count = 0;


	void Awake(){
		this.Chars = Resources.LoadAll<Sprite> ("CharsGame/Players/PresentationPlayers");
		this.NameChar = GameObject.Find ("NamePlayer").GetComponent<Text>();

		this.sLManager = GameObject.Find ("SaveLoadManager").GetComponent<SaveLoadManager>();
		this.BestCharScore = GameObject.Find ("BestCharScore");

		this.showChars = GameObject.Find ("Chars");
		this.showChars.GetComponent<SpriteRenderer> ().sprite = this.Chars [0];

		/*Sprite tempChar = this.Chars[0];

		this.Chars[0] = this.Chars[3];
		this.Chars[3] = tempChar;
        
		tempChar = this.Chars[1];
		this.Chars[1] = this.Chars[4];
		this.Chars[4] = tempChar;*/

		this.NameChar.text = this.Chars [0].name;



		//		this.gc = gameObject.AddComponent (typeof(GameController)) as GameController;

		Debug.Log( this.Chars.Length);

		
	}

	void Star()
	{
          
	}

	public string getName()
	{
		return this.NameChar.text;
	}

	public void OnEnable()
	{
        this.updateInfoChar (); 
	}

	public void changeChar(int index)
	{
		
		if (index.Equals (0)) {

			if (count == 0)
				count = this.Chars.Length;
			
			this.count -= 1;
			this.showChars.GetComponent<SpriteRenderer> ().sprite = this.Chars [count];
			SelectChar.NameSelectedPlayer = this.Chars [count].name;
			this.updateInfoChar ();

//			Debug.Log (this.Chars[count].name);
		} else if (index.Equals (1)) {
			this.count += 1;
			if (count == this.Chars.Length)
				count = 0;
			
			this.showChars.GetComponent<SpriteRenderer> ().sprite = this.Chars [count ];
			SelectChar.NameSelectedPlayer = this.Chars [count].name;
			this.updateInfoChar ();
//			Debug.Log (count);
		}

		this.NameChar.text = (this.Chars[count].name.Equals("Tony")) ?  "Tim" : this.NameChar.text = this.Chars[count].name;
		Debug.Log (this.NameChar.text);


	}

	private void updateInfoChar()
	{
		if(this.sLManager.pData != null)
			this.BestCharScore.GetComponent<Text> ().text = this.sLManager.pData.getValueDictionary (SelectChar.NameSelectedPlayer).ToString ();
//		Debug.Log(this.sLManager.pData.getValueDictionary (SelectChar.NameSelectedPlayer).ToString ());
	}

	// Update is called once per frame
	void Update () {
		this.updateInfoChar (); 
	}

	void OnDisable()
	{
//		SelectChar.NameSelectedPlayer = this.Chars [count].name;
//		Debug.Log (SelectChar.NameSelectedPlayer);
	}
}
