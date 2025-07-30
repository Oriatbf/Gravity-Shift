using UnityEngine;

public class zone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         void OnTriggerExit(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            Debug.Log("환각존 나옴");
        }
    }
    }
}
