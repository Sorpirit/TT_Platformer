using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChekPoint : MonoBehaviour
{
    [SerializeField] private GameObject chekPointRichedEffect;
    private bool isRiched;

    private void OnTriggerEnter2D(Collider2D other) {
        if(!isRiched && other.CompareTag("Player")){
            isRiched = true;
            GameManager.instance.LatestChekPoint = gameObject;
            Instantiate(chekPointRichedEffect,transform.position,Quaternion.identity);
        }
    }
}
