using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pPrefsDelete : Timer
{
    void OnGUI()
    {
        //press button to delete pPrefs score
        //not deleted until you quit the game
        if(GUI.Button(new Rect(100,200,200,60), "Delete Score"))
        {
            PlayerPrefs.DeleteAll();
        }
        
    }
}
