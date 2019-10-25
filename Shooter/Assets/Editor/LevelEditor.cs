using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    Level myLevel;

    private void OnEnable()
    {
        myLevel = target as Level;
    }

    public override void OnInspectorGUI()
    {
        myLevel.level = EditorGUILayout.ObjectField("Level : ", myLevel.level, typeof(TextAsset), false) as TextAsset;

        if (GUILayout.Button("Load Level"))
            if (myLevel.level != null)
                GenerateLevel();

        if (myLevel.objects != null && myLevel.objects.Count != 0)
            if (GUILayout.Button("Show Window"))
                LevelEditorWindow.OpenThisWindow(myLevel);

        //base.OnInspectorGUI();
    }

    /*
     * Lis le csv du level
     */
    private void GenerateLevel()
    {

        Undo.RecordObject(myLevel, "test");

        string rawContent = myLevel.level.text;
        string[] lineList = rawContent.Split(new string[] { "\n" }, System.StringSplitOptions.None);

        string[] separator = new string[] { "," };

        List<ObjectPos> ObjectPosList = new List<ObjectPos>();
        for (int i = 1; i < lineList.Length; i++)
        {
            string[] cells = lineList[i].Split(separator, System.StringSplitOptions.None);

            ObjectPos Object = new ObjectPos();

            Object.type = cells[0] == "Shark" ? global::Object.Shark : (cells[0] == "Poulpe" ? global::Object.Poulpe : global::Object.Rock);

            int posX = 0;
            int.TryParse(cells[1], out posX);
            Object.posX = posX;

            int posY = 0;
            int.TryParse(cells[2], out posY);
            Object.posY = posY;

            ObjectPosList.Add(Object);
        }

        myLevel.objects = ObjectPosList;

    }
}
