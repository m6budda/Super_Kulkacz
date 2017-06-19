using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour {

    // deklaracje: obiekt gracza i zdefiniowana później odległość między graczem a kamerą
    public GameObject Player;
    private Vector3 offset_Dist;

    // ograniczenia pozycji kamery; kolejno - top, right, bottom, left. Żeby kamera nie przekroczyła granic mapy (określone później)
    private float[] camera_Constraints = {21.5f, 4.1f, 4.5f, -4.1f};
    
	// Use this for initialization
	void Start () {
        offset_Dist = transform.position - Player.transform.position;  // definicja odległości między kamerą a graczem
	}
	
	void Update () {
        transform.position = Player.transform.position + offset_Dist; // podążanie kamery za graczem

        // określenie ograniczeń, żeby kamera nie przekroczyła granic mapy - kierunki główne
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

        // określenie ograniczeń, żeby kamera nie przekroczyła granic mapy - narożniki (inaczej kamera by wariowała)
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
