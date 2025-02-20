using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
    [SerializeField] GameObject CardSlot; // Assign in Unity Inspector
    private List<GameObject> cardSlots = new List<GameObject>(); // Store CardSlots
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

        ArrangeSlots(); // Space out slots evenly
    }

    void ArrangeSlots()
    {
        if (cardSlots.Count == 0) return;

        float totalWidth = originalSpacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < cardSlots.Count; i++)
        {
            Vector3 targetPosition = new Vector3(startX + (i * (originalSpacing/cardSlots.Count)), 0, 0);
            cardSlots[i].transform.localPosition = targetPosition;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AddCardSlot();
        ArrangeSlots();
    }

    public void OnPointerEnter(){
        transform.position = new Vector3(0, -237, 0);
    }

    public void OnPointerExit(){
        transform.position = new Vector3(0, -290, 0);
    }
}