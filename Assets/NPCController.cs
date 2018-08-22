using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

    public int speed = 1;
    public GameObject[] wayPoints;
    private bool isMoving = false;

    public int currentWayPoint = 0;
    private int targetWayPoint = 0;
    private int direction = 1;


	void Start () {
        // Start at the default first position
        if (wayPoints.Length > 0)
        {
            transform.position = wayPoints[currentWayPoint].transform.position;
            UpdateTargetWayPoint();
            isMoving = true;
        } else {
            isMoving = false;
        }
	}
	
	void Update () {

        if(isMoving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[targetWayPoint].transform.position, step);

            var distance = Vector3.Distance(transform.position, wayPoints[targetWayPoint].transform.position);
            if (distance <= Mathf.Epsilon)
            {
                UpdateTargetWayPoint();
            }
        }
	}

    // Walk back along the path
    private void UpdateTargetWayPoint() 
    {
        currentWayPoint = targetWayPoint;

        if(direction > 0) {
            targetWayPoint++;
            if (targetWayPoint >= wayPoints.Length)
            {
                targetWayPoint--;
                direction = -1;
            }
        } else {
            targetWayPoint--;
            if (targetWayPoint < 0)
            {
                targetWayPoint++;
                direction = 1;
            }
        }
    }
}
