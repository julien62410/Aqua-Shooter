using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjectPos
{
    public int posX, posY;
    public myGameObject type;
}

public enum myGameObject
{
    Shark,
    Poulpe,
    Rock
}

[CreateAssetMenu(fileName = "Level_n°.asset", menuName = "Custom/GenerateLevel", order = 100)]
public class Level : ScriptableObject
{
    public TextAsset fichierLevel;
    public List<ObjectPos> objects;

    /*
     * Constructeur d'ObjectPos
     */
    public static ObjectPos newObjectPos (int posX, int posY, myGameObject type)
    {
        ObjectPos elem = new ObjectPos();
        elem.posX = posX;
        elem.posY = posY;
        elem.type = type;
        return elem;
    }
}
