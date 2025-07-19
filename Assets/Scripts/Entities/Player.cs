using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace DSS.Entities
{
    public partial class Player : MonoBehaviour
    {
        [SerializeField] private EntityData entityData;
        [SerializeField] private Transform weapon;
        [SerializeField] private Health health;
        [SerializeField] private Attack attack;
        
        Sequence weaponSeq;
        
        private void Awake()
        {
            if (entityData == null)
            {
                Debug.LogError("EntityData is not assigned in Player script.");
                return;
            }

            health.Initialize(entityData);
            attack.Initialize(entityData);
        }
        
        private void Update()
        {
            HandleAttack();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0).normalized;
            transform.Translate(movement * (entityData.Speed * Time.deltaTime), Space.World);
        }
        
        private void HandleAttack()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RaycastHit2D[] hits = CalculateAttackHits();
                List<Health> targets = new List<Health>(hits.Length);
                
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit2D hit = hits[i];
                    Debug.Log(hit.collider.name);

                    bool isOwner = hit.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID();
                    targets.Add(isOwner ? null : hit.collider.GetComponent<Health>());
                }

                attack.PerformAttack(targets);
                PerformWeaponAnimation();
            }
        }

        private RaycastHit2D[] CalculateAttackHits()
        {
            Vector2 origin = transform.position;
            float totalAngle = 30f;
            int rayCount = 5;
            float angleStep = totalAngle / (rayCount - 1);
            float halfAngle = totalAngle / 2f;
            float range = entityData.AttackRange;

            Vector2 forward = transform.up;

            List<RaycastHit2D> allHits = new List<RaycastHit2D>(2 * rayCount);

            for (int i = 0; i < rayCount; i++)
            {
                float angle = -halfAngle + (angleStep * i);
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

            float halfLife = entityData.AttackCooldown * 0.5f;
            
            weaponSeq = DOTween.Sequence();
            weaponSeq.Append(weapon.DOLocalRotate(new Vector3(0, 0, -10), halfLife))
                .Append(weapon.DOLocalRotate(new Vector3(0, 0, -70), halfLife)).OnComplete(() => { weaponSeq = null; });

            weaponSeq.Play();
        }
    }
}