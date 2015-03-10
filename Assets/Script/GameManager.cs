using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public GameObject BlockPrefab;
    public GameObject RedBlock;
    //public GameObject Player;
    public int numberOfBlocks = 100;
    public Vector3 Array = new Vector3(3, 0, 5);
    public Vector3 BlockCoordinates = Vector3.zero;
    private bool up = false;
    Vector2 way = -Vector2.one;
    public LayerMask layerMask;
    new RaycastHit _hit;
    void Start()
    {
        StartCoroutine(GenerateMap());
    }


    IEnumerator GenerateMap()
    {
        int _i = 0;
        Transform _parent = GameObject.Find("BlockContainer").transform;
        BlockCoordinates = Array - Vector3.right;
        while (_i < numberOfBlocks)
        {
            if (Random.Range(-1, 10) > 0)
            {

                

                if (Physics.Raycast(BlockCoordinates, new Vector3(0, 0, 1), out _hit, 1, layerMask) || Physics.Raycast(BlockCoordinates, new Vector3(0, 0, -1), out _hit, 1, layerMask) || Physics.Raycast(BlockCoordinates, new Vector3(1, 0, 0), out _hit, 1, layerMask) || Physics.Raycast(BlockCoordinates, new Vector3(-1, 0, 0), out _hit, 1, layerMask))
                {
                    Debug.Log("j'instancie peut être un rouge cluster");

                    if (Random.Range(-1, 2) < 0)
                    {
                        GameObject _block = Instantiate(BlockPrefab, BlockCoordinates, Quaternion.identity) as GameObject;
                        _block.transform.SetParent(_parent);
                        _block.name = BlockPrefab.name;
                    }
                    else
                    {
                        GameObject _block = Instantiate(RedBlock, BlockCoordinates, Quaternion.identity) as GameObject;
                        _block.transform.SetParent(_parent);
                        _block.name = RedBlock.name;
                    }
                }

                else 
                {

                    if (Random.Range(-1, 6) > 0)
                    {
                        GameObject _block = Instantiate(BlockPrefab, BlockCoordinates, Quaternion.identity) as GameObject;
                        _block.transform.SetParent(_parent);
                        _block.name = BlockPrefab.name;
                    }
                    else
                    {
                        GameObject _block = Instantiate(RedBlock, BlockCoordinates, Quaternion.identity) as GameObject;
                        _block.transform.SetParent(_parent);
                        _block.name = RedBlock.name;
                    }


                }

                _i++;

            }

            if (up == false)
            {
                BlockCoordinates.x += way.x;
            }
            else
            {
                BlockCoordinates.z += way.y;
            }

            if (!up && Mathf.Abs(BlockCoordinates.x) == Array.x)
            {
                if (way.x <= 0)
                    Array.x++;
                way.x *= -1;
                up = true;
            }

            else if (up && Mathf.Abs(BlockCoordinates.z) == Array.z)
            {
                if (way.y <= 0)
                    Array.z++;
                way.y *= -1;
                up = false;
            }

            yield return new WaitForEndOfFrame();
        }


    }

    void Update()
    {

    }
}
