using UnityEngine;

public class PlayerFieldController : MonoBehaviour
{
    GameObject enemyField;
    GameObject cardZone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(this.gameObject.name == "PlayerField") enemyField = GameObject.FindGameObjectWithTag("EnemyField");
        else enemyField = GameObject.FindGameObjectWithTag("Field");
        cardZone = GameObject.FindGameObjectWithTag("CardZone");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onRound(){
        foreach (Transform child in transform)
        {
            var creature = child.GetComponent<PlayedCard>();
            if (creature != null)
            {
                creature.Attack(enemyField.transform.GetChild(child.GetSiblingIndex()).GetComponent<PlayedCard>());
            }
        }
        if(this.gameObject.name == "PlayerField") cardZone.GetComponent<ZoneController>().AddCardSlot();
    }
}
