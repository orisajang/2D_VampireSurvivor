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

    //오브젝트풀 사용해서 미리 몬스터 생성해두기
    ObjPool<Monster> monsterPool;
    protected override void Awake()
    {
        base.Awake();

        if(monstersDataList.Count > 0)
        {
            Monster mon = monstersDataList[0].monsterPrefab.GetComponent<Monster>();
            monsterPool = new ObjPool<Monster>(mon, 10, gameObject.transform);
        }
    }
}
