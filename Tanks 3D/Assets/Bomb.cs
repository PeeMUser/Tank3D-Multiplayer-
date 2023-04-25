using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Bomb : NetworkBehaviour
{
    [Networked] private TickTimer life { get; set; }
    private List<LagCompensatedHit> areaHits = new List<LagCompensatedHit>();
    public LayerMask collision;
    public ParticleSystem explosionParticles;

    public void Init()
    {
        life = TickTimer.CreateFromSeconds(Runner, 30.0f);

    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner))
        {
            Runner.Despawn(Object);

        }
        else
        {
            checkCollision();
        }
    }
    private void checkCollision()
    {
        print("pppwww");
        int col = Runner.LagCompensation.OverlapSphere(transform.position, 1.0f, Object.InputAuthority, areaHits, collision, HitOptions.IncludePhysX);
        if (col > 0)
        {
            GameObject player = areaHits[0].GameObject;
            print("ppp");
            if (player)
            {
                TankHealth target = player.GetComponent<TankHealth>();
                if (target != null)
                {

                    target.TakeDamage(1000);
                    explosionParticles.Play();
                    Destroy(explosionParticles.gameObject, 3f);
                    print("HEAL!!");
                    Runner.Despawn(Object);
                }
            }
        }

    }
}
