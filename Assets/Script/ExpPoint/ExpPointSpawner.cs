using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPointSpawner : Singleton<ExpPointSpawner>
{
    //오브젝트풀 프리팹,사이즈 , 선언
    [SerializeField] ExpPointScript _expPointPrefab;
    [SerializeField] int poolSize = 10;
    ObjPool<ExpPointScript> _expPointPool;

    protected override void Awake()
    {
        base.Awake();
        _expPointPool = new ObjPool<ExpPointScript>(_expPointPrefab, poolSize, gameObject.transform);
    }
    //생성 코드
    public void CreateExpPoint(Transform trf)
    {
        ExpPointScript expBuf =  _expPointPool.GetObject();
        expBuf.transform.position = trf.position;
        expBuf._destroyExpPoint += DisableExpPoint;
    }
    //오브젝트 반환
    private void DisableExpPoint(ExpPointScript expPoint)
    {
        expPoint._destroyExpPoint -= DisableExpPoint;
        _expPointPool.ReturnObject(expPoint);
    }
}
