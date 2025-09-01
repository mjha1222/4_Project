using System.Data;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public enum bgm
    {
        Main,
        InGame
    }

    public enum effect
    {
        click,
        bomb
    }

    public static SoundManager instance;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioClip[] effects;

    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource effectSource;

    [Range(0f, 1f)] float masterVolume;
    [Range(0f, 1f)] float bgmVolume;
    [Range(0f, 1f)] float effectVolume;

    string musicPath = "Sounds/";

    public static SoundManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<SoundManager>();

                if (instance == null)
                {
                    instance = new GameObject("SoundManager").AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        bgms = Resources.LoadAll<AudioClip>(musicPath + "BGM");
        effects = Resources.LoadAll<AudioClip>(musicPath + "Effect");

        //시작시 볼륨값 고정
        masterVolume = 0.7f;
        bgmVolume = 0.5f;
        effectVolume = 0.5f;
    }

    public void Start()
    {
        UpdateVolume();
        PlayBGM(bgm.Main);
    }

    public void StopAllSounds()
    {
        bgmSource.Stop();
        effectSource.Stop();
    }

    public void PlayBGM(bgm bgmIndex)
    {
        bgmSource.clip = bgms[(int)bgmIndex];
        bgmSource.Play();
    }

    private void UpdateVolume()
    {
        audioMixer.SetFloat("Master", LinearToDecibel(masterVolume));
        audioMixer.SetFloat("BGM", LinearToDecibel(bgmVolume));
        audioMixer.SetFloat("Effect", LinearToDecibel(effectVolume));
    }

    private float LinearToDecibel(float bgmvalue)
    {
        return Mathf.Approximately(float.MaxValue, 0f) ? -80f : Mathf.Log10(bgmvalue) * 20f;
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        UpdateVolume();
    }

    public void SetBGMVolume(float value)
    {
        bgmVolume = value;
        UpdateVolume();
    }

    public void SetEffectVolume(float value)
    {
        effectVolume = value;
        UpdateVolume();
    }
}
