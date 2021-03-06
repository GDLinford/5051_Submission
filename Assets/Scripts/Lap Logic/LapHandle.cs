using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapHandle : MonoBehaviour
{
	public int CheckpointAmount;
	private void OnTriggerEnter(Collider other) {
		if (other.GetComponent<CarLap>()) {
			CarLap car = other.GetComponent<CarLap>();

			if (car.CheckpointNumber == CheckpointAmount) {
				//kart has gone through all the checkpoints
				car.CheckpointNumber = 0;
				car.lapNumber++;
				car.score = +1000;

			}
		}
	}
}
