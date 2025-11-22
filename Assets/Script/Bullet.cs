using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _bulletSpeed = 2.0f;
    [SerializeField]Transform _moveDir;
    Rigidbody2D _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetBulletMoveDirection(Transform dir)
    {
        _moveDir = dir;
    }

    private void Start()
    {
        if (_moveDir != null)
        {
            Vector2 bulletDir = ((_moveDir.position - transform.position) * _bulletSpeed * Time.fixedDeltaTime).normalized;
            _rigidbody.AddForce(bulletDir, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (_moveDir != null)
        {
            //Vector2 bulletDir = ((_moveDir.position - transform.position) * _bulletSpeed * Time.fixedDeltaTime).normalized;
           // _rigidbody.AddForce(bulletDir, ForceMode2D.Impulse);
        }
       
    }

}
