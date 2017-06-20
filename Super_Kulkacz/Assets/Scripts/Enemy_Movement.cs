using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    // deklaracje:  m. in. wiele tablic; zachowaie każdego z dodanych potworków traktowane jest osobno, żeby można było np. pobierać poszczególne pozycje każdego z nich
    private Rigidbody[] rbod;
    public GameObject[] enemies;
    public static int tab_length;  // długość każdej tablicy, równa ilości wprowadzonych w grze potworków
    public int speed;              // prędkość potworka
    float timeOfStep;              // czas kroku aktualizacyjnego funkcji odpalanej przez InvokeRepeating
    public float[] positioningsX;  // pozycjonowania: współczynniki korekcji (pozycjonowanie do określonych pól; co kratkę, przydatne gdy timeScale = 1 a potworki są szybkie --
    public float[] positioningsY;  // -- wówczas strasznie duża bezwładność: potworki wtedy i tak wylądują w odpowiednich kratkach. Mimo, że timeScale = 3, a potworki wolne, --
                                                                                                                                                 // -- zabezpieczenia zostają (ważne)
    public int[] rands;        // tablice losowych liczb, pomagające w określaniu kierunków poruszania się potworków
    private bool[] isCollision;  // do określania czy będzie po danym ruchu kolizja ze ścianą 

    public List<int>[] nbToSelectsTab;  // tablica list z oznaczeniami kierunków (określanych z pomocą randomów), które dany potworek ma brać pod uwagę -- 
                                                                                           // -- (kierunki bezpośrednio poprzednich kolizji będą ignorowane)
    Vector3[] pozStartTurn;  // pozycje potworków na początku ich ruchu 
    RaycastHit[] hitInf;     // element wykorzystany później do przewidywania kolizji potworków

    // określenie dugości tablic, przypisania początkowych wartości
    void Start()
    {
        speed = 1;    // 1, co przy timeScalu = 3 anihiluje zbędną inercję, co wiązałoby się z traceniem strategicznych dla potworków pozycji
        tab_length = GameObject.FindGameObjectsWithTag("Enemy").Length;
        rbod = new Rigidbody[tab_length];
        isCollision = new bool[tab_length];
        enemies = new GameObject[tab_length];
        positioningsX = new float[tab_length];                 /* określenie długości wszystkich tablic */
        positioningsY = new float[tab_length];
        rands = new int[tab_length];
        nbToSelectsTab = new List<int>[tab_length];
        pozStartTurn = new Vector3[tab_length];
        hitInf = new RaycastHit[tab_length];

        for (int i = 1; i <= tab_length; i++)
        {
            string nameText = "r" + i;
            enemies[i - 1] = GameObject.Find(nameText);   // każdy potworek identyfikowany jest po nazwie
            rbod[i-1] = enemies[i-1].GetComponent<Rigidbody>();
            isCollision[i-1] = false;
            nbToSelectsTab[i - 1] = new List<int>(new int[] { 1, 2, 3, 4 });
        }

        timeOfStep = 1f / speed;  // czas kroku aktualizacji InvokeRepeating, równy czasowi potrzebnemu na przejście przez potworka 1 całego kroku (wyszło, --
        InvokeRepeating("LaunchRepeat", 0f, timeOfStep); // aktualizuje zawartość LaunchRepeat co timeStep.                          // -- że nie należy modyfikować timeScalem)
    }

    public void LaunchRepeat()
    {
        for (int i = 1; i <= tab_length; i++)
        {
            for(int c = 0; c < 1; c++)
            {
                // jeśli potworek wykonał ruch (przesunął się o pole); nie jest on zablokowany - wszystkie kierunki będą znów do wyboru w następnym ruchu
                if (enemies[i - 1].transform.position.x > pozStartTurn[i - 1].x || enemies[i - 1].transform.position.x < pozStartTurn[i - 1].x ||
                   enemies[i - 1].transform.position.y > pozStartTurn[i - 1].y || enemies[i - 1].transform.position.y < pozStartTurn[i - 1].y)
                {
                    nbToSelectsTab[i - 1].Clear();

                    for (int j = 1; j <= 4; j++)
                    {
                        nbToSelectsTab[i - 1].Add(j);
                    }
                }

                pozStartTurn[i - 1].x = enemies[i - 1].transform.position.x;  // pobranie pozycji potworków na początku ich ruchu
                pozStartTurn[i - 1].y = enemies[i - 1].transform.position.y;

                // losowe liczby kierunkowe dla potworków. Użyto listy, żeby można było dowolnie dodawać i usuwać poszczególne liczby, możliwe do wylosowaia przez random
                rands[i - 1] = nbToSelectsTab[i - 1][Random.Range(0, nbToSelectsTab[i - 1].Count)];

                // ważne: przewidzenie, czy nastąpiłaby kolizja, gdyby dany potworek przesunął się w kierunku, określonym już przez random
                if(rands[i-1] == 1)
                {
                    if (rbod[i-1].SweepTest(Vector3.left, out hitInf[i - 1], 1f, QueryTriggerInteraction.Ignore))
                    {
                        isCollision[i - 1] = true;
                    }
                }
                else if (rands[i - 1] == 2)
                {
                    if (rbod[i-1].SweepTest(Vector3.right, out hitInf[i - 1], 1f, QueryTriggerInteraction.Ignore))
                    {
                        isCollision[i - 1] = true;
                    }
                }
                else if (rands[i - 1] == 3)
                {
                    if (rbod[i-1].SweepTest(Vector3.down, out hitInf[i - 1], 1f, QueryTriggerInteraction.Ignore))
                    {
                        isCollision[i - 1] = true;
                    }
                }
                else if (rands[i - 1] == 4)
                {
                    if (rbod[i-1].SweepTest(Vector3.up, out hitInf[i - 1], 1f, QueryTriggerInteraction.Ignore))
                    {
                        isCollision[i - 1] = true;
                    }
                }

                // ustalenie kierunków ruchu potworków, z daną prędkością, na podstawie wybranej randomowo liczby, w przypadku przewidzenia braku kolizji;
                if (isCollision[i - 1] == false)
                {
                    if (rands[i - 1] == 1)
                    {
                        rbod[i-1].velocity = new Vector3(-speed, 0, 0);
                    }
                    else if (rands[i - 1] == 2)
                    {
                        rbod[i-1].velocity = new Vector3(speed, 0, 0);
                    }
                    else if (rands[i - 1] == 3)
                    {
                        rbod[i-1].velocity = new Vector3(0, -speed, 0);
                    }
                    else if (rands[i - 1] == 4)
                    {
                        rbod[i-1].velocity = new Vector3(0, speed, 0);
                    }
                }

                // obliczenia zabezpieczające (przy timeScalu = 3 efekt w zasadzie niewidoczny), żeby ruchy odbywały się co kratkę; bez stanów pośrednich (uwaga: wyjątek połówkowy --
                positioningsX[i - 1] = (Mathf.Round(enemies[i - 1].transform.position.x * 2f)) / 2f;                                                        // -- opracowany później)
                positioningsY[i - 1] = ((Mathf.Round(enemies[i - 1].transform.position.y * 2f)) / 2f) + 0.12f;

                // opracowanie wyjątków połówkowych: żeby potworki, przesuwające się co kratkę, nie wylądowały czasem w pozycjach w połowie kratek, chodząc później po tych połowach --
                if (positioningsX[i - 1] % 1f >= -0.05f && positioningsX[i - 1] % 1f <= 0.05f)      // -- przy timeScale = 3  i speed = 1 w zasadzie niewidoczne, ale zabezpieczenie zostało
                {                                                                                    // To wszystko, aby mieć ZAWSZE pewność, że potworek trafi w każdy korytarz idealnie
                    if (rands[i - 1] == 1)
                    {
                        positioningsX[i - 1] -= 0.5f;
                    }
                    else if (rands[i - 1] == 2)
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

                enemies[i - 1].transform.position = new Vector3(positioningsX[i - 1], positioningsY[i - 1], 0f);   // ostateczne ustalenie skorygowanych pozycji potworków
                
                // w przypadku przewidzenia kolizji w danym kierunku, kierunek ten jest usuwany z listy, współczynnik kolizyjny zmieniony na false,  --
                if (isCollision[i - 1] == true)                          // -- następnie od nowa wybierany będzie kierunek; z tym, że już inny
                {
                    if(nbToSelectsTab[i-1].Contains(rands[i-1]))
                    {
                        nbToSelectsTab[i - 1].Remove(rands[i - 1]);
                    }
                    c--;
                    isCollision[i - 1] = false;
                }
            }
        }
    }

    // ignorowanie kolizji międzypotworkowych
    void OnCollisionEnter(Collision collid)
    {
        if (collid.gameObject.tag == "Enemy")
        {
            Physics.IgnoreCollision(collid.collider, GetComponent<Collider>());
        }
    }
}