using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using System;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

using SaveLoad;

public class MenuController: MonoBehaviour {

	private GameObject[] Menus;

	private Banner banner;

	private SaveLoadManager sLManager;

	private GameObject FistMenu;
	private GameObject Credits;
	private GameObject SelectCharacter;
	private GameObject GameOver;

	private  GameObject ButtonMusic;

	public static bool Changed = false;

    public static bool Audio = true;

	private Sprite[] MusicTextures;

	private AudioSource AS;

	private GameObject LastScore, BestScore;

	private SocialClass socialClass;

	private int score, bestScore;

	private GameObject charsOver;

	private GameObject buttonAds;

	private Interstitiales inter;

	public GameObject hatchRight;
	public GameObject hatchLeft;

	AudioClip hatchesSound;

	void Awake()
	{
		this.LastScore = GameObject.Find("LastScore");
		this.BestScore = GameObject.Find("BestScore");

		this.sLManager = GameObject.Find ("SaveLoadManager").GetComponent<SaveLoadManager>();
		this.buttonAds = GameObject.Find ("NoAds");

		this.inter = GameObject.Find ("Interstitial").GetComponent<Interstitiales>();

		this.banner = GameObject.Find ("Banner").GetComponent<Banner> ();

		this.charsOver = GameObject.Find ("CharsOver");

		this.MusicTextures = Resources.LoadAll <Sprite>("Menu/TextureMusic");
		this.AS = GameObject.Find ("Pather").GetComponent<AudioSource> ();

		this.ButtonMusic = GameObject.Find ("Music").gameObject;

		this.Menus = GameObject.FindGameObjectsWithTag("Menus");
		this.socialClass = GameObject.Find ("Social").GetComponent<SocialClass> ();

		foreach (GameObject menu in this.Menus) {

			if (menu.name.Equals ("FistMenu"))
				this.FistMenu = menu;
			
			else if (menu.name.Equals ("Credits"))
				this.Credits = menu;
			
			else if (menu.name.Equals ("PreviewGame"))
				this.SelectCharacter = menu;
			
			else if(menu.name.Equals("GameOver"))
				this.GameOver = menu;
		}

		//this.charsOver.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("CharsGame/Players/" + SelectChar.NameSelectedPlayer + "/" + GameController.evolutionNumberStatic + "/3");
		this.charsOver.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("CharsGame/Players/" + SelectChar.NameSelectedPlayer + "/" + GameController.evolutionNumberStatic + "/3/controller") as RuntimeAnimatorController;
	}


	// Use this for initialization
	void Start () {

		this.hatchesSound = Resources.Load<AudioClip>("Sounds/HatchesClose");

		if (this.sLManager.pData.ads) {
			this.banner.showBanner ();
		}

		if (MenuController.Changed) {

			if (this.sLManager.pData.ads) {
				//this.gameObject.GetComponent<Interstitiales> ().createInter ();

				this.inter.showIntestitial ();
			}

			if (this.sLManager.pData.ads) {
				
				Debug.Log ("Show2");
			}
			else {
			//	this.buttonAds.GetComponent<MeshRenderer> ().enabled = false;
				this.buttonAds.GetComponent<Image>().sprite = Resources.Load<Sprite>("NewSprites/NoAds/no-ads-ok");


				this.buttonAds.GetComponent<Button> ().enabled = false;
			//	this.buttonAds.GetComponent<Button> ().enabled = false;
				
			
			}


			GameObject[] menus = { this.FistMenu, this.Credits, this.SelectCharacter};
			this.PresentMenu (this.GameOver, menus);
			this.score = this.sLManager.pData.score;

			this.bestScore = this.sLManager.pData.bestScore;

			if (this.score > this.bestScore) {
				this.bestScore = this.score;
				this.sLManager.SaveBestScore(this.bestScore);
				Social.ReportScore(this.bestScore, "com.pugballgames.score.leaderboard", (bool success) => {
					// handle success or failure
				});
			}

			this.LastScore.GetComponent<Text> ().text = this.score.ToString();
			this.BestScore.GetComponent<Text> ().text = this.bestScore.ToString();

			this.socialClass.assignButtonReference ();
		}

        if (MenuController.Audio) {
            MenuController.Audio = true;
			this.ButtonMusic.GetComponent<Image> ().sprite = this.MusicTextures [0].name == "Music" ? this.MusicTextures [0] : this.MusicTextures [1];
			this.AS.enabled = true;
        } else if (!MenuController.Audio) {
			MenuController.Audio = false;
			this.ButtonMusic.GetComponent<Image> ().sprite = this.MusicTextures [0].name == "No-Music" ? this.MusicTextures [0] : this.MusicTextures [1];
			this.AS.enabled = false;
		}

        //this.musicControll();
	}

