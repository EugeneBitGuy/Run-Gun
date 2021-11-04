using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoterController : MonoBehaviour
{

    public GameObject bullet;
    public Transform shotPoint;

    public void Shot()
    {
       Instantiate(bullet, shotPoint.position, transform.rotation);
    }
}
