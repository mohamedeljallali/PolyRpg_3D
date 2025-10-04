using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    CharacterStats stats;
    NavMeshAgent agent;
    public float attackRadius;
    Animator anim;
    CapsuleCollider capsuleCollider;
    public Transform damage;

    public bool Dead = false;
    bool isAttack = true;
    public float attackCooldown;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStats>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        damage = GetComponentInChildren<Transform>().GetChild(1);

    }

    void Update()
    {

        //Move & Attack
        if (!Dead && !LevelManager.Instance.playerDead)
        {
            float distance = Vector3.Distance(transform.position, LevelManager.Instance.player.transform.position);
            if (distance < attackRadius)
            {
                //Enemy Move to Player
                agent.SetDestination(LevelManager.Instance.player.position);
                if (distance <= agent.stoppingDistance)
                {
                    if (isAttack)
                    {
                        StartCoroutine(attackTime());
                        anim.SetTrigger("Attack");
                    }
                }
            }
        }

        if (Dead)
        {
            anim.Play("Dead00");
            capsuleCollider.isTrigger = true;
            capsuleCollider.enabled = false;
            //Instantiate(LevelManager.Instance.particleSystem[3], LevelManager.Instance.damage.position, LevelManager.Instance.damage.rotation);

        }
            

        //Walk
        anim.SetFloat("Speed", agent.velocity.magnitude);

    }

    //Enemy get Hit
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            stats.ChangeHealth(-other.GetComponentInParent<CharacterStats>().power);
            LevelManager.Instance.PlaySound(LevelManager.Instance.levelSounds[7], gameObject.transform.position);
            Instantiate(LevelManager.Instance.particleSystem[3], damage.position, damage.rotation);

        }
    }


    IEnumerator attackTime()
    {
        isAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        isAttack = true;
    }


    //Collider Enable & Disable
    public void EnableCollider()
    {
        transform.Find("axe").GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(DisableCollider());
    }

    IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Find("axe").GetComponent<BoxCollider>().enabled = false;
    }
}
