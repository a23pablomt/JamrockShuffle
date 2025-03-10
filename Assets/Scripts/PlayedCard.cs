using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.VisualScripting;
using System;
using System.Management.Instrumentation;
using System.Media;

public class PlayedCard : Creature
{
    private ZoneController zoneController;
    private GameManager gameManager;

    public override IEnumerator Attack(PlayedCard obj, string type)
    {
        Debug.Log(obj);
        if (attack == 0) yield break;
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        yield return this.StartCoroutine(this.SmoothMoveToPosition(transform.position + new Vector3(0f, 0.2f, 0f)));
        if (ReferenceEquals(obj, null)) {
            Debug.Log("No target");
            if(type == "Player"){
                gameManager.enemyHp -= attack;
                if(keyword.Contains(Keywords.LifeSteal))gameManager.playerHp += attack;
                }
            else{
                gameManager.playerHp -= attack;
                if(keyword.Contains(Keywords.LifeSteal))gameManager.enemyHp += attack;
                }
            yield return StartCoroutine(SmoothMoveToPosition(transform.position + new Vector3(0f, -0.2f, 0f)));
            yield break;
        }

        

        foreach (Keywords kw in keyword)
        {
            if (kw == Keywords.Flying && !obj.keyword.Contains(Keywords.SharpSight))
            {
                if(type == "Player")gameManager.enemyHp -= attack;
                else gameManager.playerHp -= attack;
            }
            else if (kw == Keywords.Overwhelm && attack > obj.health)
            {
                int damages = attack - obj.health;
                obj.TakeDamage(attack);
                if(type == "Player")gameManager.enemyHp -= damages;
                else gameManager.playerHp -= damages;
            }

            if (kw == Keywords.LifeSteal)
            {
                if(type == "Player")gameManager.playerHp += attack;
                else gameManager.enemyHp += attack;
            }
        }
        Debug.Log("Attack");
        int damage = attack;
        obj.TakeDamage(damage);
        if (obj.keyword.Contains(Keywords.Displace)){ this.TakeDamage(1); Debug.Log("Displace"); };
        yield return StartCoroutine(SmoothMoveToPosition(transform.position + new Vector3(0f, -0.2f, 0f)));
    }

    public override void Death()
    {
        foreach (Keywords kw in keyword)
        {
            if (kw == Keywords.LastWhisper)
            {
                if (transform.parent.transform.parent.tag == "Field") gameManager.enemyHp -= attack;
                else gameManager.playerHp -= attack;  
            }
        }
        Destroy(gameObject);
    }

    public override void Play()
    {
        foreach (Keywords kw in keyword)
        {
            if (kw == Keywords.QuickDraw && transform.parent.transform.parent.tag == "Field") zoneController.AddCardSlot();
        }
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Death();
    }

    // Start is called once before the first execution of Update
    new void Start()
    {
        base.Start();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        zoneController = GameObject.FindGameObjectWithTag("CardZone").GetComponent<ZoneController>();
        transform.localScale = new Vector3(0.67f, 0.67f, 0.67f);
        Play();
        UpdateVisuals(); // Ensure UI updates correctly
    }

    void Update()
    {
        Transform stats = transform.GetChild(3);
        if (stats != null)
        {
            TextMeshProUGUI healthText = stats.GetChild(1).GetComponent<TextMeshProUGUI>();
            if (healthText != null){ 
                healthText.text = health.ToString();
            }
        }
        if (health <= 0) Death();
    }

    public IEnumerator SmoothMoveToPosition(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 5f * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
    }
}
