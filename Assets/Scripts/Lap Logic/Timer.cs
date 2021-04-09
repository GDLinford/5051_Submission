using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text time;
    public Text Score;
    private bool finished;

    private float timer;
    private int gScore;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Score") != 0)
            gScore = PlayerPrefs.GetInt("Score");
        Score.text = "Score: " + gScore;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        time.text = timer.ToString("f2");

        if (finished == true && timer <= 5f)
        {
            gScore += 100;

            //We only need to update the text if the score changed.
            Score.text = gScore.ToString();
            PlayerPrefs.SetInt("Score", gScore);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "end")
        {
            finished = true;
        }
    }
}
