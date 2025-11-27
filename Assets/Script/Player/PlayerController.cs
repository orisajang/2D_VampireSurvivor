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
    Rigidbody2D rigidbody2D;

    private bool _isControllable = true;
    public bool _IsControllAble
    {
        get { return _isControllable; }
        set { _isControllable = value; }
    }
    private void Awake()
    {
        action = InputSystem.actions["Move"];
        action.performed += PlayerMove;
        action.canceled += PlayerStop;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void SetMoveSpeed(PlayerDataSO playerData)
    {
        _moveSpeed = playerData.moveSpeed;
    }
    private void PlayerMove(InputAction.CallbackContext ctx)
    {
        moveDir = ctx.ReadValue<Vector2>();
    }
    private void PlayerStop(InputAction.CallbackContext ctx)
    {
        //이동키에 손을떼면 바로 멈추도록 (1회)
        moveDir = Vector2.zero; 
        rigidbody2D.velocity = Vector2.zero;
    }
    private void FixedUpdate()
    {

        if (moveDir != Vector2.zero && _IsControllAble)
        {
            Vector3 moveVector3 = new Vector3(moveDir.x, moveDir.y, 0).normalized;
            //transform.position += (moveVector3 * Time.deltaTime * _moveSpeed);
            //이동키가 눌렸을때만 이동
            //if(moveVector3 != Vector3.zero) rigidbody2D.velocity = (moveVector3 * _moveSpeed);
            if (moveVector3 != Vector3.zero) transform.position += (moveVector3 * Time.deltaTime * _moveSpeed);

            animator.SetBool("isRun", true);

            if (moveVector3.x > 0)
            {
                playerCharacterPrefab.transform.localScale = new Vector3(Mathf.Abs(playerCharacterPrefab.transform.localScale.x), playerCharacterPrefab.transform.localScale.y, playerCharacterPrefab.transform.localScale.z);
            }
            else if (moveVector3.x < 0)
            {
                playerCharacterPrefab.transform.localScale = new Vector3(-Mathf.Abs(playerCharacterPrefab.transform.localScale.x), playerCharacterPrefab.transform.localScale.y, playerCharacterPrefab.transform.localScale.z);
            }

        }
        else
        {
            animator.SetBool("isRun", false);
        }
    }
}
