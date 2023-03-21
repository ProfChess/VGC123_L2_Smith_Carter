using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Shoot))]
public class EnemyTurret : Enemy
{
    public float projectileFireRate;
    float timeSinceLastFire;
    Shoot shootScript;
    float distance;
    
    
    



    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curClips = anim.GetCurrentAnimatorClipInfo(0);
        
        if (curClips[0].clip.name != "Shoot")
        {
           
            if (Time.time > timeSinceLastFire + projectileFireRate)
            {
                    anim.SetTrigger("Shoot");
            }
            
        }
    }

    public override void Start()
    {
        

        base.Start();

        shootScript = GetComponent<Shoot>();
        shootScript.OnProjectileSpawned.AddListener(UpdateTimeSinceLastFire);
    }

    public override void Death()
    {
        
        Destroy(gameObject);
    }



    private void OnDisable()
    {
        shootScript.OnProjectileSpawned.RemoveListener(UpdateTimeSinceLastFire);
    }

    void UpdateTimeSinceLastFire()
    {
        timeSinceLastFire = Time.time;

    }
}
