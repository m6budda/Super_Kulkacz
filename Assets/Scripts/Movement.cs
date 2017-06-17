using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private Rigidbody rb;
    public float x;
    public float y;
    public GameObject player;
    public bool blockMovement = false;
    public GameObject teleportDefence1;
    public GameObject teleportDefence2;
    public Collider telDefence1;
    public Collider telDefence2;
    public Vector3 plPosTeleportHelp1;
    public Vector3 plPosTeleportHelp2;
    public Vector3 plPos;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        telDefence1 = teleportDefence1.GetComponent<Collider>();
        telDefence2 = teleportDefence2.GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.name == "Teleport_Defence")
        {
            plPosTeleportHelp1 = player.transform.position;
            telDefence1.enabled = false;
        }
        if (coll.gameObject.name == "Teleport_Defence2")
        {
            plPosTeleportHelp2 = player.transform.position;
            telDefence2.enabled = false;
        }
        
    }

    // FixedUpdate - recommended while working with Rigidbody
    void FixedUpdate () {

        Debug.Log(telDefence1.enabled +", "+ telDefence2.enabled+", "+plPos+", "+plPosTeleportHelp1);
        plPos = player.transform.position;
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);

        if (blockMovement == false)
        {
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
            rb.velocity = new Vector3(x * 3, y * 3, 0);
        }
        
        
        if ((Mathf.Abs(plPos.x - plPosTeleportHelp1.x) > 0.9 || 
            Mathf.Abs(plPos.y - plPosTeleportHelp1.y) > 0.9))
        {
            telDefence1.enabled = true;
        }
        if ((Mathf.Abs(plPos.x - plPosTeleportHelp2.x) > 0.9 ||
            Mathf.Abs(plPos.y - plPosTeleportHelp2.y) > 0.9))
        {
            telDefence2.enabled = true;
        }



        if (player.transform.position.x <= -12.59f && player.transform.position.y <= 12.5f && player.transform.position.y >= 11.5f)
        {
            telDefence2.enabled = false;
            player.transform.position = new Vector3(12.5f, 11, 0);
            StartCoroutine(teleportFreezeEnumerator());
        }

        if (player.transform.position.x >= 12.59f && player.transform.position.y <= 11.5f && player.transform.position.y >= 10.5f)
        {
            telDefence1.enabled = false;
            player.transform.position = new Vector3(-12.5f, 12, 0);
            StartCoroutine(teleportFreezeEnumerator());
        }
    }

    public IEnumerator teleportFreezeEnumerator()
    {
        blockMovement = true;
        rb.velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.8f);
        blockMovement = false;
    }

}
