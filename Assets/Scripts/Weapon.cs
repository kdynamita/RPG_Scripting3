using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Weapon")]

public class Weapon : ScriptableObject
{
    public string type;
    [Space]
    public int strength;
    [Space]
    public Sprite wpnSprite;
}