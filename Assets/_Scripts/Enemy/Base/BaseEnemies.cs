    using System.Collections;
    using DG.Tweening;
    using UnityEngine;

    public class BaseEnemies : MonoBehaviour, IDamageable
    {
        public Rigidbody2D Rb { get; set; }
        public Animator Anim { get; set; }
        private Collider2D Coll { get; set; }

        [SerializeField] private Material blinkMaterial;
        private Material _runtimeMaterial;
        private int _blinkStrengthID;

        public int faceDirection = -1;
        public int CurrentHealth { get; set; }

        protected Vector2 StartPos;
        protected Transform Target;
        protected int chaseDirection = 0;
        protected float AttackTimer = 0f;
        protected Coroutine _loseTargetCoroutine;

        [SerializeField] protected BaseEnemiesData baseEnemiesData;
        protected float MoveSpeed;

        public enum State { Patrol, Chasing, Dead }
        protected State CurrentState = State.Patrol;

        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            Anim = GetComponent<Animator>();
            Coll = GetComponent<Collider2D>();
            _runtimeMaterial = new Material(blinkMaterial);
            GetComponent<SpriteRenderer>().material = _runtimeMaterial;
            _blinkStrengthID = Shader.PropertyToID("_BlinkStrength");
            CurrentHealth = baseEnemiesData.health;
        }

        protected virtual void Start()
        {
            StartPos = transform.position;
            MoveSpeed = baseEnemiesData.moveSpeed;
        }

        protected virtual void Update()
        {
            DetectPlayer();
        }

        private void FixedUpdate()
        {
            if (CurrentState == State.Dead) return;
            HandleState();
        }

        private void HandleState()
        {
            switch (CurrentState)
            {
                case State.Patrol:
                    Anim.SetBool("Attack", false);
                    Anim.SetBool("Patrol", true);
                    Patrol();
                    if (_loseTargetCoroutine != null)
                    {
                        StopCoroutine(_loseTargetCoroutine);
                        _loseTargetCoroutine = null;
                    }
                    break;

                case State.Chasing:
                    Anim.SetBool("Patrol", false);
                    Attack();
                    break;
            }
        }

        protected void Flip()
        {
            faceDirection *= -1;
            Rb.transform.Rotate(0f, 180f, 0f);
        }

        protected void CheckIfShouldFlip()
        {
            if ((faceDirection == -1 && transform.position.x <= StartPos.x - baseEnemiesData.patrolRange) ||
                (faceDirection == 1 && transform.position.x >= StartPos.x + baseEnemiesData.patrolRange))
            {
                Flip();
            }

            if (IsWallAhead())
            {
                Flip();
            }
        }

        protected bool IsWallAhead()
        {
            Vector2 wallCheckOrigin = transform.position + Vector3.right * faceDirection * 0.5f;
            RaycastHit2D wallHit = Physics2D.Raycast(wallCheckOrigin, Vector2.right * faceDirection, 0.3f, baseEnemiesData.groundMask);
            return wallHit.collider != null;
        }

        protected virtual void Patrol()
        {
            Rb.velocity = new Vector2(faceDirection * MoveSpeed, Rb.velocity.y);
            CheckIfShouldFlip();
        }

        protected virtual void Attack()
        {
            Anim.SetBool("Attack", true);

            float chaseDistance = Mathf.Abs(transform.position.x - StartPos.x);
            if (chaseDistance >= baseEnemiesData.patrolRange)
            {
                CurrentState = State.Patrol;
                return;
            }

            if (chaseDirection != 0)
            {
                Rb.velocity = new Vector2(chaseDirection * MoveSpeed, Rb.velocity.y);

                if (faceDirection != chaseDirection)
                {
                    Flip();
                }
            }

            if (IsWallAhead())
            {
                CurrentState = State.Patrol;
            }
        }



        protected virtual void DetectPlayer()
        {
            AttackTimer -= Time.deltaTime;

            Vector2 origin = transform.position;
            Vector2 direction = faceDirection == -1 ? Vector2.left : Vector2.right;

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, baseEnemiesData.detectRange, baseEnemiesData.playerMask);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                chaseDirection = faceDirection;
                
                if (faceDirection != chaseDirection)
                    Flip();

                CurrentState = State.Chasing;

                if (_loseTargetCoroutine != null)
                {
                    StopCoroutine(_loseTargetCoroutine);
                    _loseTargetCoroutine = null;
                }
            }
            else if (CurrentState == State.Chasing && _loseTargetCoroutine == null)
            {
                _loseTargetCoroutine = StartCoroutine(DelayToReturnToPatrol());
            }

            Debug.DrawRay(origin, direction * baseEnemiesData.detectRange, Color.red);
        }




        protected IEnumerator DelayToReturnToPatrol()
        {
            yield return new WaitForSeconds(2f);
            CurrentState = State.Patrol;
            _loseTargetCoroutine = null;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (CurrentState == State.Dead) return;

            if (other.gameObject.CompareTag("Player") && AttackTimer <= 0f)
            {
                AttackEffect(other);
            }
        }

        protected virtual void AttackEffect(Collision2D other)
        {
        }

        public virtual void Dead()
        {
            if (CurrentState == State.Dead) return;

            CurrentState = State.Dead;
            Coll.enabled = false;
            Destroy(gameObject, baseEnemiesData.destroyAfter);
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, baseEnemiesData.health);
            Debug.Log("Enemy" + CurrentHealth);
            StartCoroutine(DamageAnimation());
            if (CurrentHealth == 0)
                Dead();
        }

        IEnumerator DamageAnimation()
        {
            Tween blinkTween = DOTween.To(
                () => _runtimeMaterial.GetFloat(_blinkStrengthID),
                x => _runtimeMaterial.SetFloat(_blinkStrengthID, x),
                1f,
                0.1f)
            .SetLoops(2, LoopType.Yoyo)
            .OnComplete(() => _runtimeMaterial.SetFloat(_blinkStrengthID, 0f));

            yield return new WaitForSeconds(0.25f);
            if (CurrentState == State.Dead)
            {
                blinkTween.Kill();
            }
        }
    }
