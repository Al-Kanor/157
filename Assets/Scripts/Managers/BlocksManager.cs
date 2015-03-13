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
    public void UpdateBlocks (Vector3 pos, Vector3 dir) {
        /*
        if (pos.x + offset > nbColumns / 2 || Mathf.Abs (pos.x) + offset > nbColumns / 2 || pos.z + offset > nbLines / 2 || Mathf.Abs (pos.z) + offset > nbLines / 2) {
            // New "border" of blocks
            GameObject currentBlock;
            int rand;
            // Lines
            // Creation of the bottom lines
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

            // Creation of the top lines
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
            // Creation of the left lines
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

            // Creation of the right lines
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

            // Unloading of the far lines / columns
            if (pos.x > 0) {
                // The player is moving to the right, unloading of the leftmost column
                RaycastHit hit;
                for (int z = -nbLines / 2; z <= nbLines / 2; ++z) {
                    Debug.DrawRay (new Vector3 (-nbColumns / 2, 3, z), Vector3.down, Color.red, 3000);
                    if (Physics.Raycast(new Vector3(-nbColumns / 2, 3, z), Vector3.down, out hit, 10)) {
                        GameObject.Destroy (hit.collider.gameObject);
                    }
                }
            }
            else {
                // The player is moving to the left, unloading of the rightmost column
            }

            if (pos.z > 0) {
                // The player is moving to the top, unloading of the bottommost column
            }
            else {
                // The player is moving to the left, unloading of the topmost column
            }
        }
        */
        RaycastHit hit;
        GameObject currentBlock;
        int rand;

        if (dir.x > 0) {
            // Move to the right
            for (float z = pos.z - offset; z <= pos.z + offset; ++z) {
                // right loading
                if (Physics.Raycast (new Vector3 (pos.x + offset, 3, z), Vector3.down, out hit, 10)) {
                    if ("Ground" == hit.collider.name) {
                        rand = Random.Range (0, 100);
                        if (rand < oreBlockProba) {
                            currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                        }
                        else {
                            currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                        }
                        currentBlock.transform.parent = blocksContainerObject.transform;
                        currentBlock.transform.localPosition = new Vector3 (pos.x + offset, 0, z);
                    }
                    else if (null != hit.collider.GetComponent<Block> ()) {
                        hit.collider.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = true;
                    }
                }
                // left unloading
                if (Physics.Raycast (new Vector3 (pos.x - offset - 1, 3, z), Vector3.down, out hit, 10)) {
                    if ("Ground" != hit.collider.name) {
                        if (null != hit.collider.GetComponent<Block> ()) {
                            // It's a blocks ! Desactivation of the collider
                            hit.collider.transform.GetChild(0).GetComponent<MeshRenderer> ().enabled = false;
                        }
                        else {
                            // It's another object (dynamite ?) => bye bye
                            Destroy (hit.collider.gameObject);
                        }
                    }
                }
            }
        }
        else if (dir.x < 0) {
            // Move to the left
            for (float z = pos.z - offset; z <= pos.z + offset; ++z) {
                // left loading
                if (Physics.Raycast (new Vector3 (pos.x - offset, 3, z), Vector3.down, out hit, 10)) {
                    if ("Ground" == hit.collider.name) {
                        rand = Random.Range (0, 100);
                        if (rand < oreBlockProba) {
                            currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                        }
                        else {
                            currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                        }
                        currentBlock.transform.parent = blocksContainerObject.transform;
                        currentBlock.transform.localPosition = new Vector3 (pos.x - offset, 0, z);
                    }
                    else if (null != hit.collider.GetComponent<Block> ()) {
                        hit.collider.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = true;
                    }
                }
                // right unloading
                if (Physics.Raycast (new Vector3 (pos.x + offset + 1, 3, z), Vector3.down, out hit, 10)) {
                    if ("Ground" != hit.collider.name) {
                        if (null != hit.collider.GetComponent<Block> ()) {
                            // It's a blocks ! Desactivation of the collider
                            hit.collider.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = false;
                        }
                        else {
                            // It's another object (dynamite ?) => bye bye
                            Destroy (hit.collider.gameObject);
                        }
                    }
                }
            }
        }
        else if (dir.y > 0) {
            // Move to the top
            for (float x = pos.x - offset; x <= pos.x + offset; ++x) {
                // top loading
                if (Physics.Raycast (new Vector3 (x, 3, pos.z + offset), Vector3.down, out hit, 10)) {
                    if ("Ground" == hit.collider.name) {
                        rand = Random.Range (0, 100);
                        if (rand < oreBlockProba) {
                            currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                        }
                        else {
                            currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                        }
                        currentBlock.transform.parent = blocksContainerObject.transform;
                        currentBlock.transform.localPosition = new Vector3 (x, 0, pos.z + offset);
                    }
                    else if (null != hit.collider.GetComponent<Block> ()) {
                        hit.collider.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = true;
                    }
                }
                // bottom unloading
                if (Physics.Raycast (new Vector3 (x, 3, pos.z - offset - 1), Vector3.down, out hit, 10)) {
                    if ("Ground" != hit.collider.name) {
                        if (null != hit.collider.GetComponent<Block> ()) {
                            // It's a blocks ! Desactivation of the collider
                            hit.collider.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = false;
                        }
                        else {
                            // It's another object (dynamite ?) => bye bye
                            Destroy (hit.collider.gameObject);
                        }
                    }
                }
            }
        }
        else if (dir.y < 0) {
            // Move to the bottom
            for (float x = pos.x - offset; x <= pos.x + offset; ++x) {
                // bottom loading
                if (Physics.Raycast (new Vector3 (x, 3, pos.z - offset), Vector3.down, out hit, 10)) {
                    if ("Ground" == hit.collider.name) {
                        rand = Random.Range (0, 100);
                        if (rand < oreBlockProba) {
                            currentBlock = (GameObject)Instantiate (oreBlocPrefab);
                        }
                        else {
                            currentBlock = (GameObject)Instantiate (emptyBlocPrefab);
                        }
                        currentBlock.transform.parent = blocksContainerObject.transform;
                        currentBlock.transform.localPosition = new Vector3 (x, 0, pos.z - offset);
                    }
                    else if (null != hit.collider.GetComponent<Block> ()) {
                        hit.collider.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = true;
                    }
                }
                // top unloading
                if (Physics.Raycast (new Vector3 (x, 3, pos.z + offset + 1), Vector3.down, out hit, 10)) {
                    if ("Ground" != hit.collider.name) {
                        if (null != hit.collider.GetComponent<Block> ()) {
                            // It's a blocks ! Desactivation of the collider
                            hit.collider.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = false;
                        }
                        else {
                            // It's another object (dynamite ?) => bye bye
                            Destroy (hit.collider.gameObject);
                        }
                    }
                }
            }
        }
    }
    #endregion
}
