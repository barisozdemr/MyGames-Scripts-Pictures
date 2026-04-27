using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroRepository
{
    private List<HeroSO> heroes;
    
    private SaveData save;
    
    private Dictionary<string, HeroSO> heroSODictionary = new Dictionary<string, HeroSO>();
    
    private Dictionary<string, HeroData> heroDataDictionary = new Dictionary<string, HeroData>();

    public HeroRepository(List<HeroSO> heroes, SaveData save)
    {
        this.heroes = heroes;
        setHeroSODictionary();
        
        this.save = save;
        initializeHeroData();
        setHeroDataDictionary();
    }
    
    public List<HeroSO> getAllHeroes()
    {
        return heroes;
    }
    
    //================================================ SO
    public void setHeroSODictionary()
    {
        foreach (var heroSO in heroes)
        {
            heroSODictionary[heroSO.id] = heroSO;
        }
    }
    
    public HeroSO getHeroSO(string id)
    {
        return heroSODictionary.TryGetValue(id, out HeroSO heroSO) ? heroSO : null;
    }
    
    //================================================ Data
    public void initializeHeroData()
    {
        HashSet<string> savedHeroDataIDs = new HashSet<string>();

        foreach (var heroData in save.heroDatas)
        {
            savedHeroDataIDs.Add(heroData.id);
        }
        
        foreach (var hero in heroes)
        {
            if (savedHeroDataIDs.Add(hero.id))
            {
                save.heroDatas.Add(new HeroData
                {
                    id = hero.id,
                    level = 1,
                    xp = 0,
                    isInProgress = false
                });
            }
        }
    }

    public void setHeroDataDictionary()
    {
        foreach (var heroData in save.heroDatas)
        {
            heroDataDictionary[heroData.id] = heroData;
        }
    }

    public HeroData getHeroData(string id)
    {
        return heroDataDictionary.TryGetValue(id, out HeroData heroData) ? heroData : null;
    }

    public void updateHeroData(HeroData heroData)
    {
        heroDataDictionary[heroData.id] = heroData;

        for (int i = 0; i < save.heroDatas.Count; i++)
        {
            if (save.heroDatas[i].id == heroData.id)
            {
                save.heroDatas[i] = heroData;
                return;
            }
        }
    }
}
