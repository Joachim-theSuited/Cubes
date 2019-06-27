using System;  
using System.IO;  
using System.Collections;  
using System.Xml;
using System.Xml.Serialization;

using UnityEngine;
public static class GameProgressionPersistence {

    private static readonly XmlSerializer SERIALIZER = new XmlSerializer(typeof(SaveData));

    public static readonly String PROGRESS_FILE = Application.persistentDataPath + "/progress.xml";

    public static void saveProgress(SaveData saveData) {
        StreamWriter writer = new StreamWriter(PROGRESS_FILE);
        SERIALIZER.Serialize(writer, saveData);
        Debug.Log("Saving" + saveData);
        writer.Close();
    }

    public static SaveData loadProgress() {
        try
        {
            using (StreamReader reader = new StreamReader(PROGRESS_FILE))
            {
                return (SaveData) SERIALIZER.Deserialize(reader);
            }
        } 
        catch (Exception ex)
        {
            if (ex is FileNotFoundException)
            {
                Debug.Log("File not found, falling back");
                return SaveData.DEFAULT;
            }
            if (ex is XmlException)
            {
                Debug.Log("XmlException, falling back");
                return SaveData.DEFAULT;
            }
            throw ex;
        }
    }

}