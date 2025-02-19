using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private bool isDragging;
    private Vector3 startPosition;
    private static List<Card> allCards = new List<Card>(); // Store all cards
    private Coroutine moveCoroutine;
    private static float swapThreshold = 1.5f; // Adjust this for swap sensitivity

    [SerializeField] private float returnSpeed = 10f; // Speed of smooth return

    private float lastSwapTime = 0f; // Tracks when the last swap occurred
    private float swapCooldown = 0.5f; // Cooldown time between swaps (in seconds)

    private FieldSllot fieldSlot;
    public RectTransform fieldSlotRect;


    void Start()
    {
        allCards.Add(this); // Register this card
        startPosition = transform.parent.position; // Set initial position
    }

    void Update()
    {
        startPosition = transform.parent.position; // Update the start position
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        transform.position = Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint(mousePos), 0.3f);

            //Card closestCard = FindClosestCard();
            //if (closestCard != null && closestCard != this && Vector3.Distance(transform.position, closestCard.transform.position) < swapThreshold)
            //{
            //    SwapPositions(this, closestCard);
            //    lastSwapTime = Time.time; // Update the time of the last swap
            //}
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsOverFieldSlot()){
            fieldSlot.OnCardDrop(this);
        }
        else{
            moveCoroutine = StartCoroutine(SmoothMoveToPosition(startPosition));
        }

        isDragging = false;
        
    }

    private bool IsOverFieldSlot()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(fieldSlotRect, Input.mousePosition, null, out localPoint);
        return fieldSlotRect.rect.Contains(localPoint); // Check if the point is inside the FieldSlot's RectTransform
    }

    //private Card FindClosestCard()
    //{
    //    Card closestCard = null;
    //    float minDistance = Mathf.Infinity;

    //    foreach (Card card in allCards)
    //    {
    //        if (card == this) continue; // Skip self
    //        float distance = Vector3.Distance(transform.position, card.startPosition);
    //        if (distance < minDistance)
    //        {
    //            minDistance = distance;
    //            closestCard = card;
    //        }
    //    }
    //    return closestCard;
    //}

    private void SwapPositions(Card cardA, Card cardB)
{
    Transform parentA = cardA.transform.parent;
    Transform parentB = cardB.transform.parent;
    if (parentA == null || parentB == null) return; // Ensure both cards have a parent

    // Swap the parent slots
    cardA.transform.SetParent(parentB);
    cardB.transform.SetParent(parentA);

    // Ensure both cards update their local positions correctly within their new parents
    cardA.transform.localPosition = Vector3.zero;
    cardB.transform.localPosition = Vector3.zero;

    // Update the start positions after swap (card's new positions relative to their new parents)
    cardA.startPosition = cardA.transform.position; // Now, cardA is at the new parent's position
    cardB.startPosition = cardB.transform.position; // Now, cardB is at the new parent's position

    // Smoothly transition both cards to their new slots
    if (cardA.moveCoroutine != null) StopCoroutine(cardA.moveCoroutine);
    cardA.moveCoroutine = StartCoroutine(cardA.SmoothMoveToPosition(cardA.startPosition));

    if (cardB.moveCoroutine != null) StopCoroutine(cardB.moveCoroutine);
    cardB.moveCoroutine = StartCoroutine(cardB.SmoothMoveToPosition(cardB.startPosition));
}

private IEnumerator SmoothMoveToPosition(Vector3 targetPos)
{
    while (Vector3.Distance(transform.position, targetPos) > 0.01f)
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, returnSpeed * Time.deltaTime);
        yield return null;
    }
    transform.position = targetPos;
}

}
