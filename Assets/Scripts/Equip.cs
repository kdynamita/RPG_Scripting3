using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Equip")]

public class Equip : ScriptableObject
{
    public string eName;
    [Space]
    public int defense;
    [Space]
    public Sprite eSprite;
}