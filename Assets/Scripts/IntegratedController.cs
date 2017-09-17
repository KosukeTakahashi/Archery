using UnityEngine;

public class IntegratedController : MonoBehaviour {
    public Canvas canvas;

    private UIController uiCtrl;
    private ArrowController arrowCtrl;
    
    void Start()
    {
        arrowCtrl = GetComponent<ArrowController>();
        uiCtrl = canvas.GetComponent<UIController>();
        uiCtrl.SetReplayBtnVisibility(false);
        uiCtrl.replayBtn.onClick.AddListener(() => Replay());
    }

    public void CallbackArrow(int value)
    {
        if (value > 1)
            uiCtrl.SetMessage(string.Format("{0} pts!", value));
        else if (value == 1)
            uiCtrl.SetMessage("1 pt!");
        else if (value == -1)
            uiCtrl.SetMessage("FAIL!");

        uiCtrl.SetReplayBtnVisibility(true);
    }

    public void Replay()
    {
        arrowCtrl.ReInit();
        uiCtrl.SetReplayBtnVisibility(false);
        uiCtrl.SetMessage("");
    }
}
