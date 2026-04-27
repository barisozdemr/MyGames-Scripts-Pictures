using UnityEngine;
using VContainer.Unity;

public class GameTickService : ITickable
{
    private readonly MissionService missionService;
    private readonly MissionController missionController;
    private readonly HeroController heroController;
    private readonly SelectionService selectionService;

    public GameTickService(MissionService missionService
        , MissionController missionController
        , HeroController heroController
        , SelectionService selectionService)
    {
        this.missionService = missionService;
        this.missionController = missionController;
        this.heroController = heroController;
        this.selectionService = selectionService;
    }

    private float timer = 0;

    public void Tick()
    {
        timer += Time.deltaTime;
        if (timer < 0.2f) return;
        
        missionService.checkMissionProgress();

        if (missionService.isShowingMissionInProgress())
        {
            missionController.refreshMissionView();
        }
    }
}
