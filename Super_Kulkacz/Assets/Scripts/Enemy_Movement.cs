using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    // deklaracje:  m. in. wiele tablic; zachowaie każdego z dodanych potworków traktowane jest osobno, żeby można było np. pobierać poszczególne pozycje każdego z nich
    private Rigidbody rbod;
    public GameObject[] enemies;
    public static int tab_length;  // długość każdej tablicy, równa ilości wprowadzonych w grze potworków
    public int speed;              // prędkość potworka
    float timeOfStep;              // czas kroku aktualizacyjnego funkcji odpalanej przez InvokeRepeating
    public float[] positioningsX;  // pozycjonowania: współczynniki korekcji (pozycjonowanie do określonych pól; co kratkę, przydatne gdy timeScale = 1 a potworki są szybkie --
    public float[] positioningsY;  // -- wówczas strasznie duża bezwładność: potworki wtedy i tak wylądują w odpowiednich kratkach. Mimo, że timeScale = 3, a potworki wolne, --
                                                                                                                                                 // -- zabezpieczenia zostały
    public int[] rands;        // tablica losowych liczb (1,2,3, lub 4) oznaczających kierunki poruszania się potworków
    private bool[] isCollision;  // czy jest w danej chwili kolizja
    List<int> numbersToSelect;   // lista z oznaczeniami kierunków dla randoma, które potworek ma zignorować po kolizji w danym kierunku (takie podejście miało całkowicie wyrugować --
                                                                                                                    // -- postoje potworków. Aż tak fajnie to nie wyszło, ale jest ok.
    void Start()
    {
        rbod = GetComponent<Rigidbody>();
        speed = 1;    // 1, co przy timeScalu = 3 anihiluje zbędną inercję, co wiązałoby się z traceniem strategicznych dla potworków pozycji
        tab_length = GameObject.FindGameObjectsWithTag("Enemy").Length;
        isCollision = new bool[tab_length];
        enemies = new GameObject[tab_length];
        positioningsX = new float[tab_length];                 /* określenie długości wszystkich tablic */
        positioningsY = new float[tab_length];
        rands = new int[tab_length];
        numbersToSelect = new List<int>(new int[] { 1, 2, 3, 4, 5 });  // jest jeszcze 5, żeby użyty później razem z tym random mógł wylosować 4

        for (int i = 1; i <= tab_length; i++)
        {
            string nameText = "r" + i;
            enemies[i - 1] = GameObject.Find(nameText);   // każdy potworek identyfikowany jest po nazwie
            isCollision[i-1] = false;
        }

        timeOfStep = 1f / speed;  // czas kroku aktualizacji InvokeRepeating, równy czasowi potrzebnemu na przejście przez potworka 1 całego kroku (wyszło, --
        InvokeRepeating("LaunchProjectile", 0f, timeOfStep); // aktualizuje zawartość LaunchProjectile co timeStep.                          // -- że nie należy modyfikować timeScalem)
    }

    public void LaunchProjectile()
    {
        for (int i = 1; i <= tab_length; i++)
        {
            // losowe liczby kierunkowe dla potworków
            rands[i-1] = Random.Range(1, 5);

            // obliczenia jedynie zabezpieczające (przy timeScalu = 3 efekt w zasadzie niewidoczny), żeby ruchy odbywały się co kratkę; bez stanów pośrednich (uwaga: wyjątek połówkowy --
            positioningsX[i - 1] = (Mathf.Round(enemies[i - 1].transform.position.x * 2f)) / 2f;                                                               // -- opracowany później)
            positioningsY[i - 1] = ((Mathf.Round(enemies[i - 1].transform.position.y * 2f)) / 2f) + 0.12f;

            // opracowanie wyjątków połówkowych: żeby potworki, przesuwające się co kratkę, nie wylądowały czasem w pozycjach w połowie kratek, przesuwając się później po tych połowach --
            if (positioningsX[i - 1] % 1f >= -0.05f && positioningsX[i - 1] % 1f <= 0.05f)            // -- przy timeScale = 3  i speed = 1 w zasadzie niewidoczne, ale zabezpieczenie zostało
            {                                                                                         // To wszystko, aby mieć ZAWSZE pewność, że potworek trafi w każdy korytarz idealnie
                if(rands[i-1] == 1)
                {
                    positioningsX[i - 1] -= 0.5f;
                }
                else if(rands[i-1] == 2)
                {
                    positioningsX[i - 1] += 0.5f;
                }
            }
            if ((positioningsY[i - 1] - 0.12f) % 1f >= 0.95f && (positioningsY[i - 1] - 0.12f) % 1f <= 1.05f)
            {
                if (rands[i - 1] == 3)
                {
                    positioningsY[i - 1] -= 0.5f;
                }
                else if (rands[i - 1] == 4)
                {
                    positioningsY[i - 1] += 0.5f;
                }
            }

            enemies[i - 1].transform.position = new Vector3(positioningsX[i - 1], positioningsY[i - 1], 0f);   // ostateczne ustalenie skorygowanej pozycji potworków

            // przy kolizji, potworek zmienia kierunek na inny
            while (isCollision[i-1] == true)   
            {
                /*byk??*/numbersToSelect.Remove(rands[i - 1]);  // przy czym usuwany jest ostatnio wybrany kierunek ruchu

                rands[i - 1] = numbersToSelect[Random.Range(0, numbersToSelect.Count)]; // losujemy inny kierunek ruchu, nie licząc tych, które zaowocowały kolizją

                if (rands[i - 1] == 1)
                {
                    rbod.velocity = new Vector3(-speed, 0, 0);
                }
                else if (rands[i - 1] == 2)                             /* Nadanie prędkości potworków w określonych kierunkach */
                {
                    rbod.velocity = new Vector3(speed, 0, 0);
                }
                else if (rands[i - 1] == 3)
                {
                    rbod.velocity = new Vector3(0, -speed, 0);
                }
                else if (rands[i - 1] == 4)
                {
                    rbod.velocity = new Vector3(0, speed, 0);
                }

                // warunek wyjścia z pętli - zmiana w pozycji potworka
                if (enemies[i - 1].transform.position.x >= positioningsX[i - 1] || enemies[i - 1].transform.position.x <= positioningsX[i - 1] ||
               enemies[i - 1].transform.position.y >= positioningsY[i - 1] || enemies[i - 1].transform.position.y <= positioningsY[i - 1])
                {
                    isCollision[i - 1] = false;
                }    
            }
            
            // ustalenia kierunków ruchu potworków przy braku kolizji; czyli wartość randoma bez ograniczeń - taka jak na początku całej głównej pętli for
            if(isCollision[i-1] == false)
            {
                if (rands[i - 1] == 1)
                {
                    rbod.velocity = new Vector3(-speed, 0, 0);
                }
                else if (rands[i - 1] == 2)
                {
                    rbod.velocity = new Vector3(speed, 0, 0);
                }
                else if (rands[i - 1] == 3)
                {
                    rbod.velocity = new Vector3(0, -speed, 0);
                }
                else if (rands[i - 1] == 4)
                {
                    rbod.velocity = new Vector3(0, speed, 0);
                }
            }

            // jeśli potworek wykonał ruch; nie jest on zablokowany - wszystkie kierunki będą znów do wyboru w następnym ruchu
            if(enemies[i-1].transform.position.x >= positioningsX[i - 1] || enemies[i - 1].transform.position.x <= positioningsX[i - 1] ||
               enemies[i - 1].transform.position.y >= positioningsY[i - 1] || enemies[i - 1].transform.position.y <= positioningsY[i - 1])
            {
                numbersToSelect.Clear();

                for (int j = 1; j <= 5; j++)
                {
                    numbersToSelect.Add(j);
                }
            }
        }
    }

    // ignorowanie kolizji międzypotworkowych, wzięcie pod uwage innych kolizji
    void OnCollisionEnter(Collision collid)
    {
        if (collid.gameObject.tag == "Enemy")
        {
            Physics.IgnoreCollision(collid.collider, GetComponent<Collider>());
        }
        else
        {
            if(gameObject.name == "r1")
            {
                isCollision[0] = true;
            }
            else if (gameObject.name == "r2")
            {
                isCollision[1] = true;
            }
            else if (gameObject.name == "r3")
            {
                isCollision[2] = true;
            }
            else if (gameObject.name == "r4")
            {
                isCollision[3] = true;
            }
        }
    }

    // przy wychodzeniu z kolizji współczynnik kolizyjny = false;
    void OnCollisionExit(Collision collid)
    {
        if (gameObject.name == "r1")
        {
            isCollision[0] = false;
        }
        else if (gameObject.name == "r2")
        {
            isCollision[1] = false;
        }
        else if (gameObject.name == "r3")
        {
            isCollision[2] = false;
        }
        else if (gameObject.name == "r4")
        {
            isCollision[3] = false;
        }
    }
}

// PODSUMOWANIE: potworki poruszają się już sprawnie, lecz postoje wciąż bywają. Jest tu zatem pewne niedociągnięcie.