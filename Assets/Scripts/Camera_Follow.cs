using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour {

    public GameObject Player;
    private Vector3 offset_Dist;

    // constraints - top, right, bottom, left
    private float[] camera_Constraints = {21.5f, 4.1f, 4.5f, -4.1f};
    
	// Use this for initialization
	void Start () {
        offset_Dist = transform.position - Player.transform.position;
	}
	
	// LateUpdate is called once per frame after all from Update
	void LateUpdate () {
        transform.position = Player.transform.position + offset_Dist;

        //main directions handling
        if(Player.transform.position.y >= camera_Constraints[0])
        {
            transform.position = new Vector3(Player.transform.position.x, camera_Constraints[0], Player.transform.position.z) + offset_Dist;
        }

        if (Player.transform.position.x >= camera_Constraints[1])
        {
            transform.position = new Vector3(camera_Constraints[1], Player.transform.position.y, Player.transform.position.z) + offset_Dist;
        }

        if (Player.transform.position.y <= camera_Constraints[2])
        {
            transform.position = new Vector3(Player.transform.position.x, camera_Constraints[2], Player.transform.position.z) + offset_Dist;
        }

        if (Player.transform.position.x <= camera_Constraints[3])
        {
            transform.position = new Vector3(camera_Constraints[3], Player.transform.position.y, Player.transform.position.z) + offset_Dist;
        }

        //corners handling
        if (Player.transform.position.y >= camera_Constraints[0] && Player.transform.position.x <= camera_Constraints[3])
        {
            transform.position = new Vector3(camera_Constraints[3], camera_Constraints[0], Player.transform.position.z) + offset_Dist;
        }

        if (Player.transform.position.y >= camera_Constraints[0] && Player.transform.position.x >= camera_Constraints[1])
        {
            transform.position = new Vector3(camera_Constraints[1], camera_Constraints[0], Player.transform.position.z) + offset_Dist;
        }

        if (Player.transform.position.y <= camera_Constraints[2] && Player.transform.position.x >= camera_Constraints[1])
        {
            transform.position = new Vector3(camera_Constraints[1], camera_Constraints[2], Player.transform.position.z) + offset_Dist;
        }

        if (Player.transform.position.y <= camera_Constraints[2] && Player.transform.position.x <= camera_Constraints[3])
        {
            transform.position = new Vector3(camera_Constraints[3], camera_Constraints[2], Player.transform.position.z) + offset_Dist;
        }


    }
}
