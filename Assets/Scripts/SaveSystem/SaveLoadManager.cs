using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager
{

    public static void save(//may be a class, like player
                            Player player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(Application.persistentDataPath + "/player.wtf", FileMode.Create);
        //datapath is root of assets
        //persistent is in program files or something like that

        PlayerData data = new PlayerData(player);
        bf.Serialize(fs, data);
        fs.Close();


    }

    public static int LoadPlayer() {

        if (File.Exists(Application.persistentDataPath + "/player.wtf")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Application.persistentDataPath + "/player.wtf", FileMode.Open);

            PlayerData data = bf.Deserialize(fs) as PlayerData;
            fs.Close();

            return data.currentSelected;
        }
        return -1;
    }

    [Serializable]
    public class PlayerData
    {
        public int currentSelected;
        public PlayerData(Player player)
        {
            currentSelected = player.GetSelected();
        }
    }
}
