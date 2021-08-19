using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

using SaveLoad;

public class GameController : MonoBehaviour
{
	bool down = true;
    
	public bool touchIsEnable = true;

	public Sprite[] pressTexture;

    private bool lost = false;

    private EnterToPOC pof;
    private ChangePositionsButtons ChangeButtons;

    private SaveLoadManager sLManager;

    private GameObject Spawn;

    private GameObject bodyPlayer;

    //Assigned frome Editos
    public GameObject Brako;

    SpawnControl spawnControl;

    public int Score = 0;
    private AudioSource[] audioSources;
    PlayerBehavior PB;

    private string path = "CharsGame/Players/";

    public SocialClass socialClass;


    //Player Resorce (To Instantiate)
    private GameObject PR;
    
    private GameObject[] Buttons;

    //Rules Dificult Phases  20
    private int FistPhase = 5, SecondPhase = 40, ThirdPhase= 60;

    public static int evolutionNumberStatic = 1;

    int evolutionNumber;

    private Animator anim;

    public GameObject tutorial;
    void Awake()
    {
        GameController.evolutionNumberStatic = 1;
        this.evolutionNumber = GameController.evolutionNumberStatic;
        this.audioSources = this.gameObject.GetComponents<AudioSource> ();

        this.sLManager = GameObject.Find ("SaveLoadManager").GetComponent<SaveLoadManager> ();

        this.Spawn = GameObject.FindGameObjectWithTag ("Spawn");
        this.spawnControl = Spawn.GetComponent<SpawnControl> ();

        this.socialClass = GameObject.Find ("Social").GetComponent<SocialClass> ();

        this.Buttons = GameObject.FindGameObjectsWithTag ("Button");

        this.PR = Resources.Load<GameObject> (this.path + SelectChar.NameSelectedPlayer + "/" + SelectChar.NameSelectedPlayer);
        this.PR.tag = "Player";

        if (PR.GetComponent<PlayerBehavior> () == null) {
            this.PB = this.PR.AddComponent (typeof(PlayerBehavior)) as PlayerBehavior;
        }
        if (PR.GetComponent<AudioSource> () == null) {
            this.AudioConfigurate ();
        }

		
        Instantiate (this.PR, new Vector3(0, -4.07874f, -4), new Quaternion(0,0,0,0));

        this.bodyPlayer = GameObject.FindGameObjectWithTag ("Player");
        this.PB = this.bodyPlayer.GetComponent<PlayerBehavior> ();

        this.anim = GameObject.Find("Explotion").GetComponent<Animator>();
    }


    private void AudioConfigurate()
    {
        var audio = this.PR.AddComponent (typeof(AudioSource)) as AudioSource;
        audio.clip = Resources.Load<AudioClip> ("Sounds/Hit");
        audio.playOnAwake = false;
        var audio2 = this.PR.AddComponent (typeof(AudioSource)) as AudioSource;
        audio2.playOnAwake = false;
        audio2.clip = Resources.Load<AudioClip> ("Sounds/Jump");
    }


    void Start () {
        //Get Script EnterToPOC
        this.pof = GameObject.FindGameObjectWithTag ("Mira").gameObject.GetComponent<EnterToPOC> ();
        //Class to move buttons
        this.ChangeButtons = new ChangePositionsButtons (this.Buttons);

        //this.setTransformCollider ();

        if (!MenuController.Audio) {

            this.destroyMusic();
        }

        if(!this.sLManager.pData.getTutorial()){
            this.tutorial.SetActive(true);
            Time.timeScale = 0;
            //this.sLManager.pData.setTutorial(true);
        }else{
            this.tutorial.SetActive(false);
        }

    }

    public void destroyMusic(){
        this.audioSources[0].enabled = false;
        this.audioSources[1].enabled = false;
    }

    public void pauseMusic()
    {
        if (MenuController.Audio)
        {
            if (audioSources[0].clip.name == "Music")
            {
                audioSources[0].Stop();
            
            }
            else { 
                audioSources[1].Stop();
            }
        }
    }

