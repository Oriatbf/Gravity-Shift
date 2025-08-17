using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    private Dictionary<string, AudioClip> soundDictionary;
    
    private AudioSource audioSource;

    public AudioClip backgroundsoundClip;
    public AudioClip playerLRmoveClip;
    public AudioClip thornDieClip;
    public AudioClip fallDieClip;
    public AudioClip getCoinClip;
    public AudioClip getItemClip;
    public AudioClip illusionChangeClip;
    public AudioClip useItemClip;

    private void Awake()
    {
        Instance = this;
        
        soundDictionary = new Dictionary<string, AudioClip>();
        
        audioSource = GetComponent<AudioSource>();

        soundDictionary.Add("background", backgroundsoundClip);
        soundDictionary.Add("playerLRmove", playerLRmoveClip);
        soundDictionary.Add("thornDie", thornDieClip);
        soundDictionary.Add("fallDie", fallDieClip);
        soundDictionary.Add("getCoin", getCoinClip);
        soundDictionary.Add("getItem", getItemClip);
        soundDictionary.Add("illusionChange", illusionChangeClip);
        soundDictionary.Add("useItem", useItemClip);
    }
    
    public void PlaySound(string soundName)
    {
        audioSource.PlayOneShot(soundDictionary[soundName]);
    }
}
