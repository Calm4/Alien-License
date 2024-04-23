using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private AudioSource musicAudioSource;
    [SerializeField]
    private AudioSource sfxAudioSource;

    [SerializeField] private AudioClip backgroundMusic; 
    [SerializeField] private AudioClip alarmClockSound; 
    [SerializeField] private AudioClip NLOSound; 
    [SerializeField] private AudioClip defeatSound; 
    [SerializeField] private AudioClip completeSound; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        PlayBackgroundMusic();
    }

    public void ButtonClickSound()
    {
        sfxAudioSource.PlayOneShot(NLOSound);
    }
    public void PlayBackgroundMusic()
    {
        musicAudioSource.clip = backgroundMusic;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void StopBackgroundMusic()
    {
        musicAudioSource.Stop();
    }
    public void PlayAlarmClockSound()
    {
        sfxAudioSource.PlayOneShot(alarmClockSound);
    }

    public void PlayNLOSound()
    {
        sfxAudioSource.PlayOneShot(NLOSound);
    }

    public void PlayDefeatSound()
    {
        sfxAudioSource.PlayOneShot(defeatSound);
    }

    public void PlayCompleteSound()
    {
        sfxAudioSource.PlayOneShot(completeSound);
    }
}