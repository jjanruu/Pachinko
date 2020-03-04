using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwitchPhaseController : MonoBehaviour
{
    public List<GameObject> ToPhase2;
    public Dropdown myDropdown;
    public GameObject GameMaster;
    RestartGameController RestartGameControllerScript;

    public List<Text> ListOfNumbers1;
    public List<Text> ListOfNumbers2;

    public List<RawImage> ListOfRawImage;
    void Start()
    {
        myDropdown.onValueChanged.AddListener(delegate {
            myDropdownValueChangedHandler(myDropdown);
        });

        GameMaster = GameObject.Find("GameMaster");
        RestartGameControllerScript = GameMaster.GetComponent<RestartGameController>();
    }

    private void myDropdownValueChangedHandler(Dropdown target)
    {
        ClearRawImage();
        gameObject.transform.GetChild(0).GetComponent<Text>().text = myDropdown.captionText.text;
        ChangeListOfNumbers();
        RestartGameControllerScript.RestartGame();
        NewPhase();
        Debug.Log("selected: " + target.value);
    }

    
    public void SetDropdownIndex(int index)
    {
        myDropdown.value = index;
  
    }

    void ClearRawImage()
    {
        for (int i = 0; i < ListOfRawImage.Count; i++)
        {
            ListOfRawImage[i].texture = null;
            ListOfRawImage[i].color = new Color(255f, 255f, 255f, 0);
        }
    }
    void NewPhase()
    {
        for (int i = 0; i < ToPhase2.Count; i++)
        {
            var TempActive = ToPhase2[i].activeInHierarchy;
            ToPhase2[i].SetActive(!TempActive);
        }
    }

    void ChangeListOfNumbers()
    {
        if(myDropdown.value == 0)
        {
            for (int i = 0; i < ListOfNumbers1.Count; i++)
            {
                var NumbersValue = i + 1;
                ListOfNumbers1[i].text = NumbersValue.ToString();
            }
        }
        else
        {
            for (int i = 0; i < ListOfNumbers2.Count; i++)
            {
                var NumbersValue = i + 1;
                ListOfNumbers2[i].text = NumbersValue.ToString();
            }
        }
    }

}
