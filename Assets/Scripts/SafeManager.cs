using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Examples;

public class SafeManager : MonoBehaviour {

	#region Singleton
	public static SafeManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<SafeManager>();
			}
			return _instance;
		}
	}
	static SafeManager _instance;

	void Awake()
	{
		_instance = this;
	}

	#endregion

	#region Variables
	public GameObject safeDoor;
	public OpenableDoor oDoor;
	public bool medRoomNumberEntered = false;
	public bool bathRoomNumberEntered = false;
	public bool paintingNumberEntered = false;
	#endregion

	#region Methods
	private void Update()
	{
		if (medRoomNumberEntered && bathRoomNumberEntered && paintingNumberEntered) {
			oDoor = safeDoor.GetComponent<OpenableDoor>();
			oDoor.enabled = true;
		}
	}


	#region Checks And Sets
	public void MedRoomComplete() {
		medRoomNumberEntered = true;
	}
	public void MedRoomReset() {
		medRoomNumberEntered = false;
	}
	public void BathRoomComplete() {
		bathRoomNumberEntered = true;
	}
	public void BathRoomReset() {
		bathRoomNumberEntered = false;
	}
	public void PaintingPuzzleComplete() {
		paintingNumberEntered = true;
	}
	public void PaintingReset() {
		paintingNumberEntered = false;
	}
	#endregion
	#endregion
}
