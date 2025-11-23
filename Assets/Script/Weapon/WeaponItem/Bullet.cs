using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IWeaponInfo
{
    [SerializeField] float _bulletSpeed = 2.0f;
    [SerializeField] Transform _moveDir;
    Rigidbody2D _rigidbody;

    //무기의 공격력
    public float Damage { get; private set; }
    
    //총알의 공격력
    float _damage;
    //private 필드들 외부에서 Set해주기 위한 메서드
    public void SetBulletMoveDirection(Transform dir)
    {
        _moveDir = dir;
    }
    public void SetWeaponDamage(float dmg)
    {
        Damage = dmg;
    }
    //아래부터 주요 로직 실행
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
