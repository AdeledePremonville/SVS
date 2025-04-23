using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 moveInput;
    private Animator animator;

    public float speed = 2f;
    private bool isMoving = false;
    private bool isPunching = false;
    private bool isKicking = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = transform.Find("Model")?.GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        isMoving = moveInput.sqrMagnitude > 0.01f;
        animator.SetBool("OnMoving", isMoving);
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        Debug.Log("Crouching");
        if (context.started || context.performed)
        {
            animator.SetBool("IsCrouching", true);
        }
        else if (context.canceled)
        {
            animator.SetBool("IsCrouching", false);
        }
    }

    public void OnPunch(InputAction.CallbackContext context)
    {
        if (context.performed && !isPunching && !isKicking)
        {
            Debug.Log("Punch");
            isPunching = true;
            animator.SetBool("IsPunching", true);
            Invoke("ResetPunch", 0.5f); // Adjust based on animation length
        }
    }

    public void OnKick(InputAction.CallbackContext context)
    {
        if (context.performed && !isPunching && !isKicking)
        {
            Debug.Log("Kick");
            isKicking = true;
            animator.SetBool("IsKicking", true);
            Invoke("ResetKick", 1f); // Adjust based on animation length
        }
    }

    void ResetPunch()
    {
        isPunching = false;
        animator.SetBool("IsPunching", false);
    }

    void ResetKick()
    {
        isKicking = false;
        animator.SetBool("IsKicking", false);
    }

    void Update()
    {
        if (!isPunching && !isKicking)
        {
            Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
            controller.Move(move * speed * Time.deltaTime);

            if (move != Vector3.zero)
            {
                transform.forward = move; // Rotate character in movement direction
            }
        }
    }
}
