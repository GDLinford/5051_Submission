using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class LapCheckpoint : MonoBehaviour
{
    [HideInInspector] public WheelHp WheelHp;
    public Text checkPoint;
    public int Number;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarLap>())
        {

            CarLap car = other.GetComponent<CarLap>();
            Controller controller = other.GetComponent<Controller>();

            if (car.CheckpointNumber == Number + 1 || car.CheckpointNumber == Number - 1)
            {
				
				car.CheckpointNumber = Number;
                controller.WheelHp.WheelsHP += 1;
                Debug.Log("CarHpLapCheckpoint: " + controller.WheelHp.WheelsHP);
                car.score += 100;
            }
            checkPoint.text = "Checkpoint: " + Number;
        }
    }
}
