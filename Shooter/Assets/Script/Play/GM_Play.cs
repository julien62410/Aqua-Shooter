using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM_Play : MonoBehaviour
{
    private Dictionary<System.Type, IEnnemieObject[]> globalListe;

    public SubmarineController submarine;
    public Level[] levels;
    public Text scoreGame;

    //score de la partie
    public int score = 0;

    // Vitesse des elem de jeu
    public float backgroundSpeed = 2f;
    [Header("Speed")]
    [Range(2, 6)]
    public float poulpeAndRockSpeed = 3f;
    [Range(3, 7)]
    public float torpedoSpeed = 5f;
    [Range(2, 5)]
    public float submarineSpeed = 3f;
    [Range(7, 13)]
    public float sharkSpeed = 9f;
    [Range(4, 8)]
    public float inkSpeed = 6f;

    // Position des elem de jeu via Empty Object
    [Header("Position")]
    public Transform StartMapPosX;
    public Transform EndMapPosX;
    public Transform SubmarinePosX;
    public Transform UpPosY;
    public Transform DownPosY;
    // Position des elem de jeu en X - Y
    public float posX_StartMap;
    public float posX_EndMap;
    public float posX_Submarine;
    public float posY_Up;
    public float posY_Down;

    // Ensemble des positions possible pour Pull
    [Header("Pull")]
    public float posUp = 2.4f;
    public float diffInterPos = 0.8f;
    public int nbPos = 7;
    public Dictionary<int, float> convertPosY;

    // Decallage entre lanceur et projectile
    [Header("Shift between shooter and shoot")]
    [Tooltip("Shift beetween torpedo-submarine and poulpe-ink")]
    [Range(0.7f, 1)]
    public float shift = 1f;

    // Temps de la tache d'encre
    [Header("Ink parameters")]
    [Tooltip("Value which is added to Display Ink when the player touch ink")]
    [Range(1, 10)]
    public int timeInk = 3;
    [Tooltip("Time the big ink spot is displayed")]
    public float displayInk = 0;
    [Range(0.1f, 2)]
    public float timeBetweenShoot = 1.5f;
    public float removeIntoTime = 0.01f;
    
    public static GM_Play gm = null;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != this)
            Destroy(gameObject);
    }

    /*
     * Recupère tous les elems du jeu
     */
    void Start()
    {
        Attribute();
        Init();
        GlobalInvoke();
    }

    /*
     * update le score
     */
    void Update()
    {
        score++;
    }

    /*
     * Attribue les valeurs X et Y au champ concerne via les Transform des EmptyObject
     */
     private void Attribute()
    {
        posX_StartMap = StartMapPosX.position.x;
        posX_EndMap = EndMapPosX.position.x;
        posX_Submarine = SubmarinePosX.position.x;
        posY_Up = UpPosY.position.y;
        posY_Down = DownPosY.position.y;

        this.transform.Find("Submarine").transform.position = SubmarinePosX.position;

    }

    /*
     * Récupère l'ensemble des objets
     */
    private void Init()
    {
        this.scoreGame.gameObject.SetActive(false);

        this.transform.Find("InkEcran").gameObject.SetActive(false);

        globalListe = new Dictionary<Type, IEnnemieObject[]>();
        globalListe[typeof(Poulpe)] = this.transform.Find("Ennemies/Poulpes").GetComponentsInChildren<Poulpe>();
        globalListe[typeof(Shark)] = this.transform.Find("Ennemies/Sharks").GetComponentsInChildren<Shark>();
        globalListe[typeof(Rock)] = this.transform.Find("Objects/Rocks").GetComponentsInChildren<Rock>();
        globalListe[typeof(Torpedo)] = this.transform.Find("Projectiles/Torpedos").GetComponentsInChildren<Torpedo>();
        globalListe[typeof(Ink)] = this.transform.Find("Projectiles/Ink").GetComponentsInChildren<Ink>();

        foreach (Poulpe elem in globalListe[typeof(Poulpe)])
            elem.gameObject.SetActive(false);
        foreach (Shark elem in globalListe[typeof(Shark)])
            elem.gameObject.SetActive(false);
        foreach (Rock elem in globalListe[typeof(Rock)])
            elem.gameObject.SetActive(false);
        foreach (Torpedo elem in globalListe[typeof(Torpedo)])
            elem.gameObject.SetActive(false);
        foreach (Ink elem in globalListe[typeof(Ink)])
            elem.gameObject.SetActive(false);

        convertPosY = new Dictionary<int, float>();
        for (int i = 0; i < nbPos; i++)
            convertPosY[i] = posUp - diffInterPos * i;
    }

    /*
     * gènere l'ensemble des invoque en fonction de "level"
     */
    private void GlobalInvoke()
    {
        foreach (Level level in levels)
            foreach (ObjectPos elem in level.objects)
                if (elem.type == Object.Shark)
                StartCoroutine(PullObject<Shark>(elem.posX, posX_EndMap, convertPosY[(int)elem.posY - 1]));
            else if (elem.type == Object.Poulpe)
                StartCoroutine(PullObject<Poulpe>(elem.posX, posX_EndMap, convertPosY[(int)elem.posY - 1]));
            else if (elem.type == Object.Rock)
                StartCoroutine(PullObject<Rock>(elem.posX, posX_EndMap, convertPosY[(int)elem.posY - 1]));
    }

    /*
     * Pull un object T
     */
    public IEnumerator PullObject<T>(int delayTime,float posX, float posY) where T : IEnnemieObject
    {
        yield return new WaitForSeconds(delayTime);
        
        foreach (T elem in globalListe[typeof(T)])
        {
            MonoBehaviour go = elem as MonoBehaviour;
            if (!elem.isinUse())
            {
                go.transform.position = new Vector3(posX, posY, 0);
                go.gameObject.SetActive(true);
                elem.setinUse(true);
                yield break;
            }
        }
    }

    /*
     * Affiche une grosse tache a l'ecran
     */
    public void InkEcran()
    {
        this.transform.Find("InkEcran").gameObject.SetActive(true);
        displayInk += timeInk;
    }

    /*
     * Perdu
     */
    public void Lose()
    {
        GM_Start.gm.ConnectWrite();
        StartCoroutine(DisplayScore());
    }

    private IEnumerator DisplayScore()
    {
        Time.timeScale = 0.01f;

        scoreGame.text += score;
        scoreGame.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        scoreGame.text = "Score : ";
        scoreGame.gameObject.SetActive(false);

        Time.timeScale = 1f;

        SceneManager.LoadScene("Start");
    }
}
