using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Weapon")]

public class Weapon : ScriptableObject
{
    public string wpnName;
    [Space]
    public int damage;
    [Space]
    public Sprite wpnSprite;
}