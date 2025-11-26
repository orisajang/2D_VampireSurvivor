using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    //무기별 정보 셋팅
    GameObject _prefab;
    eWeaponType _weaponType;

    //데이터 받을때 기존 데미지와 스피드
    int _baseWeaponDamage;
    float _baseWeaponSpeed;
    //레벨 등 계산이 끝난 최종 데미지, 스피드
    int _weaponDamage;
    float _weaponSpeed;
    int _coolDown;
    Transform _firePosition;

    private int currentCoolDown;

    public eWeaponType WeaponType => _weaponType;
    List<RotateShield> rotateShieldList = new List<RotateShield>();

    public Weapon(GameObject prefab, Transform position , eWeaponType type, int dmg, int cd, float speed)
    {
        _prefab = prefab;
        _firePosition = position;
        _weaponType = type;
        _baseWeaponDamage = dmg;
        _coolDown = cd;
        _baseWeaponSpeed = speed;

        //초기 셋팅값 
        currentCoolDown = _coolDown;
        _weaponDamage = _baseWeaponDamage;
        _weaponSpeed = _baseWeaponSpeed;
    }

    //Player에서 1초마다 시간체크하고 0초되면 발사 동작 진행
    public void Tick(int time, Transform playerDir, Transform shotDir)
    {
        currentCoolDown -= time;
        if(currentCoolDown <= 0)
        {
            currentCoolDown = _coolDown;
            WeaponAttack(playerDir, shotDir);
        }
    }

    public void WeaponAttack(Transform playerDir, Transform shotDir)
    {
        switch (_weaponType)
        {
            case eWeaponType.ShootBullet:
                ShootBulletFunc(shotDir);
                break;
            case eWeaponType.RotateShield:
                ShootRotateShield(playerDir);
                break;
            default:
                Debug.Log("디폴트 공격");
                break;
        }
    }

    private void ShootBulletFunc(Transform shotDir)
    {
        //총알 발사
        Debug.Log("발사공격");
        GameObject obj = GameObject.Instantiate(_prefab, _firePosition.position, _firePosition.rotation, null);
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.SetBulletMoveDirection(shotDir);
        bullet.SetWeaponInit(_weaponDamage,_weaponSpeed);
    }
    private void ShootRotateShield(Transform plyDir)
    {
        Debug.Log("방패공격");
        Vector2 vec = new Vector2(plyDir.position.x, plyDir.position.y + 3);
        GameObject obj = GameObject.Instantiate(_prefab, vec, plyDir.rotation, plyDir);
        RotateShield rotateShield = obj.GetComponent<RotateShield>();
        rotateShield.SetRotatePoint(plyDir);
        rotateShield.SetWeaponInit(_weaponDamage,_weaponSpeed);
        rotateShieldList.Add(rotateShield);
    }

    public void LevelUp(int weaponLevel)
    {
        //레벨에 따라 데미지, 스피드 변동
        _weaponDamage = _baseWeaponDamage + weaponLevel * 2;
        _weaponSpeed = _baseWeaponSpeed + weaponLevel * 2;

        Debug.Log("현재 무기타입: " + _weaponType + " 데미지: " + _weaponDamage + " 스피드: " + _weaponSpeed);

        if(_weaponType == eWeaponType.RotateShield)
        {
            //rotateShield.SetWeaponInit(_weaponDamage, _weaponSpeed);
            foreach(RotateShield shield in rotateShieldList)
            {
                shield.SetWeaponInit(_weaponDamage, _weaponSpeed);
            }

        }
    }
}
