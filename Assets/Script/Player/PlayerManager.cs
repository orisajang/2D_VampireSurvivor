using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private Player _player;
    public Player _Player => _player;

    public void SetPlayer(Player player)
    {
        _player = player;
    }
}
