using UnityEngine;

namespace DSS.Entities
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyData entityData;
        [SerializeField] private Health health;
        [SerializeField] private Attack attack;
        
        private Vector3 wanderTarget;
        private float wanderTimer;
        private Transform player;
        private bool chasingPlayer;

        private void Awake()
        {
            if (entityData == null) Debug.LogError("EntityData not assigned.");
            health.Initialize(entityData);
            attack.Initialize(entityData);

            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            wanderTimer = entityData.wanderTime;
            PickNewWanderTarget();
        }

        private void Update()
        {
            if (health.CurrentHealth <= 0) return;

            float distanceToPlayer = player != null ? Vector3.Distance(transform.position, player.position) : Mathf.Infinity;

            // Switch between wandering and chasing
            if (distanceToPlayer <= entityData.detectionRadius)
            {
                chasingPlayer = true;
            }
            else if (distanceToPlayer > entityData.detectionRadius * 1.2f)
            {
                chasingPlayer = false;
            }

            if (chasingPlayer && player != null)
            {
                MoveTowards(player.position);
            }
            else
            {
                Wander();
            }

            HandleAttack();
        }

        private void Wander()
        {
            wanderTimer -= Time.deltaTime;

            // Re-pick wander point if time expired or reached
            if (wanderTimer <= 0f || Vector3.Distance(transform.position, wanderTarget) < 1f)
            {
                PickNewWanderTarget();
                wanderTimer = entityData.wanderTime;
            }

            MoveTowards(wanderTarget);
        }

        private void PickNewWanderTarget()
        {
            Vector2 randomCircle = Random.insideUnitCircle * entityData.wanderRadius;
            wanderTarget = new Vector3(transform.position.x + randomCircle.x, transform.position.y + randomCircle.y, transform.position.z);
        }

        private void MoveTowards(Vector3 target)
        {
            Vector3 direction = (target - transform.position).normalized;
            direction.z = 0;
            transform.position += direction * (entityData.Speed * Time.deltaTime);
        }

        private void HandleAttack()
        {
            Vector3 origin = transform.position + Vector3.up * 0.5f;
            Vector3 direction = transform.forward;

            float sphereRadius = 1f;
            float castDistance = entityData.stoppingDistance;

            if (Physics.SphereCast(origin, sphereRadius, direction, out RaycastHit hit, castDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    attack.PerformAttack(hit.collider.GetComponent<Health>());
                    Debug.DrawRay(origin, direction * hit.distance, Color.red, 0.5f);
                }
            }
        }
    }
}