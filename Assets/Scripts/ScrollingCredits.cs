using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingCredits : MonoBehaviour {

	public RectTransform creditText;
	public float scrollSpeed;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (creditText != null) {
			 //Debug.Log (creditText.anchoredPosition);
			//Debug.Log (creditText.anchoredPosition3D);
			//Debug.Log (creditText.anchoredPosition3D.y);

			//creditText.anchoredPosition3D += new Vector3 (0.0f, creditText.anchoredPosition3D.y += scrollSpeed , 0.0f);
			creditText.anchoredPosition3D += Vector3.up * scrollSpeed * Time.deltaTime;
		}
	}
}
