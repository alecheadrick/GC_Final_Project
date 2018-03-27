using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Examples;

public class DropKeyIntoPlace : MonoBehaviour {

	#region Singelton
	public static DropKeyIntoPlace instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<DropKeyIntoPlace>();
			}
			return _instance;
		}
	}
	static DropKeyIntoPlace _instance;

	void Awake()
	{
		_instance = this;
	}
	#endregion

	#region Variables
	public GameObject key;
	public Transform dropPlace;
	public GameObject door;
	public GameObject endTrigger;
	public bool keyInDropZone = false;
	#endregion

	#region Methods
	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == key.name) {
			keyInDropZone = true;
		}
	}
	public void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == key.name)
		{
			keyInDropZone = false;
		}
	}
	public void DropIntoPlace() {
			key.transform.position = dropPlace.position;
			key.GetComponent<KeyIO>().enabled = false;
			key.GetComponent<Collider>().enabled = false;
			door.GetComponent<Openable_Door>().enabled = true;
			endTrigger.SetActive(true);
	}

	#endregion
}
