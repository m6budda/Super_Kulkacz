using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{

    private Rigidbody rbod;
    public GameObject enemy;
    public int n;
    public int speed;
    public int lastDirIndex;
    float timeOfStep;




    // Use this for initialization
    void Start()
    {
        rbod = GetComponent<Rigidbody>();
        n = 0;
        speed = 1;
        lastDirIndex = 0;
        timeOfStep = 1f / speed;
        InvokeRepeating("LaunchProjectile", 0f, timeOfStep);
    }




    // Update is called once per frame
    public void LaunchProjectile()
    {

        int rand = Random.Range(1, 5);

        if (rand == 1 && lastDirIndex != 2)
        {
            rbod.velocity = new Vector3(-speed, 0, 0);
            lastDirIndex = 1;

        }
        else if (rand == 2 && lastDirIndex != 1)
        {
            rbod.velocity = new Vector3(speed, 0, 0);
            lastDirIndex = 2;
        }
        else if (rand == 3 && lastDirIndex != 4)
        {
            rbod.velocity = new Vector3(0, -speed, 0);
            lastDirIndex = 3;
        }
        else if (rand == 4 && lastDirIndex != 3)
        {
            rbod.velocity = new Vector3(0, speed, 0);
            lastDirIndex = 4;
        }
        n++;
        //Debug.Log(Time.fixedTime + ", " + n);








    }

    void OnCollisionEnter(Collision collid)
    {

    }


}
