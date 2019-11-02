using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "ScriptableObjects/Stats")]

public class Stats : ScriptableObject
{
    public string type;
    [Space]
    public int hp;
    public int maxHp;
    [Space]
    public int sp;
    public int maxSp;
    [Space]
    public int str;
    public int def;
    public int dex;
    [Space]
    public int spd;

}