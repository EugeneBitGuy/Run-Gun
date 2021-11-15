using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoterController : MonoBehaviour
{

    public GameObject bullet;
    public Transform shotPoint;
    [SerializeField] private GameObject player;
    private Animator animator;
    private bool shoting;

    private void Start()
    {
        animator = player.GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    private IEnumerator Shot()
    {
        Instantiate(bullet, shotPoint.position, transform.rotation);

        yield return new WaitForSeconds(0.25f);
        StartCoroutine(Shot());
    }



    public void Press()
    {
        animator.SetBool("isShoting", true);
        StartCoroutine(Shot());
    }

    public void Release()
    {
        StopAllCoroutines();
        animator.SetBool("isShoting", false);
    }
}
