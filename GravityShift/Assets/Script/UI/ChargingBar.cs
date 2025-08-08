
using System;
using UnityEngine;
using UnityEngine.UI;

public class ChargingBar : MonoBehaviour
{
   [SerializeField] private KeyCode key;
   [SerializeField] private Image bar;
   private float charging = 0;
   Action chargingAction;
   private bool isActive = false;
   
   private float multiplier = 1.5f;
   
   public void Init(Action action)
   {
        chargingAction = action;
   }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(key))
        {
            charging += Time.deltaTime*multiplier;
            if (charging > 1 && !isActive)
            {
                chargingAction?.Invoke();
                isActive = true;
            }
        }
        else
        {
            if(charging > 0 && !isActive)charging -= Time.deltaTime*(multiplier+1f);
        }
        
        bar.fillAmount = charging;
        
    }

    
}
