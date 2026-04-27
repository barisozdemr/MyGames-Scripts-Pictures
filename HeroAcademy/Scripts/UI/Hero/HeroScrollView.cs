using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class HeroScrollView : MonoBehaviour
{
    private HeroController heroController;
    
    [SerializeField] private Transform content;
    [SerializeField] private HeroCardView cardPrefab;
    
    private Dictionary<string, HeroCardView> cards = new();
    
    public void Initialize(List<HeroAllStats> heroes, HeroController heroController)
    {
        this.heroController = heroController;
        
        clearCards();
        
        foreach (var hero in heroes)
        {
            var card = Instantiate(cardPrefab, content);
            card.Initialize(this, hero);

            cards[hero.id] = card;
        }
    }
    
    public void clearCards()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        cards.Clear();
    }
    
    public HeroCardView getCard(string heroId)
    {
        return cards.TryGetValue(heroId, out HeroCardView card) ? card : null;
    }

    public void selectCard(string heroId)
    {
        cards[heroId].setSelected();
    }
    
    public void unselectCard(string heroId)
    {
        cards[heroId].setUnselected();
    }

    public void setCardInProgress(string heroId)
    {
        cards[heroId].setInProgress();
    }
    
    public void setCardNotInProgress(string heroId)
    {
        cards[heroId].setNotInProgress();
    }
    
    //==============================================

    public void onCardClicked(string heroId)
    {
        heroController.cardClicked(heroId);
    }
    
    public void onCardHoverEnter(string heroId)
    {
        heroController.cardHoverEnter(heroId);
    }
    
    public void onCardHoverExit(string heroId)
    {
        heroController.cardHoverExit(heroId);
    }
}
