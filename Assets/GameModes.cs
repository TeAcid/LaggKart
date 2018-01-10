using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModes : MonoBehaviour {

    public bool TimeAttack = false;
    public bool Warzone = false;
    public bool Head2Head = false;
    public bool StandardGame = false;
    public static bool ta = false;
    public static bool wz = false;
    public static bool h2h = false;
    public static bool sg = false;

    public bool GameTimer = true;
    public float cas = 0.0f;

    public float time_left = 300.0f;

    Vector3[] spawn_points = new Vector3[5]; //Spawn points za avte
    Vector3 sp1 = new Vector3(38.592f, 2.5f, 103.6f);
    Vector3 sp2 = new Vector3(38.592f, 2.5f, 116f);
    Vector3 sp3 = new Vector3(38.592f, 2.5f, 86.9f);//osnova
    Vector3 sp4 = new Vector3(47.2f, 2.5f, 116f);
    Vector3 sp5 = new Vector3(47.2f, 2.5f, 103.6f);

    public LifePoints_E lp_enemy;
    public LifePoints_P lp_player;
    public Player_checkpoints chP;

    public Rocket_launcher rl;
    public Powerups pu;

    public GameObject enemy;

    public GameObject powerup;
    public GameObject warzone;

    public Rigidbody rb;

    public bool gameover = false;

    public void Time_attack()
    {
        rl.enabled = false;
        pu.enabled = false;
        for (int i = 0; i < spawn_points.Length; i++)
        {
            Instantiate(enemy.transform, spawn_points[i], Quaternion.identity);
        }
    }

    public void War_zone()
    {
        pu.enabled = false;
        rl.enabled = true;
        //vsak avto ma rocket launcher - en hit je -10 lifepoints, obstajajo life PU (+15), 3 rivali
        //vsak krog +10 LP
    }

    public void H2h()
    {
        rl.enabled = false;
        pu.enabled = true;
        //en rival, standardni PU
    }

    public void Standard_game()
    {
        //5 rivalov, standardni PU, 3 krogi
        for (int i = 0; i < spawn_points.Length; i++)
        {
            Instantiate(enemy.transform, spawn_points[i], Quaternion.identity);
        }

        rl.enabled = false;
        pu.enabled = true;
    }


    // Update is called once per frame
    void Start () {
        gameover = false;
        rl = GetComponent<Rocket_launcher>();
        pu = GetComponent<Powerups>();
        rb = GetComponent<Rigidbody>();

        enemy = GameObject.FindGameObjectWithTag("Enemy");
        powerup = GameObject.FindGameObjectWithTag("Powerup");
        warzone = GameObject.FindGameObjectWithTag("WarZone");

        spawn_points[0] = sp1;
        spawn_points[1] = sp2;
        spawn_points[2] = sp3;
        spawn_points[3] = sp4;
        spawn_points[4] = sp5;

        if (TimeAttack == true)
        {
            powerup.SetActive(false);
            warzone.SetActive(false);
            Time_attack();
            ta = true;
            wz = false;
            sg = false;
            h2h = false;
        }
        else if(Warzone == true)
        {
            powerup.SetActive(false);
            warzone.SetActive(true);
            War_zone();
            ta = false;
            wz = true;
            sg = false;
            h2h = false;
        }
        else if(Head2Head == true)
        {
            powerup.SetActive(true);
            warzone.SetActive(false);
            H2h();
            ta = false;
            wz = false;
            sg = false;
            h2h = true;
        }
        else if(StandardGame == true)
        {
            powerup.SetActive(true);
            warzone.SetActive(false);
            Standard_game();
            ta = false;
            wz = false;
            sg = true;
            h2h = false;
        }
    }


    void OnGUI()
    {
        if (gameover)
        {
            GUI.skin.label.fontSize = 130;
            GUI.color = Color.red;
            GUI.Label(new Rect(480, 220, 550, 550), "Game Over");
        }
    }

    private void Update()
    {
        cas = Player_checkpoints.time;
        //Debug.Log(cas);

        //time_left -= Time.unscaledDeltaTime;

        //Debug.Log(time_left);
        if(time_left < 0.0f)
        {
            Time.timeScale = 0;
            Debug.Log(time_left);
            gameover = true;
            if (time_left < -3.0f)
            {
                gameover = false;
                Time.timeScale = 1;
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
}
