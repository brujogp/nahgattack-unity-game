using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace SaveLoad{
	public class SaveLoadManager: MonoBehaviour{

		private static SaveLoadManager saveLoadManager;
		private string pathFile;
		public PlayerData pData{ get; set;}

		void Awake()
		{
			if (this.pData == null) {

				this.pData = new PlayerData ();
			} 



			Debug.Log (Application.persistentDataPath);
			this.pathFile = Application.persistentDataPath + "/data.sav";

			if (SaveLoadManager.saveLoadManager == null) {
				SaveLoadManager.saveLoadManager = this;
				GameObject.DontDestroyOnLoad (gameObject);
			} else if(SaveLoadManager.saveLoadManager != null){
				Destroy (gameObject);
			}

			this.LoadData ();

			Debug.Log("State of ads: " + this.pData.ads);
		}

		public void SaveTutorialStatus(bool statusTutorial){
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			using(FileStream stream = File.Create(this.pathFile)){
				if (this.pData == null) {

                    this.pData = new PlayerData ();
                }
				this.pData.setTutorial(statusTutorial);

				binaryFormatter.Serialize(stream, this.pData);
			}
		}

		public void SaveScore(int score)
		{

			BinaryFormatter bf = new BinaryFormatter ();
			using (FileStream stream = File.Create (this.pathFile)) {
				if (this.pData == null) {

					this.pData = new PlayerData ();
				} 

				this.pData.score = score;

				bf.Serialize (stream, this.pData);
			}
		}

		public void SaveBestScore(int bestScore)
		{

			BinaryFormatter bf = new BinaryFormatter ();
			using (FileStream stream = File.Create (this.pathFile)) {
				if (this.pData == null) {

					this.pData = new PlayerData ();
				} 

				this.pData.bestScore = bestScore;

				bf.Serialize (stream, this.pData);
			}
		}

		public void SaveBestScoreChars(string name, int bestScore)
		{

			BinaryFormatter bf = new BinaryFormatter ();
			using (FileStream stream = File.Create (this.pathFile)) {
				if (this.pData == null) {

					this.pData = new PlayerData ();
				} 

				this.pData.setDictionary (name, bestScore);

				bf.Serialize (stream, this.pData);
			}
		}

		public void SaveStateAds(bool state)
		{

			BinaryFormatter bf = new BinaryFormatter ();
			using (FileStream stream = File.Create (this.pathFile)) {
				if (this.pData == null) {

					this.pData = new PlayerData ();
				} 

				this.pData.ads = state;

				bf.Serialize (stream, this.pData);
			}
		}

		public void LoadData()
		{
			if(File.Exists(this.pathFile)){
					BinaryFormatter bf = new BinaryFormatter();
					
				using(FileStream stream = File.Open(this.pathFile, FileMode.Open))
				{
					this.pData = (PlayerData)bf.Deserialize(stream);
				}
					
			}
			else{
				Debug.Log ("No existe");
				this.pData.ads = true;
			}
		}

//		public void getStateAds()
//		{
//			if(File.Exists(this.pathFile)){
//				BinaryFormatter bf = new BinaryFormatter();
//
//				using(FileStream stream = File.Open(this.pathFile, FileMode.Open))
//				{
//					this.pData = (PlayerData)bf.Deserialize(stream);
//				}
//
//			}
//			else{
//				Debug.Log ("No existe");
//			}
//		}

//		public static void removeFile()
//		{
//			if(File.Exists(Application.persistentDataPath + "/Data.sav"))
//			{
//				File.Delete (Application.persistentDataPath + "/Data.sav");	
//				Debug.Log ("Deleted");
//			}else
//			{
//				Debug.Log ("De todos modos no existe");
//			}
//		}
	}

	[Serializable]
	public class PlayerData
	{
		public int score{get; set;}
		public int bestScore { get; set;}
		private bool IsShowedTutorial = false;

		public bool ads { get; set;}

		Dictionary <string, int> ScoreChars;
		public PlayerData()
		{
			this.ScoreChars = new Dictionary<string, int> ();
		}

		public bool getTutorial(){
			return this.IsShowedTutorial;
		}

		public void setTutorial(bool status)
        {
			this.IsShowedTutorial = status;
        }

		public Dictionary<string, int> getDictionary()
		{
			return this.ScoreChars;
		}

		public int getValueDictionary(string name)
		{
//			try{
//				return this.ScoreChars[name];
//			}catch(KeyNotFoundException) {
//				this.setDictionary (name, 0);
//				return this.ScoreChars[name];
//			}

			if (this.ScoreChars.ContainsKey (name)) {
				return this.ScoreChars [name];
			} else {
				this.setDictionary (name, 0);
				return this.ScoreChars [name];
			}
		}

		public void setDictionary(string name, int score)
		{	
//			try{
//				this.ScoreChars [name] = score;
//			}
//			catch(KeyNotFoundException) {
//				this.ScoreChars.Add (name, score);
//			}

			if (this.ScoreChars.ContainsKey (name)) {
				this.ScoreChars [name] = score;
			} else {
				this.ScoreChars.Add (name, score);
			}

			foreach(KeyValuePair<string, int> item in this.ScoreChars){
				Debug.Log ("Item of dictionary: " + item.Key + " And the valute: " + item.Value);

			}
		}
	}
}
