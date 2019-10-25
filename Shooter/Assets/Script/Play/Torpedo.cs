using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour, IEnnemieObject
{
    private bool inUse = false;
    private Vector3 startCoord;

    void Start()
    {
        startCoord = this.transform.position;
    }

    /*
     * Déplace la torpille et la restock une fois sortie de l'ecran
     */
    void Update()
    {
        if (inUse)
        {
            this.transform.position = new Vector3(this.transform.position.x + GM_Play.gm.torpedoSpeed * Time.deltaTime, this.transform.position.y, 0);

            if (this.transform.position.x > GM_Play.gm.posX_EndMap)
            {
                this.inUse = false;
                this.transform.position = startCoord;
                this.gameObject.SetActive(false);
            }

        }
    }

    /*
     * Désactive la torpille
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Ink ink))
        {
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
