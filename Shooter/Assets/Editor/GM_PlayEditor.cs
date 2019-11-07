using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CanEditMultipleObjects]
[CustomEditor(typeof(GM_Play))]
public class GM_PlayEditor : Editor
{
    SerializedProperty arrayLevels;
    ReorderableList levels;

    SerializedProperty scoreProperty;
    SerializedProperty questsProperty;

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

        levels = new ReorderableList(serializedObject: serializedObject, elements: arrayLevels, draggable: true, displayHeader: true, displayAddButton: true, displayRemoveButton: true);
        levels.drawHeaderCallback += DrawHeaderCallback;
        levels.drawElementCallback += DrawElementCallback;
        levels.onAddCallback += OnAddCallback;
        levels.onRemoveCallback += OnRemoveCallback;

        scoreProperty = serializedObject.FindProperty("highScores");
        questsProperty = serializedObject.FindProperty("quests");

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

    private void OnDisable()
    {
        levels.drawHeaderCallback -= DrawHeaderCallback;
        levels.drawElementCallback -= DrawElementCallback;

        levels.onAddCallback -= OnAddCallback;
        levels.onRemoveCallback -= OnRemoveCallback;
    }       

    /*
     * Affiche le nom de la box
     */
    private void DrawHeaderCallback(Rect rect)
    {
        EditorGUI.LabelField(rect, "Levels");
    }

    /*
     * Affiche les elem de la ReorderableList
     */
    private void DrawElementCallback(Rect rect, int index, bool isactive, bool isfocused)
    {
        //Get the element we want to draw from the list.
        SerializedProperty element = levels.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;

        //We get the name property of our element so we can display this in our list.
        string elementTitle = element.objectReferenceValue == null ? "New Level" : element.objectReferenceValue.name;
        
        //Draw the list item as a property field, just like Unity does internally.
        EditorGUI.PropertyField(position: new Rect(rect.x += 10, rect.y, Screen.width * .8f, 
                                                    height: EditorGUIUtility.singleLineHeight), 
                                                    property: element, 
                                                    label: new GUIContent(elementTitle), includeChildren: true);
    }

    /*
     * Ajoute un elem a la ReorderableList
     */
    private void OnAddCallback(ReorderableList list)
    {
        //Insert an extra item add the end of our list.
        int index = list.serializedProperty.arraySize;
        list.serializedProperty.arraySize++;
        list.index = index;

        list.serializedProperty.DeleteArrayElementAtIndex(index);
    }

    /*
     * Remove un elem a la ReorderableList
     */
    private void OnRemoveCallback(ReorderableList list)
    {
        var index = list.serializedProperty.arraySize;
        list.serializedProperty.arraySize--;
        list.index = index;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        levels.DoLayoutList();

        EditorGUILayout.PropertyField(scoreProperty);
        EditorGUILayout.PropertyField(questsProperty);

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
