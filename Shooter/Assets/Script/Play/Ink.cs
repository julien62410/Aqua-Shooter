using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink : MonoBehaviour, IEnnemieObject
{
    private bool inUse = false;
    private Vector3 startCoord;

    void Start()
    {
        startCoord = this.transform.position;
    }
    /*
     * Déplace le rocher
     */
    void Update()
    {
        if (inUse)
        {
            this.transform.position = new Vector3(this.transform.position.x - GM_Play.gm.inkSpeed * Time.deltaTime, this.transform.position.y, 0);

            if (this.transform.position.x < GM_Play.gm.posX_StartMap)
            {
                this.inUse = false;
                this.transform.position = startCoord;
                this.gameObject.SetActive(false);
            }

        }
    }

    /*
     * Désactive la tache d'encre et appelle l'aparition de la GROSSE tache d'encre
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out SubmarineController submarine))
        {
            GM_Play.gm.InkEcran();
            this.inUse = false;
            this.transform.position = startCoord;
            this.gameObject.SetActive(false);
        }

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
