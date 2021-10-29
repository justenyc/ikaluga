using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public float startingColor;

    private Vector3 moveDirection = Vector3.zero;

    private Renderer myRenderer;

    private int ColorInts;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //myRenderer = GetComponent<Renderer>();
        myRenderer = GetComponent<SkinnedMeshRenderer>();

    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void ChangeColor()
    {
        if (ColorInts == 0)
        {
            myRenderer.material.SetColor("_Color", Color.red);
            ColorInts = 1;
        }
        else if (ColorInts == 1)
        {
            myRenderer.material.SetColor("_Color", Color.blue);
            ColorInts = 0;
        }
    }

    void ChangeColorOnInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ChangeColor();
        }
    }

}
