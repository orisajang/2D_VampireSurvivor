using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform playerCharacterPrefab;
    [field: SerializeField] public Transform playerShiedSpawnPoint { get; private set; }
    private float _moveSpeed = 3.0f;
    InputAction action;
    Vector2 moveDir;

    public bool isControllAble = true;
    private void Awake()
    {
        action = InputSystem.actions["Move"];

        action.performed += PlayerMove;
        action.canceled += (ctx) => moveDir = Vector2.zero;
    }
    public void SetMoveSpeed(PlayerDataSO playerData)
    {
        _moveSpeed = playerData.moveSpeed;
    }
    private void PlayerMove(InputAction.CallbackContext ctx)
    {
        moveDir = ctx.ReadValue<Vector2>();
    }
    private void Update()
    {
        if (moveDir != Vector2.zero && isControllAble)
        {
            Vector3 moveVector3 = new Vector3(moveDir.x, moveDir.y, 0).normalized;
            transform.position += (moveVector3 * Time.deltaTime * _moveSpeed);
            animator.SetBool("isRun", true);

            if(moveVector3.x > 0)
            {
                //transform.localScale = new Vector3(1, 1, 1);
                playerCharacterPrefab.transform.localScale = new Vector3(Mathf.Abs(playerCharacterPrefab.transform.localScale.x), playerCharacterPrefab.transform.localScale.y, playerCharacterPrefab.transform.localScale.z);
            }
            else if(moveVector3.x < 0)
            {
                //transform.localScale = new Vector3(-1, 1, 1);

                playerCharacterPrefab.transform.localScale = new Vector3(-Mathf.Abs(playerCharacterPrefab.transform.localScale.x), playerCharacterPrefab.transform.localScale.y, playerCharacterPrefab.transform.localScale.z);
            }

        }
        else
        {
            animator.SetBool("isRun", false);
        }
    }
}
