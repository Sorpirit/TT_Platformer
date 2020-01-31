using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    [SerializeField] private Collider2D hurtBox;
    [SerializeField] private ContactFilter2D filter2D;
    [SerializeField] private float attackRate;
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackDuration;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject SwordTrail;
    [SerializeField] private GameObject impulsSource;
    [SerializeField] private float ScreenShakeDuration;

    private float attackTimer;
    private float attackAnimTimer;
    private bool isAbleToAttack;
    private bool attakInpuct;

     

    private void Update() {
        isAbleToAttack = attackTimer < 0;
        attakInpuct = Input.GetMouseButtonDown(0);

        attackTimer -= Time.deltaTime;
        attackAnimTimer -= Time.deltaTime;

        if(attakInpuct && isAbleToAttack){
            playerAnimator.SetTrigger("Attack");
            Invoke("Attack",attackDelay);
            attackTimer = attackRate;
            attackAnimTimer = attackDuration;
        }

        if(attackAnimTimer > 0 && attackAnimTimer < .1f){
            Attack();
        }

    }

    private void Attack(){
        List<Collider2D> res = new List<Collider2D>();
        hurtBox.OverlapCollider(filter2D,res);
        foreach(Collider2D col in res){
            Destroy(col.gameObject);
            StartCoroutine(ScreenShake());
        }
    }

    private IEnumerator ScreenShake(){
        GameObject im = Instantiate(impulsSource,transform.position,Quaternion.identity);
        yield return new WaitForSeconds(ScreenShakeDuration);
        Destroy(im);
    }

}
