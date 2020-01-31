using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformStikeCol : MonoBehaviour
{
    [SerializeField] private Transform Platform;

    private void OnTriggerEnter2D(Collider2D other) {
        other.transform.SetParent(Platform);
    }
    private void OnTriggerExit2D(Collider2D other) {
        other.transform.SetParent(null);
    }
}
