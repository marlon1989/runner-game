using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLineSpawner : MonoBehaviour
{
    [SerializeField] private Pickup pickupPrefab;
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    [SerializeField] private float spaceBetweenPickups = 0.5f;

    public void SpawnPickupLine(Vector3[] skipPositions)
    {
        Vector3 currentSpawnPosition = start.position;

        while(currentSpawnPosition.z < end.position.z)
        {
            if(!ShouldSkipPosition(currentSpawnPosition, skipPositions))
            {
                Pickup pickup = Instantiate(pickupPrefab, currentSpawnPosition, Quaternion.identity, transform);
            }
            currentSpawnPosition.z += spaceBetweenPickups;
        }
    }

    private bool ShouldSkipPosition(Vector3 currentSpawnPosition, Vector3[] skipPositions)
    {
        foreach(var skipPosition in skipPositions)
        {
            float skipStart = skipPosition.z - spaceBetweenPickups * 0.5f;
            float skipEnd = skipPosition.z + spaceBetweenPickups * 0.5f;

            if(currentSpawnPosition.z >= skipStart && currentSpawnPosition.z <= skipEnd)
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Vector3 currentSpawnPosition = start.position;

        while(currentSpawnPosition.z < end.position.z)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(currentSpawnPosition, Vector3.one);
            currentSpawnPosition.z += spaceBetweenPickups;
        }
    }
}
