using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationNPC : MonoBehaviour
{
    [SerializeField] private bool faceRight;
    [SerializeField] private bool faceLeft;
    [SerializeField] private string animationName;

    private Animator animator;

    void OnEnable()
    {

        Initialize();
    }

    void Initialize()
    {
        animator = GetComponent<Animator>();
        if (faceRight)
        {
            animator.SetBool("FaceRight", faceRight);
        }
        else if (faceLeft)
        {
            animator.SetBool("FaceLeft", faceLeft);
        }

        animator.Play(animationName);
    }
}
