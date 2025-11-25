using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageModel
{
    public int RemainMonster { get; private set; }
    public int StageNumber { get; private set; }
    public void SetStageModel(int amount,int stageNum)
    {
        RemainMonster = amount;
        StageNumber = stageNum;
    }
}
