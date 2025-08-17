using UnityEngine;

public class Thorn : MonoBehaviour
{
    private bool invincible;
    
    public void Invincible()
    {
        invincible = true;
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
           PlayerCtrl playerCtrl = obj.GetComponent<PlayerCtrl>();

           if (!playerCtrl.isInvincible)
           {
               SoundManager.Instance.PlaySound("thornDie");
               playerCtrl.PlayerDead(false);
               Debug.Log("가시 디버그");
           }
        }
    }
}
