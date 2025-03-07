using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoneController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject CardSlot; // Assign in Unity Inspector
    public List<GameObject> cardSlots = new List<GameObject>(); // Store CardSlots
    [SerializeField] float originalSpacing = 200f; // Adjust spacing between slots

    void Start()
    {
        AddCardSlot();
        AddCardSlot();
        AddCardSlot();
    }

    public void AddCardSlot()
    {
        // Instantiate a new CardSlot from the prefab
        GameObject cardSlot = Instantiate(CardSlot, transform);
    
        cardSlots.Add(cardSlot);

        ArrangeSlots();
    }

    void ArrangeSlots()
    {
        if (cardSlots.Count == 0) return;

        // Calculate total width and the starting X position for slot placement
        float totalWidth = originalSpacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < cardSlots.Count; i++)
        {
            float spacing = cardSlots.Count > 1 ? originalSpacing / (cardSlots.Count - 1) : originalSpacing;
            Vector3 targetPosition = new Vector3(startX + (i * spacing), 0, 0);
            cardSlots[i].transform.localPosition = targetPosition;
        }
    }

    void Update()
    {        
        ArrangeSlots(); // Continuously space out slots as needed
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Move the parent zone relative to its original position (in local space)
        transform.localPosition = new Vector3(0, -237, 0);

        // Re-arrange child slots after moving the parent
        ArrangeSlots(); // Ensure the slots adjust their positions after parent movement
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Move the parent zone relative to its original position (in local space)
        transform.localPosition = new Vector3(0, -290, 0);

        // Re-arrange child slots after moving the parent
        ArrangeSlots(); // Ensure the slots adjust their positions after parent movement
    }
}
