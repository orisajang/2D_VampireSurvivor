using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : Singleton<MainUIManager>
{
    //MainScene에서 사용할 UI요소들
    [SerializeField] Button _btnStart;


    protected override void Awake()
    {
        isDestroyOnLoad = false;
        base.Awake();
    }

    private void Start()
    {
        //버튼에 이벤트 등록 
        SetUIForMainScene();
    }

    private void SetUIForMainScene()
    {
        _btnStart.onClick.AddListener(OnClickGameStartBtn);
    }
    private void OnClickGameStartBtn()
    {
        GameManager.Instance.ChangeGameScene(1);
    }
}
