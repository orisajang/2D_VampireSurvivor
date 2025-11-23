using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MonsterSO/Monster Data", fileName = "MonsterSOData")]
public class MonsterSO : ScriptableObject
{
    public float hp;
    public float attack;
    public float defense;
    public GameObject monsterPrefab;
    public eMonsterType monsterType;
}
