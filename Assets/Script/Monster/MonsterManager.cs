using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    //몬스터에 대한 스크립트 정보를 얻어오기 위해서 사용 (Monobehavior는 new로 클래스 생성하면 안된다)
    //[SerializeField] List<Monster> monsterPrefabs;
    [SerializeField] List<MonsterSO> monstersDataList;
    //리스트로 소환시킨 몬스터의 정보를 가지고있음.
    List<Monster> monsterList = new List<Monster>();

    //몬스터가 죽는다
    //몬스터에서 Invoke를 쏴준다.
    //몬스터 매니저에서 해당 행동에 대해 정의
    //몬스터에 있는 이벤드들을 전부 구독 += 한다.
    //


    //오브젝트풀 사용해서 미리 몬스터 생성해두기
    //딕셔너리로 몬스터 타입마다 풀을 만들기
    Dictionary<eMonsterType, ObjPool<Monster>> monsterPool = new Dictionary<eMonsterType, ObjPool<Monster>>();
    protected override void Awake()
    {
        base.Awake();

        if(monstersDataList.Count > 0)
        {
            //Monster mon = monstersDataList[0].monsterPrefab.GetComponent<Monster>();
            //monsterPool = new ObjPool<Monster>(mon, 10, gameObject.transform);
        }
        foreach(MonsterSO item in monstersDataList)
        {
            Monster mon = item.monsterPrefab.GetComponent<Monster>();
            eMonsterType type = item.monsterType;
            monsterPool[type] = new ObjPool<Monster>(mon, 10, gameObject.transform);
        }
        SpawnMonsterSetForTest();
    }
    private void Start()
    {
        CreateMonster(spawnMonsterList);
    }

    List<eMonsterType> spawnMonsterList = new List<eMonsterType>();
    public void SpawnMonsterSetForTest()
    {
        spawnMonsterList.Add(eMonsterType.Rat);
        spawnMonsterList.Add(eMonsterType.Rat);
        spawnMonsterList.Add(eMonsterType.Rat);
        spawnMonsterList.Add(eMonsterType.Ghost);
        spawnMonsterList.Add(eMonsterType.Ghost);
    }

    //몬스터 생성
    public void CreateMonster(List<eMonsterType> monsterList)
    {
        for(int i=0; i< monsterList.Count; i++)
        {
            eMonsterType type = monsterList[i];
            ObjPool<Monster> item = monsterPool[type];
            Monster mon =  item.GetObject();

            MonsterSO monsterSO = monstersDataList.Find(x => x.monsterType == type);
            mon.Init(monsterSO);
            mon.deathEvent += SendMonsterDead;
        }
    }

    public void SendMonsterDead(Monster mon)
    {
        mon.deathEvent -= SendMonsterDead;
        monsterPool[mon.MonsterType].ReturnObject(mon);
    }

}