    public void playMusic()
    {
        if (MenuController.Audio){
            if (audioSources[0].clip.name == "Music")
            {
                audioSources[0].Play();

            }
            else
            {
                audioSources[1].Play();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D colEnemy)
    {
        if (colEnemy.gameObject.CompareTag ("Enemy")) {

            //Play and Stop Sounds
            if (MenuController.Audio) {
                if (this.audioSources [0].clip.name.Equals ("Music")) {
                    this.audioSources [0].Stop ();
                    this.audioSources [1].Play ();
                } else {
                    this.audioSources [1].Stop ();
                    this.audioSources [0].Play ();
                }
            }

                this.lost = true;
        }
    }

    void Update () {
        if (!this.lost) {
            
            //Touch
          #if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount > 0){
                //Tutorial
                if (!sLManager.pData.getTutorial())
                {
                    Time.timeScale = 1;
                    this.tutorial.SetActive(false);
                    this.sLManager.pData.setTutorial(true);
                }
                if (Input.GetTouch(0).phase == TouchPhase.Ended && Time.timeScale == 1 && !gameObject.GetComponent<Pause>().isCountDown){
                    
                    

                        Vector3 position = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        foreach (GameObject b in this.Buttons)
                        {
                        if (b.gameObject.GetComponent<BoxCollider2D>().OverlapPoint(new Vector2(position.x, position.y)))
                            {
                                this.Verificator(b);
                            }
                        }
                    }
                }
            #endif
            #if UNITY_EDITOR
                //Mouse
            if (Input.GetMouseButtonDown (0)) {
                //Tutorial
                if (!sLManager.pData.getTutorial())
                {
                    Time.timeScale = 1;
                    this.tutorial.SetActive(false);
                    this.sLManager.pData.setTutorial(true);
                }

                    Vector2 position = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
                    RaycastHit2D hitInfo = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (position), Vector2.zero);
    
                if(hitInfo.collider != null && Time.timeScale == 1 && !gameObject.GetComponent<Pause>().isCountDown)
                    this.Verificator (hitInfo.collider.gameObject);
                }   
            
            #endif

            //End Mouse
        } else {
            this.lostMethod ();
            if(this.bodyPlayer.transform.position.y >= 5.5f)
         
                
                SceneManager.LoadScene ("Menus");
        }
    }

    private void Verificator(GameObject Button)
    {
        
        try {
            if (Button.CompareTag ("Button")) {

                switch (Button.name) {
                case "1":
						StartCoroutine(this.theButtonPressed(Button));
                    if (this.pof.getNahg () != null && this.pof.getNahg ().name.Equals ("1")) {
                            StartCoroutine(this.RightButton());
                    } else {
                        this.BadButton ();
                    }
                    break;
                case "2":
						StartCoroutine(this.theButtonPressed(Button));
                    if (this.pof.getNahg () != null && this.pof.getNahg ().name.Equals ("2")) {
                            StartCoroutine(this.RightButton());
                    } else {
                        this.BadButton ();
                    }
                    break;
                case "3":
						StartCoroutine(this.theButtonPressed(Button));
                    if (this.pof.getNahg () != null && this.pof.getNahg ().name.Equals ("3")) {
                            StartCoroutine(this.RightButton());
                    } else {
                        this.BadButton ();
                    }
                    break;
                case "4":
						StartCoroutine(this.theButtonPressed(Button));
                    if (this.pof.getNahg () != null && this.pof.getNahg ().name.Equals ("4")) {
                            StartCoroutine(this.RightButton());
                    } else {
                        this.BadButton ();
                    }
                    break;
                case "5":
						StartCoroutine(this.theButtonPressed(Button));
                    if (this.pof.getNahg () != null && this.pof.getNahg ().name.Equals ("5")) {
                            StartCoroutine(this.RightButton());
                    } else {
                        this.BadButton ();
                    }
                    break;
                case "6":
						StartCoroutine(this.theButtonPressed(Button));
                    if (this.pof.getNahg () != null && this.pof.getNahg ().name.Equals ("6")) {
                            StartCoroutine(this.RightButton());
                    } else {
                        this.BadButton ();
                    }
                    break;
                case "7":
						StartCoroutine(this.theButtonPressed(Button));
                    if (this.pof.getNahg () != null && this.pof.getNahg ().name.Equals ("7")) {
                            StartCoroutine(this.RightButton());
                    } else {
                        this.BadButton ();
                    }
                    break;
                case "8":
						StartCoroutine(this.theButtonPressed(Button));
                    if (this.pof.getNahg () != null && this.pof.getNahg ().name.Equals ("8")) {
                            StartCoroutine( this.RightButton ());
                    } else {
                        this.BadButton ();
                    }    
                    break;
                }
                //Need implement condition to verificate if the buttons pressed is right
            }
        } catch (NullReferenceException e) {
			e.GetBaseException();
        }
    }

