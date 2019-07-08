using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 velocity;
    [SerializeField]
    private float walkSpeed;

    private Animator animator;

    // Use this for initialization
    void Start()
    {
        walkSpeed = 3.0f;
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            //animator.SetBool("Run", moveDirection.z > 0.0f);
        }
        //velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * walkSpeed * Time.deltaTime);
    }
}
