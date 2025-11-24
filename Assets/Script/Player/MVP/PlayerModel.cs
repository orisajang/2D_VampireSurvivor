using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public int Gold { get; private set; } = 0;
    public void AddGold(int amount)
    {
        Gold += amount;
    }
}
