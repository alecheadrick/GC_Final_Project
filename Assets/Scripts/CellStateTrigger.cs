using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellStateTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log (other);

		if (other.tag == "Player") {
			other.GetComponent<Player> ().playerInCell = !other.GetComponent<Player> ().playerInCell;
		}
		//Debug.Log (other.GetComponent<Player> ().playerInCell);
	}
}
