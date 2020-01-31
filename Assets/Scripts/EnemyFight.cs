using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    [SerializeField] private Collider2D hurtBox;
    [SerializeField] private ContactFilter2D filter2D;
    [SerializeField] private float attackRate;
    [SerializeField] private float attackDelay;
    [SerializeField] private Animator enemyAnim;
    [SerializeField] private GameObject enemyBiteParticalse;

    public bool AttackInput{get => attackInput; set => attackInput = value;}

    private float attackTimer;
    private bool isAbleToAttak;
    private bool attackInput;

     

    private void Update() {
        isAbleToAttak = attackTimer < 0;

        attackTimer -= Time.deltaTime;

        if(attackInput && isAbleToAttak){
            enemyAnim.SetTrigger("Attack");
            Invoke("Attack",attackDelay);
            attackTimer = attackRate;
            Instantiate(enemyBiteParticalse,hurtBox.transform.position,Quaternion.identity);
        }

    }

    private void Attack(){
        List<Collider2D> res = new List<Collider2D>();
        hurtBox.OverlapCollider(filter2D,res);
        foreach(Collider2D col in res){
            if(col.CompareTag("Player")) {
                GameManager.instance.OnPlayerDead();
            }
        }
    }
}
