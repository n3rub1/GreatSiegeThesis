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
    
    void Awake()
    {
        actions = new PlayerActions();
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<PlayerAnimations>();
        player = GetComponent<Player>();
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
        rigidBody2D.MovePosition(rigidBody2D.position + moveDirection * (speed * Time.fixedDeltaTime));
    }

    private void ReadMovement()
    {
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

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
