using UnityEngine;

public class HeroController
{
    private readonly HeroScrollView heroScrollView;
    private readonly HeroFirstView heroFirstView;
    private readonly HeroSecondView heroSecondView;
    private readonly HeroCompareView heroCompareView;
    
    private readonly HeroService heroService;
    private readonly AllStatsService allStatsService;
    private readonly MissionTimeService missionTimeService;

    public HeroController(HeroScrollView heroScrollView
        , HeroFirstView heroFirstView
        , HeroSecondView heroSecondView
        , HeroCompareView heroCompareView
        , HeroService heroService
        , AllStatsService allStatsService
        , MissionTimeService missionTimeService
        , MissionService missionService
        , SelectionService selectionService)
    {
        this.heroScrollView = heroScrollView;
        this.heroFirstView = heroFirstView;
        this.heroSecondView = heroSecondView;
        this.heroCompareView = heroCompareView;
        this.heroService = heroService;
        this.allStatsService = allStatsService;
        this.missionTimeService = missionTimeService;

        selectionService.OnShowingMissionChanged += OnShowingMissionChanged;
        selectionService.OnShowingHeroChanged += OnShowingHeroChanged;
        selectionService.OnShowingSecondHeroChanged += OnShowingSecondHeroChanged;
        
        missionService.OnMissionStarted += OnMissionStarted;
        missionService.OnMissionCompleted += OnMissionCompleted;
    }
    
    public void Initialize()
    {
        heroFirstView.clearView();
        heroSecondView.clearView();
    }
    
    public void refreshHeroViews()
    {
        if (heroService.isShowingFirstHeroSelected()) //=== FIRST
        {
            heroFirstView.setHero(allStatsService.getHeroAllStats(heroService.getShowingFirstHeroId()));
        }
        else
        {
            heroFirstView.clearView();
        }

        if (heroService.isShowingSecondHeroSelected()) //=== SECOND
        {
            heroSecondView.setHero(allStatsService.getHeroAllStats(heroService.getShowingSecondHeroId()));
        }
        else
        {
            heroSecondView.clearView();
        }
    }

    public void refreshCompareViewColors()
    {
        HeroAllStats firstHero = allStatsService.getHeroAllStats(heroService.getShowingFirstHeroId());
        HeroAllStats secondHero = allStatsService.getHeroAllStats(heroService.getShowingSecondHeroId());
        
        if(firstHero.level > secondHero.level) heroCompareView.setFirstHeroLevelTextGreen();
        else if(firstHero.level < secondHero.level) heroCompareView.setSecondHeroLevelTextGreen();
        
        if(firstHero.xp > secondHero.xp) heroCompareView.setFirstHeroXpTextGreen();
        else if(firstHero.xp < secondHero.xp) heroCompareView.setSecondHeroXpTextGreen();
        
        if(firstHero.attack > secondHero.attack) heroCompareView.setFirstHeroAttackTextGreen();
        else if(firstHero.attack < secondHero.attack) heroCompareView.setSecondHeroAttackTextGreen();
        
        if(firstHero.defense > secondHero.defense) heroCompareView.setFirstHeroDefenseTextGreen();
        else if(firstHero.defense < secondHero.defense) heroCompareView.setSecondHeroDefenseTextGreen();
        
        if(firstHero.speed > secondHero.speed) heroCompareView.setFirstHeroSpeedTextGreen();
        else if(firstHero.speed < secondHero.speed) heroCompareView.setSecondHeroSpeedTextGreen();
    }

    public void refreshMissionTimeViewColors()
    {
        if (!heroService.isMissionSelected()) return;
        
        string firstHeroId = heroService.getShowingFirstHeroId();
        string secondHeroId = heroService.getShowingSecondHeroId();
        
        int firstHeroMissionTime = missionTimeService.getMissionTimeInSeconds(heroService.getSelectedMissionId(), firstHeroId);
        int secondHeroMissionTime = missionTimeService.getMissionTimeInSeconds(heroService.getSelectedMissionId(), secondHeroId);
        
        if(firstHeroMissionTime < secondHeroMissionTime) heroCompareView.setFirstHeroMissionCompletionTimeTextGreen();
        else if(firstHeroMissionTime > secondHeroMissionTime) heroCompareView.setSecondHeroMissionCompletionTimeTextGreen();
    }
    
    //================================================================== Event Triggers
    //========================================================== Mission
    
    public void OnMissionStarted(string missionId, string heroId)
    {
        heroScrollView.setCardInProgress(heroId);
    }
    
    public void OnMissionCompleted(string missionId, string heroId)
    {
        heroScrollView.setCardNotInProgress(heroId);
        refreshHeroViews(); // to update level and xp
    }
    
    //======================================================== Selection

    public void OnShowingHeroChanged()
    {
        refreshHeroViews();
    }
    
    public void OnShowingSecondHeroChanged()
    {
        if (heroService.isShowingSecondHeroSelected())
        {
            refreshCompareViewColors();
            refreshMissionTimeViewColors();
        }
        else
        {
            heroCompareView.setColorsNormal();
        }
        
    }
    
    public void OnShowingMissionChanged()
    {
        refreshHeroViews();
    }
    
    //=================================================================================

    public void cardClicked(string id)
    {
        if (heroService.getSelectedHeroId() == id) // same card selected, unselect
        {
            heroService.setSelectedHeroId(null);
            heroScrollView.unselectCard(id);
            return;
        }

        if (heroService.getSelectedHeroId() != null) // unselect previous card
        {
            heroScrollView.unselectCard(heroService.getSelectedHeroId());
        }
        
        heroScrollView.selectCard(id);
        
        heroService.setSelectedHeroId(id);
        heroService.setShowingFirstHeroId(id);
        heroService.setShowingSecondHeroId(null);
    }
    
    public void cardHoverEnter(string id)
    {
        if (heroService.getSelectedHeroId() == id) return;
        
        if (! heroService.isShowingFirstHeroSelected())
        {
            heroService.setShowingFirstHeroId(id);
            return;
        }

        heroService.setShowingSecondHeroId(id);
    }
    
    public void cardHoverExit(string id)
    {
        if (heroService.isShowingSecondHeroSelected())
        {
            heroService.setShowingSecondHeroId(null);
        }
        else if (!heroService.isHeroSelected())
        {
            heroService.setShowingFirstHeroId(null);
        }
    }
}
