using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explosion_Place : MonoBehaviour {

    // deklaracje: najważniejsze - seria obrazków tworzących animację wybuchu. Obrazki - jako obiekty gry (łatwe pozycjonowanie)
    public static GameObject[] imgs;
    private Vector3 playerPos;
    public static int _length;

    // wstawienie kolejnych obrazków do tablicy zbiorczej, dezaktywacja wszystkich obrazków
    private void Start()
    {
        _length = GameObject.FindGameObjectsWithTag("Explosion").Length;
        imgs = new GameObject[_length];

        for(int i = 1; i <= _length; i++)
        {
            string nameText = "i" + i;
            imgs[i-1] = GameObject.Find(nameText);
            imgs[i - 1].SetActive(false);
        }
    }

    // ustawienie, by cała seria obrazków, w trybie niewidocznym, podążała za graczem (trochę śmiesznie, ale niech już tak będzie)
    private void FixedUpdate()
    {
        playerPos = transform.position;

        for (int i = 1; i <= _length; i++)
        {
            imgs[i-1].transform.position = playerPos + new Vector3(0, 0.3f, -10);
        }
    }
}