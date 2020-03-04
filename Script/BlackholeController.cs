using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeController : MonoBehaviour
{
    public List<GameObject> Blackhole;
    public List<Vector2> BlackholePosition;
    public float speed = 1.0f;
    Animator ChumAnimator;
    GameObject GameMaster;
    SpawnBallController SpawnBallControllerScript;

    public float Timer;
    float DefaultTimer;
    public bool isTimerSimulated;
    public bool isTimerCollider;
  

    void Start()
    {
        DefaultTimer = Timer;
        SetBlackhole();
        GetBlackholePosition();
        ChumAnimator = gameObject.GetComponent<Animator>();
        GameMaster = GameObject.Find("GameMaster");
        SpawnBallControllerScript = GameMaster.GetComponent<SpawnBallController>();
    }

    void SetBlackhole()
    {
        foreach (GameObject tempBlackHole in GameObject.FindGameObjectsWithTag("BlackholePosition"))
        {
            Blackhole.Add(tempBlackHole);
        }
    }
    void GetBlackholePosition()
    {
        
        for (int i = 0; i < Blackhole.Count; i++)
        {
            BlackholePosition.Add(Blackhole[i].transform.position);
        }
    }

    void Update()
    {
        if(isTimerSimulated == true)
        {
            Timer -= Time.deltaTime;
            if(Timer <= 0)
            {
                RemoveRigidbody();
                Timer = DefaultTimer;
            }
        }
        if(isTimerCollider == true)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                RemoveCollider();
                Timer = DefaultTimer;
            }
        }
    }

    float step;
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Blackhole"))
        {
            SpawnBallControllerScript.isAtBlackhole = true;
            ChumAnimator.SetBool("isInBlackHole", true);
            ChumAnimator.enabled = true;
            gameObject.GetComponent<Rigidbody2D>().simulated = false;

            Invoke("DelayNewPosition", 1.25f);


            step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, col.gameObject.transform.position, step);
        }
    }



    void DelayNewPosition()
    {
        ChumAnimator.SetBool("isInBlackHole", false);
        ChumAnimator.SetBool("isInSpawnPoints", true);


        var RandPosition = Random.Range(0, BlackholePosition.Count);
        gameObject.transform.position = BlackholePosition[RandPosition];
        
        Invoke("DelaySimulatedChums", 1.25f);
    }

    void DelaySimulatedChums()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        ChumAnimator.SetBool("isInSpawnPoints", false);
        ChumAnimator.enabled = false;

        Invoke("DelaySimulatedChumsFalse", .25f);
        Invoke("DelaySimulatedChumsTrue", .5f);
    }

    void DelaySimulatedChumsFalse()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
    }
    void DelaySimulatedChumsTrue()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        SpawnBallControllerScript.isAtBlackhole = false;
    }

    void RemoveRigidbody()
    {
        DelaySimulatedChumsFalse();
        Invoke("DelaySimulatedChumsTrue", .25f);
    }


    //debugging chumchum
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("BlackholePosition"))
        {
            isTimerSimulated = true;
        }
        if(col.gameObject.CompareTag("Stars"))
        {
            isTimerCollider = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("BlackholePosition"))
        {
            isTimerSimulated = false;
            Timer = DefaultTimer;
        }
        if (col.gameObject.CompareTag("Stars"))
        {
            isTimerCollider = true;
            Timer = DefaultTimer;
        }
    }

    void RemoveCollider()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        Invoke("DelayActiveCollider", .25f);
    }
    
    void DelayActiveCollider()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        isTimerCollider = false;
    }

}
