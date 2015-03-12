using UnityEngine;
using System.Collections;

public class BlocksManager : Singleton<BlocksManager> {
    #region Attributs publics
    public int oreBlockProba = 25;  // Proba of ore block spawn (%)
    public int offset = 10; // Number of lines / columns that must be generated around the player
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
                        // Blocks that directly sourround the player must be blasted
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
                    currentBlock.GetComponent<Block> ().Id = currentId;
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
    public void UpdateBlocks (Vector3 pos) {
        if (pos.x + offset > nbColumns / 2 || Mathf.Abs (pos.x) + offset > nbColumns / 2 || pos.z + offset > nbLines / 2 || Mathf.Abs (pos.z) + offset > nbLines / 2) {
            // New "border" of blocks
            GameObject currentBlock;
            int rand;
            // Lines
            for (int x = -nbColumns / 2; x <= nbColumns / 2; ++x) {
                rand = Random.Range (0, 100);
                if (rand < oreBlockProba) {
                    currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                }
                else {
                    currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                }
                currentBlock.transform.parent = blocksContainerObject.transform;
                currentBlock.transform.localPosition = new Vector3 (x, 0, -nbLines / 2 - 1);
            }

            for (int x = -nbColumns / 2; x <= nbColumns / 2; ++x) {
                rand = Random.Range (0, 100);
                if (rand < oreBlockProba) {
                    currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                }
                else {
                    currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                }
                currentBlock.transform.parent = blocksContainerObject.transform;
                currentBlock.transform.localPosition = new Vector3 (x, 0, nbLines / 2 + 1);
            }
            
            nbLines += 2;

            // Columns
            for (int z = -nbLines / 2; z <= nbLines / 2; ++z) {
                rand = Random.Range (0, 100);
                if (rand < oreBlockProba) {
                    currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                }
                else {
                    currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                }
                currentBlock.transform.parent = blocksContainerObject.transform;
                currentBlock.transform.localPosition = new Vector3 (-nbColumns / 2 - 1, 0, z);
            }

            for (int z = -nbLines / 2; z <= nbLines / 2; ++z) {
                rand = Random.Range (0, 100);
                if (rand < oreBlockProba) {
                    currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                }
                else {
                    currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                }
                currentBlock.transform.parent = blocksContainerObject.transform;
                currentBlock.transform.localPosition = new Vector3 (nbColumns / 2 + 1, 0, z);
            }
            nbColumns += 2;
        }
    }
    #endregion
}
