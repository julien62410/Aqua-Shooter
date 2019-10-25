using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkEcran : MonoBehaviour
{
    /*
     * Desactive la GROSSE tache d'encre après que son temps soit épuiser
     */
    void Update()
    {
        GM_Play.gm.displayInk -= GM_Play.gm.removeIntoTime;
        if (this.gameObject.activeSelf && GM_Play.gm.displayInk < 0)
            this.gameObject.SetActive(false);
    }
}
