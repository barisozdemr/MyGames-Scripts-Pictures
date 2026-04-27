using TMPro;
using UnityEngine;

public class HeroCompareView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI firstHeroLevelText;
    [SerializeField] private TextMeshProUGUI secondHeroLevelText;
    
    [SerializeField] private TextMeshProUGUI firstHeroXpText;
    [SerializeField] private TextMeshProUGUI secondHeroXpText;
    
    [SerializeField] private TextMeshProUGUI firstHeroAttackText;
    [SerializeField] private TextMeshProUGUI secondHeroAttackText;
    
    [SerializeField] private TextMeshProUGUI firstHeroDefenseText;
    [SerializeField] private TextMeshProUGUI secondHeroDefenseText;
    
    [SerializeField] private TextMeshProUGUI firstHeroSpeedText;
    [SerializeField] private TextMeshProUGUI secondHeroSpeedText;
    
    [SerializeField] private TextMeshProUGUI firstHeroMissionCompletionTimeText;
    [SerializeField] private TextMeshProUGUI secondHeroMissionCompletionTimeText;

    public void setFirstHeroLevelTextGreen()
    {
        firstHeroLevelText.color = Color.green;
        secondHeroLevelText.color = Color.red;
    }
    public void setSecondHeroLevelTextGreen()
    {
        secondHeroLevelText.color = Color.green;
        firstHeroLevelText.color = Color.red;
    }
    //=======================================================
    public void setFirstHeroXpTextGreen()
    {
        firstHeroXpText.color = Color.green;
        secondHeroXpText.color = Color.red;
    }
    public void setSecondHeroXpTextGreen()
    {
        secondHeroXpText.color = Color.green;
        firstHeroXpText.color = Color.red;
    }
    //=======================================================
    public void setFirstHeroAttackTextGreen()
    {
        firstHeroAttackText.color = Color.green;
        secondHeroAttackText.color = Color.red;
    }
    public void setSecondHeroAttackTextGreen()
    {
        secondHeroAttackText.color = Color.green;
        firstHeroAttackText.color = Color.red;
    }
    //=======================================================
    public void setFirstHeroDefenseTextGreen()
    {
        firstHeroDefenseText.color = Color.green;
        secondHeroDefenseText.color = Color.red;
    }
    public void setSecondHeroDefenseTextGreen()
    {
        secondHeroDefenseText.color = Color.green;
        firstHeroDefenseText.color = Color.red;
    }
    //=======================================================
    public void setFirstHeroSpeedTextGreen()
    {
        firstHeroSpeedText.color = Color.green;
        secondHeroSpeedText.color = Color.red;
    }
    public void setSecondHeroSpeedTextGreen()
    {
        secondHeroSpeedText.color = Color.green;
        firstHeroSpeedText.color = Color.red;
    }
    //=======================================================
    public void setFirstHeroMissionCompletionTimeTextGreen()
    {
        firstHeroMissionCompletionTimeText.color = Color.green;
        secondHeroMissionCompletionTimeText.color = Color.red;
    }
    public void setSecondHeroMissionCompletionTimeTextGreen()
    {
        secondHeroMissionCompletionTimeText.color = Color.green;
        firstHeroMissionCompletionTimeText.color = Color.red;
    }
    
    //=======================================================
    
    public void setColorsNormal()
    {
        firstHeroLevelText.color = Color.white;
        secondHeroLevelText.color = Color.white;
        
        firstHeroXpText.color = Color.white;
        secondHeroXpText.color = Color.white;
        
        firstHeroAttackText.color = Color.white;
        secondHeroAttackText.color = Color.white;
        
        firstHeroDefenseText.color = Color.white;
        secondHeroDefenseText.color = Color.white;
        
        firstHeroSpeedText.color = Color.white;
        secondHeroSpeedText.color = Color.white;
        
        firstHeroMissionCompletionTimeText.color = Color.white;
        secondHeroMissionCompletionTimeText.color = Color.white;
    }
}
