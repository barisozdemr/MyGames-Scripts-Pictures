using UnityEngine;
using VContainer.Unity;

public class GameEntryPoint : IStartable
{
    private readonly HeroScrollView heroScrollView;
    private readonly MissionScrollView missionScrollView;
    private readonly MissionView missionView;
    
    private readonly HeroController heroController;
    private readonly MissionController missionController;
    
    private readonly AllStatsService allStatsService;

    public GameEntryPoint(HeroScrollView heroScrollView
        , MissionScrollView missionScrollView
        , MissionView missionView
        , HeroController heroController
        , MissionController missionController
        , AllStatsService allStatsService)
    {
        this.heroScrollView = heroScrollView;
        this.missionScrollView = missionScrollView;
        this.missionView = missionView;
        this.heroController = heroController;
        this.missionController = missionController;
        this.allStatsService = allStatsService;
    }

    public void Start()
    {
        heroController.Initialize();
        missionController.Initialize();
        
        var heroes = allStatsService.getAllHeroAllStats();
        var missions = allStatsService.getAllMissionAllStats();
        
        heroScrollView.Initialize(heroes, heroController);
        missionScrollView.Initialize(missions, missionController);
        missionView.Initialize(missionController);
    }
}
