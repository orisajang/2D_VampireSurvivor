using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //게임 시작, 일시정지, 재시작, 현재 게임 시간을 가지고있는다
    int currentTime;
    public event Action<int> timeChanged;
    public event Action<bool> gameEnd;

    //1초마다 동작하는 코루틴
    Coroutine secondTimeRoutine;
    WaitForSeconds _delay;

    //게임 모델
    private GameModel _gameModel;
    public GameModel _GameModel 
    { 
        get
        {
            if (_gameModel == null) _gameModel = new GameModel();
            return _gameModel;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        //무조건 1초마다 진행할 코루틴을 위해
        _delay = new WaitForSeconds(1.0f);
        //GameModel = new GameModel();
    }
    private void OnEnable()
    {
        if(secondTimeRoutine == null)
        {
            secondTimeRoutine = StartCoroutine(TimeTick());
        }
    }
    private void OnDisable()
    {
        if(secondTimeRoutine != null)
        {
            StopCoroutine(secondTimeRoutine);
            secondTimeRoutine = null;
        }
    }
    //1초마다 게임시간을 1초씩 올려준다
    IEnumerator TimeTick()
    {
        while(true)
        {
            currentTime += 1;
            timeChanged?.Invoke(currentTime);
            yield return _delay;
        }
    }

    //씬 전환
    public void ChangeGameScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    //게임 재시작하면서 초기화해야할 정보 모아둠
    public void InitGameInfo()
    {
        currentTime = 0;
    }

    //게임 종료, 클리어 여부를 UI에 넘겨줌
    public void GameOver(bool isClear)
    {
        //클리어했으면 isClear true, 아니면 false로 전달
        gameEnd?.Invoke(isClear);
    }
    public void GameStop()
    {
        Time.timeScale = 0;
    }
    public void GameResume()
    {
        Time.timeScale = 1;
    }
}
