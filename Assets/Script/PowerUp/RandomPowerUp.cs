using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPowerUp : MonoBehaviour
{
    public List<GameObject> powerups;

    private void Start()
    {
        var randomPowerup = Random.Range(0, powerups.Count);
        Instantiate(powerups[randomPowerup], transform);
    }
}
