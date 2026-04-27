using System;
using UnityEngine;

public class MissionAllStats
{
    public string id;
    public string missionName;
    
    public Sprite image;
    
    public string assignedHeroId;
    public bool isInProgress;
    public long endTime;
    
    public int xpReward;
    public int difficulty; // 1-10
}
