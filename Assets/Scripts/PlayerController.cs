using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    [SerializeField] private int speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float commonGravity;
    private Vector3 defaultScale;
    [SerializeField] private float crouchTime;
    [SerializeField] private float crouchGravity;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        defaultScale = transform.localScale;
    }

    void Update()
    {
        if (SwipeController.swipeUp && controller.isGrounded)
            Jump();

        if (SwipeController.swipeDown)
        {
            StartCoroutine(Crouch());
        }
        
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        transform.position = targetPosition;
    }

    void FixedUpdate()
    {
        direction.z = speed;
        direction.y += (-commonGravity) * Time.fixedDeltaTime;
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }
    private IEnumerator Crouch()
    {
        commonGravity += crouchGravity;
        transform.localScale = new Vector3(defaultScale.x, defaultScale.y * 0.5f, defaultScale.z);

        yield return new WaitForSeconds(0.5f);

        transform.localScale = defaultScale;
        commonGravity -= crouchGravity;
    }
}
