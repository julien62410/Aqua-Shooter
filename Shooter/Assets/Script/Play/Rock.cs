using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IEnnemieObject 
{
    private bool inUse = false;
    private Vector3 startCoord;

    void Start()
    {
        startCoord = this.transform.position;
    }
    /*
     * Déplace le rocher et le restock une fois sortie de l'ecran
     */
    void Update()
    {
        if (inUse)
        {
            this.transform.position = new Vector3(this.transform.position.x - GM_Play.gm.poulpeAndRockSpeed * Time.deltaTime, this.transform.position.y, 0);
            
            if (this.transform.position.x < GM_Play.gm.posX_StartMap)
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
        if (collision.gameObject.TryGetComponent(out Torpedo torpille))
        {
            torpille.setinUse(false);
            torpille.transform.position = startCoord;
            torpille.gameObject.SetActive(false);
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
