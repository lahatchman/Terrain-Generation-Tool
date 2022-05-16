using UnityEngine;

public class ColourMap : MonoBehaviour
{
    [SerializeField] private Attributes _attributes;
    [SerializeField] private GameObject _plane;

    #region Setters

    public void SetColourMap()
    {
        CreateColourMap();
    }

    #endregion

    private void CreateColourMap()
    {
        int mapWidth = _attributes._heightMap.GetLength(0);
        int mapHeight = _attributes._heightMap.GetLength(1);

        Color[] colours = new Color[mapWidth * mapHeight];

        Texture2D texture = new Texture2D(mapWidth, mapHeight);

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = _attributes._heightMap[x, y];

                for (int i = 0; i < _attributes.biomes.Length; i++)
                {
                    if (currentHeight <= _attributes.biomes[i].height)
                    {
                        colours[(y * mapWidth) + x] = _attributes.biomes[i].colour;
                        break;
                    }
                }
            }
        }

        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colours);
        texture.Apply();
        _plane.GetComponent<Renderer>().sharedMaterial.mainTexture = texture;
    }
}
