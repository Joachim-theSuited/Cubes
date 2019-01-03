using System;
using System.Collections;  
using System.Xml.Serialization;

[XmlRoot("SaveData")]
public class SaveData {

    public SaveData() {

    }

    public SaveData(int unlockedLevels, float[] levelTimes) {
        this.unlockedLevels = unlockedLevels;
        this.levelTimes = levelTimes;
    }

    public static SaveData DEFAULT = new SaveData(1, new float[] {});

    [XmlElement("unlockedLevels")]
    public int unlockedLevels;

    [XmlArray("levelTimes")]
    public float[] levelTimes;

}