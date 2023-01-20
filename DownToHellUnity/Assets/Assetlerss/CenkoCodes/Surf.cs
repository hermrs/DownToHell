using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surf : MonoBehaviour
{
    [Header("Deðiþkenler")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Surf")]
    public float maxSlideTime;
    public float slideForce;
    private float sliderTimer;

    public float slideYScale;
    private float startScale;

    [Header("Tuþlar")]
    public KeyCode kaymatuþu = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

    private bool sliding;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
        startScale = playerObj.localScale.y;
    }
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if(Input.GetKeyDown(kaymatuþu) &&(horizontalInput !=0 || verticalInput != 0))
        {
            StopSurf();
        }
    }
    private void FixedUpdate()
    {
        if (sliding)
        {
            Surfing();
        }
    }
    private void StartSurf()
    {
        sliding = true;
        playerObj.localScale=new Vector3(playerObj.localScale.x,slideYScale,playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f,ForceMode.Impulse);
    }
    
    private void Surfing()
    {
        Vector3 inputDirection = orientation.forward * verticalInput+ orientation.right * horizontalInput;
        rb.AddForce(inputDirection.normalized*slideForce,ForceMode.Impulse);
        sliderTimer -= Time.deltaTime;
        if(sliderTimer < 0)
        {
            StopSurf();
        }
    }
    private void StopSurf()
    {
        sliding = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startScale, playerObj.localScale.z);
    }
}
