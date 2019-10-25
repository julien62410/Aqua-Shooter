using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(GM_Play))]
public class GM_PlayEditor : Editor
{
    SerializedProperty arrayLevels;

    // Vitesse des elem de jeu
    SerializedProperty poulpeAndRockSpeedProperty;
    SerializedProperty torpedoSpeedProperty;
    SerializedProperty submarineSpeedProperty;
    SerializedProperty sharkSpeedProperty;
    SerializedProperty inkSpeedProperty;

    // Position des elem de jeu via Empty Object
    SerializedProperty StartMapPosXProperty;
    SerializedProperty EndMapPosXProperty;
    SerializedProperty SubmarinePosXProperty;
    SerializedProperty UpPosYProperty;
    SerializedProperty DownPosYProperty;
    // Position des elem de jeu en X - Y
    SerializedProperty posX_StartMapProperty;
    SerializedProperty posX_EndMapProperty;
    SerializedProperty posX_SubmarineProperty;
    SerializedProperty posY_UpProperty;
    SerializedProperty posY_DownProperty;

    // Decallage entre lanceur et projectile
    SerializedProperty shiftProperty;

    // Temps de la tache d'encre
    SerializedProperty timeInkProperty;
    SerializedProperty displayInkProperty;
    SerializedProperty timeBetweenShootProperty;

    bool foldoutcolor;

    private void OnEnable()
    {
        arrayLevels = serializedObject.FindProperty("levels");
        
        poulpeAndRockSpeedProperty = serializedObject.FindProperty("poulpeAndRockSpeed");
        torpedoSpeedProperty = serializedObject.FindProperty("torpedoSpeed");
        submarineSpeedProperty = serializedObject.FindProperty("submarineSpeed");
        sharkSpeedProperty = serializedObject.FindProperty("sharkSpeed");
        inkSpeedProperty = serializedObject.FindProperty("inkSpeed");


        StartMapPosXProperty = serializedObject.FindProperty("StartMapPosX");
        EndMapPosXProperty = serializedObject.FindProperty("EndMapPosX");
        SubmarinePosXProperty = serializedObject.FindProperty("SubmarinePosX");
        UpPosYProperty = serializedObject.FindProperty("UpPosY");
        DownPosYProperty = serializedObject.FindProperty("DownPosY");

        shiftProperty = serializedObject.FindProperty("shift");

        timeInkProperty = serializedObject.FindProperty("timeInk");
        displayInkProperty = serializedObject.FindProperty("displayInk");
        timeBetweenShootProperty = serializedObject.FindProperty("timeBetweenShoot");

    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        arrayLevels.arraySize = EditorGUILayout.IntField("Number of Levels : ", arrayLevels.arraySize);
        if (arrayLevels.arraySize > 0)
        {
            foldoutcolor = EditorGUILayout.Foldout(foldoutcolor, "Table of Levels", true);

            if (foldoutcolor)
            {
                EditorGUI.indentLevel += 2;
                for (int i = 1; i <= arrayLevels.arraySize; i++)
                {
                    SerializedProperty currentElem = arrayLevels.GetArrayElementAtIndex(i-1);
                    EditorGUILayout.PropertyField(currentElem, new GUIContent("Level n°" + i));
                }
                EditorGUI.indentLevel -= 2;
            }
        }
        
        EditorGUILayout.PropertyField(poulpeAndRockSpeedProperty);
        EditorGUILayout.PropertyField(torpedoSpeedProperty);
        EditorGUILayout.PropertyField(submarineSpeedProperty);
        EditorGUILayout.PropertyField(sharkSpeedProperty);
        EditorGUILayout.PropertyField(inkSpeedProperty);
        
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(StartMapPosXProperty);
        EditorGUILayout.PropertyField(EndMapPosXProperty);
        EditorGUILayout.PropertyField(SubmarinePosXProperty);
        EditorGUILayout.PropertyField(UpPosYProperty);
        EditorGUILayout.PropertyField(DownPosYProperty);

        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        GUI.enabled = false;

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.FloatField((StartMapPosXProperty.objectReferenceValue as Transform).position.x, GUILayout.Width(50));
        EditorGUILayout.FloatField((EndMapPosXProperty.objectReferenceValue as Transform).position.x, GUILayout.Width(50));
        EditorGUILayout.FloatField((SubmarinePosXProperty.objectReferenceValue as Transform).position.x, GUILayout.Width(50));
        EditorGUILayout.FloatField((UpPosYProperty.objectReferenceValue as Transform).position.y, GUILayout.Width(50));
        EditorGUILayout.FloatField((DownPosYProperty.objectReferenceValue as Transform).position.y, GUILayout.Width(50));

        GUI.enabled = true;
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.PropertyField(shiftProperty);

        EditorGUILayout.PropertyField(timeInkProperty);
        GUI.enabled = false;
        EditorGUILayout.PropertyField(displayInkProperty);
        GUI.enabled = true;
        EditorGUILayout.PropertyField(timeBetweenShootProperty);

        serializedObject.ApplyModifiedProperties();
    }
}
