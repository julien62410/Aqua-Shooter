using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Score : MonoBehaviour
{
    private void Start()
    {
        GM_Start.gm.tabScore = new int[10];
    }

    /*
     * Affiche les scores aux clicks
     */
    public void onClick()
    {
        GM_Start.gm.Affiche();
    }
}
