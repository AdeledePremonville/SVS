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
    private bool isCrouching = false;

    private bool isJumping = false;

    public HitboxTrigger punchHitboxTrigger;
    public HitboxTrigger kickHitboxTrigger;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        isMoving = moveInput.sqrMagnitude > 0.01f;
        animator.SetBool("OnMoving", isMoving);
    }

    public void OnJump(InputAction.CallbackContext context) {
        Debug.Log("Jumping called");
        if (context.performed && !isJumping) {
            Debug.Log("Jumping");
            animator.SetBool("IsJumping", true);
            isJumping = true;
            Invoke("ResetJump", 0.5f);
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        Debug.Log("Crouching");
        if (context.started || context.performed)
        {
            animator.SetBool("IsCrouching", true);
            isCrouching = true;
        }
        else if (context.canceled)
        {
            animator.SetBool("IsCrouching", false);
            isCrouching = false;
        }
    }

    public void OnPunch(InputAction.CallbackContext context)
    {
        if (context.performed && !isPunching && !isKicking)
        {
            Debug.Log("Punch");
            isPunching = true;
            punchHitboxTrigger.EnableDamage();
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
            kickHitboxTrigger.EnableDamage();
            animator.SetBool("IsKicking", true);
            Invoke("ResetKick", 1f); // Adjust based on animation length
        }
    }

    void ResetJump() {
        isJumping = false;
        animator.SetBool("IsJumping", false);
    }

    void ResetPunch()
    {
        isPunching = false;
        punchHitboxTrigger.DisableDamage();
        animator.SetBool("IsPunching", false);
    }

    void ResetKick()
    {
        isKicking = false;
        kickHitboxTrigger.DisableDamage();
        animator.SetBool("IsKicking", false);
    }

    void Update()
    {
        if (!isPunching && !isKicking && !isCrouching)
        {
            Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
            controller.Move(move * speed * Time.deltaTime);

            if (move != Vector3.zero)
            {
                transform.forward = move; // Rotate character in movement direction
            }
        }
    }

    public bool getKick() {
        return isKicking;
    }

    public bool getCrouch() {
        return isCrouching;
    }

    public bool getPunch() {
        return isPunching;
    }
}
