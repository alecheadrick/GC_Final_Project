using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageRoomTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {

		if (other.tag == "Player") {
			other.GetComponent<Player> ().playerInStorageRoom = !other.GetComponent<Player> ().playerInStorageRoom;
		}

		//Debug.Log (other.GetComponent<Player> ().playerInCell);
	}
}
