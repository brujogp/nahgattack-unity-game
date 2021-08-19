using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    private GameController gameController;

	// Use this for initialization
	void Awake () {
        this.gameController = GameObject.Find("GameController").GetComponent<GameController>();
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
        if(collision.gameObject.tag == "Player")
        {
            this.gameController.setTransformCollider();
        }
	}
}
