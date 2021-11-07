using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Birds : MonoBehaviour
{
    public enum BirdState
    {
        Idle,
        Thrown,
        HitSomething
    }

    public GameObject parent;

    public UnityAction OnBirdDestroyed = delegate { };
    public UnityAction<Birds> OnBirdShot = delegate { };

    protected Rigidbody2D rb;
    private CircleCollider2D collider;

    private BirdState state;
    private float minVelocity = 0.05f;
    private bool flagDestroy = false;

    public BirdState State { get => state; }

    protected void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;
        collider.enabled = false;
        state = BirdState.Idle;
    }

    void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        if (state == BirdState.Idle && rb.velocity.sqrMagnitude >= minVelocity)
        {
            state = BirdState.Thrown;
        }

        if ((state == BirdState.Thrown || state == BirdState.HitSomething ) && rb.velocity.sqrMagnitude < minVelocity && !flagDestroy)
        {
            flagDestroy = true;
            StartCoroutine(DestroyAfter(2));
        }
    }

    private IEnumerator DestroyAfter(float second)
    {
        yield return new WaitForSeconds(second);
        Destroy(gameObject);
    }

    public void MoveTo(Vector2 target, GameObject parent)
    {
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.position = target;
    }

    public void Shoot(Vector2 velocity, float distance, float speed)
    {
        collider.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = velocity * speed * distance;
        OnBirdShot(this);
    }

    public virtual void OnTap() { }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        state = BirdState.HitSomething;
    }

    private void OnDestroy()
    {
        if (state == BirdState.Thrown || state == BirdState.HitSomething)
        {
            OnBirdDestroyed();
        }
    }
}
