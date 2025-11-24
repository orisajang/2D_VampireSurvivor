using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPointScript : MonoBehaviour
{
    //코루틴으로 플레이어가 주변에 있는지 확인
    [SerializeField] private float searchDelay = 1.0f;
    [SerializeField] private float distance = 5.0f;
    private Coroutine findPlayerRoutine;
    private WaitForSeconds _delay;
    private Player _player;
    private Rigidbody2D _rigidbody;

    //오브젝트풀에 반환하기위한 이벤트
    public event Action<ExpPointScript> _destroyExpPoint;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _delay = new WaitForSeconds(searchDelay);
    }
    private void OnEnable()
    {
        if (findPlayerRoutine == null)
        {
            findPlayerRoutine = StartCoroutine(SearchPlayerInRange());
        }
    }

    private void OnDisable()
    {
        if(findPlayerRoutine != null)
        {
            StopCoroutine(findPlayerRoutine);
            findPlayerRoutine = null;
        }
    }
    IEnumerator SearchPlayerInRange()
    {
        //플레이어가 null이 아닐때까지 대기
        yield return new WaitUntil(()=> PlayerManager.Instance._Player != null);
        _player = PlayerManager.Instance._Player;
        while (true)
        {
            float diff = Vector2.Distance(_player.transform.position,transform.position);
            if(diff <= distance)
            {
                Vector2 direction = (_player.transform.position - transform.position).normalized;
                _rigidbody.velocity = direction * 2.0f;
            }
            else
            {
                _rigidbody.velocity = Vector2.zero;
            }
            yield return _delay;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("발동");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("이벤트까지발동");
            _destroyExpPoint?.Invoke(this);
        }
    }
}
