using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkEcran : MonoBehaviour
{
    private float timer = 0;

    /*
     * Desactive la GROSSE tache d'encre après que son temps soit épuiser
     */
    void Update()
    {
        timer += Time.deltaTime;
        if (this.gameObject.activeSelf && GM_Play.gm.displayInk < timer) {
            timer = 0;
            this.gameObject.SetActive(false);
        }
    }
}
