using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {

		if (other.tag == "PlayerTarget") {
			other.GetComponent<Player> ().playerInRecRoom = !other.GetComponent<Player> ().playerInRecRoom;
			other.GetComponent<Player> ().playerInBathroom = !other.GetComponent<Player> ().playerInBathroom;
		}

		//Debug.Log (other.GetComponent<Player> ().playerInCell);
	}
}
