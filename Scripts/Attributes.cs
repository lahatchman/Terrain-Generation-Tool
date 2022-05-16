using UnityEngine;

public class Attributes : MonoBehaviour
{
    [SerializeField] private NoiseMap _noiseMap;
    [SerializeField] private ColourMap _colourMap;
    [SerializeField] private TerrainMesh _terrainMesh;
    [SerializeField] public GameObject _waterDimensions;
    [SerializeField] private Material _water;
    [SerializeField] public int _width, _length, _octaves;
    [SerializeField] public float _scale, _height, _persistence, _lacunarity, _xOffset, _zOffset;
    [SerializeField] public float _waterDepth, _waterStrength, _rippleDensity, _rippleSlimness, _rippleSpeed;
    [SerializeField] public float _waveAmplitude, _waveFrequency, _waveSpeed;
    [SerializeField] public float[,] _heightMap;
    [SerializeField] public AnimationCurve meshCurve;
    [SerializeField] public Biomes[] biomes;

    [System.Serializable]
    public struct Biomes
    {
        public string terrain;
        public float height;
        public Color colour;
    }

    #region Setters

    public void SetMeshHeightMap()
    {
        _noiseMap.Noise();
        _colourMap.SetColourMap();
        _terrainMesh.SetHeightMap();
        SetWaveDimensions();
    }

    public void SetWaterAttributes()
    {
        _water.SetFloat("_depth", _waterDepth);
        _water.SetFloat("_strength", _waterStrength);
        _water.SetFloat("_rippleDensity", _rippleDensity);
        _water.SetFloat("_rippleSlimness", _rippleSlimness);
        _water.SetFloat("_rippleSpeed", _rippleSpeed);
    }

    public void SetWaveAttributes()
    {
        _water.SetFloat("_waveAmplitude", _waveAmplitude);
        _water.SetFloat("_waveFrequency", _waveFrequency);
        _water.SetFloat("_waveSpeed", _waveSpeed);
    }

    #endregion

    private void SetWaveDimensions()
    {
        _waterDimensions.transform.position = new Vector3(_width / 2, 0, _length / 2);
        _waterDimensions.transform.localScale = new Vector3(_width / 50.0f, 1, _length / 50.0f);
    }
    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetMeshHeightMap();
        }
    }*/
}
