using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
	public Camera Main;
	public Camera optionsCam;

	public void PlayGame()
    {
		SceneManager.LoadScene("Main Scene");
    }

	public void Options()
    {
		Main.gameObject.SetActive(false);
		optionsCam.gameObject.SetActive(true);
    }

	public void backToMenu()
    {
		Main.gameObject.SetActive(true);
		optionsCam.gameObject.SetActive(false);
    }

	void OnMouseOver() {
		Debug.Log("Clicked");
		if (Input.GetMouseButtonUp(0)) {
			LoadNextScene();
		}
	}

	public void LoadNextScene() {
		SceneManager.LoadScene(SceneManager.sceneCount +0);
		Debug.Log("Scene Loaded");
	}
}
