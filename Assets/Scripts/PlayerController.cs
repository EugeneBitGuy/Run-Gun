using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private CapsuleCollider col;
    private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float commonGravity;
    private Vector3 defaultScale;
    [SerializeField] private float crouchTime;
    [SerializeField] private float crouchGravity;
    [SerializeField] private int hp;
    private float maxSpeed = 150;
    private Animator animator;
    private bool isShoting;
    private bool isCrouching;
    public int Hp 
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(value, 0, int.MaxValue);
            if (hp == 0) 
            {
                speed = 0;
                animator.SetTrigger("Die");
                GameController.Instance.GameOver();
            }
        }
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        defaultScale = transform.localScale;
        animator = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        StartCoroutine(SpeedUp());
        //StartCoroutine(RunAnimation());
    }

    void Update()
    {
        if (SwipeController.swipeUp && controller.isGrounded)
        {
            Jump();
        }

        

        if (SwipeController.swipeDown)
        {
            StartCoroutine(Crouch());
        }

        if (animator.GetBool("isShoting"))
            isShoting = true;
        else
            isShoting = false;

        if (controller.isGrounded && !isShoting && !isCrouching)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
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
        animator.SetTrigger("Jump");
    }
    private IEnumerator Crouch()
    {
        isCrouching = true;
        animator.SetTrigger("Crouch");
        commonGravity += crouchGravity;

        gameObject.transform.localScale = defaultScale / 1.5f;

        controller.center = new Vector3(0, 0.5f, 0);
        controller.height = 1;



        yield return new WaitForSeconds(0.5f);

        gameObject.transform.localScale = defaultScale;

        controller.center = new Vector3(0, 1, 0);
        controller.height = 2;
        commonGravity -= crouchGravity;
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
        InGame_UI.Instance.UpdateHealthBar();
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
