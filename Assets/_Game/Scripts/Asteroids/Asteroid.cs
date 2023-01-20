using DefaultNamespace.ScriptableEvents;
using UnityEngine;
using UnityEngine.UIElements;
using Variables;
using Random = UnityEngine.Random;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private ScriptableEventInt _onAsteroidDestroyed;

        //Edited by William Isacsson
        [Header("Scriptable Object:")]
        public AsteroidType asteroidType;

        //Edited by William Isacsson
        [Header("Sprite Rendere:")]
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Header("References:")]
        [SerializeField] private Transform _shape;

        private Rigidbody2D _rigidbody;
        private Vector3 _direction;
        private int _instanceId;
        private GameObject tempAsteroid;

        static readonly int shaderPropertyColor = Shader.PropertyToID("_Color");
        private MaterialPropertyBlock _materialPropertyBlock;
        public MaterialPropertyBlock MaterialPropertyBlock
        {
            get
            {
                if (_materialPropertyBlock == null)
                {
                    _materialPropertyBlock = new MaterialPropertyBlock();
                    //return materialPropertyBlock;
                }

                return _materialPropertyBlock;
            }

        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _instanceId = GetInstanceID();

            SetDirection();
            AddForce();
            AddTorque();
            SetSize();
            SetColor();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (string.Equals(other.tag, "Laser"))
            {
                Destroy(other.gameObject);
                HitByLaser();
            }
        }

        private void HitByLaser()
        {
            _onAsteroidDestroyed.Raise(_instanceId);
            SpawnAsteroids();

            Destroy(gameObject);
        }

        //Method created by William Isacsson
        private void SpawnAsteroids()
        {

            switch (asteroidType.asteroidSize)
            {
                case AsteroidType.AsteroidSize.Large:
                    tempAsteroid = AsteroidHolder.asteroidPrefabs[1];
                    break;

                case AsteroidType.AsteroidSize.Medium:
                    tempAsteroid = AsteroidHolder.asteroidPrefabs[2];
                    break;

                case AsteroidType.AsteroidSize.Small:
                    tempAsteroid = null;
                    break;

            }

            if (tempAsteroid != null)
            {
                for (int i = 0; i < asteroidType.smallerPartsAmount; i++)
                {
                    Instantiate(tempAsteroid, transform.position, Quaternion.identity);
                }

            }
        }

        // TODO Can we move this to a single listener, something like an AsteroidDestroyer?
        public void OnHitByLaser(IntReference asteroidId)
        {
            if (_instanceId == asteroidId.GetValue())
            {

                Destroy(gameObject);
            }
        }

        public void OnHitByLaserInt(int asteroidId)
        {
            if (_instanceId == asteroidId)
            {
                Destroy(gameObject);
            }
        }

        private void SetColor()
        {
            MaterialPropertyBlock.SetColor(shaderPropertyColor, asteroidType.asteroidColor);
            _spriteRenderer.SetPropertyBlock(MaterialPropertyBlock);
        }

        private void SetDirection()
        {
            var size = new Vector2(3f, 3f);
            var target = new Vector3
            (
                Random.Range(-size.x, size.x),
                Random.Range(-size.y, size.y)
            );

            _direction = (target - transform.position).normalized;
        }

        private void AddForce()
        {
            var force = Random.Range(asteroidType.minForce, asteroidType.maxForce);
            _rigidbody.AddForce(_direction * force, ForceMode2D.Impulse);
        }

        private void AddTorque()
        {
            var torque = Random.Range(asteroidType.minTorque, asteroidType.maxTorque);
            var roll = Random.Range(0, 2);

            if (roll == 0)
                torque = -torque;

            _rigidbody.AddTorque(torque, ForceMode2D.Impulse);
        }

        private void SetSize()
        {
            var size = Random.Range(asteroidType.minSize, asteroidType.maxSize);
            _shape.localScale = new Vector3(size, size, 0f);
        }
    }
}
