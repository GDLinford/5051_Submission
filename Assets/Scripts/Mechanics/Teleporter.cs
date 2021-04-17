using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private GameObject TeleporterExit;
    private BuggyControl buggyControl;

    [HideInInspector] public bool teleporting;

    private void Start()
    {
        buggyControl = GetComponent<BuggyControl>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Boulder" || collider.gameObject.tag == "Car")
        {
            collider.gameObject.transform.position = TeleporterExit.transform.position;
            teleporting = true;
        }


        collider.gameObject.transform.rotation = new Quaternion(0, -75, 0, 0);
        
    }

}
