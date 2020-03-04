using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class WinnerController : MonoBehaviour
{
    public List<GameObject> WinnerTrigger;
    public List<GameObject> InputFieldName;
    public string WinnerName;

    public Text WinnerNameText;
    public bool hadWinner = false;
    public GameObject ButtonRestart;
    public GameObject PanelWinner;
    public List<string> TemporaryNames;

    public GameObject WinnerClosed;
    void Start()
    {

    } 
    public void TestAddNames()
    {
        for (int i = 0; i < WinnerTrigger.Count; i++)
        {
            InputFieldName[i].GetComponent<InputField>().text = TemporaryNames[i];
        }
    }
    public void SetTriggerSlots()
    {
        //WinnerTrigger.Clear();
        foreach (GameObject tempTriggerSlots in GameObject.FindGameObjectsWithTag("TriggerSlots"))
        {
            //WinnerTrigger.Add(tempTriggerSlots);
        }
    }

    public void SetInputField()
    {
        //InputFieldName.Clear();
        foreach (GameObject tempInputFieldName in GameObject.FindGameObjectsWithTag("InputFieldName"))
        {
            //InputFieldName.Add(tempInputFieldName);
        }
        RenameTriggerSlots();
    }

    public List<Text> DebugName;
    void RenameTriggerSlots()
    {
        for (int i = 0; i < WinnerTrigger.Count; i++)
        {
            WinnerTrigger[i].name = InputFieldName[i].GetComponent<InputField>().textComponent.text;
            //DebugName[i].text = InputFieldName[i].GetComponent<InputField>().textComponent.text;
        }
    }
    public void GetSetWinnerController()
    {
        var tempBall = GameObject.FindGameObjectWithTag("Ball");
        Collider2D collider = tempBall.GetComponent<Collider2D>();
        if (collider.gameObject != gameObject)
        {
            ColliderBridge cb = collider.gameObject.AddComponent<ColliderBridge>();
            cb.Initialize(this);
        }
    }

    //winner
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("TriggerSlots"))
        {
            WinnerClosed.SetActive(true);
            hadWinner = true;
            WinnerName = col.gameObject.name;
            WinnerNameText.text = "Congratulations " + WinnerName + "!";
            PanelWinner.SetActive(true);
            ButtonRestart.SetActive(true);
        }
    }

    public class ColliderBridge : MonoBehaviour
    {
        WinnerController _listener;
        public void Initialize(WinnerController l)
        {
            _listener = l;
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            _listener.OnTriggerEnter2D(other);
        }
    }
}
