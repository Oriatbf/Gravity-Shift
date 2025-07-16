using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInFadeOutManager : SingletonDontDestroyOnLoad<FadeInFadeOutManager>
{
    [SerializeField] private Image fadeImage;

    private void Start()
    {
        FadeIn();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => FadeIn(true,0.5f);

    //검은색 화면 해제
    public void FadeIn(bool useDotween = false, float dotweenTime = 0.2f, Action action = null)
    {
        fadeImage.DOComplete();
        if (!useDotween)
        {
            fadeImage.DOFade(0, 0);
            return;
        }
        fadeImage.DOFade(0, dotweenTime).OnComplete(()=>action?.Invoke());
    }

    //검은색 화면 진입
    #region FadeOut 
    
    public void FadeOut(bool useDotween = false, float dotweenTime = 0.2f, Action action = null) 
        => FadeOutInternal(useDotween, dotweenTime,action);
    
    public void FadeOut(int sceneIndex,bool useDotween = false, float dotweenTime = 0.2f) 
        => FadeOutInternal(useDotween, dotweenTime,()=>SceneManager.LoadScene(sceneIndex));
    
    public void FadeOut(string sceneName,bool useDotween = false, float dotweenTime = 0.2f)
        => FadeOutInternal(useDotween, dotweenTime,()=>SceneManager.LoadScene(sceneName));

    private void FadeOutInternal(bool useDotween,float dotweenTime,Action action = null)
    {
        fadeImage.DOComplete();
        if (!useDotween)
        {
            fadeImage.DOFade(1, 0).OnComplete(()=>action?.Invoke());
            return;
        }
        
        fadeImage.DOFade(1,dotweenTime)
            .OnComplete(()=>action?.Invoke());
    }

    #endregion
    
   

    
    
}
