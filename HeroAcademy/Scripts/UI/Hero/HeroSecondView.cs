using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSecondView : MonoBehaviour
{
    private string heroId;
    
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI classText;
    
    [SerializeField] private Image image;
    
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI speedText;
    
    public string getHeroId()
    {
        return heroId;
    }

    public void setHero(HeroAllStats hero)
    {
        nameText.text = hero.heroName;
        classText.text = hero.heroClassName;

        image.gameObject.SetActive(true);
        image.sprite = hero.image;
        
        levelText.text = hero.level.ToString();
        xpText.text = hero.xp.ToString();
        
        attackText.text = hero.attack.ToString();
        defenseText.text = hero.defense.ToString();
        speedText.text = hero.speed.ToString();
    }

    public void clearView()
    {
        nameText.text = "";
        classText.text = "";

        image.gameObject.SetActive(false);
        
        levelText.text = "";
        xpText.text = "";
        
        attackText.text = "";
        defenseText.text = "";
        speedText.text = "";
    }
}
