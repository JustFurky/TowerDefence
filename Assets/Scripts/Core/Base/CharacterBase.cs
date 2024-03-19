using TD.Managers;
using DG.Tweening;
using UnityEngine;

namespace TD.Characters
{
    public abstract class CharacterBase : MonoBehaviour
    {
        private const float _kanimTime = .25f;

        public Transform Target;

        protected int health;
        protected int damage;
        protected float range;
        protected float speed;
        protected float fireRate;
        protected CharacterSide side;
        protected int currency;
        protected float time;
        [SerializeField] protected CharacterData characterData;


        private Tween scaleTween;
        private Tween colorTween;

        [SerializeField] private MeshRenderer renderer;
        [SerializeField] private Vector3 defaultScale;

        protected virtual void Awake()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            health = characterData.Health;
            damage = characterData.Damage;
            range = characterData.Range;
            speed = characterData.Speed;
            fireRate = characterData.FireRate;
            side = characterData.Side;
            currency = characterData.Currency;
        }

        public abstract void Move();

        public abstract void Attack();

        public virtual void TakeDamage(int damageAmount)
        {
            TweenKiller();

            health -= damageAmount;
            if (health <= 0)
                Die();
            else
            {
                TakeDamageAnimation();
            }
        }

        private void TakeDamageAnimation()
        {
            scaleTween = transform.DOPunchScale(new Vector3(_kanimTime, _kanimTime, _kanimTime), _kanimTime, 1, 1);
            colorTween = renderer.material.DOColor(Color.red, _kanimTime).OnComplete(() => colorTween = renderer.material.DOColor(Color.white, _kanimTime));
        }

        private void TweenKiller()
        {
            scaleTween.Kill();
            colorTween.Kill();
            transform.localScale = defaultScale;
        }

        protected virtual void Die()
        {
            DataManager.Instance.SetCurrency(currency);
            Destroy(gameObject);
        }

        protected bool IsTargetInRange(Transform target)
        {
            return Vector3.Distance(transform.position, target.position) <= range;
        }


        public virtual void OnFixedUpdated()
        {

        }


        private void OnEnable()
        {
            CoreManager.FixedUpdateTick += OnFixedUpdated;
        }
        private void OnDisable()
        {
            CoreManager.FixedUpdateTick -= OnFixedUpdated;
        }
    } 
}