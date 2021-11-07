using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShooter : MonoBehaviour
{
    private CircleCollider2D collider;
    [SerializeField] private LineRenderer trajectory;
    private Vector2 startPos;

    [SerializeField]
    private float radius = 0.75f;

    [SerializeField]
    private float throwSpeed = 30f;

    private Birds bird;

    private void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        startPos = transform.position;
    }

    private void OnMouseUp()
    {
        collider.enabled = false;
        var velocity = startPos - (Vector2)transform.position;
        float distance = Vector2.Distance(startPos, transform.position);

        bird.Shoot(velocity, distance, throwSpeed);

        gameObject.transform.position = startPos;

        trajectory.enabled = false;
    }

    public void InitiateBird(Birds bird)
    {
        this.bird = bird;
        bird.MoveTo(gameObject.transform.position, gameObject);
        collider.enabled = true;
    }

    private void OnMouseDrag()
    {
        Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = p - startPos;
        if (dir.sqrMagnitude > radius)
        {
            dir = dir.normalized * radius;
        }
        transform.position = startPos + dir;

        float distance = Vector2.Distance(startPos, transform.position);

        if (!trajectory.enabled)
        {
            trajectory.enabled = true;
        }

        DisplayTrajectory(distance);

    }

    private void DisplayTrajectory(float distance)
    {
        if (bird == null)
        {
            return;
        }

        var velocity = startPos - (Vector2)transform.position;

        int segmentCount = 5;

        Vector2[] segments = new Vector2[segmentCount];

        segments[0] = transform.position;

        var segVelocity = velocity * throwSpeed * distance;

        for (var i = 1; i < segmentCount; i++)
        {
            float elapsedTime = i * Time.fixedDeltaTime * 5;
            segments[i] = segments[0] + segVelocity * elapsedTime + 0.5f * Physics2D.gravity * Mathf.Pow(elapsedTime, 2);
        }

        trajectory.positionCount = segmentCount;
        for (int i = 0; i < segmentCount; i++)
        {
            trajectory.SetPosition(i, segments[i]);
        }
    }
} 
