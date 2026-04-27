using System.Collections.Generic;
using UnityEngine;

public class MissionRepository
{
    private List<MissionSO> missions;
    
    private SaveData save;
    
    private Dictionary<string, MissionSO> missionSODictionary = new Dictionary<string, MissionSO>();
    
    private Dictionary<string, MissionData> missionDataDictionary = new Dictionary<string, MissionData>();

    public MissionRepository(List<MissionSO> missions, SaveData save)
    {
        this.missions = missions;
        setMissionSODictionary();
        
        this.save = save;
        initializeMissionData();
        setMissionDataDictionary();
    }
    
    public List<MissionSO> getAllMissions()
    {
        return missions;
    }
    
    //================================================ SO
    public void setMissionSODictionary()
    {
        foreach (var missionSO in missions)
        {
            missionSODictionary[missionSO.id] = missionSO;
        }
    }
    
    public MissionSO getMissionSO(string id)
    {
        return missionSODictionary.TryGetValue(id, out MissionSO missionSO) ? missionSO : null;
    }
    
    //================================================ Data
    public void initializeMissionData()
    {
        HashSet<string> savedMissionDataIDs = new HashSet<string>();

        foreach (var missionData in save.missionDatas)
        {
            savedMissionDataIDs.Add(missionData.id);
        }
        
        foreach (var mission in missions)
        {
            if (savedMissionDataIDs.Add(mission.id))
            {
                save.missionDatas.Add(new MissionData
                {
                    id = mission.id,
                    assignedHeroId = null,
                    isInProgress = false,
                    endTime = 0,
                });
            }
        }
    }

    public void setMissionDataDictionary()
    {
        foreach (var missionData in save.missionDatas)
        {
            missionDataDictionary[missionData.id] = missionData;
        }
    }

    public MissionData getMissionData(string id)
    {
        return missionDataDictionary.TryGetValue(id, out MissionData missionData) ? missionData : null;
    }
    
    public void updateMissionData(MissionData missionData)
    {
        missionDataDictionary[missionData.id] = missionData;

        for (int i = 0; i < save.missionDatas.Count; i++)
        {
            if (save.missionDatas[i].id == missionData.id)
            {
                save.missionDatas[i] = missionData;
                return;
            }
        }
    }
}
