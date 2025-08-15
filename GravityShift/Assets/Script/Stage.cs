using UnityEngine;
using DG.Tweening;


public class Stage : MonoBehaviour
{
    public int stageNumber;           
    StageSelectController controller;

    [Header("Glow Settings")]
    public Color glowColor = Color.white;
    public float intensity = 2f;
    public float transitionDuration = 0.3f;

    private Material material;
    private bool isGlowing = false;

    private Tween rotationTween;
    private Tween floatTween;

    void Start()
    {
        controller = FindObjectOfType<StageSelectController>();
        if (controller == null) Debug.LogError("StageSelectController가 씬에 없습니다.");

        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", Color.black);

        StartIdleAnimation();
    }

    void StartIdleAnimation()
    {
        floatTween = transform.DOLocalMoveY(transform.position.y + 0.5f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
        rotationTween = transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

    }

    public void StopRotation()
    {
        if(rotationTween != null) rotationTween.Kill();
    }

    public void StartRotation()
    {
        if (rotationTween != null || !rotationTween.IsActive())
        {
            rotationTween= transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

        }
    }

    void OnMouseDown()
    {
        if (controller != null)
            controller.SelectStage(stageNumber);
    }
    
    public void TurnOnGlow()
    {
        isGlowing = true;
        Color targetColor = glowColor * intensity;
        material.DOColor(targetColor, "_EmissionColor", transitionDuration);
    }

    public void TurnOffGlow()
    {
        isGlowing = false;
        material.DOColor(Color.black, "_EmissionColor", transitionDuration);
    }

    public void SetSelectedState()
    {
        TurnOnGlow();
        StopRotation();
    }

    public void ResetVisualState()
    {
        TurnOffGlow();
        StartRotation();
    }
}