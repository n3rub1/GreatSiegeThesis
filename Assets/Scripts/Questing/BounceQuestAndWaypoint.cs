using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceQuestAndWaypoint : MonoBehaviour
{

    private Vector3 startPosition;
    private Vector3 originalPosition;
    [SerializeField] float bounceHeight;
    [SerializeField] float bounceSpeed;

    void Start()
    {
        startPosition = transform.localPosition;
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
