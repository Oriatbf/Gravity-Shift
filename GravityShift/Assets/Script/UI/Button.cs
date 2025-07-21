using System;
using System.Drawing;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class Button : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private RectTransform rect;
    private Image image;
    private const float rectSize = 1.2f, duration = 0.15f,unSelectedColor = 180f;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
       // image = GetComponent<Image>();
       // Color newColor = new Color(unSelectedColor/255,unSelectedColor/255,unSelectedColor/255);
       // image.color = newColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color newColor = new Color(1,1,1);
        //image.DOColor(newColor, duration).SetUpdate(true);
        rect.DOScale(new Vector2(rectSize,rectSize), duration).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color newColor = new Color(unSelectedColor/255,unSelectedColor/255,unSelectedColor/255);
        //image.DOColor(newColor, duration).SetUpdate(true);
        rect.DOScale(Vector2.one, duration).SetUpdate(true);
    }
}
