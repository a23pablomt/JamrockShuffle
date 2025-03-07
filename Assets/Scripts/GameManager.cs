using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public bool cardThisRound { get; set; } = false;

    PlayerFieldController playerField;
    PlayerFieldController enemyField;
    PlayerFieldController waitingField;
    
    GameObject hps;

    public int playerHp = 20;
    public int enemyHp = 20;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerField = GameObject.FindGameObjectWithTag("Field").GetComponent<PlayerFieldController>();
        waitingField = GameObject.FindGameObjectWithTag("NextField").GetComponent<PlayerFieldController>();
        enemyField = GameObject.FindGameObjectWithTag("EnemyField").GetComponent<PlayerFieldController>();
        hps = GameObject.FindGameObjectWithTag("Hp");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && enemyHp > 0 && playerHp > 0)
        {
            StartCoroutine(ExecuteRoundsSequentially());
            cardThisRound = false;
        }
        if (enemyHp <= 0)
        {
            PlayerWin();
        } else if (playerHp <= 0)
        {
            PlayerLoose();
        }
        hps.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = this.enemyHp.ToString();
        hps.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = this.playerHp.ToString();
    }

    private IEnumerator ExecuteRoundsSequentially()
    {
        yield return StartCoroutine(playerField.OnRound()); // Wait until playerField.onRound() completes
        yield return StartCoroutine(waitingField.OnRound()); // Then wait for waitingField.onRound()
        yield return StartCoroutine(enemyField.OnRound()); // Finally, wait for enemyField.onRound()
    }

    public void PlayerLoose()
    {
        Debug.Log("Player lost");
    }

    public void PlayerWin()
    {
        Debug.Log("Player won");
    }

}
