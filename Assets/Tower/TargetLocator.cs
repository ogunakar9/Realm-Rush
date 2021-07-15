using UnityEngine;

    public class TargetLocator : MonoBehaviour
    {
        [SerializeField] Transform weapon;
        [SerializeField] ParticleSystem projectileParticles;
        [SerializeField] float range = 15f;
        Transform target;
    
        void Update()
        {
            //If you expand game, since this method is expensive, try adding some conditions
            // maybe find target if an enemy dies or goes out of range
            FindClosestTarget();
            AimWeapon();
        }

        void FindClosestTarget()
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            Transform closestTarget = null;
            float maxDistance = Mathf.Infinity;

            foreach (Enemy enemy in enemies)
            {
                float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

                if (targetDistance < maxDistance)
                {
                    closestTarget = enemy.transform;
                    maxDistance = targetDistance;
                }
            }

            target = closestTarget;
        }

        void AimWeapon()
        {
            float targetDistance = Vector3.Distance(transform.position, target.position);
            
            weapon.LookAt(target);

            if (targetDistance < range)
            {
                Attack(true);
            }
            else
            {
                Attack(false);
            }
        }

        void Attack(bool isActive)
        {
            var emissionModule = projectileParticles.emission;
            emissionModule.enabled = isActive;
        }
    }

