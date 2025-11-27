using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    //스크립터블 오브젝트로 몬스터를 소환할때마다 몬스터 정보를 넣을것이다. 
    //오브젝트 풀을 이용하여 몬스터 타입마다 몬스터를 미리 생성할 것이다.

    //몬스터 정보(스크립터블 오브젝트)
    [SerializeField] List<MonsterSO> monsterDatas = new List<MonsterSO>();
    //소환이 끝났는지
    bool isSpawnFinished = false;
    //앞으로 소환될 몬스터 개수
    int remainMonsterDecreaseCount = 0;
    //스테이지에 남은 몬스터 개수
    int remainMonstrCount = 0;

    //소환 지점 정보
    [SerializeField] Transform spawnPointInfo;
    List<Transform> spawnPoints = new List<Transform>();

    //오브젝트풀 (몬스터 타입별)
    [SerializeField] int poolSize = 10;
    private Dictionary<eMonsterType, ObjPool<Monster>> monsterPools = new Dictionary<eMonsterType, ObjPool<Monster>>();

    //소환 코루틴
    Coroutine spawnCoroutine;
    WaitForSeconds _spawnDelay;

    //몬스터 사망 이벤트
    //public delegate void MonsterDeadDeleagte(int reward, int remainMonster);
    //public event MonsterDeadDeleagte monsterDead; //몬스터 사망하면 금액 반환
    public event Action<int> OnGoldEarned;
    public event Action<int> OnChangedRemainMonster;

    //스테이지매니저에서 넘어오는 소환될 몬스터 enum리스트
    List<eMonsterType> spawnMonsterInfo = new List<eMonsterType>();
    [SerializeField] int spawnTime = 2;


    protected override void Awake()
    {
        isDestroyOnLoad = false;
        base.Awake();
        //초기 오브젝트풀 설정
        foreach(var item in monsterDatas)
        {
            eMonsterType type = item.monsterType;
            Monster monsterBuf = item.monsterPrefab.GetComponent<Monster>();
            monsterPools[type] = new ObjPool<Monster>(monsterBuf, poolSize, gameObject.transform);
        }

        //몬스터 소환지점 설정
        SetSpawnPoint();
        _spawnDelay = new WaitForSeconds(spawnTime);
    }
    //스테이지 매니저에서 몬스터 소환 리스트를 받음
    public void SetMonsterInfo(List<eMonsterType> type)
    {
        spawnMonsterInfo = new List<eMonsterType>(type);
        remainMonsterDecreaseCount = type.Count;
    }
    //스테이지 매니저에서 시작 명령을 내리면 몬스터 소환 시작
    public void StartSummonMonsters()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnMonsterRoutine(spawnMonsterInfo));
        }
    }

    private void SetSpawnPoint()
    {
        foreach (Transform childPos in spawnPointInfo)
        {
            spawnPoints.Add(childPos);
        }
    }

    //몬스터 생성 (오브젝트풀에서 하나씩 꺼냄)
    public void SpawnMonster(eMonsterType type)
    {
        //몬스터 초기 설정
        Monster monsterBuf = monsterPools[type].GetObject();
        monsterBuf.deathEvent += DeSpawnMonster;

        //몬스터 소환 위치 지정
        int selectPoint = UnityEngine.Random.Range(0, spawnPoints.Count);
        monsterBuf.transform.position = spawnPoints[selectPoint].position;

        //스크립터블 오브젝트에서 몬스터능력치 정보를 Set 해준다
        MonsterSO monsterInfo = monsterDatas.Find(x => x.monsterType == type);
        monsterBuf.Init(monsterInfo);

        //소환된 몬스터 개수 count
        remainMonstrCount++;
    }

    public void DeSpawnMonster(Monster mon)
    {
        //카운트 차감
        remainMonstrCount--;
        remainMonsterDecreaseCount--;
        //경험치 구슬 생성
        ExpPointSpawner.Instance.CreateExpPoint(mon.transform);
        //몬스터 사망 이벤트 Invoke
        NotifyMonsterDead(mon.Reward, remainMonsterDecreaseCount);
        //몬스터 풀에 반환
        mon.deathEvent -= DeSpawnMonster;
        eMonsterType type = mon.MonsterType;
        monsterPools[type].ReturnObject(mon);
        

        //다음스테이지로 넘어가는 조건을 만족하는지 체크
        //소환해야할 몬스터가 더이상없고, 스테이지에 남은 몬스터가 없다면
        if (isSpawnFinished && remainMonstrCount == 0)
        {
            //다음스테이지에서 사용할 수 있게 관련 필드 초기화
            InitForNextStage();
            StageManager.Instance.GoToNextStage();
        }
    }

    //몬스터 사망시 발동해야하는 이벤드들 메서드로 관리할수있게 묶음
    public void NotifyMonsterDead(int reward, int remainMonster)
    {
        OnGoldEarned?.Invoke(reward);
        OnChangedRemainMonster?.Invoke(remainMonster);
    }

    private void InitForNextStage()
    {
        if(spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
        remainMonsterDecreaseCount = 0;
        remainMonstrCount = 0;
        isSpawnFinished = false;
        spawnMonsterInfo = new List<eMonsterType>();
    }

    //코루틴으로 생성
    IEnumerator SpawnMonsterRoutine(List<eMonsterType> monsterInfos)
    {
        foreach(var item in monsterInfos)
        {
            SpawnMonster(item);
            yield return _spawnDelay;
        }
        isSpawnFinished = true;
    }


}
