using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class MissionScrollView : MonoBehaviour
{
    private MissionController missionController;
    
    [SerializeField] private Transform content;
    [SerializeField] private MissionCardView cardPrefab;
    
    private Dictionary<string, MissionCardView> cards = new();
    
    public void Initialize(List<MissionAllStats> missions, MissionController missionController)
    {
        this.missionController = missionController;
        
        clearCards();
        
        foreach (var mission in missions)
        {
            var card = Instantiate(cardPrefab, content);
            card.Initialize(this, mission);

            cards[mission.id] = card;
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
    
    public MissionCardView getCard(string missionId)
    {
        return cards.TryGetValue(missionId, out MissionCardView card) ? card : null;
    }
    
    public void selectCard(string missionId)
    {
        cards[missionId].setSelected();
    }
    
    public void unselectCard(string missionId)
    {
        cards[missionId].setUnselected();
    }

    public void setCardInProgress(string missionId)
    {
        cards[missionId].setInProgress();
    }
    
    public void setCardNotInProgress(string missionId)
    {
        cards[missionId].setNotInProgress();
    }
    
    //=============================================

    public void onCardClicked(string missionId)
    {
        missionController.cardClicked(missionId);
    }
    
    public void onCardHoverEnter(string missionId)
    {
        missionController.cardHoverEnter(missionId);
    }
    
    public void onCardHoverExit(string missionId)
    {
        missionController.cardHoverExit(missionId);
    }
}
