using UnityEngine;
using System.Collections;

public class BlocksManager : Singleton<BlocksManager> {
    #region Attributs publics
    public int offset = 10; // Number of lines / columns that must be generated around the player
    public Player player;
    public GameObject blocksContainerObject;
    public GameObject emptyBlocPrefab;
    public GameObject oreBlocPrefab;
    #endregion

    #region Attributs privés
    private int nbLines = 0;    // Current number of lines of blocks (dynamic)
    private int nbColumns = 0;  // Current number of columns of blocks (dynamic)
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
                    if (1 == Mathf.Abs (x + z)) {
                        // Blocks that directly sourround the player must be blasted
                        currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                    }
                    else {
                        if (0 == Random.Range (0, 2)) {
                            currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                        }
                        else {
                            currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                        }
                    }
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
        /*
        if (pos.x + offset > nbColumns / 2) {
            Debug.Log ("Nouvelle colonne à droite");
            for (int z = -nbLines / 2; z <= nbLines / 2; ++z) {
                GameObject currentBlock;
                if (0 == Random.Range (0, 2)) {
                    currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                }
                else {
                    currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                }
                currentBlock.transform.parent = blocksContainerObject.transform;
                currentBlock.transform.localPosition = new Vector3 (nbColumns / 2 + 1, 0, z);
            } 
            nbColumns++;
        }
        else if (Mathf.Abs (pos.x) + offset > nbColumns / 2) {
            Debug.Log ("Nouvelle colonne à gauche");
            for (int z = -nbLines / 2; z <= nbLines / 2; ++z) {
                GameObject currentBlock;
                currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                currentBlock.transform.parent = blocksContainerObject.transform;
                currentBlock.transform.localPosition = new Vector3 (-nbColumns / 2 - 1, 0, z);
            }
            nbColumns++;
        }
        else if (pos.z + offset > nbLines / 2) {
            Debug.Log ("Nouvelle ligne en haut");
            for (int x = -nbColumns / 2; x <= nbColumns / 2; ++x) {
                GameObject currentBlock;
                currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                currentBlock.transform.parent = blocksContainerObject.transform;
                currentBlock.transform.localPosition = new Vector3 (x, 0, nbLines / 2 + 1);
            }
            nbLines++;
        }
        else if (Mathf.Abs (pos.z) + offset > nbLines / 2) {
            Debug.Log ("Nouvelle ligne en bas");
            for (int x = -nbColumns / 2; x <= nbColumns / 2; ++x) {
                GameObject currentBlock;
                currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                currentBlock.transform.parent = blocksContainerObject.transform;
                currentBlock.transform.localPosition = new Vector3 (x, 0, -nbLines / 2 - 1);
            }
            nbLines++;
        }
         */
        if (pos.x + offset > nbColumns / 2 || Mathf.Abs (pos.x) + offset > nbColumns / 2 || pos.z + offset > nbLines / 2 || Mathf.Abs (pos.z) + offset > nbLines / 2) {
            // New "border" of blocks
            GameObject currentBlock;
            // Lines
            for (int x = -nbColumns / 2; x <= nbColumns / 2; ++x) {
                if (0 == Random.Range (0, 2)) {
                    currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                }
                else {
                    currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                }
                currentBlock.transform.parent = blocksContainerObject.transform;
                currentBlock.transform.localPosition = new Vector3 (x, 0, -nbLines / 2 - 1);
            }

            for (int x = -nbColumns / 2; x <= nbColumns / 2; ++x) {
                if (0 == Random.Range (0, 2)) {
                    currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                }
                else {
                    currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                }
                currentBlock.transform.parent = blocksContainerObject.transform;
                currentBlock.transform.localPosition = new Vector3 (x, 0, nbLines / 2 + 1);
            }
            
            nbLines += 2;

            // Columns
            for (int z = -nbLines / 2; z <= nbLines / 2; ++z) {
                if (0 == Random.Range (0, 2)) {
                    currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                }
                else {
                    currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                }
                currentBlock.transform.parent = blocksContainerObject.transform;
                currentBlock.transform.localPosition = new Vector3 (-nbColumns / 2 - 1, 0, z);
            }

            for (int z = -nbLines / 2; z <= nbLines / 2; ++z) {
                if (0 == Random.Range (0, 2)) {
                    currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                }
                else {
                    currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                }
                currentBlock.transform.parent = blocksContainerObject.transform;
                currentBlock.transform.localPosition = new Vector3 (nbColumns / 2 + 1, 0, z);
            }
            nbColumns += 2;
        }
    }
    #endregion
}
