using UnityEngine;

public class NoiseMap : MonoBehaviour
{
    [SerializeField] private Attributes _attributes;

    public float[,] Noise()
    {
        _attributes._heightMap = new float[_attributes._width + 1, _attributes._length + 1];
        _attributes._persistence = 0.5f;

        //float maxVal = float.MinValue;
        //float minVal = float.MaxValue;

        for (int x = 0; x < _attributes._width; x++)
        {
            for (int z = 0; z < _attributes._length; z++)
            {
                float amplitude = 1;
                float frequency = 1;
                float total = 0;

                for (int i = 0; i < _attributes._octaves; i++)
                {
                    float xVert = ((((float)x / _attributes._scale) / _attributes._width) * frequency) + _attributes._xOffset;
                    float zVert = ((((float)z / _attributes._scale) / _attributes._length) * frequency) + _attributes._zOffset;

                    float perlinVal = Mathf.PerlinNoise(xVert, zVert) * 2 - 1;
                    total += perlinVal * amplitude;

                    //maxValue += amplitude;
                    amplitude *= _attributes._persistence;
                    frequency *= _attributes._lacunarity;

                    total = Mathf.Clamp(total, -0.1f, 1.0f);
                    //total = total / maxValue;
                }

                /*if (total > maxVal)
                {
                    maxVal = total;
                }
                else if (total < minVal)
                {
                    minVal = total;
                }*/

                _attributes._heightMap[x, z] = total;
            }
        }

        /*for (int x = 0; x < _attributes._width; x++)
        {
            for (int z = 0; z < _attributes._length; z++)
            {
                _attributes._heightMap[x, z] = Mathf.InverseLerp(minVal, maxVal, _attributes._heightMap[x, z]);
            }
        }*/
        return _attributes._heightMap;
    }
}
