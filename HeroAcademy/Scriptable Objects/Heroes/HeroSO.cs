using UnityEngine;

[CreateAssetMenu(fileName = "HeroSO", menuName = "Scriptable Objects/HeroSO")]
public class HeroSO : ScriptableObject
{
    public string id;
    public string heroName;
    public string heroClassName;
    
    public Sprite cardIcon;
    public Sprite image;
    
    public int baseAttack;
    public int baseDefense;
    public int baseSpeed;
}
