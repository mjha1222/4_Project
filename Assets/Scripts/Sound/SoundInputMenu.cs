using UnityEngine;
using UnityEngine.UI;

public class SoundInputMenu : MonoBehaviour
{
    
    [Header("SoundVolumes")]
    public Slider masterVolume;
    public Slider bgmVolume;
    public Slider effectVolume;

    private void Start()
    {
        masterVolume.value = SoundManager.instance.masterVolume;
        bgmVolume.value = SoundManager.instance.bgmVolume;
        effectVolume.value = SoundManager.instance.effectVolume;

        masterVolume.onValueChanged.AddListener(SoundManager.instance.SetMasterVolume);
        bgmVolume.onValueChanged.AddListener(SoundManager.instance.SetBGMVolume);
        effectVolume.onValueChanged.AddListener(SoundManager.instance.SetEffectVolume);
    }
}
