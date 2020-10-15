using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour

{
    PlayerController stats;

    // Use this for initialization
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.transform.tag == "player")
        {
            switch (this.transform.tag)
            {
                case ("hp"):
                    if (stats.health == 100)
                    {
                        // we don't want to waste it so don't do anything
                    }
                    else if (stats.health < 100)
                    {
                        stats.health += 25;
                        Destroy(this.gameObject);
                    }
                    break;
                case ("ammo"):
                    stats.savedAmmo++;
                    Destroy(this.gameObject);
                    break;


            }
        }
    }
}
