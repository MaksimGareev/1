using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUIManager : MonoBehaviour
{
    public Text winText;

    void Start()
    {
        winText.enabled = false;
        
    }

    public void DisplayWinText()
    {
        winText.enabled = true;
        winText.text = "You Win! Game Created by <Team 42>";
    }

    
}
