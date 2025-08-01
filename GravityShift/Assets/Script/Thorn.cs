using UnityEngine;

public class Thorn : MonoBehaviour
{
    

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            SettingController.Inst.EndingUI(false);
            Debug.Log("가시 디버그");
        }
    }
}
