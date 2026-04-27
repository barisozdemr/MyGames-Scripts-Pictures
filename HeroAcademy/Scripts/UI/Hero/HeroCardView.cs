using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroCardView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private HeroScrollView heroScrollView;
    private string heroId;
    
    [SerializeField] private Image heroCardImage;
    [SerializeField] private TextMeshProUGUI heroCardNameText;
    
    [SerializeField] private Image heroCardSelectedHighlighterImage;
    [SerializeField] private Image heroCardProgressHighlighterImage;
    [SerializeField] private Image heroCardShinyHighlighterImage;
    
    public void Initialize(HeroScrollView heroScrollView, HeroAllStats hero)
    {
        this.heroScrollView = heroScrollView;
        setHero(hero);
    }
    
    public string getId()
    {
        return heroId;
    }
    
    public void setHero(HeroAllStats hero)
    {
        heroId = hero.id;
        
        heroCardNameText.text = hero.heroName;
        heroCardImage.sprite = hero.cardIcon;
        
        if(hero.isInProgress) setInProgress();
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
        heroCardSelectedHighlighterImage.gameObject.SetActive(true);
    }
    
    public void unhighlightSelected()
    {
        heroCardSelectedHighlighterImage.gameObject.SetActive(false);
    }
    
    public void highlightProgress()
    {
        heroCardProgressHighlighterImage.gameObject.SetActive(true);
    }
    
    public void unhighlightProgress()
    {
        heroCardProgressHighlighterImage.gameObject.SetActive(false);
    }

    public void makeImageColorDarker()
    {
        heroCardImage.color = new Color32(123, 123, 123, 255);
    }
    
    public void makeImageColorNormal()
    {
        heroCardImage.color = Color.white;
    }

    void highlightShiny()
    {
        heroCardShinyHighlighterImage.gameObject.SetActive(true);
    }

    void unhighlightShiny()
    {
        heroCardShinyHighlighterImage.gameObject.SetActive(false);
    }
    
    //==================================================================

    public void OnPointerClick(PointerEventData eventData)
    {
        heroScrollView.onCardClicked(heroId);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlightShiny();
        heroScrollView.onCardHoverEnter(heroId);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        unhighlightShiny();
        heroScrollView.onCardHoverExit(heroId);
    }
}