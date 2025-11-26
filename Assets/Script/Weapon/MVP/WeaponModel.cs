using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : MonoBehaviour
{
    //public int bulletShootLevel;
    //public int rotateShieldLevel;

    public Dictionary<eWeaponType, int> weaponLevel { get; private set; } = new Dictionary<eWeaponType, int>();

    public event Action<int> OnBulletLevelChanged;
    public event Action<int> OnRotateShiledLevelChanged;

    public void AddBulletShootLevel(int amount)
    {
        //bulletShootLevel += amount;
        //딕셔너리로 관리 시도 (타입에 맞는 레벨이++ 된다)
        if (!weaponLevel.ContainsKey(eWeaponType.ShootBullet)) weaponLevel[eWeaponType.ShootBullet] = 1;
        else weaponLevel[eWeaponType.ShootBullet]++;
        //레벨 변경 사실을 알림
        OnBulletLevelChanged.Invoke(weaponLevel[eWeaponType.ShootBullet]);
    }
    public void AddRotateShieldLevel(int amount)
    {
        //rotateShieldLevel += amount;
        //딕셔너리로 관리 시도 (타입에 맞는 레벨이++ 된다)
        if (!weaponLevel.ContainsKey(eWeaponType.RotateShield)) weaponLevel[eWeaponType.RotateShield] = 1;
        else weaponLevel[eWeaponType.RotateShield]++;
        //레벨 변경 사실을 알림
        OnRotateShiledLevelChanged.Invoke(weaponLevel[eWeaponType.RotateShield]);
    }
}
