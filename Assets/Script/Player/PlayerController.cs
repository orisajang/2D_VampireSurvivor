using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3.0f;
    InputAction action;
    Vector2 moveDir;
    private void Awake()
    {
        action = InputSystem.actions["Move"];

        action.performed += PlayerMove;
        action.canceled += (ctx) => moveDir = Vector2.zero;
    }
    private void PlayerMove(InputAction.CallbackContext ctx)
    {
        moveDir = ctx.ReadValue<Vector2>();
    }
    private void Update()
    {
        if (moveDir != Vector2.zero)
        {
            Vector3 moveVector3 = new Vector3(moveDir.x, moveDir.y, 0).normalized;
            transform.position += (moveVector3 * Time.deltaTime * _moveSpeed);
        }
    }
}
