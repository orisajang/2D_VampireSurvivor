using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    //[SerializeField] private Player _player;
    private Player _player;
    [SerializeField] private PlayerDataSO playerData;
    //플레이어 무기 오브젝트풀 부모 위치 설정
    [field: SerializeField] public Transform playerWeaponSpawner { get; private set; }
    public Player _Player => _player;
    public PlayerController _PlayerController { get; private set; }
    //플레이어 모델 필요할때만 생성
    private PlayerModel _playerModel = new PlayerModel();
    public PlayerModel _PlayerModel
    {
        get
        {
            return _playerModel;
        }
    }
    //Json으로 읽어올 PlayerJsonSave
    public PlayerJsonSave _playerJsonSave { get; private set; } = new PlayerJsonSave();

    //PlayerModel의 필드를 PlayerModel을 알지않고도 얻어올수있도록
    public int GetPlayerLevel() => _playerModel.Level;
    public int GetPlayerGold() => _playerModel.Gold;
    public int GetPlayerExpPoint() => _playerModel.ExpPoint;

    protected override void Awake()
    {
        base.Awake();
        //모델 생성 (UI에서 MVP형식으로 사용할)
        //Model = new PlayerModel();
        //플레이어 생성
        GameObject obj = Instantiate(playerData.playerPrefab, transform.position, Quaternion.identity, null);
        _PlayerController = obj.GetComponent<PlayerController>();
        _player = obj.GetComponent<Player>();

        //플레이어 불러와야함 뭐였지 
        PlayerDataJson playerDataJson =  _playerJsonSave.LoadData();
        _playerModel.SetPlayerModel(playerDataJson);
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }
}
