using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    protected override void Awake()
    {
        base.Awake();

    }

    public  float CalculateDamage(float hp, float attack, float defense)
    {
        //데미지 계산 및 예외처리
        float dmg = attack - defense;
        if (dmg < 0) dmg = 0;

        //hp계산
        float currentHp = hp - dmg;
        if (currentHp < 0) currentHp = 0;

        return currentHp;


    }

}
