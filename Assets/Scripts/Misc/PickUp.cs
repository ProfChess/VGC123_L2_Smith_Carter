using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public enum PickupType
    {
        Powerup = 0,
        Life = 1,
        Score = 2
    }

    public PickupType currentPickup;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController temp = collision.gameObject.GetComponent<PlayerController>();
            
            switch (currentPickup)
            {
                case PickupType.Powerup:
                    temp.StartJumpForceChange();
                    break;

                case PickupType.Life:
                    temp.lives++;
                    break;

                case PickupType.Score:
                    temp.score++;
                    break;

            }

            Destroy(gameObject);
        }
    }
}
