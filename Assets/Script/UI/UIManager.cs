using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>, IPlayerMVPView,IGameInterface
{
    [SerializeField] TextMeshProUGUI _goldText;
    [SerializeField] TextMeshProUGUI _playTimeText;
    [SerializeField] TextMeshProUGUI _expPointText;
    [SerializeField] TextMeshProUGUI _levelText;

    private PlayerMVPPresenter _playerPresenter;
    private GamePresent _gamePresent;
    
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        _goldText.text = "Gold!!";
        _playerPresenter = new PlayerMVPPresenter(this);
        _gamePresent = new GamePresent(this);
    }
    private void OnDisable()
    {
        _playerPresenter.Dispose();
        _gamePresent.Dispose();
    }

    public void UpdateGoldValue(int goldValue)
    {
        _goldText.text = "Gold: " + goldValue.ToString();
    }

    public void UpdateGameTime(int time)
    {
        _playTimeText.text = ChangeIntToTimeFormat(time);
    }

    //초 형태의 time이라는 매개변수가 왔을때 UI에 시간으로 표시하기 위해 변환하는 메서드
    private string ChangeIntToTimeFormat(int time)
    {
        //60이라는 시간이 왔다. 그러면 01:00 으로 바꿔야함
        //59라는 시간이 왔다 00:59
        //100이라는 시간이 왔다. 그러면 01:40 으로
        //100 / 60 = 1
        //100 % 60 = 40
        int minute = time / 60;
        int second = time % 60;
        string minuteStr;
        string secondStr;
        //시간을 string 형식으로
        if (minute < 10) minuteStr = "0" + minute.ToString();
        else minuteStr = minute.ToString();
        //분을 string 형식으로
        if (second < 10) secondStr = "0" + second.ToString();
        else secondStr = second.ToString();

        return minuteStr + ":" + secondStr;
    }

    public void UpdateExpValue(int expValue,int level)
    {
        _expPointText.text = expValue.ToString();
        _levelText.text = level.ToString();
    }
}
