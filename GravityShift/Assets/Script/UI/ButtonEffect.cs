using System;
using System.Drawing;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class ButtonEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private RectTransform rect;
    private const float rectSize = 1.2f, duration = 0.15f,unSelectedColor = 180f;
    private void Awake()
    {
        rect = GetComponent<RectTransform>(); ;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.DOScale(new Vector2(rectSize,rectSize), duration).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.DOScale(Vector2.one, duration).SetUpdate(true);
    }
}
