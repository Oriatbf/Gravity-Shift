using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using VInspector.Libs;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Scrollbar bgmScrollbar,sfxScrollbar;


    private void Start()
    {
        audioMixer.SetFloat("BGM",DataManager.Inst.Data.bgmVolume == 0 ? -80f : Mathf.Log10(DataManager.Inst.Data.bgmVolume) * 20);
        audioMixer.SetFloat("SFX", DataManager.Inst.Data.sfxVolume == 0 ? -80f : Mathf.Log10(DataManager.Inst.Data.sfxVolume) * 20);
    }

    public void SetScrollbar()
    {
        if (bgmScrollbar != null && sfxScrollbar != null)
        {
            bgmScrollbar.value = DataManager.Inst.Data.bgmVolume;
            sfxScrollbar.value = DataManager.Inst.Data.sfxVolume;
        }
    }

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
        DataManager.Inst.Data.bgmVolume = value;
        DataManager.Inst.JsonSave();
    }
    
    private void ChangeSFXVolume(float value)
    {
        float _volume = value == 0 ? -80f : Mathf.Log10(value) * 20;
        audioMixer.SetFloat("SFX", _volume);
        DataManager.Inst.Data.sfxVolume = value;
        DataManager.Inst.JsonSave();
    }
}
