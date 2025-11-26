using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerJsonSave
{
    private string filePath; //파일 경로
    private void SetFilePath()
    {
        filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
        Debug.Log("파일경로: " + filePath);
    }

    public void SaveData()
    {
        if (filePath == "" || filePath == null) SetFilePath();
        PlayerDataJson playerDataJson = new PlayerDataJson();
        playerDataJson.Level = PlayerManager.Instance.GetPlayerLevel();
        playerDataJson.Gold = PlayerManager.Instance.GetPlayerGold();
        playerDataJson.ExpPoint = PlayerManager.Instance.GetPlayerExpPoint();
        string json = JsonUtility.ToJson(playerDataJson, true);
        File.WriteAllText(filePath, json);
        Debug.Log("플레이어 데이터 저장완료");
    }
    public PlayerDataJson LoadData()
    {
        if (filePath == "" || filePath == null) SetFilePath();
        if (!File.Exists(filePath)) MakeInitSaveData();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerDataJson playerDataJson = JsonUtility.FromJson<PlayerDataJson>(json);
            Debug.Log("로드 완료");
            return playerDataJson;
        }
        else
        {
            return null;
        }
    }
    public void MakeInitSaveData()
    {

        if (filePath == "" || filePath == null) SetFilePath();
        PlayerDataJson playerDataJson = new PlayerDataJson();
        playerDataJson.Level = PlayerManager.Instance.GetPlayerLevel();
        playerDataJson.Gold = PlayerManager.Instance.GetPlayerGold();
        playerDataJson.ExpPoint = PlayerManager.Instance.GetPlayerExpPoint();
        string json = JsonUtility.ToJson(playerDataJson, true);
        File.WriteAllText(filePath, json);
        Debug.Log("플레이어 초기 데이터 저장완료");
    }
}
