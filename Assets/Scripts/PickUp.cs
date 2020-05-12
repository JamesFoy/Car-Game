using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
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
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<GameEventListener>().Event.Raise();
            StartCoroutine(TogglePickup());
        }
    }

    IEnumerator TogglePickup()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(4f);
        meshRenderer.enabled = true;
        boxCollider.enabled = true;

        yield return null;
    }
}