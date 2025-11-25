using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableMenuSO/Monster Data", fileName = "MonsterSOData")]
public class MonsterSO : ScriptableObject
{
    public float hp;
    public float attack;
    public float defense;
    public int reward;
    public GameObject monsterPrefab;
    public eMonsterType monsterType;
}
