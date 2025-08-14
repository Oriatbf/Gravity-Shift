using System;
using UnityEngine;
using UnityEngine.UI;

public class illusionBar : MonoBehaviour
{
    public Image IllusionBar;
    private int IllusionValue;
    private bool isIllusion;

    public float now;
    public float max;
    public PlayerCtrl player;
    

    void Start()
    {
        IllusionBar.fillAmount = 0;
        max = 5f;
        IllusionBar.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (player.isIllusion == true)
        {
            IllusionBar.gameObject.SetActive(true);
            now += (1 * Time.fixedDeltaTime) / max;
            IllusionBar.fillAmount = now;
            if (now >= 1f)
            {
                player.RandomGravity();
                now = 0f;
                IllusionBar.fillAmount = now;
            }
        }
        else if (player.isIllusion == false)
        {
            now = 0f;
            IllusionBar.fillAmount = now;
            IllusionBar.gameObject.SetActive(false); 
        }
            
    }
    
}
