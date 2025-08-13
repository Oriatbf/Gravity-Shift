using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectController : MonoBehaviour
{
    public Transform player;
    public List<Transform> stagepoints;
    public float moveSpeed = 3f;

    private int currentStageIndex = 0;
    private bool isOnstage = false;
    
    Animator animator;

    private void Awake()
    {
        animator = player.GetComponent<Animator>();
    }

    void Update()
    {
        if (isOnstage && Input.GetKeyDown(KeyCode.P))
        {
            string sceneName = "Stage" + (currentStageIndex + 1);
            SceneManager.LoadScene("Tutorial");
        }
    }
    private Coroutine moveCoroutine;

    public void SelectStage(int targetStage)
    {
        animator.SetBool("isWalk",true);
        targetStage = Mathf.Clamp(targetStage, 0, stagepoints.Count - 1); 
        StopAllCoroutines();
        StartCoroutine(MoveThroughStages(targetStage));
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




}