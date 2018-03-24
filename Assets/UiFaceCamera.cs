using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFaceCamera : MonoBehaviour {
	#region Variables
	public GameObject mCam;
	#endregion
	
	#region Methods
	void Start () {
		mCam = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	void Update () {
		if (mCam == null) {
			mCam = GameObject.FindGameObjectWithTag("MainCamera");
		}
		transform.LookAt(mCam.transform.position);
	}
	
	#endregion
}
