using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool cardThisRound { get; set; } = false;

    private PlayerFieldController playerField;
    private PlayerFieldController enemyField;
    private PlayerFieldController waitingField;

    private GameObject hps;
    private TextMeshProUGUI enemyHpText;
    private TextMeshProUGUI playerHpText;

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

    public void Reset()
    {
        playerField = GameObject.FindGameObjectWithTag("Field")?.GetComponent<PlayerFieldController>();
        waitingField = GameObject.FindGameObjectWithTag("NextField")?.GetComponent<PlayerFieldController>();
        enemyField = GameObject.FindGameObjectWithTag("EnemyField")?.GetComponent<PlayerFieldController>();
        hps = GameObject.FindGameObjectWithTag("Hp");

        playerHp = 20;
        enemyHp = 20;
        cardThisRound = false;

        if (playerField != null)
        {
            playerField.ResetField();
        }

        if (waitingField != null)
        {
            waitingField.ResetField();
        }

        if (enemyField != null)
        {
            enemyField.ResetField();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        playerField = GameObject.FindGameObjectWithTag("Field")?.GetComponent<PlayerFieldController>();
        waitingField = GameObject.FindGameObjectWithTag("NextField")?.GetComponent<PlayerFieldController>();
        enemyField = GameObject.FindGameObjectWithTag("EnemyField")?.GetComponent<PlayerFieldController>();
        hps = GameObject.FindGameObjectWithTag("Hp");

        if (hps != null)
        {
            enemyHpText = hps.transform.GetChild(0)?.GetComponent<TextMeshProUGUI>();
            playerHpText = hps.transform.GetChild(1)?.GetComponent<TextMeshProUGUI>();
        }
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && enemyHp > 0 && playerHp > 0)
        {
            if (playerField == null || waitingField == null || enemyField == null)
            {
                Debug.LogError("Cannot start round: One or more field controllers are not assigned!");
                return;
            }

            StartCoroutine(ExecuteRoundsSequentially());
        }

        if (enemyHp <= 0)
        {
            PlayerWin();
        }
        else if (playerHp <= 0)
        {
            PlayerLoose();
        }

        if (enemyHpText != null && playerHpText != null)
        {
            enemyHpText.text = enemyHp.ToString();
            playerHpText.text = playerHp.ToString();
        }
    }

    private IEnumerator ExecuteRoundsSequentially()
    {
        if (playerField == null || enemyField == null || waitingField == null)
        {
            Debug.LogError("One or more field controllers are not assigned!");
            yield break;
        }

        yield return StartCoroutine(playerField.OnRound()); // Wait until playerField.onRound() completes
        yield return StartCoroutine(waitingField.OnRound()); // Then wait for waitingField.onRound()
        yield return StartCoroutine(enemyField.OnRound()); // Finally, wait for enemyField.onRound()
        cardThisRound = false;
    }

    public void PlayerLoose()
    {
        StopAllCoroutines();
        Reset();
        GameObject.FindGameObjectWithTag("Scenes").GetComponent<SceneController>().LoadScene("GameOver");
        Destroy(this.gameObject);
    }

    public void PlayerWin()
    {
        StopAllCoroutines();
        Reset();
        GameObject.FindGameObjectWithTag("Scenes").GetComponent<SceneController>().LoadScene("Win");
        Destroy(this.gameObject);
    }
}