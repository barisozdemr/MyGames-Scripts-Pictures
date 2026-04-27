using UnityEngine;

public class HeroService
{
    private HeroRepository heroRepository;
    private AllStatsService allStatsService;
    private SelectionService selectionService;
    
    public HeroService(HeroRepository heroRepository
        , AllStatsService allStatsService
        , SelectionService selectionService)
    {
        this.heroRepository = heroRepository;
        this.allStatsService = allStatsService;
        this.selectionService = selectionService;
    }

    public bool isHeroSelected()
    {
        return selectionService.getSelectedHeroId() != null;
    }
    public string getSelectedHeroId()
    {
        return selectionService.getSelectedHeroId();
    }
    public void setSelectedHeroId(string heroId)
    {
        selectionService.setSelectedHeroId(heroId);
    }
    
    
    public bool isShowingFirstHeroSelected()
    {
        return selectionService.getShowingFirstHeroId() != null;
    }
    public string getShowingFirstHeroId()
    {
        return selectionService.getShowingFirstHeroId();
    }
    public void setShowingFirstHeroId(string heroId)
    {
        selectionService.setShowingFirstHeroId(heroId);
    }
    
    
    public bool isShowingSecondHeroSelected()
    {
        return selectionService.getShowingSecondHeroId() != null;
    }
    public string getShowingSecondHeroId()
    {
        return selectionService.getShowingSecondHeroId();
    }
    public void setShowingSecondHeroId(string heroId)
    {
        selectionService.setShowingSecondHeroId(heroId);
    }
    
    
    public bool isMissionSelected()
    {
        return selectionService.getSelectedMissionId() != null;
    }
    public string getSelectedMissionId()
    {
        return selectionService.getSelectedMissionId();
    }
}