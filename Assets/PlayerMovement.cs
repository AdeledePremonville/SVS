
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 moveInput;
    private Animator animator;

    public float speed = 5f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = transform.Find("Model")?.GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnPunch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Punch");
            animator.SetTrigger("PunchTrigger");
        }
    }

    public void OnKick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Kick");
            animator.SetTrigger("KickTrigger");
        }
    }

    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, 0);
        controller.Move(move * speed * Time.deltaTime);

    }
}