	private void musicControll()
	{
		if (!MenuController.Audio) {
			MenuController.Audio = true;
			this.ButtonMusic.GetComponent<Image> ().sprite = this.MusicTextures [0].name == "Music" ? this.MusicTextures [0] : this.MusicTextures [1];
			this.AS.enabled = true;
			this.AS.Play ();
		} else if (MenuController.Audio) {
			MenuController.Audio = false;
			this.ButtonMusic.GetComponent<Image> ().sprite = this.MusicTextures [0].name == "No-Music" ? this.MusicTextures [0] : this.MusicTextures [1];
			this.AS.enabled = false;
			this.AS.Stop ();
		}
	}

    private void stopMusic()
	{
		//this.AS.enabled = false;
        this.AS.Stop();
	}

	public void Actions(int index)
	{
		switch (index) 
		{
		//Star Game
		case 0:
			SelectChar.NameSelectedPlayer = gameObject.GetComponent<SelectChar> ().getName ();
			StartCoroutine (this.moveMenus (this.FistMenu, this.SelectCharacter));
			break;
		//Change to subMenu
		case 1:
			this.musicControll();
			break;
		//Change to Main Menu
		case 2:
				StartCoroutine (this.moveMenus (this.Credits, this.GameOver));
			break;
		//Replay
		case 3:
			//	this.gameObject.GetComponent<Interstitiales> ().showIntestitial ();

			StartCoroutine(this.Change(this.GameOver));
                if (this.sLManager.pData.ads)
                {
                    this.inter.loadInter();
                }
			break;
		case 4:
			StartCoroutine (this.moveMenus (this.GameOver, this.FistMenu));
			break;
		case 5:
			//this.destroyAds ();
                if (this.sLManager.pData.ads)
                {
                    this.inter.loadInter();
                }
			StartCoroutine (this.Change (this.SelectCharacter));
			break;
		case 7:
				StartCoroutine (this.moveMenus (this.GameOver, this.Credits));
			break;
            case 8:
                StartCoroutine(this.moveMenus(this.GameOver, this.FistMenu));
                break;

		}
	}

	private IEnumerator moveMenus(GameObject menuUp, GameObject menuDown)
	{
		while (menuUp.transform.position.y <= 10) {
			menuUp.transform.position = new Vector3 (0, menuUp.transform.position.y + 5 * Time.deltaTime , -1);
			yield return null;
		}

		yield return new WaitForSeconds (0.01f);

		while (menuDown.transform.position.y >= 0) {
			menuDown.transform.position = new Vector3 (0, menuDown.transform.position.y - 5 * Time.deltaTime , -2);
			yield return null;
		}
	}


	//Here 
	private IEnumerator Change(GameObject menuToMove)
	{
		while (menuToMove.transform.position.y <= 10) {
			menuToMove.transform.position = new Vector3 (0, menuToMove.transform.position.y + 5 * Time.deltaTime , -2);
			yield return null;
		}
		/*this.stopMusic();
		yield return new WaitForSeconds(0.5f);
		this.AS.clip = this.hatchesSound;
		this.AS.Play();
		hatchClosing();
		yield return new WaitForSeconds(1.7f);*/
		this.ShowGameScene();
	}

	private void hatchClosing()
	{
		this.hatchLeft.GetComponent<Animator>().SetTrigger("TriggerLeft");
		this.hatchRight.GetComponent<Animator>().SetTrigger("TriggerRight");

		/*if(this.hatchLeft.GetComponent<Animation>().isPlaying.Equals() && this.hatchRight.GetComponent<Animation>().isPlaying.Equals(false))
		{
			
		}*/
	}

	private void ShowGameScene()
	{
		if (this.sLManager.pData.ads) {
		this.banner.destroyBanner ();
		}
		MenuController.Changed = true;
		SceneManager.LoadScene ("GameScene");
	}

	private void PresentMenu(GameObject MenuToShow, GameObject[] otherMenu)
	{
		foreach (GameObject menu in otherMenu) {
			menu.transform.position = new Vector3 (menu.transform.position.x, 11, menu.transform.position.z);
		}

//		MenuToShow.transform.position = new Vector3 (0, 0, 0);
		StartCoroutine(this.downMenu(MenuToShow));
	}

	private IEnumerator downMenu(GameObject menuDown)
	{
		while (menuDown.transform.position.y >= 0) {
			menuDown.transform.position = new Vector3 (0, menuDown.transform.position.y - 5 * Time.deltaTime , -2);
			yield return null;
		}
	}
		

	void OnDisable()
	{
		this.socialClass.detach ();
	}
}
