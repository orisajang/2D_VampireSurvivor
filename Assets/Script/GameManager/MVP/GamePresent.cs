using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePresent
{
    GameModel _gameModel;
    IGameInterface _gameInterface;

    public GamePresent(IGameInterface game)
    {
        _gameModel = GameManager.Instance._GameModel;
        _gameInterface = game;

        GameManager.Instance.timeChanged += TimeSet;
        GameManager.Instance.gameEnd += GameClear;
    }

    public void TimeSet(int time)
    {
        _gameModel.SetTime(time);
        _gameInterface.UpdateGameTime(_gameModel.CurrentTime);
    }
    public void GameClear(bool isClear)
    {
        _gameInterface.ShowGameResult(isClear);
    }

    public void Dispose()
    {
        if (GameManager.isHaveInstance) GameManager.Instance.timeChanged -= TimeSet;
        if (GameManager.isHaveInstance) GameManager.Instance.gameEnd -= GameClear;
    }

}
