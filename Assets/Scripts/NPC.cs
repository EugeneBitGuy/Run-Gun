using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class NPC : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float raycastRange = 10f;
    private bool isLowerHit = false;
    [SerializeField] private Transform lowerRaycastPoint;
    [SerializeField] private Transform upperRaycastPoint;
    private RaycastHit lowerHit;
    private Vector3 direction;
    [SerializeField] private float commonGravity;
    [SerializeField] private float jumpForce;
    private CharacterController controller;
    private Vector3 defaultScale;
    [SerializeField] private float jumpThresholdDistance;
    private float maxSpeed = 150;
    private bool isUpperHit = false;
    private RaycastHit upperHit;
    [SerializeField] private float crouchThresholdDistance;
    [SerializeField] private float crouchGravity;
    private bool isCrouching;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        defaultScale = transform.localScale;
        StartCoroutine(SpeedUp());

    }

    // Update is called once per frame
    void Update()
    {
        Ray lowerRay = new Ray(lowerRaycastPoint.position, Vector3.forward);
        bool isLowerHit = Physics.Raycast(lowerRay, out RaycastHit lowerHit, raycastRange);
        this.isLowerHit = isLowerHit;

        Ray upperRay = new Ray(upperRaycastPoint.position, Vector3.forward);
        bool isUpperHit = Physics.Raycast(upperRay, out RaycastHit upperHit, raycastRange);
        this.isUpperHit = isUpperHit;

        if (isLowerHit && lowerHit.collider.CompareTag("Obstacle"))
        {
            this.lowerHit = lowerHit;
            if (lowerHit.distance < jumpThresholdDistance && controller.isGrounded)
            {
                Jump();
            }
        }

        if(isUpperHit && upperHit.collider.CompareTag("Obstacle"))
        {
            this.upperHit = upperHit;
            if (upperHit.distance < crouchThresholdDistance && !isCrouching)
            {
                StartCoroutine(Crouch());
            }
        }
    }
    private IEnumerator Crouch()
    {
        isCrouching = true;
        commonGravity += crouchGravity;
        transform.localScale = new Vector3(defaultScale.x, defaultScale.y * 0.5f, defaultScale.z);
        controller.height /= 3;
        GetComponent<CapsuleCollider>().height /= 3;

        yield return new WaitForSeconds(1);

        transform.localScale = defaultScale;
        commonGravity -= crouchGravity;
        controller.height *= 3;
        GetComponent<CapsuleCollider>().height *= 3;
        isCrouching = false;
    }
    private void OnDrawGizmos()
    {
        if (!isLowerHit) return;
        Gizmos.DrawLine(lowerRaycastPoint.position, lowerHit.point);
    }
    private void Jump()
    {
        direction.y = jumpForce;
    }
    private void FixedUpdate()
    {
        direction.z = speed;
        direction.y += (-commonGravity) * Time.fixedDeltaTime;
        controller.Move (direction * Time.fixedDeltaTime);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.tag == "Obstacle")
        {
            TakeDamage();
            Destroy(hit.gameObject);
        }
    }

    private void TakeDamage()
    {
        //throw new NotImplementedException();
    }
    private IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(10);
        if (speed < maxSpeed)
        {
            speed += 1.5f;
            StartCoroutine(SpeedUp());
        }


    }
}
