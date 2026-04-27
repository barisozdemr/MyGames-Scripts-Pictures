using TMPro;
using UnityEngine;

public class MissionTimeView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI missionTimeFirstText;
    [SerializeField] private TextMeshProUGUI missionTimeSecondText;

    public void setMissionTimeFirstText(string text)
    {
        missionTimeFirstText.text = text;
    }
    
    public void clearMissionTimeFirstText()
    {
        missionTimeFirstText.text = "";
    }
    
    public void setMissionTimeSecondText(string text)
    {
        missionTimeSecondText.text = text;
    }
    
    public void clearMissionTimeSecondText()
    {
        missionTimeSecondText.text = "";
    }
}
