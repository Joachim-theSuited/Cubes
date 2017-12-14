using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A 2d Perlin noise that can be evaluated for points in [0,1]^2.
/// Returned values should be in [-1,1].
/// </summary>
public class Perlin2D {

    readonly int gridSize;
    readonly Vector2[][] gradients;

    readonly float cellLength;
    readonly float norm;

    public Perlin2D(int _gridSize) {
        gridSize = Mathf.Max(_gridSize, 2);
        gradients = new Vector2[gridSize][];
        for(var i = 0; i < gridSize; ++i) {
            gradients[i] = new Vector2[gridSize];
            for(var j = 0; j < gridSize; ++j) {
                float angle = Random.Range(0, 2 * Mathf.PI);
                gradients[i][j] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            }
        }

        cellLength = 1.0f / (gridSize - 1);
        norm = cellLength * Mathf.Sqrt(2) / 2;
    }

    public float Eval(float x, float y) {
        return Eval(new Vector2(x, y));
    }

    // at must be in [0,1]^2
    public float Eval(Vector2 at) {
        int i = pos2Idx(at.x);
        int j = pos2Idx(at.y);

        return WeightedDistDot(at, i, j) + WeightedDistDot(at, i + 1, j) + WeightedDistDot(at, i, j + 1) + WeightedDistDot(at, i + 1, j + 1);
    }

    float WeightedDistDot(Vector2 at, int i, int j) {
        Vector2 gridPoint = new Vector2(i * cellLength, j * cellLength);
        Vector2 dist = at - gridPoint;

        float weight = (1 - _smoothAbs(dist.x / cellLength)) * (1 - _smoothAbs(dist.y / cellLength));
        return weight * Vector2.Dot(dist, gradients[i][j]) / norm;
    }

    // idx of the grid cell the position falls in
    int pos2Idx(float pos) {
        for(var idx = 0; idx < gridSize - 1; ++idx) {
            if(pos < (idx + 1) * cellLength)
                return idx;
        }
        return gridSize - 2;
    }

    float _smoothAbs(float x) {
        x = Mathf.Abs(x);
        return x * x * (3 - 2 * x);
    }

}
    