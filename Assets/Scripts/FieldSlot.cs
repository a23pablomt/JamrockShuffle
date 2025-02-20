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
    public void OnCardDrop(Card card){
        card.startPosition = transform.position;
        card.transform.SetParent(transform);
        card.transform.position = transform.position;
        card.transform.localScale = card.originalScale;
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
