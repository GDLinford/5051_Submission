using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pPrefsDelete : Timer
{
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayerPrefs.DeleteAll();
            Application.Quit();
        }
        
    }

    public void deleteScore()
    {
        PlayerPrefs.DeleteAll();
    }
}
