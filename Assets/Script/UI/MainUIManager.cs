using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : Singleton<MainUIManager>
{
    //MainScene에서 사용할 UI요소들
    [SerializeField] Button _btnStart;
    [SerializeField] Button _btnExit;


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
        _btnExit.onClick.AddListener(OnClickGameExitBtn);
    }
    //게임 시작 버튼 클릭 시 게임플레이 화면으로 이동 
    private void OnClickGameStartBtn()
    {
        GameManager.Instance.ChangeGameScene(1);
    }
    //게임 종료 버튼 클릭 시 게임 종료
    private void OnClickGameExitBtn()
    {
        Application.Quit();
    }
}
