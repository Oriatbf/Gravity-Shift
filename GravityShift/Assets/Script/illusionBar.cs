using System;
using UnityEngine;
using UnityEngine.UI;

public class illusionBar : MonoBehaviour
{
    public Slider IllusionBar;
    private int IllusionValue;
    private bool isIllusion;
    public PlayerCtrl player;
    

    void Start()
    {
        IllusionBar.value = 0;
        IllusionBar.maxValue = 5;
        IllusionBar.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (player.isIllusion == true)
        {
            IllusionBar.gameObject.SetActive(true);
            IllusionBar.value += 1 * Time.fixedDeltaTime;
            if (IllusionBar.value == IllusionBar.maxValue)
            {
                player.RandomGravity();
                IllusionBar.value = 0;
            }
        }
        else
            IllusionBar.gameObject.SetActive(false); 
    }
    
}
