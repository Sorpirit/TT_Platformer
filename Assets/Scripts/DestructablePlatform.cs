using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructablePlatform : MonoBehaviour
{

    [SerializeField] private float destroyDelay;
    [SerializeField] private float contactDelay;
    [SerializeField] private Collider2D platformColider;
    [SerializeField] private GameObject sprite;

    private bool isInteract;
    private bool readyToIntreact;
    private float contactDelayTimer;
    private float destroyDelayTimer;

    private void Update() {
        if(!readyToIntreact && isInteract){
            contactDelayTimer += Time.deltaTime;

            readyToIntreact = contactDelayTimer >= contactDelay;
        }

        if(!isInteract && contactDelayTimer != 0){
            contactDelayTimer = 0;
            readyToIntreact = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        isInteract = other.gameObject.CompareTag("Player") && other.contacts[0].normal == Vector2.down;
        Debug.Log(isInteract);
        if(isInteract && readyToIntreact) Invoke("Destroy",destroyDelay);
    }

    private void OnCollisionExit2D(Collision2D other) {
        isInteract = false;
        contactDelayTimer = 0;
        readyToIntreact = false;
    }

    private void Destroy(){
        platformColider.enabled = false;
        sprite.SetActive(false);
    }
}
