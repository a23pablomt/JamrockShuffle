using System.Collections;
using UnityEngine;

public class PlayerFieldController : MonoBehaviour
{
    GameObject enemyField;
    GameObject cardZone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(this.gameObject.tag == "PlayerField" || this.gameObject.tag == "NextField") enemyField = GameObject.FindGameObjectWithTag("EnemyField");
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
                if (child.GetChild(0).GetComponent<PlayedCard>() != null && enemyField.transform.GetChild(child.GetSiblingIndex()) == null)
                {
                    PlayedCard card = child.GetChild(0).GetComponent<PlayedCard>();
                    card.transform.SetParent(enemyField.transform);
                    card.SmoothMoveToPosition(enemyField.transform.position);
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
                    CardDataB cardData = SQLiteDBManager.Instance.GetCard(Random.Range(1, 16));

                    newCard.GetComponent<PlayedCard>().SetupCard(new CardStaple(cardData.Name, cardData.Keywords, cardData.Attack, cardData.Defense));
                    occupiedSlots=4;
                }
            }
        }
        else foreach (Transform child in transform)
        {
            var creature = child.GetComponent<PlayedCard>();
            creature.Attack(enemyField.transform.GetChild(child.GetSiblingIndex()).GetChild(0).GetComponent<PlayedCard>());
        }
        if(this.gameObject.name == "PlayerField") cardZone.GetComponent<ZoneController>().AddCardSlot();
        yield return null;
    }
}
