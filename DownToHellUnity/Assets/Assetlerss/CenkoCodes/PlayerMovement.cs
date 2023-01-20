 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
   
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultipler;
    bool readyToJump;
    
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    
    [Header("KeyBinds")]
    public KeyCode JumpKey = KeyCode.Space;
    public KeyCode SprintKey = KeyCode.LeftShift;
    public KeyCode CrouchKey = KeyCode.LeftControl;
    
    [Header("E�ilmece")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;
    [Header("Slope Handling")]
    public float maxSlopeHandle;
    private RaycastHit slopeHit;
    private bool exitingSlope;
    
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    public int Animo;
    public MovementState state;
    public enum MovementState
    {
        y�r�me,
        ko�ma,
        havada,
        ��k,
    }

    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        ResetJump();
        startYScale = transform.localScale.y;
    }
    private void Update()
    {
        // ground kontrol� 
        grounded=Physics.Raycast(transform.position,Vector3.down,playerHeight * 0.5f+0.2f,whatIsGround);
        MyInput();
        SpeedControl();
        StateUygulay�c�();
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(JumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            
            Jump();
            
            Invoke(nameof(ResetJump), jumpCooldown);
           
        }
        if (Input.GetKeyDown(CrouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f,ForceMode.Impulse);
        }
        if (Input.GetKeyUp(CrouchKey))
        {

            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }

    }
   private void StateUygulay�c�()
    {
        if (Input.GetKey(CrouchKey))
        {
            state = MovementState.��k;
            moveSpeed = crouchSpeed;
            
        }
       
        else if(grounded && Input.GetKey(SprintKey))
        {
            state = MovementState.ko�ma;
            moveSpeed = sprintSpeed;
            Animo = 2;

        }
        else if (grounded)
        {
            state = MovementState.y�r�me;
            moveSpeed = walkSpeed;
            
        }
        else
        {
            state = MovementState.havada;
            Animo=3;
        }
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultipler, ForceMode.Force);
        }
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f,ForceMode.Force);
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity= new Vector3(limitedVel.x,rb.velocity.y,limitedVel.z);
        }
    }
    private void Jump()
    {
        exitingSlope = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }
    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position,Vector3.down,out slopeHit,playerHeight * 0.5f + 0.3f))
        {
            float angel = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angel < maxSlopeHandle && angel != 0;
          
        }
        return false;
    }
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
