using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    //deklaracje: dane dot. m. in. gracza, jego parametrów ruchu, zmienna statyczna dot. szybkości odtwarzania gry
    public static Rigidbody rb;
    public float x;
    public float y;
    public GameObject player;
    public static bool blockMovement;  // jeśli true; gracz nie może się już poruszać
    public static float timeSpeed = 3f;  // czas w grze leci 3x szybciej; przy obniżeniu prędkości ruchu x3 - mniejsza bezwładność potworków - dobre trzymanie przez nie wymaganych pozycji

    // przypasanie początkowych wartości
    void Start () {
        rb = GetComponent<Rigidbody>();
        blockMovement = false;
        Time.timeScale = timeSpeed;
    }

    // ignorowana jest kolizja gracza z miejscami występowania teleportów. Dzięki temu gracz może z nich korzystać; bez obawy, że wejdą tam potworki
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "tel_defence")
        {
            Physics.IgnoreCollision(coll.collider, GetComponent<Collider>());
        }
    }

    void FixedUpdate () {

        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);  // pilnowanie, by zawsze współrzędna 'z' była równa 0. Powód: --
                                         // -- podczas grania, 'z' nieznacznie się zwiększało przy ocieraniu gracza o ścianki. Bardzo bardzo nieznacznie, ale zabezpieczenie zrobione

        // jeśli true - zablokowanie możliwości ruchu gracza - ważne po teleportacji, żeby gracz nie wskoczył szybkim pędem w potworka, który może stać przy tym drugim teleporcie
        if (blockMovement == false)
        {
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
            rb.velocity = new Vector3(x * 1, y * 1, 0);
        }

        // obsługa teleportacji gracza z teleportu 1 do teleportu 2 oraz na odwrót
        if (player.transform.position.x <= -12.59f && player.transform.position.y <= 12.5f && player.transform.position.y >= 11.5f)
        {
            player.transform.position = new Vector3(12.5f, 11, 0);
            StartCoroutine(teleportFreezeEnumerator());  // start korutyny zatrzymującej i blokującej gracza na małą chwilę
        }

        if (player.transform.position.x >= 12.59f && player.transform.position.y <= 11.5f && player.transform.position.y >= 10.5f)
        {
            player.transform.position = new Vector3(-12.5f, 12, 0);
            StartCoroutine(teleportFreezeEnumerator());
        }
    }

    // korutyna zatrzymująca i blokująca gracza na małą chwilę
    public IEnumerator teleportFreezeEnumerator()
    {
        blockMovement = true;
        rb.velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.8f * Movement.timeSpeed);
        blockMovement = false;
    }
}