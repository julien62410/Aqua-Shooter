using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    /*
     * Gère le déplacement du joueur et le lancer de torpille
     */
    void Update()
    {
        float newY;
        if (Input.GetKey(KeyCode.DownArrow))
        {
            newY = this.transform.position.y - GM_Play.gm.submarineSpeed * Time.deltaTime;
            this.transform.position = new Vector3(GM_Play.gm.posX_Submarine, newY > GM_Play.gm.posY_Down ? newY : GM_Play.gm.posY_Down, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            newY = this.transform.position.y + GM_Play.gm.submarineSpeed * Time.deltaTime;
            this.transform.position = new Vector3(GM_Play.gm.posX_Submarine, newY < GM_Play.gm.posY_Up ? newY : GM_Play.gm.posY_Up, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(GM_Play.gm.PullObject<Torpedo>(0, GM_Play.gm.posX_Submarine + GM_Play.gm.shift, this.transform.position.y));
    }

    /*
     * Appelle la fin du jeu si la collision n'ai pas avec l'encre
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Ink ink))
            GM_Play.gm.Lose();
    }
}
