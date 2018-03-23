using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public bool medRoomNumberEntered = false;
	public bool bathRoomNumberEntered = false;
	public bool recRoomNumberEntered = false;
	public bool paintingNumberEntered = false;
	#endregion

	#region Methods
	private void Update()
	{
		if (medRoomNumberEntered && bathRoomNumberEntered && recRoomNumberEntered && paintingNumberEntered) {
			//open safe
		}
	}


	#region Checks And Sets
	public void MedroomComplete() {
		medRoomNumberEntered = true;
	}
	public void MedroomReset() {
		medRoomNumberEntered = false;
	}
	public void BathroomComplete()
	{
		bathRoomNumberEntered = true;
	}
	public void BathroomReset()
	{
		bathRoomNumberEntered = false;
	}
	public void RecroomComplete()
	{
		recRoomNumberEntered = true;
	}
	public void RecroomReset()
	{
		recRoomNumberEntered = false;
	}
	public void PaintingComplete()
	{
		paintingNumberEntered = true;
	}
	public void PaintingReset()
	{
		paintingNumberEntered = false;
	}
	# endregion
	#endregion
}
