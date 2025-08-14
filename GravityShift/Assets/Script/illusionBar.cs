using System;
using UnityEngine;
using UnityEngine.UI;

public class illusionBar : MonoBehaviour
{
    public Image IllusionBar;
    private int IllusionValue;
    private bool isIllusion;

    private float now;
    private float max;
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
            now = 1 * Time.fixedDeltaTime;
            IllusionBar.fillAmount += now;
            if (now == max)
            {
                player.RandomGravity();
                IllusionBar.fillAmount = 0;
            }
        }
        else if (player.isIllusion == false)
        {
            IllusionBar.fillAmount = 0;
            IllusionBar.gameObject.SetActive(false); 
        }
            
    }
    
}
