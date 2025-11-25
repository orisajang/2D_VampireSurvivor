using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    //스테이지 정보 (스크립터블오브젝트에서 가져옴)
    [SerializeField] private List<StageDataSO> _stageDataList;
    //현재 스테이지 번호
    public int _currentStageNum { get; private set; } = 0;
    //MVP패턴으로 Presenter와 연결하기위한 model을 정의
    private StageModel _stageModel;
    //프러퍼티로 _stageModel이 필요할때 new 해줘서 클래스간 Awake순서 상관없이 미리 정의할 수 있도록 처리
    public StageModel _StageModel { 
        get
        {
            if (_stageModel == null) _stageModel = new StageModel();
            return _stageModel;
        }
    }
    //초기 스테이지 정보 넘겨줌
    public event Action<int> _readStageFirstInfo;
    protected override void Awake()
    {
        base.Awake();
        //_stageModel = new StageModel();
    }
    private void Start()
    {
        SendStageMonsterInfo();
        SendStageStart();
    }

    //현재 스테이지 정보를 보낸다.
    public void SendStageMonsterInfo()
    {
        MonsterManager.Instance.SetMonsterInfo(_stageDataList[_currentStageNum].monsterInfoList);
    }
    //스테이지 시작 명령
    public void SendStageStart()
    {
        Debug.Log("현재 스테이지는 " + _currentStageNum);
        MonsterManager.Instance.StartSummonMonsters();

        //스테이지 시작할때마다 초기 정보 넣어줌
        int monsterRemainCount = _stageDataList[_currentStageNum].monsterInfoList.Count;
        _readStageFirstInfo?.Invoke(monsterRemainCount);
    }
    public void GoToNextStage()
    {
        _currentStageNum++;
        if(_currentStageNum < _stageDataList.Count)
        {
            SendStageMonsterInfo();
            SendStageStart();
        }
    }

    //처음에는 스테이지 0번을 설정시킨다
    //게임시작 버튼이 눌리면 스테이지매니저의 시작메서드가 호출된다
    //스테이지 매니저가 가지고있는 몬스터enum리스트를 몬스터매니저에 전달한다
    //몬스터를 다잡으면? -> 다음 스테이지로 가야한다.
    //다 잡았다는 기준은? -> 몬스터 매니저가 가지고있다.
    //몬스터매니저 풀에서 다 사라졌으면? -> 이벤트를 발동
    //StageManager가 이벤트 받으면 tage번호를 ++하고 SO에서 찾아서 정보를 set하고 다음 스테이지정보를 다시 몬스터 매니저에 전달.
    //몬스터매니저는 다시 몬스터를 소환한다.

}
