using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public List<GameObject> coinImage;
    private List<Image> _image = new();
    private int currentCoinCount = 0;

    void Awake()
    {
        for (int i = 0; i < 3; i++)
        {

            Image img = coinImage[i].GetComponent<Image>();
            _image.Add(img);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            _image[i].color = new Vector4(0f, 0f, 0f, 1f);

        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCoinUI()
    {
        if (currentCoinCount >= 3)
        {
            return;
        }

        _image[currentCoinCount].color = Color.yellow;
        currentCoinCount++;
    }
}
