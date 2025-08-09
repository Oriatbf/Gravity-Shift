using UnityEngine;

public class Stage : MonoBehaviour
{
    public int stageNumber;           
    StageSelectController controller;

    void Start()
    {
        controller = FindObjectOfType<StageSelectController>();
        if (controller == null) Debug.LogError("StageSelectController가 씬에 없습니다.");
    }

    void OnMouseDown()
    {
        if (controller != null)
            controller.SelectStage(stageNumber);
    }
}