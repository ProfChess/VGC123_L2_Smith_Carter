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
    public AudioClip PickupSound;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            switch (currentPickup)
            {
                case PickupType.Powerup:
                    collision.gameObject.GetComponent<PlayerController>().StartJumpForceChange();
                    break;

                case PickupType.Life:
                    GameManager.Instance.lives++;
                    break;

                case PickupType.Score:
                    
                    break;

            }

            if (PickupSound)
            {
                collision.gameObject.GetComponent<AudioSourceManager>().PlayOneShot(PickupSound, false);
            }

            Destroy(gameObject);
        }
    }
}
