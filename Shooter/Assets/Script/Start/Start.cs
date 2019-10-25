using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    /*
     * Load la scene de jeu
     */
    public void onClick ()
    {
        SceneManager.LoadScene("Play");
    }
}
