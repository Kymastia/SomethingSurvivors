using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private Mob mobPrefab;
    private PlayerMovement player;

    private IEnumerator Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        if (!player)
        {
            enabled = false;
        }

        foreach (var position in EnumerateSpawnPositions())
        {
            yield return new WaitForSeconds(1.0f);
            var instance = Instantiate(mobPrefab, position, new Quaternion(0, 0, 0, 0));
            instance.GetComponentInChildren<Mob>().SetTarget(player.transform);
        }
    }

    private IEnumerable<Vector2> EnumerateSpawnPositions()
    {
        const int distanceFromPlayer = 10;
        while (true)
        {
            Vector3 direction = Random.insideUnitCircle;
            direction.Normalize();
            yield return player.transform.position + direction * distanceFromPlayer;
        }
    }
}
