using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class StateChanger : MonoBehaviour
{
	[HideInInspector] public DummyBuggy dummyBuggy;

	public GameObject car;

	public GameObject character;

	public GameObject character2;

	public GameObject character3;

	public Camera camera1;

	public Camera camera2;

	public Camera camera3;

	public Camera camera4;

	[HideInInspector] public bool changed;

    //public Camera[] cameras;
    //private int currentCameraIndex;	

    private void Start()
    {
		dummyBuggy = FindObjectOfType<DummyBuggy>();
    }

    private void Update() 
	{
		CallChanger();	
	}

	//change to car
	public void Changer () 
	{	 
		camera1.gameObject.SetActive(false);
		camera2.gameObject.SetActive(true);
		camera3.gameObject.SetActive(false);
		car.SetActive(true);
		character.SetActive(false);
		character2.SetActive(false);
		character3.SetActive(false);
	}

	//change to character in ScrapYard1
	public void Changer2() {
		character2.SetActive(true);
		camera2.gameObject.SetActive(false);
		camera3.gameObject.SetActive(true);
		//StateChange();
	}

	//change to character in ScrapYard2
	public void Changer3() {
		car.SetActive(true);
		camera2.gameObject.SetActive(true);

		character3.SetActive(false);
		character2.SetActive(false);
		Debug.Log("change 3");
	}

	//change to character in ScrapYard1
	public void Changer4() {
		character3.SetActive(true);
		camera4.gameObject.SetActive(true);
		camera1.gameObject.SetActive(false);
		character2.SetActive(false);
		Debug.Log("change 4");
	}

    private void CallChanger()
    {
		if (dummyBuggy.completed == true)
        {
			if (Input.GetKeyUp(KeyCode.U))
			{
				Changer();
				changed = true;
			}
			if (Input.GetKeyDown(KeyCode.I))
			{
				Changer2();
				changed = false;
			}
			if (Input.GetKeyDown(KeyCode.O))
			{
				Changer3();
				changed = true;
			}
			if (Input.GetKeyDown(KeyCode.P))
			{
				Changer4();
				changed = false;
			}
		}
		

	}

	//private void StateChange() {
	//	if (currentCameraIndex < cameras.Length) {
	//		cameras[currentCameraIndex - 1].gameObject.SetActive(false);
	//		cameras[currentCameraIndex].gameObject.SetActive(true);
	//		Debug.Log("Camera with name: " + 0 + " is now enabled");
	//	} else {
	//		cameras[currentCameraIndex - 1].gameObject.SetActive(false);
	//		currentCameraIndex = 0;
	//		cameras[currentCameraIndex].gameObject.SetActive(true);
	//		Debug.Log("Camera with name: " + 1 + " is now enabled");
	//	}
	//}

	//private void Start() {
	//	//Turn all cameras off, except the first default one
	//	for (int i = 1; i < cameras.Length; i++) {
	//		cameras[i].gameObject.SetActive(false);
	//	}

	//	//If any cameras were added to the controller, enable the first one
	//	if (cameras.Length > 0) {
	//		cameras[0].gameObject.SetActive(true);
	//		Debug.Log("Camera with name: " + ", is now enabled");
	//	}
	//}
}
