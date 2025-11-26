using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponInfo
{
    public float Damage { get;}
    public float Speed { get; }

    public void SetWeaponInit(float dmg,float speed);
}
