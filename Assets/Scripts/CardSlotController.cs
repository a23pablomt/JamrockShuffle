using UnityEngine;

public class CardSlotController : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddCard();
    }


    public void AddCard()
    {
        if (Deck.Instance == null)
        {
            Debug.LogError("Deck.Instance is null!");
            return;
        }

        GameObject newCard = Instantiate(Resources.Load<GameObject>("Prefabs/Card"));
        newCard.GetComponent<Card>().SetupCard(Deck.Instance.drawCard());
        
        
        if (newCard == null)
        {
            Debug.LogWarning("No more cards left in the deck!");
            return;
        }

        newCard.transform.SetParent(transform, false);
    }

}
