using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShield : MonoBehaviour, IWeaponInfo
{

    [SerializeField] Transform rotatePoint;
    [SerializeField] float rotateSpeed = 10.0f;

    //무기의 공격력
    public float Damage { get; private set; }
    public void SetRotatePoint(Transform trf)
    {
        rotatePoint = trf;
    }
    public void SetWeaponDamage(float dmg)
    {
        Damage = dmg;
    }
    private void FixedUpdate()
    {
        if(rotatePoint != null)
        {
            transform.RotateAround(rotatePoint.position, Vector3.forward, rotateSpeed * Time.fixedDeltaTime);
        }
    }
}
