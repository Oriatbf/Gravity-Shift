using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    private Dictionary<string, AudioClip> soundDictionary;
    
    private AudioSource audioSource;
    
    public AudioClip playerLRmoveClip;
    public AudioClip thornDieClip;
    public AudioClip fallDieClip;
    public AudioClip getCoinClip;
    public AudioClip getItemClip;
    public AudioClip illusionChangeClip;
    public AudioClip useItemClip;
    public AudioClip gravityClip;

    private void Awake()
    {
        
        soundDictionary = new Dictionary<string, AudioClip>();
        
        audioSource = GetComponent<AudioSource>();
        
        soundDictionary.Add("playerLRmove", playerLRmoveClip);
        soundDictionary.Add("thornDie", thornDieClip);
        soundDictionary.Add("fallDie", fallDieClip);
        soundDictionary.Add("getCoin", getCoinClip);
        soundDictionary.Add("getItem", getItemClip);
        soundDictionary.Add("illusionChange", illusionChangeClip);
        soundDictionary.Add("useItem", useItemClip);
        soundDictionary.Add("gravity", gravityClip);
    }
    
    public void PlaySound(string soundName)
    {
        audioSource.PlayOneShot(soundDictionary[soundName]);
    }
}
