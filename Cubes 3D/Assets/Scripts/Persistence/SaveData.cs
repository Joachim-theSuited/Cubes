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
            TimeSpan timeSpan = TimeSpan.FromSeconds(levelTimes[i]);
            dictionary.Add(String.Format("levelTimes[{0}]", i), String.Format("{0:00}:{1:00}.{2:000}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds));
        }

        return dictionary;
    }

    public bool IsUnlocked(int levelNumber)
    {
        return (unlockedLevels & (1 << levelNumber)) != 0;
    }

    public float GetBestTime(int levelNumber)
    {
        if (levelNumber < levelTimes.Length)
        {
            return levelTimes[levelNumber];
        }
        return -1;
    }

    public void LevelFinished(int levelNumber, float timeInSeconds)
    {
        unlockedLevels |= 1 << (levelNumber + 1);

        int oldLength = levelTimes.Length;
        if (oldLength <= levelNumber)
        {
            Array.Resize(ref levelTimes, levelNumber + 1);
            for (int i = oldLength; i < levelTimes.Length; ++i)
                levelTimes[i] = -1;
        }

        if (levelTimes[levelNumber] == -1)
            levelTimes[levelNumber] = timeInSeconds;
        else
            levelTimes[levelNumber] = Math.Min(levelTimes[levelNumber], timeInSeconds);
    }

}