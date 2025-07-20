using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace DSS.Entities
{
    public class Enemy : MonoBehaviour
    {
        [field: SerializeField] public Health Health { get; private set; }
        [SerializeField] private Attack attack;
        [SerializeField] private Transform weapon;
        private EnemyData entityData;
        
        private Vector3 wanderTarget;
        private float wanderTimer;
        private Transform player;
        private bool chasingPlayer;
        
        private Sequence weaponSeq;
        
        public void Initialize(EnemyData enemyData)
        {
            entityData = enemyData;
            
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            Health.Initialize(entityData);
            attack.Initialize(entityData);

            wanderTimer = entityData.wanderTime;
            PickNewWanderTarget();
        }

        private void Update()
        {
            if (Health.CurrentHealth <= 0) return;

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

            if (distanceToPlayer > entityData.stoppingDistance)
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

            RotatePlayer();
            HandleAttack(distanceToPlayer);
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
            Vector3 spawnPosition = new Vector3(Random.Range(-13f, 13f), Random.Range(-13f, 13f), 0);
            wanderTarget = new Vector3(spawnPosition.x, spawnPosition.y, transform.position.z);
        }

        private void MoveTowards(Vector3 target)
        {
            Vector3 direction = (target - transform.position).normalized;
            direction.z = 0;
            transform.position += direction * (entityData.Speed * Time.deltaTime);
        }

        private void HandleAttack(float distance)
        {
            if (attack.CooldownTimer > 0 || distance > entityData.AttackRange) return;
            
            PerformWeaponAnimation();
            
            RaycastHit2D[] hits = CalculateAttackHits();
            List<Health> targets = new List<Health>(hits.Length);
            
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                bool isOwner = hit.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID();
                targets.Add(isOwner ? null : hit.collider.GetComponent<Health>());
            }

            attack.PerformAttack(targets);
        }
        
        private RaycastHit2D[] CalculateAttackHits()
        {
            const float totalAngle = 55f;
            const int rayCount = 8;
            float angleStep = totalAngle / (rayCount - 1);
            float halfAngle = totalAngle / 2f;
            float range = entityData.AttackRange;

            Vector2 origin = weapon.position;
            Vector2 forward = transform.up;

            List<RaycastHit2D> allHits = new List<RaycastHit2D>(2 * rayCount);

            for (int i = 0; i < rayCount; i++)
            {
                float angle = -halfAngle + (angleStep * i) + 10f;
                Vector2 direction = Quaternion.Euler(0, 0, angle) * forward;

                RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, range);
                allHits.AddRange(hits);

                Debug.DrawRay(origin, direction * range, Color.red, 0.5f);
            }

            RaycastHit2D[] uniqueHits = allHits
                .GroupBy(hit => hit.collider)
                .Select(group => group.First())
                .ToArray();

            return uniqueHits;
        }
        
        private void PerformWeaponAnimation()
        {
            if (weaponSeq != null)
            {
                weaponSeq.Kill();
                weaponSeq = null;
            }
            
            weaponSeq = DOTween.Sequence();
            weaponSeq.Append(weapon.DOLocalRotate(new Vector3(0, 0, -10), 0.1f))
                .Append(weapon.DOLocalRotate(new Vector3(0, 0, -70), 0.1f)).OnComplete(() => { weaponSeq = null; });

            weaponSeq.Play();
        }

        private void RotatePlayer()
        {
            Vector3 direction;

            if (chasingPlayer)
            {
                direction = (player.transform.position - transform.position).normalized;
            }
            else
            {
                direction = (wanderTarget - transform.position).normalized;
            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }
    }
}