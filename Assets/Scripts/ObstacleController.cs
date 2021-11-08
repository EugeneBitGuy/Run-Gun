using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private bool isDestroyable;
    [SerializeField] private int hp;
    
    private void Update()
    {
        if (isDestroyable && hp <= 0)
            Destroy(gameObject);

    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"{gameObject.name} was shot!\n It is {(isDestroyable ? "destroyable" : "undestroayble")}\n Current hp is {hp}");
    }
}