	IEnumerator theButtonPressed(GameObject button){
		SpriteRenderer spriteRenderer = button.GetComponent<SpriteRenderer>();
		Sprite beforePress = spriteRenderer.sprite;

		spriteRenderer.sprite = this.pressTexture[Int32.Parse(button.name) - 1];

		yield return new WaitForSeconds(0.1f);
		spriteRenderer.sprite = beforePress;
	}

    //The button pressed is right
    private IEnumerator RightButton()
    {
          this.Score += 1;

        this.bodyPlayer.GetComponent<PlayerBehavior>().Jump();

        //
        this.AddDifficulty();



        this.Brako.GetComponent<BrakoBehavior> ().AddToScore (this.Score);

        this.killerNahg ();
        this.checkToEvolve();   
        yield return new WaitForSeconds(0.1f);
        this.ChangeButtons.ChangePosition();
    }

    private void AddDifficulty()
    {

        if (this.Score <= this.FistPhase) {
            this.Spawn.GetComponent<SpawnControl> ().fistPhase ();
            Debug.Log ("Fist Phase");
            Debug.Log ("Score: " + this.Score);
        } else if (this.Score <= this.SecondPhase) {
            this.Spawn.GetComponent<SpawnControl> ().secondPhase ();

            Debug.Log ("Second Phase");
            Debug.Log ("Score: " + this.Score);
         
        } 
    }

    private void checkToEvolve()
    {
		//10
        if(this.Score == 10)
        {
            this.evolution(2);
            this.anim.SetTrigger("Trigger");
            
            this.achievement();
           }
		//50
        else if(this.Score == 50)
        {
            this.evolution(3);
            this.anim.SetTrigger("Trigger");
            
            this.achievement ();
        }
		//100
        else if(this.Score == 100)
        {
            this.evolution(4);
            this.anim.SetTrigger("Trigger");
            
            this.achievement ();

        }
    }

    private void achievement()
    {
        Debug.Log ("Se ejecutó");
        if (SelectChar.NameSelectedPlayer.Equals ("Alex"))
        {
            if(this.evolutionNumber.Equals(2)){
                this.socialClass.unlockAchievement (NahgResources.achievement_change_to_boogie);
                this.socialClass.revealingAchievement (NahgResources.achievement_change_to_boogie_tiger);
            }else if(this.evolutionNumber.Equals(3)){
                this.socialClass.unlockAchievement (NahgResources.achievement_change_to_boogie_tiger);
                this.socialClass.revealingAchievement (NahgResources.achievement_change_to_alex_gladiator);
            }else if(this.evolutionNumber.Equals(4)){
                this.socialClass.unlockAchievement (NahgResources.achievement_change_to_alex_gladiator);
            }
        }else if(SelectChar.NameSelectedPlayer.Equals("Brako"))
        {
            if(this.evolutionNumber.Equals(2)){
                this.socialClass.unlockAchievement (NahgResources.achievement_change_to_cool_brako);
                this.socialClass.revealingAchievement (NahgResources.achievement_change_to_brako_imitator);
            }else if(this.evolutionNumber.Equals(3)){
                this.socialClass.unlockAchievement (NahgResources.achievement_change_to_brako_imitator);
				this.socialClass.revealingAchievement(NahgResources.achievement_change_to_brako_nahg);
            }else if(this.evolutionNumber.Equals(4)){
				this.socialClass.unlockAchievement (NahgResources.achievement_change_to_brako_nahg);
            }
        }
        else if(SelectChar.NameSelectedPlayer.Equals("Roy"))
        {
            if(this.evolutionNumber.Equals(2)){
                this.socialClass.unlockAchievement (NahgResources.achievement_change_to_sean);
                this.socialClass.revealingAchievement (NahgResources.achievement_change_to_dino_sean);
            }else if(this.evolutionNumber.Equals(3)){
                this.socialClass.unlockAchievement (NahgResources.achievement_change_to_dino_sean);
                this.socialClass.revealingAchievement (NahgResources.achievement_change_to_mr_morgan);
            }else if(this.evolutionNumber.Equals(4)){
                this.socialClass.unlockAchievement (NahgResources.achievement_change_to_mr_morgan);
            }
        }
        else if(SelectChar.NameSelectedPlayer.Equals("Tony"))
        {
            if(this.evolutionNumber.Equals(2)){
                this.socialClass.unlockAchievement (NahgResources.achievement_change_to_elvis);
                this.socialClass.revealingAchievement (NahgResources.achievement_change_to_beibi);
            }else if(this.evolutionNumber.Equals(3)){
                this.socialClass.unlockAchievement (NahgResources.achievement_change_to_beibi);
                this.socialClass.revealingAchievement (NahgResources.achievement_change_to_emilio);
            }else if(this.evolutionNumber.Equals(4)){
                this.socialClass.unlockAchievement (NahgResources.achievement_change_to_emilio);
            }
        }
        else if(SelectChar.NameSelectedPlayer.Equals("Vane"))
        {
            if(this.evolutionNumber.Equals(2)){
                this.socialClass.unlockAchievement (NahgResources.achievement_change_to_vane_francesca);
                this.socialClass.revealingAchievement (NahgResources.achievement_change_to_cherry);
            }else if(this.evolutionNumber.Equals(3)){
                this.socialClass.unlockAchievement (NahgResources.achievement_change_to_cherry);
                this.socialClass.revealingAchievement (NahgResources.achievement_change_to_queen_vane);
            }else if(this.evolutionNumber.Equals(4)){
                this.socialClass.unlockAchievement (NahgResources.achievement_change_to_queen_vane);
            }
        }
    }


