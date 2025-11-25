using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePresenter
{
    StageModel _stageModel;
    IStageMVPView _stageMVPView;

    public StagePresenter(IStageMVPView stageView)
    {
        _stageModel = StageManager.Instance._StageModel;
        _stageMVPView = stageView;

        //스테이지 시작하기전에 초기 UI정보 넣기
        StageManager.Instance._readStageFirstInfo += SetFirstStageData;
        //몬스터 정보가 바뀔때마다 이벤트 호출되기 위해서 사용
        MonsterManager.Instance.OnChangedRemainMonster += SetMonsterRemain;
    }
    private void SetFirstStageData(int remainMonster)
    {
        _stageModel.SetStageModel(remainMonster, StageManager.Instance._currentStageNum);
        _stageMVPView.SetRemainMonsterView(_stageModel.RemainMonster, _stageModel.StageNumber);
    }

    private void SetMonsterRemain(int remainMonster)
    {
        _stageModel.SetStageModel(remainMonster, StageManager.Instance._currentStageNum);
        _stageMVPView.SetRemainMonsterView(_stageModel.RemainMonster, _stageModel.StageNumber);
    }
    public void Dispose()
    {
        StageManager.Instance._readStageFirstInfo -= SetFirstStageData;
        MonsterManager.Instance.OnChangedRemainMonster -= SetMonsterRemain;
    }
}
