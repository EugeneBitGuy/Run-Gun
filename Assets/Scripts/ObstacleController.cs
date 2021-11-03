using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private int hp;
    private void Update()
    {
        if (hp <= 0)
            Destroy(gameObject);

    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }
}
