using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Quests))]
public class QuestEditor : Editor
{
    Quests myQuests;

    private void OnEnable()
    {
        myQuests = target as Quests;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Generate Quests"))
            if (myQuests.fichierQuests != null)
                GenerateQuests();

        base.OnInspectorGUI();
    }

    /*
     * Lis le csv des quests
     */
    private void GenerateQuests()
    {

        Undo.RecordObject(myQuests, "test");

        string rawContent = myQuests.fichierQuests.text;
        string[] lineList = rawContent.Split(new string[] { "\n" }, System.StringSplitOptions.None);

        string[] separator = new string[] { "," };

        List<Quest> QuestsList = new List<Quest>();
        for (int i = 1; i < lineList.Length; i++)
        {
            string[] cells = lineList[i].Split(separator, System.StringSplitOptions.None);

            Quest quest = new Quest();

            quest.description = cells[0];

            quest.type = cells[1] == "Shark" ? Enemy.Shark : Enemy.Poulpe ;

            int posY = 0;
            int.TryParse(cells[2], out posY);
            quest.number = posY;

            QuestsList.Add(quest);
        }

        myQuests.objects = QuestsList;

    }
}
