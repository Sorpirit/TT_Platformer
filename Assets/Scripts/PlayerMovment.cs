using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovment : MonoBehaviour
{

    [SerializeField] private float maxWalkSpeed;
    [SerializeField] private float maxRunSpeed;

    [SerializeField,Range(0f,1f)] private float horizontalDamping;
    [SerializeField,Range(0f,1f)] private float horizontalDampingStoping;
    [SerializeField,Range(0f,1f)] private float horizontalDampingTurning;

    [SerializeField] private float jumpVelocity;
    [SerializeField] private float cutJumpSpeed;

    [SerializeField] private Transform groundChek;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpPressDelay;
    [SerializeField] private float jumpGroundedDelay;
    [SerializeField] private float gravityScale;

    [SerializeField] private ParticleSystem LandingEffect;
    [SerializeField] private ParticleSystem dustEffect;

    [SerializeField] private Animator playerAnim;

    private Rigidbody2D myRb;
    private float horInput;
    private bool runInput;
    private bool jumpInput;
    private Direction myDir;

    private enum Direction{Right = 1,Left = -1}
    
    private float jumpPressDelayTimer;
    private float jumpGroundedDelayTimer;
    private bool grounded = true;

    private void Start() {
        myRb = GetComponent<Rigidbody2D>();
        myDir = Direction.Right;
    }

    private void Update() {
        horInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
        runInput = Input.GetKey(KeyCode.LeftShift);
        
        if(horInput > 0 && myDir == Direction.Left){
            Flip();
        }
        if(horInput < 0 && myDir == Direction.Right){
            Flip();
        }

        jumpPressDelayTimer -= Time.deltaTime;
        jumpGroundedDelayTimer -= Time.deltaTime;

        if(jumpInput){
            jumpPressDelayTimer = jumpPressDelay;
        }
        if(grounded){
            jumpGroundedDelayTimer = jumpGroundedDelay;
        }


    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void MovePlayer(){
        float speed = runInput ? maxRunSpeed : maxWalkSpeed;
        grounded = IsGrounded();

        bool delayInput = jumpPressDelayTimer > 0;
        bool delayPhysics = jumpGroundedDelayTimer > 0;

        if(delayInput && delayPhysics){ 
            myRb.velocity = new Vector2(myRb.velocity.x,jumpVelocity);
            jumpPressDelayTimer = 0;
            jumpGroundedDelayTimer = 0;
            dustEffect.Play();
            playerAnim.SetTrigger("Jump");
        }
        
        if(Input.GetButtonUp("Jump")){
            if(myRb.velocity.y > 0){
                myRb.velocity = new Vector2(myRb.velocity.x,myRb.velocity.y * cutJumpSpeed);
            }
        }

        if(myRb.velocity.y < 0){
            myRb.gravityScale = gravityScale;
        }else if(myRb.velocity.y == 0){
            myRb.gravityScale = 1;
        }

        float horVelocity = myRb.velocity.x;
        horVelocity +=(int) horInput;
        if(Mathf.Abs(horInput) > .1f){
            horVelocity *= Mathf.Pow(1f-horizontalDampingStoping,Time.deltaTime * speed);
        }else if(Mathf.Sign(horInput) != Mathf.Sign(horVelocity)){
            horVelocity *= Mathf.Pow(1f-horizontalDampingTurning,Time.deltaTime * speed);
        }else{
            horVelocity *= Mathf.Pow(1f-horizontalDamping,Time.deltaTime * speed);
        }

        if(horInput != 0 && myRb.velocity.y == 0){
            if(runInput){
                playerAnim.SetBool("isRuning", true);
                playerAnim.SetBool("isWalking", false);
            }else{
                playerAnim.SetBool("isRuning", false);
                playerAnim.SetBool("isWalking", true);
            }
        }else{
            playerAnim.SetBool("isRuning", false);
            playerAnim.SetBool("isWalking", false);
        }

        myRb.velocity = new Vector2(horVelocity,myRb.velocity.y);
    }

    private bool IsGrounded(){
        bool ground = Physics2D.Raycast(groundChek.position,Vector2.down,.1f,groundLayer);
        if(ground && !grounded) {
            LandingEffect.Play();
            playerAnim.SetTrigger("Lend");
        }

        return ground;
    }

    private void Flip(){
        dustEffect.Play();
        myDir = Direction.Left == myDir ? Direction.Right : Direction.Left;
        Vector3 tempScale = transform.localScale;
        tempScale.x = (int) myDir;
        transform.localScale = tempScale;
        
    }

}
