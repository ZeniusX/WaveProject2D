using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_VOLUME = "SoundVolume";

    public static SoundManager Instance { get; private set; }

    private float soundVolume = 1f;

    private void Awake()
    {
        Instance = this;

        soundVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_VOLUME, 1f);
    }

    public void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * soundVolume);
    }

    public void SetSoundVolume(float soundVolume)
    {
        this.soundVolume = soundVolume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_VOLUME, soundVolume);
    }

    public float GetSoundVolume() => soundVolume;
}
