using System;
using System.Collections.Generic;
using UnityEngine;

public class MissionService
{
    private MissionRepository missionRepository;
    private HeroRepository heroRepository;
    private AllStatsService allStatsService;
    private SelectionService selectionService;
    private MissionTimeService missionTimeService;
    private SaveService saveService;

    public MissionService(MissionRepository missionRepository
        , HeroRepository heroRepository
        , AllStatsService allStatsService
        , SelectionService selectionService
        , MissionTimeService missionTimeService
        , SaveService saveService)
    {
        this.missionRepository = missionRepository;
        this.heroRepository = heroRepository;
        this.allStatsService = allStatsService;
        this.selectionService = selectionService;
        this.missionTimeService = missionTimeService;
        this.saveService = saveService;
    }
    
    public event Action<string, string> OnMissionStarted;
    public event Action<string, string> OnMissionCompleted;
    
    public bool isMissionSelected()
    {
        return selectionService.getSelectedMissionId() != null;
    }
    public string getSelectedMissionId()
    {
        return selectionService.getSelectedMissionId();
    }
    public void setSelectedMissionId(string missionId)
    {
        selectionService.setSelectedMissionId(missionId);
    }
    
    public bool isShowingMissionSelected()
    {
        return selectionService.getShowingMissionId() != null;
    }
    public string getShowingMissionId()
    {
        return selectionService.getShowingMissionId();
    }
    public void setShowingMissionId(string missionId)
    {
        selectionService.setShowingMissionId(missionId);
    }

    public bool isHeroSelected()
    {
        return selectionService.getSelectedHeroId() != null;
    }
    public bool isShowingFirstHeroSelected()
    {
        return selectionService.getShowingFirstHeroId() != null;
    }
    public bool isShowingSecondHeroSelected()
    {
        return selectionService.getShowingSecondHeroId() != null;
    }

    public Sprite getHeroIcon(string id)
    {
        return heroRepository.getHeroSO(id).cardIcon;
    }

    public bool isMissionStartable()
    {
        if(!isMissionSelected() || !isHeroSelected()) return false; // mission or hero not selected
        
        MissionData missionData = missionRepository.getMissionData(selectionService.getSelectedMissionId());
        HeroData heroData = heroRepository.getHeroData(selectionService.getSelectedHeroId());
        
        if(missionData.isInProgress || heroData.isInProgress) return false; // already in progress

        return true;
    }

    public int getFirstHeroMissionTimeInSeconds()
    {
        return missionTimeService.getMissionTimeInSeconds(selectionService.getShowingMissionId(), selectionService.getShowingFirstHeroId());
    }
    
    public int getSecondHeroMissionTimeInSeconds()
    {
        return missionTimeService.getMissionTimeInSeconds(selectionService.getShowingMissionId(), selectionService.getShowingSecondHeroId());
    }

    public void startMission()
    {
        string missionId = selectionService.getSelectedMissionId();
        string heroId = selectionService.getSelectedHeroId();
        
        // modify missionData
        MissionData missionData = missionRepository.getMissionData(missionId);
        int missionDuration = missionTimeService.getMissionTimeInSeconds(missionId, heroId);
        missionData.isInProgress = true;
        missionData.assignedHeroId = heroId;
        missionData.endTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + missionDuration;
        
        missionRepository.updateMissionData(missionData);
        
        // modify heroData
        HeroData heroData = heroRepository.getHeroData(heroId);
        heroData.isInProgress = true;
        
        heroRepository.updateHeroData(heroData);

        saveService.save();
        
        OnMissionStarted?.Invoke(missionId, heroId);
    }

    public bool isShowingMissionInProgress()
    {
        string id = selectionService.getShowingMissionId();
        if (id == null) return false;
        return missionRepository.getMissionData(id).isInProgress;
    }
    
    public void checkMissionProgress()
    {
        long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        var missionSOs = missionRepository.getAllMissions();

        foreach (var mission in missionSOs)
        {
            var missionData = missionRepository.getMissionData(mission.id); 
            
            if (!missionData.isInProgress)
                continue;

            if (missionData.endTime > now)
                continue;
            
            completeMission(missionData);
        }
    }

    public void completeMission(MissionData missionData)
    {
        MissionSO missionSO = missionRepository.getMissionSO(missionData.id);
        
        HeroData heroData = heroRepository.getHeroData(missionData.assignedHeroId);
        heroData.isInProgress = false;
        heroData.xp += missionSO.xpReward;
        
        int xpToLevelUp = (100 + (heroData.level - 1) * 50);
        
        if(heroData.xp >= xpToLevelUp)
        {
            heroData.xp -= xpToLevelUp;
            heroData.level++;
        }
        
        string assignedHeroId = missionData.assignedHeroId;
        
        missionData.isInProgress = false;
        missionData.endTime = 0;
        missionData.assignedHeroId = null;
        
        missionRepository.updateMissionData(missionData);
        heroRepository.updateHeroData(heroData);
        
        saveService.save();
        
        OnMissionCompleted?.Invoke(missionData.id, assignedHeroId);
    }
}
