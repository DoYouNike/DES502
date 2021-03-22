using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterUI : MonoBehaviour
{
    // Start is called before the first frame update
    private float healthPercent;
    public GameObject enemy;
    public GameObject player;
    public Sprite enemyfull;
    public Sprite enemy50;
    public Sprite enemy20;
    public Sprite enemy10;
    private float enemyFullhealth;
    private float playerFullhealth;
    public Sprite playerfull;
    public Sprite player50;
    public Sprite player20;
    public Sprite player10;
    void Start()
    {
        enemyFullhealth = EnemyControl.instance.maxHealth;
        playerFullhealth = PlayerControl.instance.maxH;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyImage();
        PlayerImage();
    }
    public void EnemyImage()
    {
        if (EnemyControl.instance.maxHealth < 0)
        {
            EnemyControl.instance.maxHealth = 0;
        }
        healthPercent = Mathf.RoundToInt(EnemyControl.instance.maxHealth / enemyFullhealth * 100);
        Debug.Log(healthPercent);
        if (healthPercent == 100)
        {
            enemy.GetComponent<Image>().sprite = enemyfull;
        }
        else if (healthPercent ==50)
        {
            enemy.GetComponent<Image>().sprite = enemy50;

        }
        else if (healthPercent == 20)
        {
            enemy.GetComponent<Image>().sprite = enemy20;

        }
        else if (healthPercent == 10)
        {
            enemy.GetComponent<Image>().sprite = enemy10;

        }
    }
    public void PlayerImage()
    {
        if (PlayerControl.instance.maxH < 0)
        {
            PlayerControl.instance.maxH = 0;
        }
        healthPercent = Mathf.RoundToInt(PlayerControl.instance.maxH / playerFullhealth * 100);
       
        if (healthPercent == 100)
        {
            player.GetComponent<Image>().sprite = playerfull;
        }
        else if (healthPercent == 50)
        {
            player.GetComponent<Image>().sprite = player50;

        }
        else if (healthPercent == 20)
        {
            player.GetComponent<Image>().sprite = player20;

        }
        else if (healthPercent == 10)
        {
            player.GetComponent<Image>().sprite = player10;

        }
    }
}
