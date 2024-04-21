using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    
    public float slowAmount = 0.5f; 

    
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            
            controller.ApplySpeedModifier(slowAmount);
        }
    }

    
    void OnTriggerStay2D(Collider2D other)
    {
        
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            
            controller.RemoveSpeedModifier(slowAmount);
        }
    }
}
