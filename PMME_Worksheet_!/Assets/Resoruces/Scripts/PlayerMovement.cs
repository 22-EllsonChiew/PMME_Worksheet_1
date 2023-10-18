using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    private Vector3 moveDirection = Vector3.zero;
    private bool isGrounded;
    private float playerSpeed = 2.0f;
    private float speed = 5f;
    private float sprint = 15f;

    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        CharacterController controller = GetComponent<CharacterController>();

       
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        anim.SetFloat("Posx", moveDirection.x);

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


        controller.Move(moveDirection * Time.deltaTime);


    }
    
}
