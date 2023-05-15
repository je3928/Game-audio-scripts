using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainChecker : MonoBehaviour
{
  
    
    // Function checks for the closest texture to the player and returns them as weightings.
    private float[] GetTextureWeights(Vector3 playerPos, Terrain t)
    {

        // Terrain position
        Vector3 tPos = t.transform.position;

        // Terrain data
        TerrainData tData = t.terrainData;

        // Distance of player to terrain relative to the size of the map
        int mapX = Mathf.RoundToInt((playerPos.x - tPos.x) / tData.size.x * tData.alphamapWidth);
        int mapZ = Mathf.RoundToInt((playerPos.z - tPos.z) / tData.size.z * tData.alphamapHeight);
        float[,,] splatMapData = tData.GetAlphamaps(mapX, mapZ, 1, 1);


        float[] cellmix = new float[splatMapData.GetUpperBound(2) + 1];
        for (int i = 0; i < cellmix.Length; i++)
        {
            cellmix[i] = splatMapData[0, 0, i];
        }
        return cellmix;

    }


    // Returns the label of the terrain 
    public string GetLayerName(Vector3 playerPos, Terrain t)
    {

        float[] cellMix = GetTextureWeights(playerPos, t);
        float strongest = 0;
        int maxIndex = 0;

        // 
        for (int i = 0; i < cellMix.Length; i++)
        {
            if (cellMix[i] > strongest)
            {
                maxIndex = i;
                strongest = cellMix[i];
            }
        }
        return t.terrainData.terrainLayers[maxIndex].name;
    }

}
