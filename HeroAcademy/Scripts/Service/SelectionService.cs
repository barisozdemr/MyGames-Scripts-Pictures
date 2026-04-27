using System;
using UnityEngine;

public class SelectionService
{
    public event Action OnSelectedMissionChanged;
    public event Action OnShowingMissionChanged;
    
    public event Action OnSelectedHeroChanged;
    public event Action OnShowingHeroChanged;
    public event Action OnShowingSecondHeroChanged;
    
    private string selectedMissionId;
    private string showingMissionId;
    
    private string selectedHeroId;
    private string showingFirstHeroId;
    private string showingSecondHeroId;

    
    //======================================================= Selected Mission
    
    public void setSelectedMissionId(string missionId)
    {
        this.selectedMissionId = missionId;
        OnSelectedMissionChanged?.Invoke();
    }
    public string getSelectedMissionId()
    {
        return selectedMissionId;
    }
    
    //======================================================= Showing Mission
    
    public void setShowingMissionId(string missionId)
    {
        this.showingMissionId = missionId;
        OnShowingMissionChanged?.Invoke();
    }
    public string getShowingMissionId()
    {
        return showingMissionId;
    }
    
    //======================================================= Selected Hero

    public void setSelectedHeroId(string heroId)
    {
        this.selectedHeroId = heroId;
        OnSelectedHeroChanged?.Invoke();
    }
    public string getSelectedHeroId()
    {
        return selectedHeroId;
    }
    
    //======================================================= Showing First Hero
    
    public void setShowingFirstHeroId(string heroId)
    {
        this.showingFirstHeroId = heroId;
        OnShowingHeroChanged?.Invoke();
    }
    public string getShowingFirstHeroId()
    {
        return showingFirstHeroId;
    }
    
    //======================================================= Showing Second Hero
    
    public void setShowingSecondHeroId(string heroId)
    {
        this.showingSecondHeroId = heroId;
        OnShowingHeroChanged?.Invoke();
        OnShowingSecondHeroChanged?.Invoke();
    }
    public string getShowingSecondHeroId()
    {
        return showingSecondHeroId;
    }
}
