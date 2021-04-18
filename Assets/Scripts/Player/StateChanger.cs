using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class StateChanger : MonoBehaviour
{
	[HideInInspector] public DummyBuggy dummyBuggy;
	public GameObject pressU;

	public GameObject car;

	public GameObject character;

	public Camera camera1;

	public Camera camera2;

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
		car.SetActive(true);
		character.SetActive(false);
	}

    private void CallChanger()
    {
		if (dummyBuggy.completed == true)
        {
			pressU.SetActive(true);

			if (Input.GetKeyUp(KeyCode.U))
			{

				Changer();
				changed = true;
				
			}

			if (changed == true)
            {
				pressU.SetActive(false);
            }
		}
	}
}
