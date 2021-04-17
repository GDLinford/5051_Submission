using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

	//public string playerPrefix = null;

	public Transform objectDestination;

	private AudioSource gSource;
	[SerializeField] AudioClip PickupSound;
	[SerializeField] AudioClip Drop;

	private float timer;

	void Start() {
		gSource = GetComponent<AudioSource>();
		
	}

	void OnMouseDown() 
	{
		GetComponent<BoxCollider>().enabled = false;
		GetComponent<Rigidbody>().useGravity = false;
		this.transform.position = objectDestination.position;
		this.transform.parent = GameObject.Find("Hand").transform;
		//IdentifyItem();
		gSource.PlayOneShot(PickupSound);
		
	}

	void OnMouseUp()
	{

		this.transform.parent = null;
		GetComponent<Rigidbody>().useGravity = true;
		GetComponent<BoxCollider>().enabled = true;
		//gSource.PlayOneShot(Drop);


	}
}
