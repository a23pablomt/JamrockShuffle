using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayedCard : Creature
{
    public override void Attack(PlayedCard source)
    {
        foreach(Keywords kw in keyword)
        {
            if(kw.ToString() == "Flying" && !source.keyword.Contains(Keywords.SharpSight))
            {

            }
            else if(kw.ToString() == "Overwhelm")
            {
                if(attack > source.health)
                {
                    
                }
            }
            
            if(kw.ToString() == "LifeSteal")
            {
                
            }
        }
        source.TakeDamage();
    }

    public override void Death()
    {
        foreach(Keywords kw in keyword)
        {
            if(kw.ToString() == "LastWhisper")
            {

            }
        }
        Destroy(gameObject);
    }

    public override void Play()
    {
        foreach(Keywords kw in keyword)
        {
            if(kw.ToString() == "QuickDraw")
            {

            }
        }
    }

    public override void TakeDamage()
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
        transform.localScale = new Vector3(0.66f, 0.66f, 0.66f);
        SmoothMoveToPosition(transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SmoothMoveToPosition(Vector3 targetPos)
{
    while (Vector3.Distance(transform.position, targetPos) > 0.01f)
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, 5f * Time.deltaTime);
        yield return null;
    }
    transform.position = targetPos;
}

}
