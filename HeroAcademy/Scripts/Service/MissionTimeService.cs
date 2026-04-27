using UnityEngine;

public class MissionTimeService
{
    private MissionRepository missionRepository;
    private HeroRepository heroRepository;
    private AllStatsService allStatsService;

    public MissionTimeService(MissionRepository missionRepository, HeroRepository heroRepository, AllStatsService allStatsService)
    {
        this.missionRepository = missionRepository;
        this.heroRepository = heroRepository;
        this.allStatsService = allStatsService;
    }

    public int getMissionTimeInSeconds(string missionId, string heroId)
    {
        MissionAllStats mission = allStatsService.getMissionAllStats(missionId);
        HeroAllStats hero = allStatsService.getHeroAllStats(heroId);

        int baseDuration = mission.difficulty * 60;
        
        int heroSkillCount = hero.attack + hero.defense + hero.speed;

        float reductionFactor = heroSkillCount / 150f;
        reductionFactor = Mathf.Clamp01(reductionFactor);

        float finalMultiplier = 1f - (0.75f * reductionFactor);

        int secondDuration = Mathf.RoundToInt(baseDuration * finalMultiplier);
        
        return secondDuration;
    }
    
    public string formatSecondsToTime(int seconds)
    {
        int hour = seconds / 3600;
        int minute = (seconds % 3600) / 60;
        int second = seconds % 60;

        string timeString = "";
        if(hour != 0) timeString += hour + "h ";
        if(minute != 0) timeString += minute + "m ";
        timeString += second + "s";
        
        return timeString;
    }
}
