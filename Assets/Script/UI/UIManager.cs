using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>, IPlayerMVPView
{
    [SerializeField] TextMeshProUGUI goldText;

    private PlayerMVPPresenter _presenter;
    
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        goldText.text = "Gold!!";
        _presenter = new PlayerMVPPresenter(this);
    }

    public void UpdateGoldValue(int goldValue)
    {
        goldText.text = "Gold: " + goldValue.ToString();
    }
}
