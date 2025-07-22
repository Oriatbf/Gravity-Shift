using UnityEngine;

public class capsule : MonoBehaviour
{
    public int coin;
    private Rigidbody rb;
    [SerializeField] private UImanager uImanager;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coin = 0;
    }
    
    void OnTriggerEnter(Collider obj)
    {
        Debug.Log($"Trigger Enter : {obj} - {obj.tag}");
        if (obj.gameObject.tag == "coin")
        {
            Debug.Log("코인디버그");
            uImanager.SetCoinUI();
            Destroy(obj.gameObject);
            coin += 1;

        }
        //코인 닿으면 디버그, 사라짐

        if (obj.gameObject.tag == "zone")
        {
            Debug.Log("환각존 들어감");

        }
        //환각존 닿으면 디버그


        if (obj.gameObject.tag == "item1")
        {
            Debug.Log("item1 디버그");
            Destroy(obj.gameObject);
        }
        //item1 닿으면 디버그

        if (obj.gameObject.tag == "item2")
        {
            Debug.Log("item2 디버그");
            Destroy(obj.gameObject);
        }
        //item2 닿으면 디버그
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.gameObject.tag == "zone")
        {
            Debug.Log("환각존 나옴");
        }
    }
}


