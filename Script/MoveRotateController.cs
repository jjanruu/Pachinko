using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRotateController : MonoBehaviour
{
    public float RotationSpeed;
    float DefaultRotationSpeed;
    public float MoveSpeed;
    public bool isClockwise;
    public bool isMoving;

    public bool isMoveToRight;
    GameObject GameMaster;
    SpawnBallController SpawnBallControllerScript;
    WinnerController WinnerControllerScript;
    void Start()
    {
        DefaultRotationSpeed = RotationSpeed;
        GameMaster = GameObject.Find("GameMaster");
        SpawnBallControllerScript = GameMaster.GetComponent<SpawnBallController>();
        WinnerControllerScript = GameMaster.GetComponent<WinnerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnBallControllerScript.isAtBlackhole == false)
        {
            RotateSprite();
            MoveSprite();
        }
    }

    void RotateSprite()
    {
        if (isMoving == false)
        {
            if(RotationSpeed != DefaultRotationSpeed)
            {
                RotationSpeed = DefaultRotationSpeed;
            }

            if (isClockwise)
            {
                transform.Rotate(Vector3.forward * RotationSpeed);
            }
            else
            {
                transform.Rotate(-Vector3.forward * RotationSpeed);
            }
        }
    }

    void MoveSprite()
    {
            if (isMoving == true)
            {
                if (isMoveToRight == true)
                {
                    transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
                }
                else
                {
                    transform.Translate(-MoveSpeed * Time.deltaTime, 0, 0);
                }
            }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("TriggerLeft"))
        {
            isMoveToRight = true;
        }
        else if (col.gameObject.CompareTag("TriggerRight"))
        {
            isMoveToRight = false;
        }
    }
}
