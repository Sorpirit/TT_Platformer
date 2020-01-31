using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovment : MonoBehaviour
{
    [SerializeField] private float enemyVelocity;
    [SerializeField]private Collider2D endoOfPlatformChek;
    [SerializeField]private ContactFilter2D platformFilter;

    public Direction CurrentDirection{get => currentDir;}
    public bool Stop;

    private Rigidbody2D myRb;
    private Direction currentDir;
    public enum Direction{Left = -1,Right = 1}

    private void Start() {
        myRb = GetComponent<Rigidbody2D>();
        currentDir = Direction.Right;
    }

    private void Update() {
        if(isRichEndOfPlatform()){
            ChengDirection();
        }

        myRb.velocity = new Vector2(enemyVelocity * (int) currentDir, myRb.velocity.y );

        if(Stop) myRb.velocity = Vector2.zero;
    }

    private bool isRichEndOfPlatform(){
        List<Collider2D> res = new List<Collider2D>();
        endoOfPlatformChek.OverlapCollider(platformFilter,res);
        return res.Count == 0;
    }

    private void Flip(){
        Vector3 tempScale = transform.localScale;
        tempScale.x = (int) currentDir;
        transform.localScale = tempScale;
    }

    public void ChengDirection(){
        currentDir = currentDir == Direction.Left ? Direction.Right : Direction.Left;
        Flip();
    }
}
