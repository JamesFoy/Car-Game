using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    float missileVelocity = 30f;
    float turn = 20;
    float distanceClosestCar = Mathf.Infinity;
    float distance = Mathf.Infinity;

    public List<GameObject> cars = new List<GameObject>();

    Transform target;
    GameObject closestTarget;
    Rigidbody rb;

    public GameObject explosion;
    CarInfo carInfo;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Fire();
    }

    //Method that will find the closest target and assign it to the target transform
    private void Fire()
    {


        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            cars.Add(go);  

            float diff = (go.transform.position - transform.position).sqrMagnitude;

            if (diff < distanceClosestCar)
            {
                distanceClosestCar = diff;

                closestTarget = go;

            }
        }

        RemoveClosestCar(closestTarget);

        foreach (GameObject car in cars)
        {
            float diffCars = (car.transform.position - transform.position).sqrMagnitude;

            if (diffCars < distance)
            {
                distance = diffCars;

                target = car.transform;
            }
        }
    }

    private void RemoveClosestCar(GameObject closestCar)
    {
        cars.Remove(closestTarget);
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }

        rb.velocity = transform.forward * missileVelocity;

        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);

        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            carInfo = other.gameObject.GetComponent<CarInfo>();

            if (!carInfo.carStats.isShieldEnabled)
            {
                carInfo.carStats.health += 10;
            }
        }

        GameObject clonedExplosion = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
