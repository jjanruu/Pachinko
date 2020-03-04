using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartGameController : MonoBehaviour
{
    SpawnBallController SpawnBallControllerScript;
    WinnerController WinnerControllerScript;
    public List<GameObject> PrizeContainer;

    void Start()
    {
        SpawnBallControllerScript = gameObject.GetComponent<SpawnBallController>();
        WinnerControllerScript = gameObject.GetComponent<WinnerController>();
    }
    
    public void ResetPrizeContainer()
    {
        for (int i = 0; i < PrizeContainer.Count; i++)
        {
            PrizeContainer[i].GetComponent<RawImage>().color = new Color(255f, 255f, 255f, 0);
        }
    }
    public void RestartGame()
    {
        var tempIsTesting = this.SpawnBallControllerScript.isForTesting;
        ClearInputFieldName();
        var ClassWinnerController = WinnerControllerScript;
        ClassWinnerController.WinnerClosed.SetActive(false);
        ClassWinnerController.WinnerName = "";
        ClassWinnerController.WinnerNameText.text = "";
        ClassWinnerController.hadWinner = false;
        ClassWinnerController.ButtonRestart.SetActive(false);

        SpawnBallControllerScript.isBallInstantiated = false;
        SpawnBallControllerScript.isReady = false;
        DestroyBall();
        WinnerControllerScript.PanelWinner.SetActive(false);
    }

    public void ClearInputFieldName()
    {
        var tempIsTesting = SpawnBallControllerScript.isForTesting;
        if (tempIsTesting == false)
        {
            for (int i = 0; i < WinnerControllerScript.InputFieldName.Count; i++)
            {
                WinnerControllerScript.InputFieldName[i].GetComponent<InputField>().text = "";
            }
        }
    }
    public void DestroyBall()
    {

        var ball = GameObject.FindGameObjectWithTag("Ball");
        Destroy(ball);
    }
}
