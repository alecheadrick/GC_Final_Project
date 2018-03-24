using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class WayPoints : MonoBehaviour
{

	public Transform BombSpot1;
	public Transform BombSpot2;
	public Transform BombSpot3;
	public Transform playerSpot;

	public Vector3 Bomb1Location;
	public Vector3 Bomb2Location;
	public Vector3 Bomb3Location;

	public float Distance1;
	public float Distance2;
	public float Distance3;

	void Update()
	{
		Bomb1Location = Camera.main.WorldToScreenPoint(BombSpot1.position);
		Bomb2Location = Camera.main.WorldToScreenPoint(BombSpot2.position);
		Bomb3Location = Camera.main.WorldToScreenPoint(BombSpot3.position);
	}
	void OnGUI()
	{
		Distance1 = (int)Vector3.Distance(playerSpot.position, BombSpot1.position);

		Distance2 = (int)Vector3.Distance(playerSpot.position, BombSpot2.position);

		Distance3 = (int)Vector3.Distance(playerSpot.position, BombSpot3.position);

		GUI.Label(new Rect(Bomb1Location.x, 101, 100, 20), Distance1.ToString("") + "m");
		GUI.Label(new Rect(Bomb1Location.x + 3, 116, 100, 20), "▲");
		GUI.Label(new Rect(Bomb2Location.x, Bomb2Location.y, 100, 20), Distance2.ToString("") + "m");
		GUI.Label(new Rect(Bomb2Location.x + 3, Bomb2Location.y + 15, 100, 20), "▲");
		GUI.Label(new Rect(Bomb3Location.x, Bomb3Location.y, 100, 20), Distance3.ToString("") + "m");
		GUI.Label(new Rect(Bomb3Location.x + 3, Bomb3Location.y + 15, 100, 20), "▲");
	}
}