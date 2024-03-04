using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxScript : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private float minBounds = -1f;
    private float maxBounds = 148f;

    private void Update()
    {
        if(playerTransform != null && playerTransform.position.x > minBounds && playerTransform.position.x < maxBounds)
        {
            transform.position = new Vector3(playerTransform.position.x, 0f, -10f);
        }
    }
}
