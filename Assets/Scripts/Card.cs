using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : Creature, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    GameManager gameManager;
    private bool isDragging;
    public Vector3 startPosition;
    private Coroutine moveCoroutine;

    [SerializeField] private float returnSpeed = 5f; // Speed of smooth return

    public GameObject field;
    private List<FieldSlot> fieldSlot = new List<FieldSlot>(); // Use a list instead of array

    // Initialization
    new void Start()
    {   
        base.Start();

        gameManager = GameManager.Instance;
        field = GameObject.FindGameObjectWithTag("Field"); // Find the Field GameObject
        startPosition = transform.parent.position; // Set initial position
        transform.position = new Vector3(400, 50, -1); // Set card to be on top of other cards
        StartCoroutine(SmoothMoveToPosition(startPosition)); // Move card to start position

        // Populate fieldSlot with FieldSlot components found in children of field GameObject
        foreach (FieldSlot item in field.GetComponentsInChildren<FieldSlot>())
        {
            fieldSlot.Add(item);
        }

        
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
        transform.position = Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint(mousePos), 1f);

            //Card closestCard = FindClosestCard();
            //if (closestCard != null && closestCard != this && Vector3.Distance(transform.position, closestCard.transform.position) < swapThreshold)
            //{
            //    SwapPositions(this, closestCard);
            //    lastSwapTime = Time.time; // Update the time of the last swap
            //}
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localScale = originalScale;
        foreach (FieldSlot slot in fieldSlot)
        {
            if (slot.isOver && !gameManager.cardThisRound){
                slot.SetCard(this.info);
                transform.parent.parent.GetComponent<ZoneController>().cardSlots.Remove(transform.parent.gameObject);
                Destroy(transform.parent.gameObject);
                Destroy(gameObject);
                gameManager.cardThisRound = true;
                
            }
            else
            {
                moveCoroutine = StartCoroutine(SmoothMoveToPosition(startPosition));
                isDragging = false;
            }
        }

        
        
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

    public Vector3 originalScale;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDragging) return; // Skip scaling up if the card is being dragged
        originalScale = transform.localScale;  // Save the original scale
        transform.localScale *= 1.5f;           // Decrease the card size by 33%
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y+50, transform.localPosition.z);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDragging) return; // Skip scaling down if the card is being dragged
        transform.localScale = originalScale;  // Restore the original scale
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y-50, transform.localPosition.z);
    }

    public override void Play()
    {
        throw new System.NotImplementedException();
    }

    public override void Death()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Attack(PlayedCard target, string type)
    {
        throw new System.NotImplementedException();
    }
}