using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Hazard : MonoBehaviour
{
    Vector3 rotation;

    [SerializeField] private ParticleSystem breakingEffect;
    [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;
    private Player player;

    private void Start()
    {
        var xRotation = Random.Range(-180f, 180f);
        rotation = new Vector3(xRotation, 0);

        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Hazard"))
        {
            Destroy(gameObject);
            Instantiate(breakingEffect, transform.position, Quaternion.identity);

            if (player != null)
            {
                var distance = Vector3.Distance(transform.position, player.transform.position);
                var force = 1 / distance;

                cinemachineImpulseSource.GenerateImpulse(force);
            }
            
        }
    }

}
