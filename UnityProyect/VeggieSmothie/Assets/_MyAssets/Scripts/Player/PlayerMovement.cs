using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    #region Variables

    private Rigidbody rb;
    private float inputDir;
    private Vector3 moveDir;

    [SerializeField] private float playerSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpForceSeta;
    [SerializeField] private float jumpForceNormal;
    [SerializeField] private float jumpForceWallUp;
    [SerializeField] private float jumpForceWallRight;
    [SerializeField] private float onWallJumpTime;
    [SerializeField] private float onWallJumpTimeLimit;
    [SerializeField] private float jumpBufferTime;
    [SerializeField] private float jumpBufferTimeLimit;


    [SerializeField] private bool onGround;
    [SerializeField] private bool jumpAvaliable;
    [SerializeField] private bool jumpBuffer;
    [SerializeField] private bool onWall;
    [SerializeField] private bool onWallJump;
    [SerializeField] private bool onEnredadera;
    [SerializeField] private bool onSeta;

    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private LayerMask enredaderaLayerMask;
    [SerializeField] private LayerMask setaLayerMask;

    [SerializeField] private Animator animController;
    [SerializeField] private Transform visual;

    public float rayGroundLenght;
    private float rayWallLenght;
    private int inWichWall;

    public float slideFallSpeed;

    #endregion

    #region UnityFunctions

    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody>();

        moveSpeed = 12f;
        climbSpeed = 6f;
        playerSpeed = moveSpeed;
        jumpForceNormal = 16f;
        jumpForceSeta = 12f;
        jumpForceWallUp = 0.5f;
        jumpForceWallRight = 10f;
        jumpForce = jumpForceNormal;
        rayGroundLenght = 0.5f;
        rayWallLenght = 0.6f;
        onWallJumpTimeLimit = 0.18f;
        jumpBufferTimeLimit = 1.5f;

    }

    private void Update()
    {
        if (GameManager.THIS.gameStarted) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SetJumpBuffer();
                PlayerWallJump();
            }

            if (jumpBuffer) JumpBufferDisableTimer();

                PlayerFloorJump();

            ResetWallJump();
            slideFallAcceleration();
            resetSlideFallAcceleration();
            OnWallJumpCount();
            ResetOnWallJumpCount();

            animController.SetBool("onGround", onGround);
            animController.SetBool("isMoving", rb.velocity.x > 0.1f || rb.velocity.x < -0.1f);
            animController.SetBool("up", rb.velocity.y > 0);
            animController.SetBool("down", rb.velocity.y < 0);
            animController.SetBool("hold", (onWall || onEnredadera) && rb.velocity.y <= 0);
            animController.SetBool("enredadera", onEnredadera && rb.velocity.y > 0);
            if (rb.velocity.x > 0.1f) visual.localScale = new Vector3(1, 1, 1);
            if (rb.velocity.x < -0.1f) visual.localScale = new Vector3(1, 1, -1);

        }
        
    }



    private void FixedUpdate()
    {
        if (GameManager.THIS.gameStarted) {
            if (!onEnredadera) {
                SetInputDir();
                SetMovement();
            } else {
                SetInputDirEnredadera();
                SetMovementEnredadera();
            }

            GroundRaycast();
            JumpGroundRaycast();
            WallRaycast();
            PlayerOnWall();
            SetaRaycast();
        }    
    }

    #endregion

    #region FloorMovement

    private void SetInputDir()
    {

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) inputDir = 0f;

        else if (Input.GetKey(KeyCode.A)) inputDir = -1f;
       
        else if (Input.GetKey(KeyCode.D)) inputDir = 1f;
       
        else inputDir = 0f;
       
    }

    private void SetMovement()
    {
        playerSpeed = moveSpeed;

        if (!onWall && !onWallJump) {
            rb.velocity = new Vector3(inputDir * playerSpeed, rb.velocity.y, rb.velocity.z);
            
        }
    }

    #endregion

    #region EnredaderaMovement

    private void SetInputDirEnredadera()
    {

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) inputDir = 0f;

        else if (Input.GetKey(KeyCode.W)) inputDir = 1f;

        else if (Input.GetKey(KeyCode.S)) inputDir = -1f;

        else inputDir = 0f;

    }

    private void SetMovementEnredadera()
    {
        playerSpeed = climbSpeed;

        if (!onWall && !onWallJump) rb.velocity = new Vector3(rb.velocity.x , inputDir * playerSpeed, rb.velocity.z);
    }


    #endregion

    #region FloorJump

    private void SetJumpBuffer()
    {
        if (jumpAvaliable && !onSeta)
        {
            jumpBuffer = true;
            jumpBufferTime = 0f;
        }
            
            
    }

    private void PlayerFloorJump()
    {

        if (jumpBuffer && onGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpBuffer = false;
            jumpBufferTime = 0f;
            Debug.Log("jumpBuffer disabled");
        }
    }

    private void JumpBufferDisableTimer()
    {       
            if (jumpBufferTime < jumpBufferTimeLimit)
            {
                jumpBufferTime += Time.deltaTime;
            }
            else
            {
                jumpBuffer = false;
                jumpBufferTime = 0f;
            }   
    }

    #endregion

    #region SetaJump

    public void SetaJump()
    {
        jumpBuffer = false;
        rb.velocity = new Vector3(rb.velocity.x,0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForceSeta, ForceMode.Impulse);
    }
   

    /*public void SetJumpForce()
    {
        jumpForce = jumpForceSeta;
    }

    public void ResetJumpForce()
    {
        jumpForce = jumpForceNormal;

    }*/


    #endregion

    #region PlayerOnWall

    private void PlayerWallJump()
    {

        if (onWall || onEnredadera && !onGround) 
        {
            onWallJump = true;
            onWallJumpTime = 0.01f;
            //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(new Vector3(-inWichWall, jumpForceWallUp, 0f) * jumpForceWallRight, ForceMode.Impulse);
        } 

    }
    private void ResetWallJump()
    {
        if (onWallJump)
        {
            if ((inWichWall == inputDir && onWallJumpTime ==0f) || onGround ) onWallJump = false;
        }
    }


    private void PlayerOnWall()
    {
        if (onWall)
        {
            rb.velocity = new Vector3(0f, slideFallSpeed, 0f);
            rb.useGravity = false;
        }
        else rb.useGravity = true;
    }

    private void OnWallJumpCount()
    {
        if (onWallJump)
        {            
            if (onWallJumpTime < onWallJumpTimeLimit) onWallJumpTime += Time.deltaTime;
            else onWallJumpTime = 0f;
        }
    }

    private void ResetOnWallJumpCount()
    {
        if(onGround) onWallJumpTime = 0f;
    }

   
    #region SlideWall

    private void slideFallAcceleration()
    {
        if (onWall) slideFallSpeed -= Time.deltaTime * -slideFallSpeed;         
    }

    private void resetSlideFallAcceleration()
    {
        if (onGround) slideFallSpeed = -1f;
    }

    #endregion


    #endregion

    #region Raycast

    private void GroundRaycast()
    {
        float offset = 0.3f;
        Ray ray = new Ray(transform.position + Vector3.up * offset, -Vector3.up);
        RaycastHit hit;

        onGround = Physics.Raycast(ray, out hit, rayGroundLenght);

        Debug.DrawRay(transform.position + Vector3.up * offset, -Vector3.up * rayGroundLenght, Color.red);

       
    }
    private void JumpGroundRaycast()
    {
        float offset = 0.02f;
        float initialRaylenght = 1.5f;
        float rayLenghtAdjustement = 0.2f;
        Ray ray = new Ray(transform.position + Vector3.up * offset, -Vector3.up);
        RaycastHit hit;

        jumpAvaliable = Physics.Raycast(ray, out hit, initialRaylenght + rayGroundLenght * (-rb.velocity.y * rayLenghtAdjustement));

        //Debug.DrawRay(transform.position + Vector3.up * offset, -Vector3.up * (initialRaylenght + rayGroundLenght * (-rb.velocity.y * rayLenghtAdjustement)), Color.green);

    }

    private void SetaRaycast()
    {
        float offset = 0.02f;
        Ray ray = new Ray(transform.position + Vector3.up * offset, -Vector3.up);
        RaycastHit hit;

        onSeta = Physics.Raycast(ray, out hit, (3 * rayGroundLenght), setaLayerMask);

    }

    private void WallRaycast()
    {
        float offset = 0.02f;

        // Right raycast

        Ray rayRight = new Ray(transform.position + Vector3.right * offset, Vector3.right);
        RaycastHit rightHit;

        bool onWallRight = Physics.Raycast(rayRight, out rightHit, rayWallLenght, wallLayerMask);  
        bool onEnredaderaRight = Physics.Raycast(rayRight, out rightHit, rayWallLenght, enredaderaLayerMask);



        // Left raycast

        Ray rayLeft = new Ray(transform.position + Vector3.left * offset, Vector3.left);
        RaycastHit leftHit;

        bool onWallLeft = Physics.Raycast(rayLeft, out leftHit, rayWallLenght, wallLayerMask);
        bool onEnredaderaLeft = Physics.Raycast(rayLeft, out leftHit, rayWallLenght, enredaderaLayerMask);



        if (onWallRight || onEnredaderaRight) inWichWall = 1;
        if (onWallLeft || onEnredaderaLeft) inWichWall = -1;

        // Set onWall

        //if ((onWallLeft || onWallRight) && (!onGround) && rb.velocity.y < 1f) onWall = true; 
        //else onWall = false;

        // Set onEnredadera

        if ((onEnredaderaRight || onEnredaderaLeft) /*&& (!onGround)*/)
        {
            onEnredadera = true;
           // onWallJump = false;
        }
           
        else onEnredadera = false;


    }


    
    #endregion
}
