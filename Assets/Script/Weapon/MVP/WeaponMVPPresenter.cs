using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMVPPresenter
{
    WeaponModel weaponModel;
    //View view; 

    public WeaponMVPPresenter(WeaponModel weapon)
    {
        weaponModel = weapon;
    }

    public void OnClickBulletUpMethod()
    {
        weaponModel.AddBulletShootLevel(1);
    }
    public void OnClickRotateShieldMethod()
    {
        weaponModel.AddRotateShieldLevel(1);
    }
}
