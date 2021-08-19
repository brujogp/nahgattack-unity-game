using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnControl : MonoBehaviour {

	private List<GameObject> listNahgs;

	private GameObject Nahg;
	public bool SpawnActive = true;
	public float SpawnRate = 5;


	void Awake()
	{
		this.Nahg = Resources.Load<GameObject> ("Prefabs/Nahg/NahgTemplate");
	}

	// Use this for initialization
	void Start () 
	{
		//Start The spawn
		this.listNahgs = new List<GameObject>();
		if(this.Nahg != null)
			StartCoroutine(this.Spawn());	
	}

	public IEnumerator Spawn()
	{      
		//FIXME: Bad!
		while (this.SpawnActive) {
//			GameObject nahg = this.Nahg;
			this.listNahgs.Add(Instantiate (this.Nahg, gameObject.transform));
            //this.listNahgs[this.listNahgs.Count -1].name = this.listNahgs[this.listNahgs.Count - 1].get
//			this.listNahgs.Add (nahg);
			Debug.Log ("Number of Items: " + this.listNahgs.Count);
			Debug.Log("Rate: " + this.SpawnRate);
			yield return new WaitForSeconds (this.SpawnRate);
		}
	}

	void Update()
	{
//		this.listNahgs.Add(GameObject.FindGameObjectWithTag("Enemy"));
	}

    
	public void fistPhase() 
	{
		if(this.SpawnRate >= 2)
		{
			this.SpawnRate -= 0.6f;
		}
	}

	public void secondPhase()
	{
		if (this.SpawnRate >= 0.8f)
        {
			this.SpawnRate -= 0.032f;
        }
		if(this.SpawnRate >= 1.1f && this.SpawnRate <= 1.6f){
			foreach(GameObject nahg  in this.listNahgs){
				nahg.GetComponent<NahgBehavior>().addVelocity();
				Debug.Log("Added velocity");
			}
		}
	}

	public void thirdPhase()
	{
		if(this.SpawnRate >= 0.5f)
		{
			this.SpawnRate -= 0.042f;
		}
	}

	public GameObject getNahgByIndex(int index)
	{
		return listNahgs [index];
	}

	public void deleteItemNahg(int itemNahg)
	{
		this.listNahgs.RemoveAt(itemNahg);
	}

//	public GameObject[] getListNahgs()
//	{
//		object[] array = this.listNahgs.ToArray();
//		return array as GameObject;
//	}

	public void stopAllNahgs()
	{
		try{	
			foreach (GameObject NGameObject in this.listNahgs) {
				NGameObject.GetComponent<NahgBehavior> ().stopNahg ();
		}
		}
		catch(MissingReferenceException e)
		{
			e.GetBaseException();
		}
	}

	public void playAllNahgs()
    {
        try
        {
            foreach (GameObject NGameObject in this.listNahgs)
            {
				NGameObject.GetComponent<NahgBehavior>().playNahg();
            }
        }
        catch (MissingReferenceException e)
        {

        }
    }

	public void reverse()
	{	

		foreach (GameObject nGO in this.listNahgs) {
			StartCoroutine (nGO.GetComponent<NahgBehavior> (). reverseNahg());

		}
	}

	public void setGravity(float grabity)
	{
		if (grabity == 0)
			this.Nahg.GetComponent<NahgBehavior>().setGravity(0.05f);
		else
			this.Nahg.GetComponent<NahgBehavior>().setGravity(grabity);
	}

	private void addVelocity()
	{
		this.Nahg.GetComponent<NahgBehavior> ().addVelocity ();
	}

	public void setBoolSpawn(bool Boolean)
	{
		this.SpawnActive = Boolean;
	}

//	public float getDificult()
//	{
//		return this.levelDificulted;
//	}
//
//	public void setDificult(float increment)
//	{
//		this.levelDificulted += increment;
//	}

	void OnDisable()
	{
		this.Nahg.GetComponent<NahgBehavior> ().setGravity (0.05f);
	}
}
