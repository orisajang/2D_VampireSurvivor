using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScanTarget))]
public class Player : Unit
{
    //플레이어의 무가가 있는대로 무기 종류에 따라 공격을 한다.
    [SerializeField] private List<WeaponSO> _weaponsData = new List<WeaponSO>();
    private List<Weapon> _weaponHaveList = new List<Weapon>();

    //무기 공격 시간체크용 코루틴
    Coroutine _weaponTimeCoroutine;
    WaitForSeconds _delay = new WaitForSeconds(1);

    //범위내의 가장 가까운적 탐색용 클래스
    ScanTarget _scanTarget;

    private void Awake()
    {
        _scanTarget = GetComponent<ScanTarget>();
    }

    //모든 플레이어 무기를 가지고 공격을 한다
    public void PlayerAttackWithWeapon()
    {
        foreach (var item in _weaponsData)
        {
            eWeaponType type = item.weaponType;
            Weapon weapon = new Weapon(item.weaponPrefab,transform, type, item.weaponDamage, item.coolDown);
            AddWeapon(weapon);
        }
    }

    private void AddWeapon(Weapon weapon)
    {
        //리스트에 추가
        _weaponHaveList.Add(weapon);
    }
    private void RemoveAllWeapon()
    {
        _weaponHaveList = null;
    }
    private void StartWeaponAttack()
    {
        if(_weaponTimeCoroutine == null)
        {
            _weaponTimeCoroutine = StartCoroutine(AttackCoroutine());
        }
    }
    private void StopWeaponAttack()
    {
        if(_weaponTimeCoroutine != null)
        {
            StopCoroutine(_weaponTimeCoroutine);
        }
    }
    IEnumerator AttackCoroutine()
    {
        while(true)
        {
            foreach(var item in _weaponHaveList)
            {
                item.Tick(1, gameObject.transform, _scanTarget.NearestTarget);
            }
            yield return _delay;
        }

        
    }

    private void OnEnable()
    {
        PlayerAttackWithWeapon();
        StartWeaponAttack();
    }
    private void OnDisable()
    {
        RemoveAllWeapon();
        StopWeaponAttack();
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void TakeDamage()
    {
        throw new System.NotImplementedException();
    }
}
