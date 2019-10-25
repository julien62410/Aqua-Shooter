using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poulpe : MonoBehaviour, IEnnemieObject
{
    private bool inUse = false;
    private Vector3 startCoord;
    private float timer = 0;

    void Start()
    {
        startCoord = this.transform.position;
    }

    /*
     * Déplace le poulpe et le restock une fois sortie de l'ecran
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
                return;
            }

            timer += Time.deltaTime;
            if (timer > GM_Play.gm.timeBetweenShoot)
            {
                StartCoroutine(GM_Play.gm.PullObject<Ink>(0, this.transform.position.x - GM_Play.gm.shift, this.transform.position.y));
                timer = 0;
            }
        }
    }

    /*
     * Désactive le poulpe
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.inUse = false;
        this.transform.position = startCoord;
        this.gameObject.SetActive(false);
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
