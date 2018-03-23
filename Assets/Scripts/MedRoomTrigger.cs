using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedRoomTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {

		if (other.tag == "PlayerTarget") {
			other.GetComponent<Player> ().playerInMedRoom = !other.GetComponent<Player> ().playerInMedRoom;
		}

		//Debug.Log (other.GetComponent<Player> ().playerInCell);
	}
}
