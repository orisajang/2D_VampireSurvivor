using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponInfo
{
    public float Damage { get;}

    public void SetWeaponDamage(float dmg);
}
