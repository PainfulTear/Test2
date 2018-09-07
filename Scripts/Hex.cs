using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QPath;

public class Hex// : IQPathTile
{
    public Hex(int q, int r)
    {
        this.Q = q;
        this.R = r;
        this.S = -(q + r);
    }

    public readonly int Q;  //  Column
    public readonly int R;  //  Row
    public readonly int S;

    static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;

    public Vector3 Position()
    {
        float radius = 2f;
        float height = radius * 2;
        float width = WIDTH_MULTIPLIER * height;

        float vert = height * 3 / 4;
        float horiz = width;

        return new Vector3(
            horiz * (this.Q + this.R/2f),
            0,
            vert * this.R
        );
    }

    Hex[] neighbours;
    /*
    public Hex[] GetNeighbours ()
    {
        if (this.neighbours != null)
        {
            return this.neighbours;
        }

        List<Hex> neighbours = new List<Hex>();

        neighbours.Add(Map.GetHexAt(Q + 1, R + 0));
        neighbours.Add(Map.GetHexAt(Q - 1, R + 0));
        neighbours.Add(Map.GetHexAt(Q + 0, R + 1));
        neighbours.Add(Map.GetHexAt(Q + 0, R - 1));
        neighbours.Add(Map.GetHexAt(Q + 1, R - 1));
        neighbours.Add(Map.GetHexAt(Q - 1, R + 1));

        return this.neighbours;
    }*/
}
