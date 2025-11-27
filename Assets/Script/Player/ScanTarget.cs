using UnityEngine;

public class ScanTarget : MonoBehaviour
{
    //플레이어와 가장 가까운 적을 탐색하는 클래스

    Transform _nearestTarget;
    [SerializeField] float _radius = 10f;
    [SerializeField] LayerMask _layer;
    RaycastHit2D[] _raycastHits;
    float _distance;

    //Get 전용 프러퍼티
    public Transform NearestTarget => _nearestTarget;

    private void FixedUpdate()
    {
        //범위안 모든 적들 탐색
        _raycastHits = Physics2D.CircleCastAll(transform.position, _radius, Vector2.zero, 0, _layer);
        //제일 가까운 몬스터 탐색
        if (_raycastHits.Length > 0)
        {
            //처음에는 첫번째 거리로 넣음
            _distance = Vector2.Distance(transform.position, _raycastHits[0].transform.position);
            _nearestTarget = _raycastHits[0].transform;
        }
        else
        {
            //주변에 목표가 없으면 null로 설정해서 총알 발사 안하도록, 
            _nearestTarget = null; 
        }
        foreach (var item in _raycastHits)
        {
            //거리가 더 작다면 작은쪽을 저장한다.
            float diff = Vector2.Distance(transform.position, item.transform.position);
            if (_distance > diff)
            {
                _distance = diff;
                _nearestTarget = item.transform;
            }
        }
    }
}
