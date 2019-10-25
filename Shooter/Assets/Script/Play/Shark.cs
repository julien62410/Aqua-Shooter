using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour, IEnnemieObject
{
    private bool inUse = false;
    private Vector3 startCoord;

    void Start()
    {
        startCoord = this.transform.position;
    }
    /*
     * Déplace le requin
     */
    void Update()
    {
        if (inUse)
        {
            this.transform.position = new Vector3(this.transform.position.x - GM_Play.gm.sharkSpeed * Time.deltaTime, this.transform.position.y, 0);
            
            if (this.transform.position.x < GM_Play.gm.posX_StartMap)
            {
                this.inUse = false;
                this.transform.position = startCoord;
                this.gameObject.SetActive(false);
            }

        }
    }

    /*
     * Désactive le requin et le restock une fois sortie de l'ecran
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.inUse = false;
        this.transform.position = startCoord;
        this.gameObject.SetActive(false);
        GM_Play.gm.sharkKill++;
    }

    // Get Set de inUse
    public bool isinUse()
    {
        return inUse;
    }

    public void setinUse(bool flag)
    {
        inUse = flag;
    }
}


