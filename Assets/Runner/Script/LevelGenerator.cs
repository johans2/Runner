using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public GameParams gameParams;
    public List<GameObject> blockPrefabs;

    private List<GameObject> blockPool = new List<GameObject>();
    private List<GameObject> activeBlocks = new List<GameObject>();
    private GameObject lastActiveBlock = null;

    private const int numStartBlocks = 3;
    private Plane[] mainCamFrustrumPlanes;

    void Start() {
        mainCamFrustrumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        InitializeBlocks();
        SpawnStartBlocks();
    }

    void InitializeBlocks() {
        for(int i = 0; i < blockPrefabs.Count; i++) {
            GameObject block = Instantiate(blockPrefabs[i]);
            block.SetActive(false);
            blockPool.Add(block);
        }
    }

    void SpawnStartBlocks() {
        for(int i = 0; i < numStartBlocks; i++) {
            SpawnBlock();
        }
    }
    
    void Update() {
        for(int i = 0; i < activeBlocks.Count; i++) {
            activeBlocks[i].transform.position -= new Vector3(gameParams.RunSpeed * Time.deltaTime, 0, 0);
        }
        
        var firstBlock = activeBlocks[0];

        if(!GeometryUtility.TestPlanesAABB(mainCamFrustrumPlanes, firstBlock.GetComponent<SpriteRenderer>().bounds)) {
            firstBlock.SetActive(false);
            activeBlocks.Remove(firstBlock);
            blockPool.Add(firstBlock);
            SpawnBlock();
        }
    }

    void SpawnBlock() {
        var block = GetRandomPooledBlock();
        if(lastActiveBlock == null) {
            block.transform.position = Vector2.zero;
        }
        else {
            Vector2 lastBlockPos = lastActiveBlock.GetComponent<SpriteRenderer>().bounds.max;
            Vector2 spawnPos = new Vector2(lastBlockPos.x + block.GetComponent<SpriteRenderer>().bounds.extents.x, 0);
            block.transform.position = spawnPos;
        }
        activeBlocks.Add(block);
        block.SetActive(true);
        lastActiveBlock = block;
    }
    
    GameObject GetRandomPooledBlock() {
        var block = blockPool[Random.Range(0, blockPool.Count)];
        blockPool.Remove(block);
        return block;
    }


}
