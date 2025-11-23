using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("감지");
        if(collision.CompareTag("Weapon"))
        {
            //데미지를 받음 -> 배틀매니저를 통해서 데미지 계산
            //무기의 공격력을 가져와야함
            IWeaponInfo dmgInfo = collision.gameObject.GetComponent<IWeaponInfo>(); //혹은 Shield임
            float currentHp = BattleManager.Instance.CalculateDamage(_hp, dmgInfo.Damage,_defense);

            _hp = currentHp;
            Debug.Log("현재몬스터HP: " + _hp);
        }
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
