using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawnBallController : MonoBehaviour
{
    public GameObject Ball;
    Vector3 BallSize;
    public GameObject BackgroundFloor;
    private Vector3 MousePosition;
    private GameObject BallHandler;
    public Text DebugText;

    public bool isForTesting;
    public bool isBallInstantiated = false;
    public bool isAtBlackhole = false;

    public GameObject Phase2;
    SwitchPhaseController SwitchPhaseControllerScript;
    void Start()
    {
        BallSize = Ball.transform.localScale;
        SwitchPhaseControllerScript = Phase2.GetComponent<SwitchPhaseController>();
    }

    // Update is called once per frame
    void Update()
    {
        //AtInstantiateArea();
    }


    public void AtInstantiateArea()
    {
        bool hadWinner = gameObject.GetComponent<WinnerController>().hadWinner;

        MousePosition = Input.mousePosition;
        MousePosition.z = 10;
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);

        RaycastHit2D hit = Physics2D.Raycast(MousePosition, Vector2.zero);
        if (hit.collider != null)
        {
            CheckFieldName();
            if (isReady == true && isBallInstantiated == false && hadWinner == false)
            {
                if(SwitchPhaseControllerScript.myDropdown.value == 0)
                {
                    if (countReady == 10)
                    {
                        BallHandler = Instantiate(Ball, MousePosition, Quaternion.identity) as GameObject;
                        gameObject.GetComponent<WinnerController>().GetSetWinnerController();
                        isBallInstantiated = true;
                    }
                }
                else
                {
                    if (countReady == 6 || countReady == 10)
                    {
                        BallHandler = Instantiate(Ball, MousePosition, Quaternion.identity) as GameObject;
                        gameObject.GetComponent<WinnerController>().GetSetWinnerController();
                        isBallInstantiated = true;
                    }
                }
               
            }

            else if(isForTesting == true && isBallInstantiated == false)
            {
                BallHandler = Instantiate(Ball, MousePosition, Quaternion.identity) as GameObject;
                gameObject.GetComponent<WinnerController>().GetSetWinnerController();
                isBallInstantiated = true;
            }
        }
    }

    public bool isReady = false;
    public int countReady = 0;
    void CheckFieldName()
    {
        gameObject.GetComponent<WinnerController>().SetTriggerSlots();
        gameObject.GetComponent<WinnerController>().SetInputField();
        countReady = 0;
        var tempInputNames = gameObject.GetComponent<WinnerController>().InputFieldName;
        for (int i = 0; i < tempInputNames.Count; i++)
        {
            var tempInputFieldName = tempInputNames[i].GetComponent<InputField>().textComponent;
            if (tempInputFieldName.text == "")
            {

            }
            else
            {
                isReady = true;
                countReady++;
            }
        }
    }

    public void SwitchTesting()
    {
        isForTesting = !isForTesting;
        gameObject.GetComponent<WinnerController>().TestAddNames();
        if(isForTesting == true)
        {

        }
        else
        {
            gameObject.GetComponent<RestartGameController>().ClearInputFieldName();
            gameObject.GetComponent<RestartGameController>().ResetPrizeContainer();
        }
    }


}
