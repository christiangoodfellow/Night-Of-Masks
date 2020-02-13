using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class LevelSaveLoader 
{
    public static void SaveLevelBeat(int level)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/playerdata.data", FileMode.Create);

        Debug.Log(level);

        LevelBeatNum data = new LevelBeatNum(level);

        bf.Serialize(stream, data);
        stream.Close();

    }

    public static int LoadLevelsBeat()
    {
        if (File.Exists(Application.persistentDataPath + "/playerdata.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/playerdata.data", FileMode.Open);

            LevelBeatNum data = bf.Deserialize(stream) as LevelBeatNum;

            stream.Close();
            return data.numOfLevels;
        }
        else
        {
            Debug.LogError("Save data not Found");
            int fault = 0;
            return fault;
        }
    }

    [Serializable]
    public class LevelBeatNum
    {
        public int numOfLevels;
        public LevelBeatNum(int level)
        {
            numOfLevels = level;
        }
    }


}
