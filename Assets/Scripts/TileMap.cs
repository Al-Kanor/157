using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {
    #region Attributs publics
    public GameObject blocsObject;
    public GameObject emptyBlocPrefab;
    #endregion

    #region Attributs privés
    private Tile[,] tiles;
    private int nbLines = 10;
    private int nbColumns = 10;
    #endregion

    #region Méthodes publiques
    public void CreateMap (int _nbLines, int _nbColumns) {
        nbLines = _nbLines;
        nbColumns = _nbColumns;
        tiles = new Tile[nbColumns, nbLines];
        for (int x = 0; x < nbColumns; ++x) {
            for (int z = 0; z < nbLines; ++z) {
                GameObject blocObject = (GameObject)Instantiate (emptyBlocPrefab);
                blocObject.name = "Empty Bloc";
                blocObject.transform.parent = blocsObject.transform;
                blocObject.transform.localPosition = new Vector3 (x, 0, z);
                Debug.Log (blocObject.name);
                tiles[x, z].Block = blocObject.GetComponent<Block> ();
            }
        }
    }
    #endregion

    #region Méthodes privées
    void Start () {
        
    }
    #endregion
}
