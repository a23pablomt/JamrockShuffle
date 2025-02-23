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
    public void AddCard(Card card)
    {
        Card newCard = Instantiate(card);
        newCard.transform.SetParent(transform, false);
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
