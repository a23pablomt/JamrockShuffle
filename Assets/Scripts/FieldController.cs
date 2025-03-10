using System.Collections;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFieldController : MonoBehaviour
{
    GameObject enemyField;
    GameObject cardZone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(this.gameObject.tag == "Field" || this.gameObject.tag == "NextField") enemyField = GameObject.FindGameObjectWithTag("EnemyField");
        else enemyField = GameObject.FindGameObjectWithTag("Field");
        cardZone = GameObject.FindGameObjectWithTag("CardZone");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator OnRound(){

        if(this.gameObject.tag == "NextField") {
            foreach (Transform child in transform)
            {
                
                if (child.childCount > 0 && enemyField.transform.GetChild(child.GetSiblingIndex()).childCount == 1)
                {
                    PlayedCard card = child.GetComponentInChildren<PlayedCard>();
                    card.transform.SetParent(enemyField.transform.GetChild(child.GetSiblingIndex()));
                    yield return StartCoroutine(card.SmoothMoveToPosition(new Vector3(card.transform.position.x, enemyField.transform.position.y, 0)));
                }
            }
            int occupiedSlots = 0;
            while (occupiedSlots < 4)
            {
                int randomIndex = Random.Range(0, 4);
                Transform slot = this.transform.GetChild(randomIndex);
                if (slot.childCount != 0)
                {
                    occupiedSlots++;
                }
                else
                {
                    GameObject newCard = Instantiate(Resources.Load<GameObject>("Prefabs/PlayedCard"), slot);
                    CardDataB cardData = SQLiteDBManager.Instance.GetCard(Random.Range(1, 26));

                    newCard.GetComponent<PlayedCard>().SetupCard(new CardStaple(cardData.Name, cardData.Keywords, cardData.Attack, cardData.Defense));
                    occupiedSlots+=Random.Range(3, 4);
                }
            }
        }
        else foreach (Transform child in this.transform)
        {
            var creature = child.GetComponentInChildren<PlayedCard>();
            PlayedCard enemy = enemyField.transform.GetChild(child.GetSiblingIndex()).GetComponentInChildren<PlayedCard>();
            if (creature == null) {
                Debug.Log("No creature found");
                continue;
            }
            yield return creature.Attack(enemy, this.tag == "Field" ? "Player" : "Enemy");
        }
        if(this.gameObject.name == "PlayerField") cardZone.GetComponent<ZoneController>().AddCardSlot();
        yield return null;
    }

    internal void ResetField()
    {
        foreach (Transform child in transform)
        {
            if (child.childCount > 1)
            {
                Destroy(child.GetChild(1).gameObject);
            }
        }
    }
}
