using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Quest
{
    public string description;
    public int number;
    public Enemy type;
}

public enum Enemy
{
    Shark,
    Poulpe,
}

[CreateAssetMenu(fileName = "Quests.asset", menuName = "Custom/GenerateQuests", order = 100)]
public class Quests : ScriptableObject
{
    public TextAsset fichierQuests;
    public List<Quest> objects;

    /*
     * Constructeur d'ObjectPos
     */
    public static ObjectPos newObjectPos(int posX, int posY, myGameObject type)
    {
        ObjectPos elem = new ObjectPos();
        elem.posX = posX;
        elem.posY = posY;
        elem.type = type;
        return elem;
    }
}