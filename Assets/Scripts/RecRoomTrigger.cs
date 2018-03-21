using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecRoomTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {

		if (other.tag == "Player") {
			other.GetComponent<Player> ().playerInRecRoom = !other.GetComponent<Player> ().playerInRecRoom;
		}

		//Debug.Log (other.GetComponent<Player> ().playerInCell);
	}
}
