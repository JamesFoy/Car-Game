using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler<T> : MonoBehaviour where T : MonoBehaviour
{
    public static ObjectPooler<T> Instance;
    public T prefabToPool;
    public List<T> poolOfAvailableInstances = new List<T>();
    public List<T> poolOfUsedInstances = new List<T>();
    protected void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void ExpandPool()
    {
        T newObject = Instantiate(prefabToPool);
        poolOfAvailableInstances.Add(newObject);
    }
    /// <summary>
    /// Fetches an object from the requested pool, and sets it active.
    /// </summary>
    public T GetObjectFromPool()
    {
        if (poolOfAvailableInstances.Count <= 0)
        {
            ExpandPool();
        }
        T objectToActivate = poolOfAvailableInstances[poolOfAvailableInstances.Count - 1];
        objectToActivate.gameObject.SetActive(true);

        poolOfAvailableInstances.Remove(objectToActivate);
        poolOfUsedInstances.Add(objectToActivate);
        return objectToActivate;
    }
    public void ReturnObjectToPool(T objectToReset)
    {
        objectToReset.transform.parent = transform;
        objectToReset.transform.localPosition = Vector3.zero;
        objectToReset.gameObject.SetActive(false);

        poolOfUsedInstances.Remove(objectToReset);
        poolOfAvailableInstances.Add(objectToReset);
    }
}