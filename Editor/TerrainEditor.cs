using UnityEngine;
using UnityEditor;

public class TerrainEditor : EditorWindow
{
    [SerializeField] private Attributes _attributes;
    [SerializeField] private GameObject _preFab;
    [SerializeField] private int _start, _guide = 0, _selected, terrainselected;
    [SerializeField] private string[] _options = new string[] { "Select An Option", "Mesh Dimensions" };

    [MenuItem("Tools/Terrain Editor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(TerrainEditor));
    }

    private void OnGUI()
    {
        switch (_start)
        {
            default:
                if (GUILayout.Button("Create Mesh"))
                {
                    GameObject myInstantiatedObj = Instantiate(_preFab, new Vector3(0, 0, 0), Quaternion.identity);
                    GameObject t = myInstantiatedObj.transform.Find("Attributes").gameObject;
                    _attributes = t.GetComponent<Attributes>();
                    _start++;
                }
                break;
            case 1:
                EditorGUI();
                break;
        }
    }

    private void EditorGUI()
    {
        switch (_selected)
        {
            default:
                break;

            case 0:
                if (_guide == 0)
                {
                    _options = new string[] { "Mesh Dimensions" };
                    _selected = EditorGUILayout.Popup("Editor Options", _selected, _options);
                    EditorGUILayout.Space(10);
                }
                else
                {
                    _selected = EditorGUILayout.Popup("Editor Options", _selected, _options);
                    EditorGUILayout.Space(10);
                }

                _attributes._width = (int)EditorGUILayout.Slider("Width", _attributes._width, 50, 2000);
                _attributes._length = (int)EditorGUILayout.Slider("Length", _attributes._length, 50, 2000);

                EditorGUILayout.Space(10);
                if (GUILayout.Button("Apply Changes"))
                {
                    _attributes.SetMeshHeightMap();

                    if (_guide == 0) 
                    {
                        _guide++;
                        _selected = 1;
                    }
                }
                break;

            case 1:
                if (_guide == 1)
                {
                    _options = new string[] { "Mesh Dimensions", "Terrain Attributes" };
                    _selected = EditorGUILayout.Popup("Editor Options", _selected, _options);
                    EditorGUILayout.Space(10);
                }
                else
                {
                    _selected = EditorGUILayout.Popup("Editor Options", _selected, _options);
                    EditorGUILayout.Space(10);
                }

                _attributes._scale = EditorGUILayout.Slider("Scale", _attributes._scale, 0, 1);
                _attributes._height = EditorGUILayout.Slider("Height", _attributes._height, 0, 1000);
                _attributes._octaves = (int)EditorGUILayout.Slider("Octaves", _attributes._octaves, 1, 4);
                _attributes._lacunarity = EditorGUILayout.Slider("Lacunarity", _attributes._lacunarity, 0.1f, 2);
                _attributes._xOffset = EditorGUILayout.Slider("X Offset", _attributes._xOffset, 0, 100);
                _attributes._zOffset = EditorGUILayout.Slider("Z Offset", _attributes._zOffset, 0, 100);

                EditorGUILayout.Space(10);
                _attributes.meshCurve = EditorGUILayout.CurveField("Mesh Slopes", _attributes.meshCurve);

                EditorGUILayout.Space(10);
                if (GUILayout.Button("Apply Changes"))
                {
                    _attributes.SetMeshHeightMap();

                    if (_guide == 1)
                    {
                        _guide++;
                        _selected = 2;
                    }
                }
                break;

            case 2:
                if (_guide == 2)
                {
                    _options = new string[] { "Mesh Dimensions", "Terrain Attributes", "Terrain Colours" };
                    _selected =  EditorGUILayout.Popup("Editor Options", _selected, _options);
                    EditorGUILayout.Space(10);
                }
                else
                {
                    _selected = EditorGUILayout.Popup("Editor Options", _selected, _options);
                    EditorGUILayout.Space(10);
                }

                string[] colours = new string[_attributes.biomes.Length];

                for (int i = 0; i < _attributes.biomes.Length; i++)
                {
                    colours[i] = _attributes.biomes[i].terrain;
                }

                terrainselected = EditorGUILayout.Popup("Terrain", terrainselected, colours);

                switch (_attributes.biomes.Length)
                {
                    default:
                        _attributes.biomes[terrainselected].colour = EditorGUILayout.ColorField("Colour", _attributes.biomes[terrainselected].colour);
                        _attributes.biomes[terrainselected].height = EditorGUILayout.Slider("Terrain Height", _attributes.biomes[terrainselected].height, -0.1f, 1.0f);
                        break;
                }

                EditorGUILayout.Space(10);
                if (GUILayout.Button("Apply Changes"))
                {
                    _attributes.SetMeshHeightMap();

                    if (_guide == 2)
                    {
                        _guide++;
                        _selected = 3;
                    }
                }
                break;

            case 3:
                if (_guide == 3)
                {
                    _attributes._waterDimensions.SetActive(true);
                    _options = new string[] { "Mesh Dimensions", "Terrain Attributes", "Terrain Colours", "Water Details" };
                    _selected = EditorGUILayout.Popup("Editor Options", _selected, _options);
                    EditorGUILayout.Space(10);
                }
                else 
                {
                    _selected = EditorGUILayout.Popup("Editor Options", _selected, _options);
                    EditorGUILayout.Space(10);
                }

                _attributes._waterDepth = EditorGUILayout.Slider("Water Depth", _attributes._waterDepth, 0, 100);
                _attributes._waterStrength = EditorGUILayout.Slider("Water Strength", _attributes._waterStrength, 0, 2);
                _attributes._rippleDensity = EditorGUILayout.Slider("Ripple Density", _attributes._rippleDensity, 0, 20);
                _attributes._rippleSlimness = EditorGUILayout.Slider("Ripple Slimness", _attributes._rippleSlimness, 0, 20);
                _attributes._rippleSpeed = EditorGUILayout.Slider("Ripple Speed", _attributes._rippleSpeed, 0, 1);
                EditorGUILayout.Space(10);

                if (GUILayout.Button("Apply Changes"))
                {
                    _attributes.SetWaterAttributes();

                    if (_guide == 3)
                    {
                        _guide++;
                        _selected = 4;
                    }
                }
                break;

            case 4:
                if (_guide == 4)
                {
                    _options = new string[] { "Mesh Dimensions", "Terrain Attributes", "Terrain Colours", "Water Details", "Wave Details" };
                    _selected = EditorGUILayout.Popup("Editor Options", _selected, _options);
                    EditorGUILayout.Space(10);
                }
                else
                {
                    _selected = EditorGUILayout.Popup("Editor Options", _selected, _options);
                    EditorGUILayout.Space(10);
                }

                _attributes._waveAmplitude = (int)EditorGUILayout.Slider("Wave Amplitude", _attributes._waveAmplitude, 1, 10);
                _attributes._waveFrequency = (int)EditorGUILayout.Slider("Wave Frequency", _attributes._waveFrequency, 1, 10);
                _attributes._waveSpeed = (int)EditorGUILayout.Slider("Wave Speed", _attributes._waveSpeed, 1, 10);

                EditorGUILayout.Space(10);
                if (GUILayout.Button("Apply Changes"))
                {
                    _attributes.SetWaveAttributes();
                }
                break;
        }
    }
}
