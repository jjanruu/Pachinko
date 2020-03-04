using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBallController : MonoBehaviour
{
    public GameObject Ball;
    Vector3 BallSize;
    public GameObject BackgroundFloor;
    private Vector3 MousePosition;
    void Start()
    {
        BallSize = Ball.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "TopFloor")
                {
                    MousePosition = Input.mousePosition;
                    MousePosition.z = 18;

                    MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);
                    GameObject TempBall = Instantiate(Ball, MousePosition, Quaternion.identity) as GameObject;
                    TempBall.transform.SetParent(BackgroundFloor.transform);
                    TempBall.transform.localScale = BallSize;
                }
            }

           
        }
    }

}
