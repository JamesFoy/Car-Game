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
            cars.Add(go); //adds every object with the tag of player to the list

            //finds the closest object to the current position and assigns it to closest target varibale
            float diff = (go.transform.position - transform.position).sqrMagnitude;

            if (diff < distanceClosestCar)
            {
                distanceClosestCar = diff;

                closestTarget = go;
            }
        }

        //Method for removing the closest target
        RemoveClosestCar(closestTarget);

        //goes through the new list of cars (with the car that shot the ability being removed from the list) and assigns the target for the missle to the closest one
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

    //Method for removing the closest target
    private void RemoveClosestCar(GameObject closestCar)
    {
        cars.Remove(closestTarget);
    }

    //Fires the rocket if it has a target assigned
    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }

        //moves the missile forward
        rb.velocity = transform.forward * missileVelocity;

        //Finds the targetRotation for the missile
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);

        //Assigns the targetRotation of the missile to it (makes the missle turn towards the target while travelling)
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));
    }

    //Method for when the missile collides with the player or other object
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

        //Destorys the missile and spawns the explosion effect
        GameObject clonedExplosion = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
