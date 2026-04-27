using System.Collections.Generic;
using UnityEngine;

public class AllStatsService
{
    private HeroRepository heroRepository;
    private MissionRepository missionRepository;
    
    public AllStatsService(HeroRepository heroRepository, MissionRepository missionRepository)
    {
        this.heroRepository = heroRepository;
        this.missionRepository = missionRepository;
    }

    public HeroAllStats getHeroAllStats(string id)
    {
        HeroSO heroSO = heroRepository.getHeroSO(id);
        HeroData heroData = heroRepository.getHeroData(id);
        
        HeroAllStats heroAllStats = new HeroAllStats();
        
        heroAllStats.id = id;
        heroAllStats.heroName = heroSO.heroName;
        heroAllStats.heroClassName = heroSO.heroClassName;
        
        heroAllStats.cardIcon = heroSO.cardIcon;
        heroAllStats.image = heroSO.image;
        
        heroAllStats.level = heroData.level;
        heroAllStats.xp = heroData.xp;
        heroAllStats.isInProgress = heroData.isInProgress;

        int attack = heroSO.baseAttack;
        if (heroSO.heroClassName == "Knight") attack += ((heroData.level - 1) * 5);
        if (heroSO.heroClassName == "Archer") attack += ((heroData.level - 1) * 6);
        if (heroSO.heroClassName == "Mage") attack += ((heroData.level - 1) * 7);
        heroAllStats.attack = attack;
        
        int defense = heroSO.baseDefense;
        if (heroSO.heroClassName == "Knight") defense += ((heroData.level - 1) * 6);
        if (heroSO.heroClassName == "Archer") defense += ((heroData.level - 1) * 4);
        if (heroSO.heroClassName == "Mage") defense += ((heroData.level - 1) * 3);
        heroAllStats.defense = defense;
        
        int speed = heroSO.baseSpeed;
        if (heroSO.heroClassName == "Knight") speed += ((heroData.level - 1) * 3);
        if (heroSO.heroClassName == "Archer") speed += ((heroData.level - 1) * 6);
        if (heroSO.heroClassName == "Mage") speed += ((heroData.level - 1) * 4);
        heroAllStats.speed = speed;
        
        return heroAllStats;
    }
    
    public MissionAllStats getMissionAllStats(string id)
    {
        MissionSO missionSO = missionRepository.getMissionSO(id);
        MissionData missionData = missionRepository.getMissionData(id);
        
        MissionAllStats missionAllStats = new MissionAllStats();
        
        missionAllStats.id = id;
        missionAllStats.missionName = missionSO.missionName;
        
        missionAllStats.image = missionSO.image;
        
        missionAllStats.assignedHeroId = missionData.assignedHeroId;
        missionAllStats.isInProgress = missionData.isInProgress;
        missionAllStats.endTime = missionData.endTime;
        
        missionAllStats.xpReward = missionSO.xpReward;
        missionAllStats.difficulty = missionSO.difficulty;
        
        return missionAllStats;
    }

    public List<HeroAllStats> getAllHeroAllStats()
    {
        var heroes = heroRepository.getAllHeroes();
        
        List<HeroAllStats> list = new List<HeroAllStats>();

        foreach (var hero in heroes)
        {
            list.Add(getHeroAllStats(hero.id));
        }
        
        return list;
    }
    
    public List<MissionAllStats> getAllMissionAllStats()
    {
        var missions = missionRepository.getAllMissions();
        
        List<MissionAllStats> list = new List<MissionAllStats>();

        foreach (var mission in missions)
        {
            list.Add(getMissionAllStats(mission.id));
        }
        
        return list;
    }
}
