using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    public Transform player; // Reference to the player's position
    public float moveSpeed = 1.0f; // Speed of the zombie's movement
    public Animator animator; // Reference to the animator component
    public float attackRange = 1.5f; // Distance at which the zombie starts attacking

    private NavMeshAgent navMeshAgent;
    private bool isWalking = false;

    void Start()
    {
        // Get the NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
        }
        // Ensure player reference is assigned
        if (player == null)
        {
            Debug.LogError("Player Transform not assigned in the inspector");
        }
        // Ensure animator reference is assigned
        if (animator == null)
        {
            Debug.LogError("Animator component not assigned in the inspector");
        }
        // Set the speed of the NavMeshAgent
        navMeshAgent.speed = moveSpeed;
    }

    void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the walking animation is active
        isWalking = animator.GetCurrentAnimatorStateInfo(0).IsName("ZombieFisherman_Torch_Walk");

        if (isWalking)
        {
            if (distanceToPlayer <= attackRange)
            {
                // If within attack range, stop moving and set the attack animation
                navMeshAgent.isStopped = true; // Stop the NavMeshAgent
                animator.SetBool("Attack1", true); // Set the attack animation
            }
            else
            {
                // If not within attack range, continue walking towards the player
                navMeshAgent.SetDestination(player.position);
                navMeshAgent.isStopped = false; // Ensure the NavMeshAgent is not stopped
                animator.SetBool("Attack1", false); // Ensure the attack animation is not set
            }
        }
        else
        {
            // Stop the zombie if not walking
            navMeshAgent.ResetPath();
            animator.SetBool("Attack1", false); // Ensure the attack animation is not set
        }
    }
}
