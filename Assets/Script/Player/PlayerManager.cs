using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private Player _player;
    public Player _Player => _player;
    public PlayerModel Model { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        Model = new PlayerModel();
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }
}
