using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    // Change the color of the object when it collides with the ball
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the renderer component of the object
            Renderer renderer = GetComponent<Renderer>();

            // Change the color to red
            renderer.material.color = Color.yellow;
            renderer.material.SetColor("_EmissionColor", Color.red);
        }
    }
}
