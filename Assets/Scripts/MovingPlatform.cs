using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] travelPoints;
    [SerializeField] private float speed;
    [SerializeField] private bool cycleMove;
    
    private int currentPoint;
    private Direction currentDir;
    private float accuracy = .3f;
    
    private enum Direction{ Forward = 1,Revers = -1}

    private void Start() {
        currentDir = Direction.Forward;
        currentPoint = 0;
    }

    private void FixedUpdate() {
        Vector2 newPos = Vector2.Lerp(transform.position,travelPoints[currentPoint].position,Time.deltaTime * speed);
        transform.position = newPos;

        if(Vector2.Distance(newPos,travelPoints[currentPoint].position) <= accuracy){
            currentPoint += (int) currentDir;
        }

        if(currentPoint >= travelPoints.Length){
            if(cycleMove){
                currentPoint = 0;
            }else{
                currentPoint -= 2;
                currentDir = Direction.Revers;
            }
        }

        if(currentPoint < 0){
            currentPoint = 1;
            currentDir = Direction.Forward;
        }
    }
}