    //If player is wrong
    private void BadButton()
    {
        StartCoroutine(pof.Wrong ());
    }

    private void lostMethod()
    {
        this.spawnControl.setBoolSpawn (false);
        this.spawnControl.StopAllCoroutines ();

        gameObject.GetComponent<Pause>().buttonPause.SetActive(false);

        //ControlNahgs
        this.bodyPlayer.GetComponent<Rigidbody2D> ().simulated = false;

        StartCoroutine(this.Sequence());

        var n = this.spawnControl.getNahgByIndex (0);

        if (this.down) {
            if (n.transform.position.y >= gameObject.transform.position.y + 0.5f  && down) {
                
                StartCoroutine (n.GetComponent<NahgBehavior> ().theWinnerNahg ());
            } else {
                this.down = false;
            }
        }

		foreach(GameObject b in this.Buttons){
			b.SetActive(false);
		}

		pof.gameObject.SetActive(false);
    }

    IEnumerator Sequence()
    {
        this.spawnControl.stopAllNahgs ();

        //this.PB.up ();

        yield return new WaitForSeconds (2);



        this.spawnControl.reverse ();
        PB.Loss ();

        yield return 0;
    }



    private void killerNahg()
    {
        GameObject nahg = this.pof.getNahg ().gameObject;
        //Get the right texture for current nahg

        this.spawnControl.deleteItemNahg (0);
        this.ChangeTextureLossOfNahg();

        Destroy(this.pof.getNahg(), 1.5f);
    }

    public void ChangeTextureLossOfNahg()
    {
        /*GameObject nahg = this.pof.getNahg ().gameObject;
        String name = nahg.GetComponent<SpriteRenderer> ().sprite.name;
        Sprite t = Resources.Load<Sprite> ("CharsGame/Nahgs/NahgsKO/" + name);
        nahg.GetComponent<SpriteRenderer> ().sprite = t;*/

        GameObject nahg = this.pof.getNahg().gameObject;
        nahg.GetComponent<Animator>().SetTrigger("HitNahg");
    }

    void OnDisable()
    {
        this.sLManager.SaveScore (this.Score);

        this.setBestScoreChar ();

        SelectChar.NameSelectedPlayer = this.PR.name;
    }

    private void setBestScoreChar()
    {
        int saveScore = this.sLManager.pData.getValueDictionary (SelectChar.NameSelectedPlayer);

        if(saveScore <= this.Score){
            this.sLManager.SaveBestScoreChars(SelectChar.NameSelectedPlayer, this.Score);
        }
    }

    public void evolution(int phase)
    {
        GameController.evolutionNumberStatic = phase;
        this.evolutionNumber = phase;
        this.PB.changeTextures (phase);
        this.PB.changeBoxCollidersSize ();
    }

    public void setTransformCollider()
    {
        Vector2 v = new Vector2(this.PB.getSpritesSize ().x, this.PB.getSpritesSize ().y);

        gameObject.GetComponent<BoxCollider2D> ().size = v;
        gameObject.transform.position = new Vector3(this.bodyPlayer.transform.position.x, this.bodyPlayer.transform.position.y + 0.05f, 0);

    }
}
