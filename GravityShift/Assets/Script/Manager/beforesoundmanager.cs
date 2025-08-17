using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class beforesoundmanager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Scrollbar bgmScrollbar,sfxScrollbar;
    

    public void ChangeScrollbar(Scrollbar _bgmScrollbar, Scrollbar _sfxScrollbar)
    {
        bgmScrollbar = _bgmScrollbar;
        sfxScrollbar = _sfxScrollbar;
        bgmScrollbar?.onValueChanged.AddListener(ChangeBGMVolume);
        sfxScrollbar?.onValueChanged.AddListener(ChangeSFXVolume);
    }
    
    private void ChangeBGMVolume(float value)
    {
        float _volume = value == 0 ? -80f : Mathf.Log10(value) * 20;
        audioMixer.SetFloat("BGM", _volume);
    }
    
    private void ChangeSFXVolume(float value)
    {
        float _volume = value == 0 ? -80f : Mathf.Log10(value) * 20;
        audioMixer.SetFloat("SFX", _volume);
    }
}
