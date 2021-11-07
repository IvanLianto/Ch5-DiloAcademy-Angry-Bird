using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 50f;

    public UnityAction<GameObject> OnEnemyDestroyed = delegate { };

    private bool isHit = false;

    private void OnDestroy()
    {
        if (isHit)
        {
            OnEnemyDestroyed(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;

        if (collision.gameObject.GetComponent<Rigidbody2D>() == null) return;

        if (tag == "Bird")
        {
            isHit = true;
            Destroy(gameObject);
        }
        else if (tag == "Obstacle")
        {
            float damage = collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            health -= damage;
            if (health <= 0)
            {
                isHit = true;
                Destroy(gameObject);
            }
        }
    }
}
