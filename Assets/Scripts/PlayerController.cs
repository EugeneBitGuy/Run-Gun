using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ShoterController shoterController;
    private CharacterController controller;
    private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float commonGravity;
    private Vector3 defaultScale;
    [SerializeField] private float crouchTime;
    [SerializeField] private float crouchGravity;
    [SerializeField] private int hp;
    private float maxSpeed = 150;
    private bool isCrouching;
    public int Hp 
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(value, 0, int.MaxValue);
            if (hp == 0)
                GameController.Instance.GameOver();
        }
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        defaultScale = transform.localScale;
        StartCoroutine(SpeedUp());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shoterController.Shot();
        }
        if (SwipeController.swipeUp && controller.isGrounded)
            Jump();

        if (SwipeController.swipeDown && !isCrouching)
        {
            StartCoroutine(Crouch());
        }
        
        //Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        //transform.position = targetPosition;
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
        Hp--;
    }

    private IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(10);
        if(speed < maxSpeed)
        {
            speed += 1.5f;
            StartCoroutine(SpeedUp());
        }
        

    }
}
