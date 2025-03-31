using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorProjectile : SpawnedAbility
{
    public float decayRate = 0.01f;
    public float detectionDistance = 0.1f;
    public override float LifeTime { get; set; }
    public int damage = 10;

    private readonly List<GameObject> _alreadyHit = new List<GameObject>();
    private Rigidbody _rb;
    private bool _stopped;

    public override void Initialize(float lifeTime)
    {
        LifeTime = lifeTime;
        _alreadyHit.Clear();
        if (_rb == null && GetComponent<Rigidbody>() != null)
        {
            _rb = GetComponent<Rigidbody>();
        }
        else Debug.Log("No rigidbody attached");
    }

    public override void Activate()
    {
        _stopped = false;
        transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        StartCoroutine(LifeDuration());
        StartCoroutine(SlowDown());
    }


    private void FixedUpdate()
    {
        if (_stopped) return;
        RaycastHit hit;
        Vector3 distance = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
        if (Physics.Raycast(distance, transform.TransformDirection(-Vector3.up), out hit, detectionDistance))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        Debug.DrawRay(distance,transform.TransformDirection(-Vector3.up*detectionDistance), Color.red);
    }

    private IEnumerator SlowDown()
    {
        float t = LifeTime;
        while (t < 0)
        {
            _rb.linearVelocity = Vector3.Lerp(Vector3.zero, _rb.linearVelocity, t);
            t -= decayRate;
            yield return new WaitForSeconds(0.1f);
        }

        _stopped = true;
    }

    private IEnumerator LifeDuration()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(LifeTime);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Enemy") || _alreadyHit.Contains(other.gameObject) ||
            other.gameObject.GetComponent<Damageable>() == null) return;
        other.gameObject.GetComponent<Damageable>().TakeDamage(damage);
        _alreadyHit.Add(other.gameObject);
    }
}