using System;
using UnityEngine;

namespace DSS.Entities
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private EntityData entityData;
        [SerializeField] private Health health;
        [SerializeField] private Attack attack;
        
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
                Vector2 origin = transform.position;
                Vector2 direction = transform.up;

                float range = entityData.AttackRange;

                RaycastHit2D hit = Physics2D.CircleCast(origin, range, direction, range);

                if (hit.collider != null && hit.collider.gameObject.GetInstanceID() != gameObject.GetInstanceID())
                {
                    attack.PerformAttack(hit.collider.GetComponent<Health>());
                }

                // Optional: visualize the attack area
                Debug.DrawRay(origin, direction * range, Color.red, 0.5f);
            }
        }
    }
}