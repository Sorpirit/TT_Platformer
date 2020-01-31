using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimisationRenderer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        SpriteRenderer renderer = other.GetComponent<SpriteRenderer>();
        if(renderer!= null)
        {
                         renderer.enabled = true;
        }

    }

    private void OnTriggerExit(Collider other) {
        SpriteRenderer renderer = other.GetComponent<SpriteRenderer>();
        if(renderer!= null) renderer.enabled = false;
    }
}
