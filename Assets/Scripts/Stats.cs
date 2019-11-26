using UnityEngine;


//[CreateAssetMenu (fileName = "Data", menuName = "ScriptableObjects/Stats")]
[System.Serializable]
public class Stats 
{
    public string type;
    [Space]
    public int lvl;
    [Space]
    public int exp;
    public int lvlUp;

    [Space]
    public int hp;
    public int maxHp;
    [Space]
    public int dex;
    public int def;
    [Space]
    public int spd;
    [Space]
    public Weapon weapon;

}