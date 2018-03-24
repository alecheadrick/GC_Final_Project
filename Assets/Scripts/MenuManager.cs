using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	public static bool playMusic = true;
	public static string level = "Master Level";

	public Button begin;

	public void Begin()
	{
		SceneManager.LoadScene(level);
	}
}