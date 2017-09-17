using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public Text messageTxt;
    public Button replayBtn;
    
    public void SetMessage(string msg)
    {
        messageTxt.text = msg;
    }

    public void SetReplayBtnVisibility(bool visibility)
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.name == "Replay")
            {
                child.gameObject.SetActive(visibility);
                return;
            }
        }

        Debug.Log("Cound not find");
    }
}
