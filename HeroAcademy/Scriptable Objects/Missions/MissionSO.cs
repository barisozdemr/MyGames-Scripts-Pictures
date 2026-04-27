using UnityEngine;

[CreateAssetMenu(fileName = "MissionSO", menuName = "Scriptable Objects/MissionSO")]
public class MissionSO : ScriptableObject
{
    public string id;
    public string missionName;
    
    public Sprite image;
    
    public int xpReward;
    public int difficulty; // 1-10
}
