using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTerrain : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public float scale = 20f;

    // Optional parameters for more control
    public float heightMultiplier = 20f;
    public float offsetX = 100f;  // Random offset to make each terrain different
    public float offsetY = 100f;

    void Start()
    {
        offsetX = Random.Range(0f, 9999f);  // Randomize the terrain
        offsetY = Random.Range(0f, 9999f);

        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, heightMultiplier, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        float[,] falloffMap = GenerateFalloffMap();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = Mathf.Clamp01(CalculateHeight(x, y) - falloffMap[x, y]);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;
        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    float[,] GenerateFalloffMap()
    {
        float[,] map = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = x / (float)width * 2 - 1;
                float yCoord = y / (float)height * 2 - 1;
                float value = Mathf.Max(Mathf.Abs(xCoord), Mathf.Abs(yCoord));
                map[x, y] = Evaluate(value);
            }
        }
        return map;
    }

    float Evaluate(float value)
    {
        float a = 3;
        float b = 2.2f;
        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
    }
}