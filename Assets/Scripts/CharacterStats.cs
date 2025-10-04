using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    public int power = 10;
    int killScore = 200;
    //public bool isAlive = true;
    EnemyController enemyController;
    
    public int currentHealth {  get; private set; }

    private void Start()
    {
        currentHealth = maxHealth;
        enemyController = GetComponent<EnemyController>();
    }

    public void ChangeHealth(int value)
    {
        //currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
        Debug.Log("Health = " + currentHealth + " / " + maxHealth);

        if (currentHealth <= 0)
        {
            Die();

        }
            
    }

    void Die()
    {
        if (transform.CompareTag("Player"))
        {
            LevelManager.Instance.playerDead = true;
            LevelManager.Instance.PlaySound(LevelManager.Instance.levelSounds[8], LevelManager.Instance.player.position);
            LevelManager.Instance.PlaySound(LevelManager.Instance.levelSounds[11], LevelManager.Instance.player.position);
            LevelManager.Instance.AudioSource.Stop();
        }

        else
        {
            enemyController.Dead = true;

            LevelManager.Instance.score += killScore;
            LevelManager.Instance.PlaySound(LevelManager.Instance.levelSounds[9], LevelManager.Instance.Enemy.transform.position);
        }
        
    }
}
