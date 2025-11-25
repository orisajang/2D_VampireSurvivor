using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMVPPresenter
{
    private PlayerModel _model;
    private IPlayerMVPView _playerView;

    public PlayerMVPPresenter(IPlayerMVPView view)
    {
        _model = PlayerManager.Instance._PlayerModel;
        _playerView = view;

        //초기에 경험치와 Gold을 UI에 표시해주기 위해서 사용
        Init();

        MonsterManager.Instance.OnGoldEarned += OnMonsterDead;
        ExpPointSpawner.Instance._getExpPoint += PickExpPoint;
    }

    private void Init()
    {
        _playerView.UpdateGoldValue(_model.Gold);
        _playerView.UpdateExpValue(_model.ExpPoint, _model.Level);
    }

    private void OnMonsterDead(int reward)
    {
        _model.AddGold(reward);
        _playerView.UpdateGoldValue(_model.Gold);
    }
    private void PickExpPoint(int amount)
    {
        _model.AddExpPoint(amount);
        _playerView.UpdateExpValue(_model.ExpPoint, _model.Level);
    }

    public void Dispose()
    {
        MonsterManager.Instance.OnGoldEarned -= OnMonsterDead;
        ExpPointSpawner.Instance._getExpPoint -= PickExpPoint;
    }
}
