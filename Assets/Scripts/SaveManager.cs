using UnityEngine;

public static class SaveManager
{
    public static int GetUnlockedLevelCount()
    {
        return PlayerPrefs.GetInt("UnlockedLevels", 1);
    }

    public static void UnlockLevel(int levelNumber)
    {
        int current = GetUnlockedLevelCount();

        if (levelNumber > current)
        {
            PlayerPrefs.SetInt("UnlockedLevels", levelNumber);
            PlayerPrefs.Save();
        }
    }

    public static bool IsLevelUnlocked(int levelNumber)
    {
        return levelNumber <= GetUnlockedLevelCount();
    }

    // --- Volume Settings ---

    public static void SaveVolumes(float master, float music, float sfx)
    {
        PlayerPrefs.SetFloat("MasterVolume", master);
        PlayerPrefs.SetFloat("MusicVolume", music);
        PlayerPrefs.SetFloat("SFXVolume", sfx);
        PlayerPrefs.Save();
    }

    public static float LoadMasterVolume() { return PlayerPrefs.GetFloat("MasterVolume", 1f); }
    public static float LoadMusicVolume() { return PlayerPrefs.GetFloat("MusicVolume", 1f); }
    public static float LoadSFXVolume() { return PlayerPrefs.GetFloat("SFXVolume", 1f); }
}