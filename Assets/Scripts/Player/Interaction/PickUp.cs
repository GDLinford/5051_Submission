using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

	//public string playerPrefix = null;
	private bool isClose;

	public Transform objectDestination;

	private AudioSource gSource;
	[SerializeField] AudioClip PickupSound;
	[SerializeField] AudioClip Drop;
	public GameObject objectInHand;

	private float timer;

	void Start() {
		isClose = false;
		gSource = GetComponent<AudioSource>();
		
	}

	private void OnTriggerEnter(Collider other) {
		isClose = true;
	}

	private void OnTriggerExit(Collider other) {
		isClose = false;
	}

	void OnMouseDown() {
		if (isClose) {
			GetComponent<Rigidbody>().isKinematic = true;
			GetComponent<Rigidbody>().useGravity = false;
			this.transform.position = objectDestination.position;
			this.transform.parent = GameObject.Find("Hand").transform;
			objectInHand = this.gameObject;
			//IdentifyItem();
			gSource.PlayOneShot(PickupSound);
		}
	}

	void OnMouseUp()
	{
		timer = 2f;
		timer -= 1f;
		this.transform.parent = null;
		GetComponent<Rigidbody>().useGravity = true;
		GetComponent<Rigidbody>().isKinematic = false;

		if (timer <= 0)
        {
			gSource.PlayOneShot(Drop);
			timer = 2f;
        }
		
	}
}
