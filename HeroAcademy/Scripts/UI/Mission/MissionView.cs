using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class MissionView : MonoBehaviour
{
    private MissionController missionController;
    private string missionId;
    
    [SerializeField] private Image missionImage;
    [SerializeField] private TextMeshProUGUI missionNameText;
    [SerializeField] private TextMeshProUGUI missionDifficultyText;
    [SerializeField] private TextMeshProUGUI missionXpText;
    
    [SerializeField] private TextMeshProUGUI isInProgressText;
    [SerializeField] private TextMeshProUGUI timeLeftText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI heroText;
    [SerializeField] private Image assignedHeroIcon;
    
    [SerializeField] private Button startMissionButton;
    
    public void Initialize(MissionController missionController)
    {
        this.missionController = missionController;
    }

    public string getMissionId()
    {
        return missionId;
    }
    
    public void setMission(MissionAllStats mission)
    {
        clearMission();
        
        missionId = mission.id;
        
        missionImage.gameObject.SetActive(true);
        missionImage.sprite = mission.image;
        
        missionNameText.text = mission.missionName;
        missionDifficultyText.text = mission.difficulty.ToString() + "/10";
        missionXpText.text = mission.xpReward.ToString();
    }

    public void setMissionProgress(MissionAllStats mission, string timeString, Sprite heroIcon)
    {
        isInProgressText.gameObject.SetActive(true);
        timeLeftText.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        timeText.text = timeString;
        
        heroText.gameObject.SetActive(true);
        assignedHeroIcon.gameObject.SetActive(true);
        assignedHeroIcon.sprite = heroIcon;
        
        startMissionButton.image.color = UnityEngine.ColorUtility.TryParseHtmlString("#66005C", out var c) ? c : Color.white;
    }
    
    public void clearMission()
    {
        missionId = null;
        
        missionImage.gameObject.SetActive(false);
        
        missionNameText.text = "";
        missionDifficultyText.text = "";
        missionXpText.text = "";
        
        isInProgressText.gameObject.SetActive(false);
        timeLeftText.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
        heroText.gameObject.SetActive(false);
        assignedHeroIcon.gameObject.SetActive(false);
        
        startMissionButton.image.color = UnityEngine.ColorUtility.TryParseHtmlString("#9627DD", out var c) ? c : Color.white;
    }

    public void startMissionButtonClicked()
    {
        missionController.startMission();
    }
}
