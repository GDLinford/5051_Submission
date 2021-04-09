using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nonSACarAudio : MonoBehaviour
{

    //this can probably be seperated later
    [Header("Audio")]
    [SerializeField] private AudioClip wheelsDriving;
    [SerializeField] private AudioClip CarSlowingDown;
    [SerializeField] private AudioClip carCrash;
    [SerializeField] private AudioClip CarStop;
    public float AudioSpeed = 20.0f;
    [Range(0.0f, 3.0f)]
    public float minPitch = 0.7f;
    [Range(0.0f, 0.1f)]
    public float pitchSpeed = 0.05f;


    private AudioSource wSource;
    private StateChanger stateChange;
    private Controller car;
    // Start is called before the first frame update
    void Start()
    {
        wSource = GetComponent<AudioSource>();
        car = GetComponent<Controller>();
        stateChange = GetComponent<StateChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (car.braking && wSource.clip == CarSlowingDown && stateChange.changed == true)
        {
            wSource.clip = CarStop;
            wSource.Play();
        }
        if (!car.braking && !wSource.isPlaying && car.Accelerating == true && stateChange.changed == true)
        {
            wSource.clip = wheelsDriving;
            wSource.Play();
        }

        if (wSource.clip == wheelsDriving)
        {
            wSource.pitch = Mathf.Lerp(wSource.pitch, minPitch + Mathf.Abs(car.motorForce) / AudioSpeed, pitchSpeed);
        }
    }
}
