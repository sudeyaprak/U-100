using System.Collections;
using Enemy;
using Managers;
using UnityEngine;

namespace Weapons
{
    public class Ammo : MonoBehaviour
    {
        public PooledObject PooledObject;
        private float _damage;
        private float _range;
        private float _speed;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyController>().TakeDamage(_damage);
                ReturnToPool();
            }
        }

        public void SetAmmoProperty(float damage, float speed, float range)
        {
            _damage = damage;
            _speed = speed;
            _range = range;
        }

        public void ReturnToPool()
        {
            StopAllCoroutines();
            PooledObject.ReturnToPool();
        }

        public void Shoot(Vector2 direction)
        {
            StartCoroutine(ShootCoroutine(direction));
        }

        private IEnumerator ShootCoroutine(Vector2 direction)
        {
            var distance = 0f;
            while (distance < _range)
            {
                distance += _speed * Time.deltaTime;
                transform.Translate(direction * (_speed * Time.deltaTime));
                yield return null;
            }

            ReturnToPool();
        }
    }
}