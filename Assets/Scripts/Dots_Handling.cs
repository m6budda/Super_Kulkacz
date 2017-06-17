using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dots_Handling : MonoBehaviour {

    public Text pointsText;
    public int points;
    public AudioClip collectSound;
    private AudioSource src;

    void Awake()
    {
        src = GetComponent<AudioSource>();

    }

    void OnTriggerEnter(Collider collid)
    {
        if (collid.gameObject.tag == "dot")
        {
            src.PlayOneShot(collectSound, 1f);
            Destroy(collid.gameObject);
            points++;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("You_Lost");
        }
    }

    void Update()
    {
        pointsText.text = "Points: " + points;

        if(GameObject.FindGameObjectsWithTag("dot").Length == 0)
        {
            SceneManager.LoadScene("You_Won");
        }
    }

}
