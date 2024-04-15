using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public GameObject PickUpPrefab;
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ChangeHealth(1);
            Instantiate(PickUpPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            controller.PlaySound(collectedClip);
        }
    }
}
