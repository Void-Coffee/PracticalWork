using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music Clips")]
    public AudioClip menuMusic;
    public AudioClip levelMusic;
    public string menuSceneName = "MainMenu";

    [Header("Volumes (0 to 1)")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        masterVolume = SaveManager.LoadMasterVolume();
        musicVolume = SaveManager.LoadMusicVolume();
        sfxVolume = SaveManager.LoadSFXVolume();

        ApplyVolumes();
    }

    void OnEnable() { SceneManager.sceneLoaded += HandleSceneLoaded; }
    void OnDisable() { SceneManager.sceneLoaded -= HandleSceneLoaded; }

    void Start()
    {
        HandleSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == menuSceneName)
            PlayMusic(menuMusic);
        else
            PlayMusic(levelMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        ApplyVolumes();
        SaveManager.SaveVolumes(masterVolume, musicVolume, sfxVolume);
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        ApplyVolumes();
        SaveManager.SaveVolumes(masterVolume, musicVolume, sfxVolume);
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        ApplyVolumes();
        SaveManager.SaveVolumes(masterVolume, musicVolume, sfxVolume);
    }

    void ApplyVolumes()
    {
        musicSource.volume = masterVolume * musicVolume;
        sfxSource.volume = masterVolume * sfxVolume;
    }
}