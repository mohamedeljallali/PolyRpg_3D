using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public CharacterMovement charMove;
    public EnemyController enemyController;
    

    //Collider Enable Event in Attack Animation
    public void PlayerAttack()
    {
        charMove.EnableCollider();
        LevelManager.Instance.PlaySound(LevelManager.Instance.levelSounds[1], LevelManager.Instance.player.position);
    }

    public void EnemyAttack()
    {
        enemyController.EnableCollider();
        LevelManager.Instance.PlaySound(LevelManager.Instance.levelSounds[5], LevelManager.Instance.gameObject.transform.position);
    }

    /*public void MoveSoundPlayer()
    {
        LevelManager.Instance.PlaySound(LevelManager.Instance.levelSounds[0], LevelManager.Instance.player.position);
        //Instantiate(LevelManager.Instance.particleSystem[2], LevelManager.Instance.legs.position, LevelManager.Instance.legs.rotation);
    }*/

    public void RightLeg()
    {
        LevelManager.Instance.PlaySound(LevelManager.Instance.levelSounds[0], LevelManager.Instance.player.position);
        Instantiate(LevelManager.Instance.particleSystem[2], LevelManager.Instance.legRight.position, LevelManager.Instance.legRight.rotation);
    }

    public void LeftLeg()
    {
        LevelManager.Instance.PlaySound(LevelManager.Instance.levelSounds[10], LevelManager.Instance.player.position);
        Instantiate(LevelManager.Instance.particleSystem[2], LevelManager.Instance.legLeft.position, LevelManager.Instance.legLeft.rotation);
    }

    public void MoveSoundEnemy()
    {
        LevelManager.Instance.PlaySound(LevelManager.Instance.levelSounds[2], LevelManager.Instance.Enemy.position);

    }
}
