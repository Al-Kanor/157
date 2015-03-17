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
    public GameObject destroyedBlockPrefab;
    #endregion

    #region Attributs privés
    private int nbLines = 0;    // Current number of lines of blocks (dynamic)
    private int nbColumns = 0;  // Current number of columns of blocks (dynamic)
    private Dictionary<Vector2, GameObject> blockObjects;
    private Transform blocksContainerTransform;
    #endregion

    #region Méthodes publiques
    public void DestroyBlock (GameObject blockObject) {
        // Instantiate an invisible block for avoid the proccedural generation
        GameObject destroyedBlockObject = Instantiate (destroyedBlockPrefab, blockObject.transform.position, Quaternion.identity) as GameObject;
        destroyedBlockObject.transform.parent = blocksContainerTransform;
        blockObjects[new Vector2 (blockObject.transform.position.x, blockObject.transform.position.z)] = null;
        GameObject.Destroy (blockObject);
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
                            currentBlock = (GameObject)Instantiate (oreBlocPrefab);
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
                GenerateBlock (pos, dir, originX, z);
            }
        }
        else {
            // Move to the top or the bottom
            for (float x = pos.x - offset; x <= pos.x + offset; ++x) {
                float originZ = dir.y < 0 ? pos.z - offset : pos.z + offset;
                GenerateBlock (pos, dir, x, originZ);
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

    void GenerateBlock (Vector3 pos, Vector3 dir, float x, float z) {
        // Loading
        GameObject currentBlock;
        int rand;
        Vector2 coords = new Vector2 (x, z);
        if (!blockObjects.ContainsKey (coords)) {
            rand = Random.Range (0, 100);
            if (rand < oreBlockProba) {
                currentBlock = (GameObject)Instantiate (oreBlocPrefab);
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
