using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IWeaponInfo
{
    //[SerializeField] float _bulletSpeed = 2.0f;
    [SerializeField] Transform _moveDir;
    Rigidbody2D _rigidbody;

    public event Action<Bullet> destroyBullet;

    //무기의 공격력
    public float Damage { get; private set; }
    public float Speed { get; private set; }
    
    //총알의 공격력
    //float _damage;
    //총알을 쏘는 방향, 발사할 위치를 지정해주기 위해
    public void SetBulletMoveDirection(Transform dir, Vector3 shootPosition)
    {
        _moveDir = dir;
        transform.position = shootPosition;
    }
    public void SetWeaponInit(float dmg,float speed)
    {
        Damage = dmg;
        Speed = speed;
    }
    //아래부터 주요 로직 실행
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void BulletAddForce()
    {
        if (_moveDir != null)
        {
            Vector2 bulletDir = ((_moveDir.position - transform.position) * Time.fixedDeltaTime).normalized;
            bulletDir *= Speed;
            _rigidbody.AddForce(bulletDir, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            destroyBullet?.Invoke(this);
        }
    }

}
