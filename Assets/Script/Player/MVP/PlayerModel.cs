using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public int Gold { get; private set; } = 0;
    public int ExpPoint { get; private set; }
    public int Level { get; private set; } = 1;
    public event Action OnLevelUp;
    public void AddGold(int amount)
    {
        Gold += amount;
    }

    public void AddExpPoint(int amount)
    {
        ExpPoint += amount;
        //레벨업을 위해 레벨이 얼마나 올랐는지, 경험치는 얼마나 남기는지 계산
        if (ExpPoint >= 50)
        {
            int levelUp = ExpPoint / 50;
            int remain = ExpPoint % 50;

            ExpPoint = remain;
            Level += levelUp;
            OnLevelUp?.Invoke();
        }
    }
    public void SetPlayerModel(PlayerDataJson data)
    {
        Gold = data.Gold;
        ExpPoint = data.ExpPoint;
        Level = data.Level;
    }
}
