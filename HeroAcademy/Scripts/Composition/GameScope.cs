using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameScope : LifetimeScope
{
    [SerializeField] private List<HeroSO> heroSOList;
    [SerializeField] private List<MissionSO> missionSOList;
    
    [SerializeField] private HeroScrollView heroScrollView;
    [SerializeField] private HeroFirstView heroFirstView;
    [SerializeField] private HeroSecondView heroSecondView;
    [SerializeField] private HeroCompareView heroCompareView;
    
    [SerializeField] private MissionScrollView missionScrollView;
    [SerializeField] private MissionView missionView;
    [SerializeField] private MissionTimeView missionTimeView;

    protected override void Configure(IContainerBuilder builder)
    {
        var saveData = SaveSystem.Load();
        builder.RegisterInstance(saveData);
        
        builder.RegisterInstance(heroSOList);
        builder.RegisterInstance(missionSOList);
        
        // Repositories
        builder.Register<HeroRepository>(Lifetime.Singleton);
        builder.Register<MissionRepository>(Lifetime.Singleton);
        
        // Services
        builder.Register<HeroService>(Lifetime.Singleton);
        builder.Register<MissionService>(Lifetime.Singleton);
        builder.Register<MissionTimeService>(Lifetime.Singleton);
        builder.Register<AllStatsService>(Lifetime.Singleton);
        builder.Register<SelectionService>(Lifetime.Singleton);
        builder.Register<SaveService>(Lifetime.Singleton);
        builder.Register<GameTickService>(Lifetime.Singleton).AsImplementedInterfaces();

        // Controllers
        builder.Register<HeroController>(Lifetime.Singleton);
        builder.Register<MissionController>(Lifetime.Singleton);

        // Scene UI (MonoBehaviour)
        builder.RegisterComponent(heroScrollView);
        builder.RegisterComponent(heroFirstView);
        builder.RegisterComponent(heroSecondView);
        builder.RegisterComponent(heroCompareView);
        builder.RegisterComponent(missionScrollView);
        builder.RegisterComponent(missionView);
        builder.RegisterComponent(missionTimeView);
        
        builder.RegisterEntryPoint<GameEntryPoint>();
    }
}
