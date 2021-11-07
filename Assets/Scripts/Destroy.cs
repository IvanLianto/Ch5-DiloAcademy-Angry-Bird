using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "Bird" || tag == "Enemy" || tag == "Obstacle")
        {
            Destroy(collision.gameObject);
        }
    }
}
