using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableMenuSO/Player Data", fileName = "PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public float hp;
    public float defense;
    public float moveSpeed;
    public GameObject playerPrefab;
}
