using System;
using UnityEngine;

[Serializable]
public class MissionData
{
    public string id;
    public string assignedHeroId;

    public bool isInProgress;
    public long endTime;
}
