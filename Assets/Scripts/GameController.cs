using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private SlingShooter slingShooter;
    [SerializeField] private TrailController trailController;
    [SerializeField] private List<Birds> birds;

    [SerializeField] private List<Enemy> enemies;
    private bool isGameEnded = false;

    public BoxCollider2D tapCollider;

    [SerializeField]private Birds shotBird;

    void Start()
    {
        for (var i = 0; i < birds.Count; i++)
        {
            birds[i].OnBirdDestroyed += ChangeBird;
            birds[i].OnBirdShot += AssignTrail;
        }

        for (var i = 0; i < enemies.Count; i++)
        {
            enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        tapCollider.enabled = false;
        shotBird = birds[0];
        slingShooter.InitiateBird(birds[0]);
    }

    public void ChangeBird()
    {
        tapCollider.enabled = false;
        if (isGameEnded)
        {
            return;
        }

        birds.RemoveAt(0);

        if (birds.Count > 0)
        {
            slingShooter.InitiateBird(birds[0]);
        }
    }

    public void AssignTrail(Birds bird)
    {
        trailController.SetBird(bird);
        StartCoroutine(trailController.SpawnTrail());
        tapCollider.enabled = true;
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (var i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].gameObject == destroyedEnemy)
            {
                enemies.RemoveAt(i);
                break;
            }
        }

        if (enemies.Count == 0)
        {
            isGameEnded = true;
        }
    }

    private void OnMouseUp()
    {
        if (shotBird != null)
        {
            shotBird.OnTap();
        }
    }
}
