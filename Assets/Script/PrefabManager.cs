using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : Singleton<PrefabManager>
{
    [System.Serializable]
    public class PrefabEntry
    {
        public string key;       // CSV에서 사용할 이름
        public GameObject prefab; // 실제 Prefab
    }

    [SerializeField] private PrefabEntry[] prefabs;

    private Dictionary<string, GameObject> prefabDict;

    protected override void Awake()
    {
        isDestroyOnLoad = false;
        base.Awake();
    }

    public GameObject GetPrefab(string key)
    {
        //초기 설정
        if (prefabDict == null) SetInit();
        //키값으로 탐색
        if (prefabDict.TryGetValue(key, out GameObject prefab))
            return prefab;
        Debug.LogError("Prefab not found: " + key);
        return null;
    }

    private void SetInit()
    {
        prefabDict = new Dictionary<string, GameObject>();
        foreach (var entry in prefabs)
        {
            if (!prefabDict.ContainsKey(entry.key))
                prefabDict.Add(entry.key, entry.prefab);
        }
    }

}
