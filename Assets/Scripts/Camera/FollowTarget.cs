using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;
    private float startYPosition;
    private void Awake()
    {
        offset = transform.position - target.position;
        startYPosition = transform.position.y;
    }
    private void LateUpdate()
    {
        Vector3 positionToSet = target.position + offset;
        positionToSet.y = startYPosition;
        transform.position = positionToSet;

    }
}
