using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    // Health Points
    [SerializeField] private int HP = 100;
    private Animator animator;

    private NavMeshAgent navAgent;

    public bool isDead;

    // default components
    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    // collison with bullet and zombie and triggers animations
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            int randomValue = Random.Range(0, 2); // 0 or 1

            if (randomValue == 0)
            {
                animator.SetTrigger("DIE1");
            }
            else
            {
                animator.SetTrigger("DIE2");
            }

            isDead = true;

            // death sound
            SoundManager.Instance.zombieChannel2.PlayOneShot(SoundManager.Instance.zombieDeath);
            GlobalReferences.Instance.totalZombies -= 1;
            print(GlobalReferences.Instance.totalZombies);
        }
        else
        {
            animator.SetTrigger("DAMAGE");

            // zombie hurt sound
            SoundManager.Instance.zombieChannel2.PlayOneShot(SoundManager.Instance.zombieHurt);
        }
    }

    private void OnDrawGizmos()
    {
        // hard coded can be changed in Unity Editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f); // attack and stop attacking

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1000f); // Chasing and Stop Chasing

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1010f); // Chasing and Stop Chasing but player detected and start runnning
    }
}