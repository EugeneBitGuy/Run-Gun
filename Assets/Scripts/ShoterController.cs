using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoterController : MonoBehaviour
{

    public GameObject bullet;
    public Transform shotPoint;

    private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots;
    void Update()
    {
        if(timeBtwShots <= 0) { 
            if (Input.GetMouseButton(1))
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);
            }
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
