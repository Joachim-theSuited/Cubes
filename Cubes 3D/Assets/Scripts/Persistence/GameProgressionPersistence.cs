using System;  
using System.IO;  
using System.Collections;  
using System.Xml.Serialization;

using UnityEngine;
static class GameProgressionPersistence {

    private static readonly XmlSerializer SERIALIZER = new XmlSerializer(typeof(SaveData));

    private static readonly String PROGRESS_FILE = Application.persistentDataPath + "/progress.xml";

    public static void saveProgress(SaveData saveData) {
        StreamWriter writer = new StreamWriter(PROGRESS_FILE);
        SERIALIZER.Serialize(writer, saveData);
        Debug.Log("Saving" + saveData);
        writer.Close();
    }

    public static SaveData loadProgress() {
        try
        {
            StreamReader reader = new StreamReader(PROGRESS_FILE);
            return (SaveData) SERIALIZER.Deserialize(reader);
        } 
        catch (FileNotFoundException)
        {
            Debug.Log("Failed to load, falling back");
            return SaveData.DEFAULT;
        }
    }

}