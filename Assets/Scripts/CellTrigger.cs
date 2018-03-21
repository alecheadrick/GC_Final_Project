using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {

		if (other.tag == "Player") {
			other.GetComponent<Player> ().playerInStorageRoom = !other.GetComponent<Player> ().playerInStorageRoom;
			other.GetComponent<Player> ().playerInCell = !other.GetComponent<Player> ().playerInCell;
		}

		//Debug.Log (other.GetComponent<Player> ().playerInCell);
	}
}
