using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static void Save(string data,string file_name) {

        Debug.Log(Application.persistentDataPath);
        File.WriteAllText(Application.persistentDataPath + "/"+ file_name+".json", data);
    }

    public static string Load(string file_name)
    {
        if (File.Exists(Application.persistentDataPath + "/" + file_name + ".json"))
            return File.ReadAllText(Application.persistentDataPath + "/" + file_name + ".json");
        return null;
    }


}
