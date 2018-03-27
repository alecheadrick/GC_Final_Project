using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	public static bool playMusic = true;

	public Button begin;

	public void Begin()
	{
		SceneManager.LoadScene("Master Level");
	}

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "PlayerTarget") {
            SceneManager.LoadScene("Credits");
        }
        
    }
}