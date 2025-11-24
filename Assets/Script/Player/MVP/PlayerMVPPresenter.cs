using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMVPPresenter
{
    private PlayerModel _model;
    private IPlayerMVPView _playerView;

    public PlayerMVPPresenter(IPlayerMVPView view)
    {
        _model = PlayerManager.Instance.Model;
        _playerView = view;

        MonsterManager.Instance.monsterDead += OnMonsterDead;
    }

    private void OnMonsterDead(int reward)
    {
        _model.AddGold(reward);
        _playerView.UpdateGoldValue(_model.Gold);
    }

    public void Dispose()
    {
        MonsterManager.Instance.monsterDead -= OnMonsterDead;
    }
}
