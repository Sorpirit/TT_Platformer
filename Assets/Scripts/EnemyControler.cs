using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    [SerializeField] private float searchRadious;
    [SerializeField] private float attackRadious;

    [SerializeField] private LayerMask ignoreLayer;

    private EnemyMovment movment;
    private EnemyFight fight;

    private void Start() {
        ignoreLayer = ~ignoreLayer;

        movment = GetComponent<EnemyMovment>();
        fight = GetComponent<EnemyFight>();
    }

    private void Update() {
        RaycastHit2D leftSearch = Physics2D.Raycast(transform.position,Vector2.left,searchRadious,ignoreLayer);
        RaycastHit2D rightSearch = Physics2D.Raycast(transform.position,Vector2.right,searchRadious,ignoreLayer);

        if(leftSearch && leftSearch.collider.CompareTag("Player")){
            
            if(movment.CurrentDirection == EnemyMovment.Direction.Right){
                movment.ChengDirection();
            }

            if(Vector2.Distance(leftSearch.transform.position,transform.position) <= attackRadious){
                fight.AttackInput = true;
                movment.Stop = true;
            }else{
                fight.AttackInput = false;
                movment.Stop = false;
            }

        }else if(rightSearch && rightSearch.collider.CompareTag("Player")){

            if(movment.CurrentDirection == EnemyMovment.Direction.Left){
                movment.ChengDirection();
            }

            if(Vector2.Distance(rightSearch.transform.position,transform.position) <= attackRadious){
                fight.AttackInput = true;
                movment.Stop = false;
            }else{
                fight.AttackInput = false;
                movment.Stop = false;
            }

        }else{
            fight.AttackInput = false;
            movment.Stop = false;
        }

    }
}
