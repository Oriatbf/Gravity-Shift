using System;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageSelectController : MonoBehaviour
{
    public Transform player;
    public List<Transform> stagepoints;
    public float moveSpeed = 3f;

    private int currentStageIndex = 0;
    private bool isOnstage = true;

    private Stage currentGlowingStage;
    
    Animator animator;

    [Header("UI")]
    public TMP_Text stageMessageText;
    

    private void Awake()
    {
        animator = player.GetComponent<Animator>();
    }

    private void Start()
    {
        player.position = stagepoints[currentStageIndex].position;

        if (stageMessageText != null) { stageMessageText.gameObject.SetActive(false); }
    }

    void Update()
    {

    }

    public void SetStage()
    {
        DataManager.Inst.SetStage(currentStageIndex);
        FadeInFadeOutManager.Inst.FadeOut(1,true);
    }
    
    private Coroutine moveCoroutine;

    public void SelectStage(int targetStage)
    {
        animator.SetBool("isWalk",true);
        targetStage = Mathf.Clamp(targetStage, 0, stagepoints.Count - 1); 
        
        if(currentGlowingStage != null)
        {
            currentGlowingStage.ResetVisualState();
        }

        ShowStageMessage(targetStage);

        currentGlowingStage = FindStageByNumber(targetStage);
        if(currentGlowingStage != null) {currentGlowingStage.SetSelectedState(); }

        StopAllCoroutines();
        StartCoroutine(MoveThroughStages(targetStage));
    }

    private void ShowStageMessage(int stageNumber)
    {
        if (stageMessageText != null)
        {
            stageMessageText.gameObject.SetActive(true);
        }
    }

   

    private IEnumerator MoveThroughStages(int targetStage)
    {
        isOnstage = false;

        int step = (targetStage > currentStageIndex) ? 1 : -1;
      
        for (int i = currentStageIndex; i != targetStage; i += step)
        {
            int nextIndex = i + step;
            if (nextIndex < 0 || nextIndex >= stagepoints.Count)
                break;
            Vector3 direction = (stagepoints[nextIndex].position - player.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            player.DOLocalRotate(targetRotation.eulerAngles, 1f);
            yield return StartCoroutine(MovePlayerTo(stagepoints[i + step].position));
            currentStageIndex = i + step;
        }

        isOnstage = true;
        animator.SetBool("isWalk",false);
    }

    private IEnumerator MovePlayerTo(Vector3 targetPos)
    {
        while (Vector3.Distance(player.position, targetPos) > 0.05f)
        {
            player.position = Vector3.MoveTowards(player.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private Stage FindStageByNumber(int stageNumber)
    {
        Stage[] stages = FindObjectsOfType<Stage>();
        foreach (var s in stages)
        {
            if(s.stageNumber == stageNumber)
                return s;
        }
        return null;
    }

    


}