using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public List<HeroData> heroDatas = new();
    public List<MissionData> missionDatas = new();
}
