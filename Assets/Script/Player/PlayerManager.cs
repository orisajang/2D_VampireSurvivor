using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    //[SerializeField] private Player _player;
    private Player _player;
    [SerializeField] private PlayerDataSO playerData;
    public Player _Player => _player;
    //플레이어 모델 필요할때만 생성
    private PlayerModel _playerModel;
    public PlayerModel _PlayerModel 
    {
        get
        {
            if (_playerModel == null) _playerModel = new PlayerModel();
            return _playerModel;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        //모델 생성 (UI에서 MVP형식으로 사용할)
        //Model = new PlayerModel();
        //플레이어 생성
        GameObject obj = Instantiate(playerData.playerPrefab, transform.position, Quaternion.identity, null);
        PlayerController playerController = obj.GetComponent<PlayerController>();
        _player = obj.GetComponent<Player>();
        //스크립터블 오브젝트에 있는 초기 플레이어정보를 Player에 넣어줌
        playerController.SetMoveSpeed(playerData);
        _player.SetPlayerData(playerData);
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }
}
