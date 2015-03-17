using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlocksManager : Singleton<BlocksManager> {
    #region Attributs publics
    public int oreBlockProba = 25;  // Proba of ore block spawn (%)
    public int offset = 7; // Number of lines / columns that must be generated around the player
    public Player player;
    public GameObject blocksContainerObject;
    public GameObject emptyBlocPrefab;
    public GameObject oreBlocPrefab;
    #endregion

    #region Attributs privés
    private int nbLines = 0;    // Current number of lines of blocks (dynamic)
    private int nbColumns = 0;  // Current number of columns of blocks (dynamic)
    private Dictionary<Vector2, GameObject> blockObjects;
    private Transform blocksContainerTransform;
    #endregion

    #region Méthodes publiques
    public void DestroyBlock (GameObject blockObject) {
        blockObjects[new Vector2 (blockObject.transform.position.x, blockObject.transform.position.z)] = null;
        GameObject.Destroy (blockObject);
    }

    public void Detonate (Vector3 pos, Dynamite.Type dynamiteType) {
        Vector2 coords;
        for (float x = pos.x - 1; x <= pos.x + 1; ++x) {
            for (float z = pos.z - 1; z <= pos.z + 1; ++z) {
                if (x == pos.x || z == pos.z || Dynamite.Type.CLOSE == dynamiteType) {
                    // x == pos.x || z == pos.z => pas les diagonales
                    coords = new Vector2 (x, z);
                    if (blockObjects.ContainsKey (coords) && null != blockObjects[coords]) {
                        blockObjects[coords].GetComponent<Block> ().Die ();
                    }
                    else if (GameManager.Instance.player.transform.position.x == x && GameManager.Instance.player.transform.position.z == z) {
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
                        currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                    }
                    else {
                        if (rand < oreBlockProba) {
                            //currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                            currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                        }
                        else {
                            currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                        }
                    }
                    CreateBlock (currentBlock, x, z);
                }
            }
        }
        nbLines = 2 * offset + 1;
        nbColumns = 2 * offset + 1;
    }

    /*
     * Generate a new line or a new column according the position in parameter
     */
    public void UpdateBlocks (Vector3 pos, Vector3 dir) {
        if (dir.x != 0) {
            // Move to the left or the right
            for (float z = pos.z - offset; z <= pos.z + offset; ++z) {
                float originX = dir.x < 0 ? pos.x - offset : pos.x + offset;
                GenerateEmptyBlock (pos, dir, originX, z);
            }
        }
        else {
            // Move to the top or the bottom
            for (float x = pos.x - offset; x <= pos.x + offset; ++x) {
                float originZ = dir.y < 0 ? pos.z - offset : pos.z + offset;
                GenerateEmptyBlock (pos, dir, x, originZ);
            }
        }
        
        // Chunk ?
        float cx = pos.x + dir.x * offset * 2;
        float cz = pos.z + dir.y * offset * 2;
        Vector2 coords = new Vector2 (cx, cz);
        int rand;
        GameObject currentBlock;
        if (!blockObjects.ContainsKey (coords)) {
            currentBlock = (GameObject)Instantiate (oreBlocPrefab);
            CreateBlock (currentBlock, cx, cz);
        }

        if (0 == Random.Range (0, 10)) {
            rand = Random.Range (0, 100);
            if (rand < 10) {
                // Block of 2
                
            }
            else if (rand < 20) {
                // Block of 3
            }
            else if (rand < 45) {
                // Block of 4
            }
            else if (rand < 65) {
                // Block of 5
            }
            else if (rand < 77) {
                // Block of 6
            }
            else if (rand < 87) {
                // Block of 7
            }
            else if (rand < 95) {
                // Block of 8
            }
            else {
                // Block of 9
            }
        }
    }
    #endregion

    #region Méthodes privées
    void CreateBlock (GameObject blockObject, float x, float z) {
        blockObject.transform.parent = blocksContainerObject.transform;
        blockObject.transform.localPosition = new Vector3 (x, 0, z);
        blockObjects.Add (new Vector2 (x, z), blockObject);
    }

    void GenerateEmptyBlock (Vector3 pos, Vector3 dir, float x, float z) {
        // Loading
        GameObject currentBlock;
        int rand;
        Vector2 coords = new Vector2 (x, z);
        if (!blockObjects.ContainsKey (coords)) {
            rand = Random.Range (0, 100);
            if (rand < oreBlockProba) {
                //currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
            }
            else {
                currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
            }
            CreateBlock (currentBlock, x, z);
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
    }

    void Start () {
        blocksContainerTransform = GameObject.Find ("BlocksContainer").transform;
    }
    #endregion
}
