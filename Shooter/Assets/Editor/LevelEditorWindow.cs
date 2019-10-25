using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;


public class LevelEditorWindow : EditorWindow
{
    private Level currentLevel;

    public static void OpenThisWindow(Level level)
    {
        LevelEditorWindow myWindow = EditorWindow.GetWindow(typeof(LevelEditorWindow)) as LevelEditorWindow;
        myWindow.Init(level);

        
    }

    private void Init(Level level)
    {
        minSize = new Vector2(1500, 200);

        titleContent = new GUIContent("Level Editor");

        currentLevel = level;

        Show();
    }

    /*
     * Dessine ma fenetre
     */
    public void OnGUI()
    {
        Color defaultColor = GUI.color;

        AfficheMap(defaultColor);
        AfficheSpace();
        AfficheLegende();
    }

    /*
     *  Affiche la map correspondante au level choisi
     */
    private void AfficheMap(Color defaultColor)
    {
        EditorGUILayout.BeginHorizontal();

        for (int i = 1; i < 51; i++)
        {
            EditorGUILayout.BeginVertical();

            for (int j = 1; j < 8; j++)
            {
                IEnumerable<ObjectPos> temp = currentLevel.objects.Where(elem => elem.posX == i && elem.posY == j);
                
                if (temp.Count() != 0) 
                    if (temp.First().type == Object.Poulpe)
                        GUI.color = Color.red;
                    else if (temp.First().type == Object.Shark)
                        GUI.color = Color.blue;
                    else if (temp.First().type == Object.Rock)
                        GUI.color = Color.green;

                if (GUILayout.Button(""))
                {
                    GenericMenu menu = new GenericMenu();

                    menu.AddItem(new GUIContent("None"), GUI.color == defaultColor, noneObject, new Tuple<int, int>(i, j));
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("Shark"), GUI.color == Color.blue, sharkObject, new Tuple<int, int>(i, j));
                    menu.AddItem(new GUIContent("Poulpe"), GUI.color == Color.red, poulpeObject, new Tuple<int, int>(i, j));
                    menu.AddItem(new GUIContent("Rock"), GUI.color == Color.green, rockObject, new Tuple<int, int>(i, j));

                    menu.ShowAsContext();
                }

                GUI.color = defaultColor;
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndHorizontal();
    }

    /*
     * Supprime tout les elements de currentLevel aux positions (i,j)
     */
    public void noneObject (object obj)
    {
        Tuple<int, int> elems = (Tuple<int, int>)obj;

        for (int i = 0; i < currentLevel.objects.Count(); i++)
            if (currentLevel.objects[i].posX == elems.Item1 && currentLevel.objects[i].posY == elems.Item2)
                currentLevel.objects.RemoveAt(i--);
    }

    /*
     * Ajoute ou update l'element a la position (i,j) en Shark
     */
    public void sharkObject(object obj)
    {
        noneObject(obj);

        Tuple<int, int> elems = (Tuple<int, int>)obj;
        currentLevel.objects.Add(Level.newObjectPos(elems.Item1, elems.Item2, Object.Shark));
    }

    /*
     * Ajoute ou update l'element a la position (i,j) en Poulpe
     */
    public void poulpeObject(object obj)
    {
        noneObject(obj);

        Tuple<int, int> elems = (Tuple<int, int>)obj;
        currentLevel.objects.Add(Level.newObjectPos(elems.Item1, elems.Item2, Object.Poulpe));
    }

    /*
     * Ajoute ou update l'element a la position (i,j) en Rock
     */
    public void rockObject(object obj)
    {
        noneObject(obj);

        Tuple<int, int> elems = (Tuple<int, int>)obj;
        currentLevel.objects.Add(Level.newObjectPos(elems.Item1, elems.Item2, Object.Rock));
    }

    private void AfficheSpace()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }

    private void AfficheLegende()
    {
        GUILayout.BeginHorizontal();
        EditorGUI.BeginDisabledGroup(true);

        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = Color.black;
        style.fontStyle = FontStyle.Bold;
        style.fontSize = 20;

        GUILayout.Button("Vide", style);
        GUI.color = Color.red;
        GUILayout.Button("Poulpe", style);
        GUI.color = Color.blue;
        GUILayout.Button("Shark", style);
        GUI.color = Color.green;
        GUILayout.Button("Rock", style);

        EditorGUI.EndDisabledGroup();
        GUILayout.EndHorizontal();
    }
}
