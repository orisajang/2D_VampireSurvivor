using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected float _hp;
    protected float _attack;
    protected float _defense;
    protected abstract void Attack();
    protected abstract void TakeDamage();
}
