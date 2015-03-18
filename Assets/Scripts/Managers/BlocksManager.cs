using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlocksManager : Singleton<BlocksManager> {
    #region Attributs publics
    public int offset = 7; // Number of lines / columns that must be generated around the player
    public int oreBlockProba = 10;  // Proba of ore block spawn (%) in the base
    public float vehicleBlockProba = 0.5f; // Proba of vehicle blocks spawning (%)
    public float explosiveBlockProba = 20f; //Proba of explosive blocks spawning
    public int chunkProba = 40;
    public int undestructibleChunkProba = 10;
    public int chunkOre1proba = 60;
    public int chunkOre2proba = 30;
    public int chunkOre3proba = 10;
    public int chunk2blocksProba = 10;
    public int chunk3blocksProba = 10;
    public int chunk4blocksProba = 25;
    public int chunk5blocksProba = 20;
    public int chunk6blocksProba = 12;
    public int chunk7blocksProba = 10;
    public int chunk8blocksProba = 8;
    public int chunk9blocksProba = 5;
    public int emptyBlocksForVehicle = 30;
    
    public Player player;
    public GameObject blocksContainerObject;
    public GameObject emptyBlocPrefab;
    public GameObject explosiveBlockPrefab;
    public GameObject ore1BlocPrefab;
    public GameObject ore2BlocPrefab;
    public GameObject ore3BlocPrefab;
    public GameObject undestructibleBlockPrefab;
    public GameObject vehicleBlockPrefab;
    //public GameObject orePrefab;
    //public GameObject vehiclePrefab;
    #endregion

    #region Attributs privés
    //private int nbLines = 0;    // Current number of lines of blocks (dynamic)
    //private int nbColumns = 0;  // Current number of columns of blocks (dynamic)
    private Dictionary<Vector2, GameObject> blockObjects;
    private int emptyBlocks = 0;
    //private Transform blocksContainerTransform;
    #endregion

    #region Méthodes publiques
    public void DestroyBlock (GameObject blockObject) {
        // Boom !
        /*GameObject explosion = Instantiate (explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy (explosion, 2);*/
        //BoomManager.Instance.Boom (transform.position);

        // Ore
        /*if (null != orePrefab) {
            Instantiate (orePrefab, blockObject.transform.position, Quaternion.identity);
        }

        if (null != vehiclePrefab)
        {
            Instantiate (vehiclePrefab, blockObject.transform.position, Quaternion.identity);
        }*/

        blockObjects[new Vector2 (blockObject.transform.position.x, blockObject.transform.position.z)] = null;
        GameObject.Destroy (blockObject);
    }

    public void Detonate (Vector3 pos, Dynamite.Type dynamiteType) 
    {
        Vector2 coords;
        for (float x = pos.x - 1; x <= pos.x + 1; ++x) {
            for (float z = pos.z - 1; z <= pos.z + 1; ++z) {
                if (x == pos.x || z == pos.z || Dynamite.Type.CLOSE == dynamiteType) {
                    // x == pos.x || z == pos.z => pas les diagonales
                    coords = new Vector2 (x, z);
                    if (blockObjects.ContainsKey (coords) && null != blockObjects[coords]) {
                        //DestroyBlock (blockObjects[coords]);

                        if ("Undestructible Block" != blockObjects[coords].tag) {
                            blockObjects[coords].GetComponent<Block> ().Die ();
                        }
                    }
                    else if (GameManager.Instance.player.transform.position.x == x && GameManager.Instance.player.transform.position.z == z) {
                        GameManager.Instance.player.Stuned = true;
                    }
                }
            }
        }
    }



     public void BlockDetonate(Vector3 pos, Block.Type dynamiteType)
     {
         Vector2 coords;
         for (float x = pos.x - 1; x <= pos.x + 1; ++x)
         {
             for (float z = pos.z - 1; z <= pos.z + 1; ++z)
             {
                 if (x == pos.x || z == pos.z || Block.Type.CLOSE == dynamiteType)
                 {
                     // x == pos.x || z == pos.z => pas les diagonales
                     coords = new Vector2(x, z);
                     if (blockObjects.ContainsKey(coords) && null != blockObjects[coords])
                     {
                         //DestroyBlock (blockObjects[coords]);
                         blockObjects[coords].GetComponent<Block>().Die();
                     }
                     else if (GameManager.Instance.player.transform.position.x == x && GameManager.Instance.player.transform.position.z == z)
                     {
                         GameManager.Instance.player.Stuned = true;
                     }
                 }
             }
         }
     }

    /*
     * Generates the blocks that are presents at the beginning of the game
     */
    public void GenerateBase () {
        // Création de la collection de blocs
        blockObjects = new Dictionary<Vector2, GameObject> ();

        // Génération des blocs de la base
        GameObject currentBlock;
        for (int x = -offset; x <= offset; x++) {
            for (int z = -offset; z <= offset; z++) {
                if (x != 0 || z != 0) {
                    int rand = Random.Range (0, 100);
                    if (1 == Mathf.Abs (x + z)) {
                        // Blocks that directly sourround the player must be blastable
                        currentBlock = Instantiate (emptyBlocPrefab) as GameObject;
                    }
                    else {
                        if (rand < oreBlockProba) {
                            currentBlock = Instantiate (ore1BlocPrefab) as GameObject;
                        }
                        else {
                            currentBlock = Instantiate (emptyBlocPrefab) as GameObject;
                        }
                    }
                    CreateBlock (currentBlock, x, z);
                }
            }
        }
    }

    /*
     * Generate a new line or a new column according the position in parameter
     */
    public void UpdateBlocks (Vector3 pos, Vector3 dir) {
        bool hasNewBlocks = false;  // Will are there new blocks generated ?
        if (dir.x != 0) {
            // Move to the left or the right
            for (float z = pos.z - offset; z <= pos.z + offset; ++z) {
                float originX = dir.x < 0 ? pos.x - offset : pos.x + offset;
                hasNewBlocks = GenerateEmptyBlock (pos, dir, originX, z) || hasNewBlocks;
            }
        }
        else {
            // Move to the top or the bottom
            for (float x = pos.x - offset; x <= pos.x + offset; ++x) {
                float originZ = dir.y < 0 ? pos.z - offset : pos.z + offset;
                hasNewBlocks = GenerateEmptyBlock (pos, dir, x, originZ) || hasNewBlocks;
            }
        }
        
        // A chunk is generated only if new blocks just generated and according a probability
        if (hasNewBlocks && Random.Range (0, 100) < chunkProba) {
            // Ok let's chunk
            GenerateChunk (pos, dir);
        }
        else if (hasNewBlocks && Random.Range (0, 100) < undestructibleChunkProba) {
            GenerateUndestructibleChunk (pos, dir);
        }
    }
    #endregion

    #region Méthodes privées
    void CreateBlock (GameObject blockObject, float x, float z) {
        blockObject.transform.parent = blocksContainerObject.transform;
        blockObject.transform.localPosition = new Vector3 (x, 0, z);
        blockObjects.Add (new Vector2 (x, z), blockObject);
    }

    void GenerateUndestructibleChunk (Vector3 pos, Vector3 dir) {
        // How much blocks in the chunk ?
        int nbBlocks;
        int rand;
        rand = Random.Range (0, 100);
        int sumProba = 0;
        if (rand < (sumProba += chunk2blocksProba)) {
            // Block of 2
            nbBlocks = 2;
        }
        else if (rand < (sumProba += chunk3blocksProba)) {
            // Block of 3
            nbBlocks = 2;
        }
        else if (rand < (sumProba += chunk4blocksProba)) {
            // Block of 4
            nbBlocks = 4;
        }
        else if (rand < (sumProba += chunk5blocksProba)) {
            // Block of 5
            nbBlocks = 5;
        }
        else if (rand < (sumProba += chunk6blocksProba)) {
            // Block of 6
            nbBlocks = 6;
        }
        else if (rand < (sumProba += chunk7blocksProba)) {
            // Block of 7
            nbBlocks = 7;
        }
        else if (rand < (sumProba += chunk8blocksProba)) {
            // Block of 8
            nbBlocks = 8;
        }
        else {
            // Block of 9
            nbBlocks = 9;
        }

        float x, z;
        if (dir.x != 0) {
            x = dir.x < 0 ? pos.x - offset - nbBlocks : pos.x + offset + nbBlocks;
            z = Random.Range ((int)(pos.z - offset), (int)(pos.z + offset));
        }
        else {
            x = Random.Range ((int)(pos.x - offset), (int)(pos.x + offset));
            z = dir.y < 0 ? pos.z - offset - nbBlocks : pos.z + offset + nbBlocks;
        }

        if (blockObjects.ContainsKey (new Vector2 (x, z))) {
            // Already a chunk here ? No conflict, I give up !
            return;
        }

        // Creation of the "pivot block"
        GameObject currentBlock = (GameObject)Instantiate (undestructibleBlockPrefab);
        CreateBlock (currentBlock, x, z);

        // Creation of the blocks around
        for (int i = 0; i < nbBlocks - 1; ++i) {
            bool blockExist = false;
            rand = Random.Range (0, 4);
            switch (rand) {
                case 0: // Up
                    if (blockObjects.ContainsKey (new Vector2 (x, z + 1))) {
                        // Already a block here
                        blockExist = true;
                        break;
                    }
                    z++;
                    break;
                case 1: // Down
                    if (blockObjects.ContainsKey (new Vector2 (x, z - 1))) {
                        // Already a block here
                        blockExist = true;
                        break;
                    }
                    z--;
                    break;
                case 2: // Left
                    if (blockObjects.ContainsKey (new Vector2 (x - 1, z))) {
                        // Already a block here
                        blockExist = true;
                        break;
                    }
                    x--;
                    break;
                case 3: // Right
                    if (blockObjects.ContainsKey (new Vector2 (x + 1, z))) {
                        // Already a block here
                        blockExist = true;
                        break;
                    }
                    x++;
                    break;
            }
            if (blockExist) {
                i--;    // Loop again
            }
            else {
                currentBlock = (GameObject)Instantiate (undestructibleBlockPrefab);
                CreateBlock (currentBlock, x, z);
            }
        }
    }

    void GenerateChunk (Vector3 pos, Vector3 dir) {
        // How much blocks in the chunk ?
        int nbBlocks;
        int rand;
        rand = Random.Range (0, 100);
        int sumProba = 0;
        if (rand < (sumProba += chunk2blocksProba)) {
            // Block of 2
            nbBlocks = 2;
        }
        else if (rand < (sumProba += chunk3blocksProba)) {
            // Block of 3
            nbBlocks = 2;
        }
        else if (rand < (sumProba += chunk4blocksProba)) {
            // Block of 4
            nbBlocks = 4;
        }
        else if (rand < (sumProba += chunk5blocksProba)) {
            // Block of 5
            nbBlocks = 5;
        }
        else if (rand < (sumProba += chunk6blocksProba)) {
            // Block of 6
            nbBlocks = 6;
        }
        else if (rand < (sumProba += chunk7blocksProba)) {
            // Block of 7
            nbBlocks = 7;
        }
        else if (rand < (sumProba += chunk8blocksProba)) {
            // Block of 8
            nbBlocks = 8;
        }
        else {
            // Block of 9
            nbBlocks = 9;
        }
        
        // What type of ore ?
        int oreType;
        rand = Random.Range (0, 100);
        if (rand < chunkOre1proba) {
            oreType = 0;
        }
        else if (rand < chunkOre2proba) {
            oreType = 1;
        }
        else {
            oreType = 2;
        }
        
        float x, z;
        if (dir.x != 0) {
            x = dir.x < 0 ? pos.x - offset - nbBlocks : pos.x + offset + nbBlocks;
            z = Random.Range ((int)(pos.z - offset), (int)(pos.z + offset));
        }
        else {
            x = Random.Range ((int)(pos.x - offset), (int)(pos.x + offset));
            z = dir.y < 0 ? pos.z - offset - nbBlocks : pos.z + offset + nbBlocks;
        }

        if (blockObjects.ContainsKey (new Vector2 (x, z))) {
            // Already a chunk here ? No conflict, I give up !
            return;
        }

        // Creation of the "pivot block"
        GameObject currentBlock = null;
        switch (oreType) {
            case 0: currentBlock = (GameObject)Instantiate (ore1BlocPrefab); break;
            case 1: currentBlock = (GameObject)Instantiate (ore2BlocPrefab); break;
            case 2: currentBlock = (GameObject)Instantiate (ore3BlocPrefab); break;
        }
        
        CreateBlock (currentBlock, x, z);

        // Creation of the blocks around
        for (int i = 0; i < nbBlocks - 1; ++i) {
            bool blockExist = false;
            rand = Random.Range (0, 4);
            switch (rand) {
                case 0: // Up
                    if (blockObjects.ContainsKey (new Vector2 (x, z + 1))) {
                        // Already a block here
                        blockExist = true;
                        break;
                    }
                    z++;
                    break;
                case 1: // Down
                    if (blockObjects.ContainsKey (new Vector2 (x, z - 1))) {
                        // Already a block here
                        blockExist = true;
                        break;
                    }
                    z--;
                    break;
                case 2: // Left
                    if (blockObjects.ContainsKey (new Vector2 (x - 1, z))) {
                        // Already a block here
                        blockExist = true;
                        break;
                    }
                    x--;
                    break;
                case 3: // Right
                    if (blockObjects.ContainsKey (new Vector2 (x + 1, z))) {
                        // Already a block here
                        blockExist = true;
                        break;
                    }
                    x++;
                    break;
            }
            if (blockExist) {
                i--;    // Loop again
            }
            else {
                switch (oreType) {
                    case 0: currentBlock = (GameObject)Instantiate (ore1BlocPrefab); break;
                    case 1: currentBlock = (GameObject)Instantiate (ore2BlocPrefab); break;
                    case 2: currentBlock = (GameObject)Instantiate (ore3BlocPrefab); break;
                }
                
                CreateBlock (currentBlock, x, z);
            }
        }
    }

    bool GenerateEmptyBlock (Vector3 pos, Vector3 dir, float x, float z) {
        bool isBlockGenerated = false; // Will the block finally generated ?
        // Loading
        GameObject currentBlock;
        int rand;
        Vector2 coords = new Vector2 (x, z);
        if (!blockObjects.ContainsKey (coords)) {
            rand = Random.Range (0, 100);
            
            if(rand < vehicleBlockProba && emptyBlocks<=0)
            {
                emptyBlocks = emptyBlocksForVehicle;
                currentBlock = Instantiate(vehicleBlockPrefab) as GameObject;
            }

            else
            {
                currentBlock = Instantiate(emptyBlocPrefab) as GameObject;
                emptyBlocks--;
            }
            CreateBlock (currentBlock, x, z);
            isBlockGenerated = true;
        }
        else if (null != blockObjects[coords]) {
            blockObjects[coords].transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = true;
        }

        // Unloading
        if (dir.x != 0) {
            x = dir.x < 0 ? pos.x + offset + 1 : pos.x - offset - 1;
        }
        else {
            z = dir.y < 0 ? pos.z + offset + 1 : pos.z - offset - 1;
        }
        coords = new Vector2 (x, z);
        if (blockObjects.ContainsKey (coords) && null != blockObjects[coords]) {
            blockObjects[coords].transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = false;
        }
        return isBlockGenerated;
    }

    void Start () {
        //blocksContainerTransform = GameObject.Find ("BlocksContainer").transform;
        emptyBlocks = emptyBlocksForVehicle;
    }
    #endregion
}
