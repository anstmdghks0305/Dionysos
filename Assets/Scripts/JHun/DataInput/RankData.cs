using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public static class RankData
{
    private static string _path = Directory.GetCurrentDirectory() + "/" + "Data";
    [SerializeField] private static string name;
    [SerializeField] private static int score;

    public static void RankSave(UserData saveData)
    {
        if(!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }

        string data = JsonUtility.ToJson(saveData);
        Debug.Log(data);
        File.WriteAllText(_path + "/"+ "RankData" + ".json", data);
        Debug.Log("저장 성공");
    }

    public static UserData RankLoad()
    {
        UserData saveData = new UserData();
        string _filePath = _path + "/RankData.json";

        if (!File.Exists(_filePath))
        {
            Debug.LogError($"해당 파일이 {_filePath}에 존재하지 않습니다.");
            return saveData;
        }

        string readData = File.ReadAllText(_filePath);
        saveData = JsonUtility.FromJson<UserData>(readData);
        Debug.Log("유저 데이터 로드 성공!");
        return saveData;
    }
}
