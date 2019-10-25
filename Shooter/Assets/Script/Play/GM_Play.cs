using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM_Play : MonoBehaviour
{
    private Dictionary<System.Type, IEnnemieObject[]> globalListe;
    private Text scoreGame;
    private Text TextQuests;

    public SubmarineController submarine;
    public List<Level> levels;

    [Header("Quests")]
    public Quests quests;
    public int poulpekKill = 0;
    public int sharkKill = 0;
    private int actualQuests = -1;
    private bool firstLoad = true;

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
    
    // Timer pour les levels et les quests
    private int actualLevel = -1;
    private int timeBeforeQuests = 20;
    private int timeAfficheQuests = 4;
    private float timer = -4;

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
    }

    /*
     * update le score
     */
    void Update()
    {
        score++;
        
        timer += Time.deltaTime;
        if ((timer > timeBeforeQuests && timer < timeBeforeQuests + timeAfficheQuests) || timer < 0)
        {
            this.transform.Find("InkEcran").gameObject.SetActive(false);
            displayInk = 0;
            AfficheQuests();
        }

        else if ((timer > timeBeforeQuests + timeAfficheQuests) || firstLoad)
        {
            firstLoad = false;
            TextQuests.gameObject.SetActive(false);
            actualLevel = (++actualLevel) % levels.Count == 0 ? 0 : actualLevel;
            GlobalInvoke(actualLevel);
            timer = 0;
        }
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
        scoreGame = this.transform.Find("Canvas/Score").GetComponentInChildren<Text>();
        TextQuests = this.transform.Find("Canvas/Quests").GetComponentInChildren<Text>();
        scoreGame.gameObject.SetActive(false);

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
    private void GlobalInvoke(int ind)
    {
        foreach (ObjectPos elem in levels[ind].objects)
            if (elem.type == global::myGameObject.Shark)
                StartCoroutine(PullObject<Shark>(elem.posX, posX_EndMap, convertPosY[(int)elem.posY - 1]));
            else if (elem.type == global::myGameObject.Poulpe)
                StartCoroutine(PullObject<Poulpe>(elem.posX, posX_EndMap, convertPosY[(int)elem.posY - 1]));
            else if (elem.type == global::myGameObject.Rock)
                StartCoroutine(PullObject<Rock>(elem.posX, posX_EndMap, convertPosY[(int)elem.posY - 1]));

    }

    /*
     * Pull un object T
     */
    public IEnumerator PullObject<T>(int delayTime,float posX, float posY) where T : IEnnemieObject
    {
        if (delayTime == 0) yield return new WaitForSeconds(delayTime);
        else yield return new WaitForSeconds(delayTime - (0.7f * (delayTime - 1)));
        
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

    /*
     * Affiche les quetes
     */
    private void AfficheQuests()
    {
        TextQuests.text = "";
        TextQuests.gameObject.SetActive(true);

        if (actualQuests == -1 || ConditionComplete())
            actualQuests = (UnityEngine.Random.Range(1, quests.objects.Count) - 1);

        TextQuests.text = quests.objects[actualQuests].description +
                            "\n" + (quests.objects[actualQuests].type == Enemy.Shark ? sharkKill.ToString() : poulpekKill.ToString()) +
                            " / " + quests.objects[actualQuests].number.ToString();
    }

    /*
     * Verifie si la quete courante est complete
     */
    private Boolean ConditionComplete()
    {
        if (quests.objects[actualQuests].type == Enemy.Shark && sharkKill >= quests.objects[actualQuests].number ||
            quests.objects[actualQuests].type == Enemy.Poulpe && poulpekKill >= quests.objects[actualQuests].number)
        {
            sharkKill = 0;
            poulpekKill = 0;
            return true;
        }
        
        return false;
    }
}
