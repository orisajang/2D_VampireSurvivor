using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMVPView 
{
    public void UpdateGoldValue(int goldValue);
    public void UpdateExpValue(int expValue,int level);
}
