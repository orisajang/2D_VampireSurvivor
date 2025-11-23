using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponSO/Weapon Data", fileName = "WeaponSOData")]
public class WeaponSO : ScriptableObject
{
    public int weaponDamage;
    public int coolDown;
    public eWeaponType weaponType;
    public GameObject weaponPrefab;
}
