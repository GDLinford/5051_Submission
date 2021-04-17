using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pPrefsDelete : Timer
{
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

    void OnGUI()
    {
        //press button to delete pPrefs score
        //not deleted until you quit the game
        if(GUI.Button(new Rect(100,200,200,60), "Delete Score"))
        {
            PlayerPrefs.DeleteAll();
        }
        
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
