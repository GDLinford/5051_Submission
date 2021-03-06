using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text time;
    public Text Score;

    private float timer;
    private int gScore;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Score") != 0)
            gScore = PlayerPrefs.GetInt("Score");
        Score.text = " :" + gScore;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        time.text = timer.ToString("f2");


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car") &&  timer <= 100f)
        {
            gScore += 1000;

            Score.text = gScore.ToString();
            PlayerPrefs.SetInt("Score", gScore);

            SceneManager.LoadScene("END");
        }

        if (collision.gameObject.CompareTag("Car") && timer <= 200f)
        {
            gScore += 800;

            Score.text = gScore.ToString();
            PlayerPrefs.SetInt("Score", gScore);

            SceneManager.LoadScene("END");
        }

        if (collision.gameObject.tag == "Car" && timer <= 300f)
        {
            gScore += 600;

            Score.text = gScore.ToString();
            PlayerPrefs.SetInt("Score", gScore);

            SceneManager.LoadScene("END");
        }

        if (collision.gameObject.tag == "Car" && timer <= 400f)
        {
            gScore += 400;

            Score.text = gScore.ToString();
            PlayerPrefs.SetInt("Score", gScore);

            SceneManager.LoadScene("END");
        }

        if (collision.gameObject.tag == "Car" && timer <= 500f)
        {
            gScore += 200;

            Score.text = gScore.ToString();
            PlayerPrefs.SetInt("Score", gScore);

            SceneManager.LoadScene("END");
        }

        if (collision.gameObject.tag == "Car" && timer <= 600f)
        {
            gScore += 100;

            Score.text = gScore.ToString();
            PlayerPrefs.SetInt("Score", gScore);

            SceneManager.LoadScene("END");
        }

        if (collision.gameObject.tag == "Car" && timer <= 700f)
        {
            gScore += 50;

            Score.text = gScore.ToString();
            PlayerPrefs.SetInt("Score", gScore);

            SceneManager.LoadScene("END");
        }

        if (collision.gameObject.tag == "Car" && timer <= 800f)
        {
            gScore += 25;

            Score.text = gScore.ToString();
            PlayerPrefs.SetInt("Score", gScore);

            SceneManager.LoadScene("END");
        }
    }
}
