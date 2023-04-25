using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class LocationHP : NetworkBehaviour
{

    public bool _isDead;
    public float _currentHealth;
    public float startingHealth = 100f;

    public GameObject explosionPrefab;
    public GameObject WholeManager;

    private AudioSource _explosionAudio;
    private ParticleSystem _explosionParticles;

    //private float _currentHealth;
    //private bool _isDead;

    private void Awake()
    {
        _explosionParticles = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        _explosionAudio = _explosionParticles.GetComponent<AudioSource>();

        _explosionParticles.gameObject.SetActive(false);
    }
    public override void FixedUpdateNetwork()
    {
        if (_currentHealth <= 0f && !_isDead)
            OnDeath();
    }

    // pt.  OnEnable() play before Start()
    private void Start()
    {
        WholeManager = GameObject.Find("WholeManager");
        WholeManager.GetComponent<WholeManager>().AddLocation(this.gameObject);

        Respawn();
    }
    public void Respawn()
    {
        _currentHealth = startingHealth;
        _isDead = false;

    }
    public void TakeDamage(float amount) // change to fixUpdate()
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        _currentHealth -= amount;
        print("hit!");


        // change to fixUpdate()
        /* if (_currentHealth <= 0f && !_isDead)
            OnDeath(); */
    }
    public void TakeHP(float amount) // change to fixUpdate()
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        _currentHealth += amount;


        // change to fixUpdate()
        /* if (_currentHealth <= 0f && !_isDead)
            OnDeath(); */
    }

   

    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        _isDead = true;

        _explosionParticles.transform.position = this.transform.position;
        _explosionParticles.gameObject.SetActive(true);
        _explosionParticles.Play();
        this.GetComponent<WholeManager>().SetLocation(this.gameObject);
        _explosionAudio.Play();
        //this.gameObject.SetActive(false);
    }

    static void OnStateChange(Changed<LocationHP> changed)
    {
        print(changed.Behaviour._isDead);
        if (changed.Behaviour._isDead)
        {
            changed.Behaviour.WholeManager.GetComponent<WholeManager>().SetLocation(changed.Behaviour.gameObject);
        }
    }
}
