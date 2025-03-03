using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCard(Card card)
    {
        GameObject newCard = Instantiate(Resources.Load<GameObject>("Prefabs/Card"));
        newCard.GetComponent<PlayedCard>().SetupCard(card.info);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
    }
}
