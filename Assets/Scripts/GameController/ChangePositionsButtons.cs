using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionsButtons{

	private GameObject[] Buttons;
	private ArrayList Positions = new ArrayList(); 
	private ArrayList Register = new ArrayList ();
	private int Touches = 0;

	public ChangePositionsButtons(GameObject[] Buttons)
	{
		this.Buttons = Buttons;

		foreach (GameObject p in this.Buttons) {
			this.Positions.Add (p.transform.position);
		}
	}

	public void ChangePosition()
	{
		this.Touches += 1;
		int index = Random.Range (1, this.Buttons.Length);

		if (this.Register.Count > 0) {
			if (this.Register [this.Register.Count - 1].Equals(index)) {
				index += 1;
			}
		}

		this.Register.Add (index);
			
		if(index == this.Buttons.Length){
			this.PatterOne ();
		}
		else{

			switch (index) {
			case 1:
				this.PatterOne ();
				break;
			case 2:
				this.PatterTwo ();
				break;
			case 3:
				this.PatterThree ();
				break;
			case 4:
				this.PatterFour ();
				break;
			case 5:
				this.PatterFive ();
				break;
			case 6:
				this.PatterSix ();
				break;
			case 7:
				this.PatterSeven ();
				break;
			default:
				Debug.Log("Error");
				break;
			}
		}
	}

	private void PatterOne()
	{
	this.Buttons [0].transform.position = (Vector3) this.Positions [1];
	this.Buttons [1].transform.position = (Vector3)this.Positions [2];
	this.Buttons [2].transform.position = (Vector3)this.Positions [3];
	this.Buttons [3].transform.position = (Vector3)this.Positions [4];
	this.Buttons [4].transform.position = (Vector3)this.Positions [5];
	this.Buttons [5].transform.position = (Vector3)this.Positions [6];
	this.Buttons [6].transform.position = (Vector3)this.Positions [7];
	this.Buttons [7].transform.position = (Vector3)this.Positions [0];

	}

	private void PatterTwo()
	{
	this.Buttons [0].transform.position = (Vector3)this.Positions [2];
	this.Buttons [2].transform.position = (Vector3)this.Positions [4];
	this.Buttons [4].transform.position = (Vector3)this.Positions [6];
	this.Buttons [6].transform.position = (Vector3)this.Positions [0];
	this.Buttons [1].transform.position = (Vector3)this.Positions [3];
	this.Buttons [3].transform.position = (Vector3)this.Positions [5];
	this.Buttons [5].transform.position = (Vector3)this.Positions [7];
	this.Buttons [7].transform.position = (Vector3)this.Positions [1];

	}

	private void PatterThree()
	{
	this.Buttons [0].transform.position = (Vector3)this.Positions [3];
	this.Buttons [3].transform.position = (Vector3)this.Positions [6];
	this.Buttons [6].transform.position = (Vector3)this.Positions [1];
	this.Buttons [1].transform.position = (Vector3)this.Positions [4];
	this.Buttons [4].transform.position = (Vector3)this.Positions [7];
	this.Buttons [7].transform.position = (Vector3)this.Positions [2];
	this.Buttons [2].transform.position = (Vector3)this.Positions [5];
	this.Buttons [5].transform.position = (Vector3)this.Positions [0];

	}

	private void PatterFour()
	{
	this.Buttons [0].transform.position = (Vector3)this.Positions [4];
	this.Buttons [4].transform.position = (Vector3)this.Positions [0];
	this.Buttons [1].transform.position = (Vector3)this.Positions [5];
	this.Buttons [5].transform.position = (Vector3)this.Positions [1];
	this.Buttons [2].transform.position = (Vector3)this.Positions [6];
	this.Buttons [6].transform.position = (Vector3)this.Positions [2];
	this.Buttons [3].transform.position = (Vector3)this.Positions [7];
	this.Buttons [7].transform.position = (Vector3)this.Positions [3];

	}

	private void PatterFive()
	{
	this.Buttons [0].transform.position = (Vector3)this.Positions [5];
	this.Buttons [5].transform.position = (Vector3)this.Positions [2];
	this.Buttons [2].transform.position = (Vector3)this.Positions [7];
	this.Buttons [7].transform.position = (Vector3)this.Positions [4];
	this.Buttons [4].transform.position = (Vector3)this.Positions [1];
	this.Buttons [1].transform.position = (Vector3)this.Positions [6];
	this.Buttons [6].transform.position = (Vector3)this.Positions [3];
	this.Buttons [3].transform.position = (Vector3)this.Positions [0];

	}

	private void PatterSix()
	{
	this.Buttons [0].transform.position = (Vector3)this.Positions [6];
	this.Buttons [6].transform.position = (Vector3)this.Positions [4];
	this.Buttons [4].transform.position = (Vector3)this.Positions [2];
	this.Buttons [2].transform.position = (Vector3)this.Positions [0];
	this.Buttons [1].transform.position = (Vector3)this.Positions [7];
	this.Buttons [7].transform.position = (Vector3)this.Positions [5];
	this.Buttons [5].transform.position = (Vector3)this.Positions [3];
	this.Buttons [3].transform.position = (Vector3)this.Positions [1];

	}

	private void PatterSeven()
	{
	this.Buttons [0].transform.position = (Vector3)this.Positions [7];
	this.Buttons [7].transform.position = (Vector3)this.Positions [6];
	this.Buttons [6].transform.position = (Vector3)this.Positions [5];
	this.Buttons [5].transform.position = (Vector3)this.Positions [4];
	this.Buttons [4].transform.position = (Vector3)this.Positions [3];
	this.Buttons [3].transform.position = (Vector3)this.Positions [2];
	this.Buttons [2].transform.position = (Vector3)this.Positions [1];
	this.Buttons [1].transform.position = (Vector3)this.Positions [0];

	}
}
