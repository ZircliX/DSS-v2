using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DSS.Entities
{
    public partial class Player : MonoBehaviour
    {
        [SerializeField] private EntityData entityData;
        [SerializeField] private Transform weapon;
        [field : SerializeField] public Health Health {get; private set;}
        [field : SerializeField] public Attack Attack {get; private set;}

        private float BonusSpeed;
        
        private Sequence weaponSeq;
        
        private Vector2 moveInput;
        private Vector2 attackInput;

        private const float MIN_THRESHOLD = 0.001f;
        
        private bool isAttacking => attackInput.sqrMagnitude > MIN_THRESHOLD;
        private bool isMoving => moveInput.sqrMagnitude > MIN_THRESHOLD;
        
        public static Player Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
            }
            
            if (entityData == null)
            {
                Debug.LogError("EntityData is not assigned in Player script.");
                return;
            }

            Health.Initialize(entityData);
            Attack.Initialize(entityData);
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
            if (!isMoving) return;
            
            Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0).normalized;
            transform.Translate(movement * (entityData.Speed * Time.deltaTime), Space.World);
        }

        public void UpdateMovementSpeed(float value)
        {
            Debug.Log($"BonusSpeed before: {BonusSpeed}");
            BonusSpeed += value;
            Debug.Log($"BonusSpeed after: {BonusSpeed}");
        }
        
        private void HandleAttack()
        {
            if (!isAttacking) return;
            
            RotatePlayer();
            
            if (Attack.CooldownTimer > 0) return;
            
            PerformWeaponAnimation();
            
            RaycastHit2D[] hits = CalculateAttackHits();
            List<Health> targets = new List<Health>(hits.Length);
                
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                bool isOwner = hit.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID();
                targets.Add(isOwner ? null : hit.collider.GetComponent<Health>());
            }

            Attack.PerformAttack(targets);
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
            float angle = Mathf.Atan2(attackInput.y, attackInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }

        public void OnMoveInput(InputAction.CallbackContext ctx)
        {
            moveInput = ctx.ReadValue<Vector2>();
        }
        
        public void OnAttackInput(InputAction.CallbackContext ctx)
        {
            attackInput = ctx.ReadValue<Vector2>();
        }
    }
}