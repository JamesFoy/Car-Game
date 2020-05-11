using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
    public UnityEvent player1Pickup;
    public UnityEvent player2Pickup;

    MeshRenderer meshRenderer;
    BoxCollider boxCollider;

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject.GetComponentInParent<CarController>().thisNumber == CarController.PlayerNumber.p1)
        {
            StartCoroutine(Player1Pickup());
            Debug.Log("This is a PickUp!");
        }
        else if (other.CompareTag("Player") && other.gameObject.GetComponent<CarController>().thisNumber == CarController.PlayerNumber.p2)
        {
            StartCoroutine(Player2Pickup());
            Debug.Log("This is a PickUp!");
        }
    }

    IEnumerator Player1Pickup()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        player1Pickup.Invoke();
        yield return new WaitForSeconds(4f);
        meshRenderer.enabled = true;
        boxCollider.enabled = true;

        yield return null;
    }

    IEnumerator Player2Pickup()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        player2Pickup.Invoke();
        yield return new WaitForSeconds(4f);
        meshRenderer.enabled = true;
        boxCollider.enabled = true;

        yield return null;
    }
}