using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;
    [SerializeField] private AudioSource footstepsAudio;

    private PlayerAnimations playerAnimations;
    private Player player;
    private PlayerActions actions;
    private Rigidbody2D rigidBody2D;
    private Vector2 moveDirection;

    private bool canMove = true;

    void Awake()
    {
        actions = new PlayerActions();
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<PlayerAnimations>();
        player = GetComponent<Player>();
        canMove = true;
}

    void Update()
    {
        ReadMovement();
        HandleFootsteps();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //        if (player.Stats.Health <= 0) return;
        if (!canMove) return;
        rigidBody2D.MovePosition(rigidBody2D.position + moveDirection * (speed * Time.fixedDeltaTime));
    }

    private void ReadMovement()
    {

        if (!canMove)
        {
            moveDirection = Vector2.zero;
            playerAnimations.SetMoveBoolTransition(false);
            return;
        }


        moveDirection = actions.Movement.Move.ReadValue<Vector2>().normalized;

        //do not update the animation when there is no movement on the player, keeping the last animation
        if (moveDirection == Vector2.zero)
        {
            playerAnimations.SetMoveBoolTransition(false);
            return;
        }

        playerAnimations.SetMoveBoolTransition(true);
        playerAnimations.SetMoveAnimation(moveDirection);
    }

    private void HandleFootsteps()
    {

        if (!canMove)
        {
            if (footstepsAudio.isPlaying) footstepsAudio.Stop();

            return;
            
        }

        bool isMoving = moveDirection != Vector2.zero;

        if (isMoving && !footstepsAudio.isPlaying)
        {
            footstepsAudio.Play();
        }
        else if (!isMoving && footstepsAudio.isPlaying)
        {
            footstepsAudio.Stop();
        }
    }

    public void DisableMovement()
    {
        canMove = false;
        moveDirection = Vector2.zero;
        rigidBody2D.velocity = Vector2.zero;
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
