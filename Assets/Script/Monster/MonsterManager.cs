using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    //스크립터블 오브젝트로 몬스터를 소환할때마다 몬스터 정보를 넣을것이다. 
    //오브젝트 풀을 이용하여 몬스터 타입마다 몬스터를 미리 생성할 것이다.

    //몬스터 정보(스크립터블 오브젝트)
    [SerializeField] List<MonsterSO> monsterDatas = new List<MonsterSO>();
    int monsterCount = 0;

    //소환 지점 정보
    [SerializeField] Transform spawnPointInfo;
    List<Transform> spawnPoints = new List<Transform>();

    //오브젝트풀 (몬스터 타입별)
    [SerializeField] int poolSize = 10;
    private Dictionary<eMonsterType, ObjPool<Monster>> monsterPools = new Dictionary<eMonsterType, ObjPool<Monster>>();

    //소환 코루틴
    Coroutine spawnCoroutine;
    WaitForSeconds _spawnDelay;

    //삭제예정. 테스트용. 스테이지매니저에서 넘어오는 소환될 몬스터 enum리스트
    List<eMonsterType> spawnMonsterInfo = new List<eMonsterType>();
    int spawnTime = 2;

    //삭제예정. 테스트용.
    public void SetMonsterInfo()
    {
        spawnMonsterInfo.Add(eMonsterType.Rat);
        spawnMonsterInfo.Add(eMonsterType.Rat);
        spawnMonsterInfo.Add(eMonsterType.Rat);
        spawnMonsterInfo.Add(eMonsterType.Ghost);
        spawnMonsterInfo.Add(eMonsterType.Ghost);
    }

    protected override void Awake()
    {
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

        SetMonsterInfo(); //테스트용. 삭제예정
    }

    private void Start()
    {
        if(spawnCoroutine == null)
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
        //foreach(eMonsterType type in monsterInfos)
        {
            //몬스터 초기 설정
            Monster monsterBuf =  monsterPools[type].GetObject();
            monsterBuf.deathEvent += DeSpawnMonster;

            //몬스터 소환 위치 지정
            int selectPoint = Random.Range(0, spawnPoints.Count);
            monsterBuf.transform.position = spawnPoints[selectPoint].position;

            //스크립터블 오브젝트에서 몬스터능력치 정보를 Set 해준다
            MonsterSO monsterInfo = monsterDatas.Find(x => x.monsterType == type);
            monsterBuf.Init(monsterInfo);


            monsterCount++;

        }
    }

    public void DeSpawnMonster(Monster mon)
    {
        mon.deathEvent -= DeSpawnMonster;
        eMonsterType type = mon.MonsterType;
        monsterPools[type].ReturnObject(mon);

        monsterCount--;
    }

    //코루틴으로 생성
    IEnumerator SpawnMonsterRoutine(List<eMonsterType> monsterInfos)
    {
        foreach(var item in monsterInfos)
        {
            SpawnMonster(item);
            yield return _spawnDelay;
        }

    }


}
