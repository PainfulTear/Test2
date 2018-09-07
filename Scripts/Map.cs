using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QPath;

public class Map : MonoBehaviour {

    void Start()
    {
        GenerateMap();
    }

    public GameObject hexPrefab;
    public Material[] hexMaterials;

    public bool AllowWrapEastWest = true;
    public bool AllowWrapNorthSouth = true;

    int NumColumns = 10;
    int NumRows = 10;

    float xOffset = 3.45f;
    float zOffset = 3f;

    private Hex[,] hexes;
    private Dictionary<Hex, GameObject> hexToGameObjectMap;
    private Dictionary<GameObject, Hex> gameObjectToHexMap;

    public void GenerateMap () {

        hexes = new Hex[NumColumns, NumRows];
        hexToGameObjectMap = new Dictionary<Hex, GameObject>();
        gameObjectToHexMap = new Dictionary<GameObject, Hex>();

        for (int column = 0; column < NumColumns; column++)
        {
            for (int row = 0; row < NumRows; row++)
            {
                Hex h = new Hex(column, row);

                hexes[column, row] = h;

                GameObject hex_go = Instantiate(
                    hexPrefab, 
                    h.Position(), 
                    Quaternion.identity,
                    this.transform
                );

                MeshRenderer mr = hex_go.GetComponentInChildren<MeshRenderer>();
                mr.material = hexMaterials[Random.Range(0, hexMaterials.Length)];

                hex_go.name = "Hex_" + column + "_" + row;

                hex_go.transform.SetParent(this.transform);

                hexToGameObjectMap[h] = hex_go;
                gameObjectToHexMap[hex_go] = h;
            }
        }

        //StaticBatchingUtility.Combine(this.gameObject);

    }

    public Hex GetHexAt(int x, int y)
    {
        if (hexes == null)
        {
            return null;
        }

        if (AllowWrapEastWest)
        {
            x = x % NumColumns;
            if (x < 0)
            {
                x += NumColumns;
            }
        }

        if (AllowWrapNorthSouth)
        {
            y = y % NumRows;
            if (y < 0)
            {
                y += NumRows;
            }
        }

        try
        {
            return hexes[x, y];
        }

        catch
        {
            return null;
        }
    }

    public Hex GetHexFromGameObject(GameObject hexGO)
    {
        if(gameObjectToHexMap.ContainsKey(hexGO))
        {
            return gameObjectToHexMap[hexGO];
        }

        return null;
    }
}
