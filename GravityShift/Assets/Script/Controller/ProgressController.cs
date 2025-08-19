using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VInspector;

public class ProgressController : MonoBehaviour
{
    [SerializeField] private bool checkingFinshTimer;

    [SerializeField]private float curTimer;
    [SerializeField]private float maxTimer;
    [SerializeField] private Scrollbar progressBar;
    [SerializeField] private Image progressImage;

    private bool inTimer = true;

    public void Update()
    {
        if (inTimer)
        {
            curTimer += Time.deltaTime;
            if (!checkingFinshTimer)
            {
                progressBar.value = curTimer / maxTimer;
                progressImage.fillAmount = curTimer / maxTimer;
                if(curTimer >= maxTimer)StopTimer();   
            }
        }
            
    }

    [Button]
    public void StopTimer()
    {
        //if(checkingFinshTimer)DataManager.Inst.AddStageTime(curTimer);
        inTimer = false;
    }
}
