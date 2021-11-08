using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private float distance;
    [SerializeField] private int damage;

    public LayerMask whatIsSolid;

    private void Update()
    {
        RaycastHit hitInfo;

        transform.Translate(Vector3.forward * (speed) * Time.deltaTime); //SpeedOfBullet = playerSpeed + inspectorSpeed ???

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, distance, whatIsSolid))
        {
            if (hitInfo.collider.tag == "Obstacle")
            {
                hitInfo.collider.GetComponent <ObstacleController>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        

        lifetime -= Time.deltaTime;

        if(lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}
