using System;
using UnityEngine;

public class BoxReceiver : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Box")) return;

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
