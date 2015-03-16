using UnityEngine;
using System.Collections;

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
    private int currentId = 1;
    #endregion

    #region Méthodes publiques
    /*
     * Generates the blocks that are presents at the beginning of the game
     */
    public void GenerateBase () {
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
                    currentId++;
                    currentBlock.transform.parent = blocksContainerObject.transform;
                    currentBlock.transform.localPosition = new Vector3 (x, 0, z);
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
    private void GenerateBlock (Vector3 pos, Vector3 dir, float x, float z) {
        GameObject currentBlock;
        RaycastHit hit;
        int rand;
        if (Physics.Raycast (new Vector3 (x, 3, z), Vector3.down, out hit, 10)) {
            if ("Ground" == hit.collider.name) {
                rand = Random.Range (0, 100);
                if (rand < oreBlockProba) {
                    currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                }
                else {
                    currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                }
                currentBlock.transform.parent = blocksContainerObject.transform;
                currentBlock.transform.localPosition = new Vector3 (x, 0, z);
            }
            else if (null != hit.collider.GetComponent<Block> ()) {
                hit.collider.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = true;
            }
        }
        // Unloading
        if (dir.x != 0) {
            x = dir.x < 0 ? pos.x + offset + 1 : pos.x - offset - 1;
        }
        else {
            z = dir.y < 0 ? pos.z + offset + 1 : pos.z - offset - 1;
        }

        if (Physics.Raycast (new Vector3 (x, 3, z), Vector3.down, out hit, 10)) {
            if ("Ground" != hit.collider.name) {
                if (null != hit.collider.GetComponent<Block> ()) {
                    // It's a blocks ! Desactivation of the renderer
                    hit.collider.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = false;
                }
                else {
                    // It's another object (dynamite ?) => bye bye
                    //Destroy (hit.collider.gameObject);
                }
            }
        }
    }
    #endregion
}
