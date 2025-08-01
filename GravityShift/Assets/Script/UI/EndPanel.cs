using UnityEngine;

public class EndPanel : Panel
{
    [SerializeField] private GameObject winTxt, loseTxt;
    public void Show(bool isWin)
    {
        SetPosition(PanelStates.Show,true);
        winTxt.SetActive(isWin);
        loseTxt.SetActive(!isWin);
    }
}
