using Unity.VisualScripting;
using UnityEngine;

public class FieldSllot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCardDrop(Card card){
        card.transform.SetParent(transform);
        card.transform.position = transform.position;
    }
}
