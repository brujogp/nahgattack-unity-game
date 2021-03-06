using UnityEngine;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms.GameCenter;

using UnityEngine.UI;

public class SocialClass: MonoBehaviour {

	public static SocialClass InstanceSocial { get; private set;}

	private Button leaderboard, achievements;

	private void Awake()
	{

		//singleton
		if (InstanceSocial == null) {
			InstanceSocial = this;
			DontDestroyOnLoad (gameObject);
		}
		else {
			Destroy (gameObject);
		}
		//

		this.assignButtonReference ();

		//PlayGamesPlatform.DebugLogEnabled = true;
		//PlayGamesPlatform.Activate ();
	}


	void Start()
	{


		Social.localUser.Authenticate ((bool success) =>{
			if(success)
			{
				//((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.BOTTOM);

			}
			else
			{
			}
		});
	}
	// Us

	void OnEnable()
	{
		this.assignButtonReference ();
		Debug.Log ("OnEnable");
	}

	public void revealingAchievement(string ID)
	{
		Social.ReportProgress (ID, 00.0f, (bool success) =>{
			
		});
	}

	public void unlockAchievement(string ID)
	{
		GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
		Social.ReportProgress (ID, 100.0f, (bool success) =>{

		});
	}

	public void showLeaderboardIU()
	{
		//PlayGamesPlatform.Instance.ShowLeaderboardUI ();
		Social.ShowLeaderboardUI();
		Debug.Log ("Run run");
	}

	public void showAchievenentIU()
	{
		Social.ShowAchievementsUI ();
		Debug.Log ("Run run");
	}


	public void assignButtonReference()
	{
		GameObject leaderboardG, achievementsG;

		if (GameObject.Find ("GameScoreCenter") != null && GameObject.Find ("Achievements") != null) {
			leaderboardG = GameObject.Find ("GameScoreCenter");
			achievementsG = GameObject.Find ("Achievements");

			this.leaderboard = leaderboardG.GetComponent<Button> ();
			this.achievements = achievementsG.GetComponent<Button> ();
		}

		this.leaderboard.onClick.AddListener (this.showLeaderboardIU);
		this.achievements.onClick.AddListener (this.showAchievenentIU);
	}

	public void detach()
	{
		this.leaderboard = null;
		this.achievements = null;
	}

	void OnDisable()
	{
		Debug.Log ("OnDisable");
		this.detach ();
	}
}
