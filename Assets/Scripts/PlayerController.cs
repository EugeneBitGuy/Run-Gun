using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    [SerializeField] private int speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (SwipeController.swipeUp && controller.isGrounded)
            Jump();
        

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        transform.position = targetPosition;
    }

    void FixedUpdate()
    {
        direction.z = speed;
        direction.y += gravity * Time.fixedDeltaTime;
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }
}
