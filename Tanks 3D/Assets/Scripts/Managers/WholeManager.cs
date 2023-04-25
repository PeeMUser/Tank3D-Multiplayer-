using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class WholeManager : NetworkBehaviour
{
    [Networked] private TickTimer repawnDelay { get; set; }
    [Networked] private TickTimer repawnplaceDelay { get; set; }
    [Networked] private TickTimer hpBoxDelay { get; set; }

    [Networked] private TickTimer bombsDelay { get; set; }

    public List<GameObject> allPlayers = new List<GameObject>();
    public List<GameObject> allLocation = new List<GameObject>();

    public HPbox hpbox;
    public Bomb bomb;

    public override void FixedUpdateNetwork()
    {
        if (hpBoxDelay.ExpiredOrNotRunning(Runner))
        {
            hpBoxDelay = TickTimer.CreateFromSeconds(Runner, 3f);
            Runner.Spawn(hpbox, new Vector3(Random.Range(-20, 20), 1, Random.Range(-20, 20)), Quaternion.identity, Object.InputAuthority, (runner, o) => { o.GetComponent<HPbox>().Init(); });
        }
        if (bombsDelay.ExpiredOrNotRunning(Runner))
        {
            bombsDelay = TickTimer.CreateFromSeconds(Runner, 5f);
            Runner.Spawn(bomb, new Vector3(Random.Range(-20, 20), 1, Random.Range(-20, 20)), Quaternion.identity, Object.InputAuthority, (runner, o) => { o.GetComponent<Bomb>().Init(); });
        }
    }
    public void AddPlayer(GameObject player)
    {
        allPlayers.Add(player);
        print(player.name + "added!!");
    }
    public void AddLocation(GameObject location)
    {
        allLocation.Add(location);
        print(location.name + "added!!");
    }

    public void SetPlayer(GameObject player)
    {
        if (allPlayers.Contains(player))
        {
            player.SetActive(false);
            repawnDelay = TickTimer.CreateFromSeconds(Runner, 3f);
            StartCoroutine(respawnPlayer(player));
        }
    }
    public void SetLocation(GameObject location)
    {
        if (allLocation.Contains(location))
        {
            location.SetActive(false);
            repawnplaceDelay = TickTimer.CreateFromSeconds(Runner, 15f);
            StartCoroutine(respawnLocation(location));
        }
    }

    IEnumerator respawnPlayer(GameObject player)
    {
        yield return new WaitUntil(() => repawnDelay.ExpiredOrNotRunning(Runner));
        player.transform.position = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
        player.SetActive(true);
        player.GetComponent<TankHealth>().Respawn();

    }
    IEnumerator respawnLocation(GameObject location)
    {
        yield return new WaitUntil(() => repawnDelay.ExpiredOrNotRunning(Runner));
        
        location.SetActive(true);
        location.GetComponent<LocationHP>().Respawn();

    }
}
