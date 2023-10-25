using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
   
    private Vector3 moveDirection = Vector3.zero;
    private bool isGrounded;
    //private float playerSpeed = 2.0f;
    private float speed = 2f;
    private float sprint = 15f;
    public float rotationSpeed = 15f;

    public Animator anim;

    private void Start()
    {
        //anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        CharacterController controller = GetComponentInChildren<CharacterController>();


        //Feed moveDirection with input.
        // moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        anim.SetFloat("PosX", hInput);
        anim.SetFloat("PosY", vInput);

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        
        if (Input.GetKey(KeyCode.LeftShift))
        {

            speed = sprint;

        }
        else
        {
            speed = 5f;
        }

        transform.Rotate(0.0f, hInput * rotationSpeed * Time.deltaTime, 0.0f);

        Vector3 forward = transform.TransformDirection(Vector3.forward).normalized;
        forward.y = 0.0f;

        
        controller.Move(forward * vInput * speed * Time.deltaTime);


    }
    
}
