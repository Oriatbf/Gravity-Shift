using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectController : MonoBehaviour
{
    public Transform player;
    public List<Transform> stagepoints;

    public float moveSpeed = 3f;
    public float rotateSpeed=720f;
    private Coroutine moveCoroutine;

    public void SelectStage(int targetStage)
    {
        if (targetStage <= 0)
        {
            Debug.LogWarning("잘못된 스테이지 번호" + targetStage);
            return;

        }

        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveThroughStages(targetStage));


    }

    private IEnumerator MoveThroughStages(int targetStage)
    {
        for (int i = 0; i < targetStage; i++)
        {
            yield return StartCoroutine(MovePlayerTo(stagepoints[i].position));
            yield return new WaitForSeconds(0.15f);


        }

        SceneManager.LoadScene("stage" + targetStage);
    }

    private IEnumerator MovePlayerTo(Vector3 targetPos)
    {
        Vector3 flatTarget = new Vector3(targetPos.x, player.position.y, targetPos.z);

        while (Vector3.Distance(player.position, flatTarget) > 0.05f)
        {
            Vector3 dir = flatTarget - player.position;

            if (dir.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir);
                player.rotation = Quaternion.RotateTowards(player.rotation, targetRot, rotateSpeed * Time.deltaTime);

            }

            player.position = Vector3.MoveTowards(player.position, flatTarget, moveSpeed * Time.deltaTime);

            yield return null;
        }
    }




}