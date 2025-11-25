using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableMenuSO/Stage Monster Data", fileName = "StageMonsterData")]
public class StageDataSO : ScriptableObject
{
    public int stageNumber;
    public List<eMonsterType> monsterInfoList;
    
}
