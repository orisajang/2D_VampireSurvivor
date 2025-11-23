using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    //Instance 프로퍼티와 Awake에서 둘다 null체크를 하는 이유:
    //외부에서 Instance로 호출하는경우 생성되고
    //이미 씬에서 생성되어있을경우 프로퍼티로 호출되지는않고 Awake에서 최초 호출되므로

    //외부 호출용 프로퍼티, 해당 타입의 싱글톤이 없으면 찾아보고 그래도 없으면 새로 생성 후 결정
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>(); //찾아봄

                if (_instance == null)
                {
                    GameObject singletonObj = new GameObject();
                    _instance = singletonObj.AddComponent<T>();
                    singletonObj.name = typeof(T).ToString();
                }
            }
            return _instance;
        }
    }
    protected virtual void Awake() //중복체크 및 연결기능 구현 (재정의 가능)
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}
