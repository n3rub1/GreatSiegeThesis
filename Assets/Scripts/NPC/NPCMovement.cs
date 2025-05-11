using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float moveSpeed;

    private readonly int moveX = Animator.StringToHash("MoveX");
    private readonly int moveY = Animator.StringToHash("MoveY");

    private Waypoint waypoint;
    private Animator animator;
    private Vector3 previousPosition;
    private int currentPointIndex;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        waypoint = GetComponent<Waypoint>();
    }

    private void Update()
    {
        Vector3 nextPosition = waypoint.GetPosition(currentPointIndex);
        UpdateMoveValues(nextPosition);
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, nextPosition) <= 0.2f)
        {
            previousPosition = nextPosition;
            currentPointIndex = (currentPointIndex + 1) % waypoint.Points.Length;  //reset to 0 when reach the max waypoints
        }
    }

    private void UpdateMoveValues(Vector3 nextPosition)
    {
        Vector2 direction = Vector2.zero;

        if (previousPosition.x < nextPosition.x) direction = new Vector2(1f, 0f);
        if (previousPosition.x > nextPosition.x) direction = new Vector2(-1f, 0f);
        if (previousPosition.y < nextPosition.y) direction = new Vector2(0f, 1f);
        if (previousPosition.y > nextPosition.y) direction = new Vector2(0f, -1f);

        animator.SetFloat(moveX, direction.x);
        animator.SetFloat(moveY, direction.y);

    }

}
