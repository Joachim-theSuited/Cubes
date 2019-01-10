using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("SaveData")]
public class SaveData {

    public SaveData() {}

    public SaveData(int unlockedLevels, float[] levelTimes) {
        this.unlockedLevels = unlockedLevels;
        this.levelTimes = levelTimes;
    }

    public static SaveData DEFAULT = new SaveData(1, new float[] {});

    [XmlElement("unlockedLevels")]
    public int unlockedLevels;

    [XmlArray("levelTimes")]
    public float[] levelTimes;

    public Dictionary<string, string> toFormattedDictionary() {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary.Add("unlockedLevels", unlockedLevels.ToString());
        for (int i = 0; i < levelTimes.Length; i++) {
            // TODO: format times properly
            dictionary.Add(String.Format("levelTimes[{0}]", i), levelTimes[i].ToString());
        }

        return dictionary;
    }
}