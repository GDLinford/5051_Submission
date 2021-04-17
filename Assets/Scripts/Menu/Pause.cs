using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
	public static bool GameIsPaused = false;

	public GameObject pauseMenu;

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (GameIsPaused) {
				Resume();
			} else {
				PauseGame();
			}
		}
	}

	public void MainMenu () {
		SceneManager.LoadScene(1);
	}

	public void Resume() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	void PauseGame() {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		pauseMenu.SetActive(true);
		Time.timeScale = 0.005f;
		GameIsPaused = true;
	}
}
