using UnityEngine;

public class Thorn : MonoBehaviour
{

    void Update()
    {




    }
    

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            Debug.Log("가시 디버그");
        }
    }
}
