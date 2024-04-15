using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixManager : MonoBehaviour
{
    public Text fixedRobotsText;
    private int fixedRobotsCount = 0;

    public int totalRobotsCount;

    public static FixManager instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }


    public void IncrementFixetRobotsCount()
    {
        fixedRobotsCount++;
        UpdateFixedRobotsText();
    }

    private void UpdateFixedRobotsText()
    {
        fixedRobotsText.text = "Fixed Robots: " + fixedRobotsCount;
    }

    public bool AllRobotsFixed()
    {
        return fixedRobotsCount >= totalRobotsCount;
    }
}
