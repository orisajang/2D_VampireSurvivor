using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eWeaponType
{
    ShootBullet,
    RotateShield
}

public class Weapon
{
    //무기별 정보 셋팅
    GameObject _prefab;
    eWeaponType _weaponType;
    int _weaponDamage;
    int _coolDown;
    Transform _firePosition;

    private int currentCoolDown;



    public Weapon(GameObject prefab, Transform position, eWeaponType type, int dmg, int cd)
    {
        _prefab = prefab;
        _firePosition = position;
        _weaponType = type;
        _weaponDamage = dmg;
        _coolDown = cd;

        currentCoolDown = _coolDown;
    }

    //Player에서 1초마다 시간체크하고 0초되면 발사 동작 진행
    public void Tick(int time)
    {
        currentCoolDown -= time;
        if(currentCoolDown <= 0)
        {
            currentCoolDown = _coolDown;
            WeaponAttack();
        }
    }

    public void WeaponAttack()
    {
        switch (_weaponType)
        {
            case eWeaponType.ShootBullet:
                ShootBulletFunc();
                break;
            case eWeaponType.RotateShield:
                Debug.Log("쉴드 공격");
                break;
            default:
                Debug.Log("디폴트 공격");
                break;
        }
    }

    private void ShootBulletFunc()
    {
        //총알 발사
        Debug.Log("발사공격");
        GameObject obj = GameObject.Instantiate(_prefab, _firePosition.position, _firePosition.rotation, null);
    }
}
