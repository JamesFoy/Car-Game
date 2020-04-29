using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
    public UnityEvent triggerPickup;

    MeshRenderer meshRenderer;
    BoxCollider boxCollider;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 90 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(ObjectPickedUp());
            Debug.Log("This is a PickUp!");
        }
    }

    IEnumerator ObjectPickedUp()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        triggerPickup.Invoke();
        yield return new WaitForSeconds(4f);
        meshRenderer.enabled = true;
        boxCollider.enabled = true;

        yield return null;
    }
}