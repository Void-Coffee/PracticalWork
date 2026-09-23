using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderHookup : MonoBehaviour
{
    public enum SliderType { Master, Music, SFX }
    public SliderType type;

    void Start()
    {
        if (AudioManager.Instance == null) return;

        Slider slider = GetComponent<Slider>();
        if (slider == null) return;

        switch (type)
        {
            case SliderType.Master: slider.value = AudioManager.Instance.masterVolume; break;
            case SliderType.Music: slider.value = AudioManager.Instance.musicVolume; break;
            case SliderType.SFX: slider.value = AudioManager.Instance.sfxVolume; break;
        }
    }

    public void OnSliderChanged(float value)
    {
        if (AudioManager.Instance == null) return;

        switch (type)
        {
            case SliderType.Master: AudioManager.Instance.SetMasterVolume(value); break;
            case SliderType.Music: AudioManager.Instance.SetMusicVolume(value); break;
            case SliderType.SFX: AudioManager.Instance.SetSFXVolume(value); break;
        }
    }
}