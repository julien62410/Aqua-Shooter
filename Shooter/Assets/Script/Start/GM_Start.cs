using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class GM_Start : MonoBehaviour
{
    public Canvas canvas;
       
    private Button start;
    private Button commande;
    private Button quit;

    private Text touche;

    private Toggle retour;

    public int[] tabScore;

    public static GM_Start gm = null;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != this)
            Destroy(gameObject);

    }

    private void Start()
    {
        start = canvas.transform.Find("Start").GetComponentInChildren<Button>();
        commande = canvas.transform.Find("Commande").GetComponentInChildren<Button>();
        quit = canvas.transform.Find("Quit").GetComponentInChildren<Button>();
        retour = canvas.transform.Find("Retour").GetComponentInChildren<Toggle>();
        touche = canvas.transform.Find("Touche").GetComponentInChildren<Text>();
    }

    /*
     * Passe à l'écran des scores
     */
    public void Affiche()
    {
        start.gameObject.SetActive(false);
        commande.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        
        retour.gameObject.SetActive(true);
        touche.gameObject.SetActive(true);
    }

    /*
     * Revien à l'écran de départ
     */
    public void Retour()
    {
        start.gameObject.SetActive(true);
        commande.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
        this.gameObject.SetActive(true);
        
        retour.gameObject.SetActive(false);
        touche.gameObject.SetActive(false);
    } 
}
