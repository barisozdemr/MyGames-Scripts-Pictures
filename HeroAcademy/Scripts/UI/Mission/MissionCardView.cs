using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MissionCardView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private MissionScrollView missionScrollView;
    private string missionId;
    
    [SerializeField] private Image missionCardImage;
    [SerializeField] private TextMeshProUGUI missionCardNameText;
    
    [SerializeField] private Image missionCardSelectedHighlighterImage;
    [SerializeField] private Image missionCardProgressHighlighterImage;
    [SerializeField] private Image missionCardShinyHighlighterImage;
    
    public void Initialize(MissionScrollView missionScrollView, MissionAllStats mission)
    {
        this.missionScrollView = missionScrollView;
        setMission(mission);
    }
    
    public string getMissionId()
    {
        return missionId;
    }
    
    public void setMission(MissionAllStats mission)
    {
        missionId = mission.id;
        
        missionCardNameText.text = mission.missionName;
        missionCardImage.sprite = mission.image;
        
        if(mission.isInProgress) setInProgress();
    }
    
    public void setSelected()
    {
        highlightSelected();
    }
    
    public void setUnselected()
    {
        unhighlightSelected();
    }

    public void setInProgress()
    {
        highlightProgress();
        makeImageColorDarker();
    }
    
    public void setNotInProgress()
    {
        unhighlightProgress();
        makeImageColorNormal();
    }
    
    //==================================================================

    public void highlightSelected()
    {
        missionCardSelectedHighlighterImage.gameObject.SetActive(true);
    }
    
    public void unhighlightSelected()
    {
        missionCardSelectedHighlighterImage.gameObject.SetActive(false);
    }
    
    public void highlightProgress()
    {
        missionCardProgressHighlighterImage.gameObject.SetActive(true);
    }
    
    public void unhighlightProgress()
    {
        missionCardProgressHighlighterImage.gameObject.SetActive(false);
    }
    
    public void makeImageColorDarker()
    {
        missionCardImage.color = new Color32(123, 123, 123, 255);
    }
    
    public void makeImageColorNormal()
    {
        missionCardImage.color = Color.white;
    }

    void highlightShiny()
    {
        missionCardShinyHighlighterImage.gameObject.SetActive(true);
    }

    void unhighlightShiny()
    {
        missionCardShinyHighlighterImage.gameObject.SetActive(false);
    }
    
    //==================================================================
    
    public void OnPointerClick(PointerEventData eventData)
    {
        missionScrollView.onCardClicked(missionId);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlightShiny();
        missionScrollView.onCardHoverEnter(missionId);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        unhighlightShiny();
        missionScrollView.onCardHoverExit(missionId);
    }
}