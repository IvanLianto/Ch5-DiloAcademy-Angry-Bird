using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Birds
{
    [SerializeField] private float boostForce = 100f;
    
    public bool hasBoost = false;

    private void Start()
    {
        Init();
    }

    public void Boost()
    {
        if (State == BirdState.Thrown && !hasBoost)
        {
            rb.AddForce(rb.velocity * boostForce);
            hasBoost = true;
        }
    }

    public override void OnTap()
    {
        Debug.Log("tes");
        Boost();
    }

}
