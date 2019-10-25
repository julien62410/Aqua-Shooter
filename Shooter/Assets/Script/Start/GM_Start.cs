using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class GM_Start : MonoBehaviour
{
    public Canvas canvas;

    private Button start;
    private Button score;
    private Button quit;

    private Text scoring;
    private Text highScore;

    private Toggle retour;

    public int[] tabScore;

    public static GM_Start gm = null;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != this)
            Destroy(gameObject);

    }

    private void Start()
    {
        start = canvas.transform.Find("Start").GetComponentInChildren<Button>();
        score = canvas.transform.Find("Score").GetComponentInChildren<Button>();
        quit = canvas.transform.Find("Quit").GetComponentInChildren<Button>();
        scoring = canvas.transform.Find("Scores").GetComponentInChildren<Text>();
        highScore = canvas.transform.Find("HighScore").GetComponentInChildren<Text>();
        retour = canvas.transform.Find("Retour").GetComponentInChildren<Toggle>();
    }

    /*
     * Passe à l'écran des scores
     */
    public void Affiche()
    {
        start.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        this.gameObject.SetActive(false);

        scoring.gameObject.SetActive(true);
        highScore.gameObject.SetActive(true);
        retour.gameObject.SetActive(true);
        ConnectRead();
    }

    /*
     * Revien à l'écran de départ
     */
    public void Retour()
    {
        start.gameObject.SetActive(true);
        score.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
        this.gameObject.SetActive(true);

        scoring.gameObject.SetActive(false);
        highScore.gameObject.SetActive(false);
        retour.gameObject.SetActive(false);
    }

    /*
     * Lis la base de donnée et l'affiche
     */
    private void ConnectRead()
    {
        string Connection = "URI=file:" + System.IO.Directory.GetCurrentDirectory() + "/BD/scoring.db";
        IDbConnection dbConnection = new SqliteConnection(Connection); ;
        dbConnection.Open();
        IDbCommand dbCmd = dbConnection.CreateCommand();
        dbCmd.CommandText = "SELECT * FROM Scores ORDER BY Score DESC";
        IDataReader reader = dbCmd.ExecuteReader();

        while (reader.Read())
            scoring.text += reader["Score"].ToString() + "\n";

        reader.Close();
        dbConnection.Dispose();
        dbConnection.Close();
    }

    /*
     * Ecrit dans la base de donnée le nouveau score
     */
    public void ConnectWrite()
    {
        string id = "";

        string Connection = "URI=file:" + System.IO.Directory.GetCurrentDirectory() + "/BD/scoring.db";
        IDbConnection dbConnection = new SqliteConnection(Connection); ;
        dbConnection.Open();
        IDbCommand dbCmd = dbConnection.CreateCommand();
        
        dbCmd.CommandText = "SELECT id, MIN(Score) FROM Scores WHERE Score < " + GM_Play.gm.score;
        IDataReader reader = dbCmd.ExecuteReader();
        
        while (reader.Read())
            id = reader["id"].ToString();
        reader.Close();

        if (id != "")
        {
            dbCmd.CommandText = "UPDATE Scores SET Score = " + GM_Play.gm.score + " WHERE id = " + id;
            dbCmd.ExecuteReader();
        }

        dbConnection.Dispose();
        dbConnection.Close();
    }

    public Button getStart()
    {
        return this.start;
    }
    public Button getQuit()
    {
        return this.quit;
    }
    public Button getScore()
    {
        return this.score;
    }
}
