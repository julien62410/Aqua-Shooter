using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float restart = -20.5f;
    
    /*
     * Déplace le background
     */
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x - GM_Play.gm.backgroundSpeed * Time.deltaTime, 0, 0);

        if (this.transform.position.x < restart)
            this.transform.position = new Vector3(0, 0, 0);
    }
}
