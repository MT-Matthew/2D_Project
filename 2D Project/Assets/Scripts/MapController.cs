using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    public Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    PlayerController pm;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist; //Must be greater than the length and width of the tilemap
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur;

    Dictionary<Vector3, GameObject> spawnedChunkPositions = new Dictionary<Vector3, GameObject>();



    void Start()
    {
        pm = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimzer();
    }

    void ChunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }

        if (pm.direction.x > 0 && pm.direction.y == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;  //Right
                SpawnChunk(noTerrainPosition);
            }
        }
        else if (pm.direction.x < 0 && pm.direction.y == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;    //Left
                SpawnChunk(noTerrainPosition);
            }
        }
        else if (pm.direction.y > 0 && pm.direction.x == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Top").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Top").position; //Up
                SpawnChunk(noTerrainPosition);
            }
        }
        else if (pm.direction.y < 0 && pm.direction.x == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;    //Down
                SpawnChunk(noTerrainPosition);
            }
        }
        else if (pm.direction.x > 0 && pm.direction.y > 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Top Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Top Right").position;   //Right up
                SpawnChunk(noTerrainPosition);
            }
        }
        else if (pm.direction.x > 0 && pm.direction.y < 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down Right").position;  //Right down
                SpawnChunk(noTerrainPosition);
            }
        }
        else if (pm.direction.x < 0 && pm.direction.y > 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Top Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Top Left").position;  //Left up
                SpawnChunk(noTerrainPosition);
            }
        }
        else if (pm.direction.x < 0 && pm.direction.y < 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down Left").position; //Left down
                SpawnChunk(noTerrainPosition);
            }
        }
    }

    void SpawnChunk(Vector3 pos)
    {
        if (!spawnedChunkPositions.ContainsKey(pos))
        {
            int rand = Random.Range(0, terrainChunks.Count);
            latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
            spawnedChunks.Add(latestChunk);
            spawnedChunkPositions.Add(pos, latestChunk);
        }
    }

    void ChunkOptimzer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;   //Check every 1 second to save cost, change this value to lower to check more times
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
