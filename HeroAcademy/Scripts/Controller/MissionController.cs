using System;
using UnityEngine;

public class MissionController
{
    private MissionScrollView missionScrollView;
    private MissionView missionView;
    private MissionTimeView missionTimeView;
    
    private MissionService missionService;
    private MissionTimeService missionTimeService;
    private AllStatsService allStatsService;
    
    public MissionController(MissionScrollView missionScrollView
        , MissionView missionView
        , MissionTimeView missionTimeView
        , MissionService missionService
        , MissionTimeService missionTimeService
        , AllStatsService allStatsService
        , SelectionService selectionService)
    {
        this.missionScrollView = missionScrollView;
        this.missionView = missionView;
        this.missionTimeView = missionTimeView;
        this.missionService = missionService;
        this.missionTimeService = missionTimeService;
        this.allStatsService = allStatsService;
        
        selectionService.OnSelectedMissionChanged += OnSelectedMissionChanged;
        selectionService.OnSelectedHeroChanged += OnSelectedHeroChanged;
        selectionService.OnShowingMissionChanged += OnShowingMissionChanged;
        selectionService.OnShowingHeroChanged += OnShowingHeroChanged;
        
        missionService.OnMissionStarted += OnMissionStarted;
        missionService.OnMissionCompleted += OnMissionCompleted;
    }

    public void Initialize()
    {
        missionView.clearMission();
        refreshMissionTimeView();
    }

    public void setMissionView(MissionAllStats mission)
    {
        missionView.setMission(mission);
        if (mission.isInProgress) setMissionViewProgress(mission);
    }

    public void setMissionViewProgress(MissionAllStats mission)
    {
        var now = DateTimeOffset.Now.ToUnixTimeSeconds();
        int timeLeft = (int)(mission.endTime - now);
        
        string timeString = missionTimeService.formatSecondsToTime(timeLeft);
        
        Sprite heroIcon = missionService.getHeroIcon(mission.assignedHeroId);
        
        missionView.setMissionProgress(mission, timeString, heroIcon);
    }
    
    public void refreshMissionView()
    {
        if (!missionService.isShowingMissionSelected())
        {
            clearMissionView();
            return;
        }
        
        setMissionView(allStatsService.getMissionAllStats(missionService.getShowingMissionId()));
    }

    public void clearMissionView()
    {
        missionView.clearMission();
    }
    
    //======================================================
    
    public void refreshMissionTimeView()
    {
        if (! missionService.isShowingMissionSelected())
        {
            clearMissionTimeView();
            return;
        }

        setFirstHeroMissionTime();
        setSecondHeroMissionTime();
    }

    public void clearMissionTimeView()
    {
        missionTimeView.clearMissionTimeFirstText();
        missionTimeView.clearMissionTimeSecondText();
    }

    public void setFirstHeroMissionTime()
    {
        if (missionService.isShowingFirstHeroSelected())
        {
            int seconds = missionService.getFirstHeroMissionTimeInSeconds();
            missionTimeView.setMissionTimeFirstText(missionTimeService.formatSecondsToTime(seconds));
        }
        else
        {
            missionTimeView.clearMissionTimeFirstText();
        }
    }

    public void setSecondHeroMissionTime()
    {
        if (missionService.isShowingSecondHeroSelected())
        {
            int seconds = missionService.getSecondHeroMissionTimeInSeconds();
            missionTimeView.setMissionTimeSecondText(missionTimeService.formatSecondsToTime(seconds));
        }
        else
        {
            missionTimeView.clearMissionTimeSecondText();
        }
    }

    public void startMission()
    {
        if (missionService.isMissionStartable()) missionService.startMission();
    }
    
    //================================================================== Event Triggers
    //========================================================== Mission

    public void OnMissionStarted(string missionId, string heroId)
    {
        missionScrollView.setCardInProgress(missionId);
    }

    public void OnMissionCompleted(string missionId, string heroId)
    {
        missionScrollView.setCardNotInProgress(missionId);
        refreshMissionView();
        refreshMissionTimeView();
    }
    
    //======================================================== Selection
    
    public void OnShowingMissionChanged()
    {
        refreshMissionView();
        refreshMissionTimeView();
    }
    public void OnSelectedMissionChanged()
    {
        refreshMissionView();
        refreshMissionTimeView();
    }

    public void OnShowingHeroChanged()
    {
        refreshMissionTimeView();
    }
    public void OnSelectedHeroChanged()
    {
        refreshMissionTimeView();
    }
    
    //=================================================================================

    public void cardClicked(string id)
    {
        if (missionService.getSelectedMissionId() == id) // same card selected, unselect
        {
            missionService.setSelectedMissionId(null);
            missionScrollView.unselectCard(id);
            return;
        }

        if (missionService.getSelectedMissionId() != null) // unselect previous card
        {
            missionScrollView.unselectCard(missionService.getSelectedMissionId());
        }
        
        missionService.setShowingMissionId(id);
        missionService.setSelectedMissionId(id);
        missionScrollView.selectCard(id);
    }
    
    public void cardHoverEnter(string id)
    {
        missionService.setShowingMissionId(id);
    }
    
    public void cardHoverExit(string id)
    {
        if (missionService.isMissionSelected())
        {
            missionService.setShowingMissionId(missionService.getSelectedMissionId());
        }
        else
        {
            missionService.setShowingMissionId(null);
        }
    }
}
