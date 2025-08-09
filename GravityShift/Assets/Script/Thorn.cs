using UnityEngine;

public class Thorn : MonoBehaviour
{
    

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
           // PlayerCtrl playerCtrl = obj.GetComponent<PlayerCtrl>();

            SettingController.Inst.EndingUI(false);
            Debug.Log("가시 디버그");
        }
    }
}
