using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSound;

    public void PlayClickSound()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(clickSound);
    }
}