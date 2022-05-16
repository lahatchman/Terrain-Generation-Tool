using UnityEngine;

public class TerrainMesh : MonoBehaviour
{
    [SerializeField] private Attributes _attributes;
    [SerializeField] private Mesh _mesh;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] public Vector3[] _vertices;
    [SerializeField] private Vector3[] _normals;
    [SerializeField] private Vector2[] _uvs;
    [SerializeField] private int[] _triangles;
    [SerializeField] private int _tris, _vertex;

    #region Setters

    public void SetHeightMap()
    {
        CreateMesh();
        UpdateMesh();
    }

    #endregion

    private void CreateMesh()
    {
        if (_meshFilter.sharedMesh == null) _meshFilter.sharedMesh = new Mesh();

        _mesh = _meshFilter.sharedMesh;

        _vertices = new Vector3[(_attributes._width + 1) * (_attributes._length + 1)];

        _uvs = new Vector2[(_attributes._width + 1) * (_attributes._length + 1)];

        for (int i = 0, z = 0; z <= _attributes._length; z++)
        {
            for (int x = 0; x <= _attributes._width; x++)
            {
                float y = _attributes.meshCurve.Evaluate(_attributes._heightMap[x, z]);
                //Debug.Log(_attributes._heightMap[x, z]);
                _vertices[i] = new Vector3(x, y * _attributes._height, z);
                _uvs[i] = new Vector2(x / (float)_attributes._width , z / (float)_attributes._length);
                i++;
            }
            //i++;
        }

        _triangles = new int[(_attributes._width * _attributes._length) * 6];

        _tris = 0;
        _vertex = 0;
        for (int z = 0; z < _attributes._length; z++)
        {
            for (int x = 0; x < _attributes._width; x++)
            {
                _triangles[_tris + 0] = _vertex + 0;
                _triangles[_tris + 1] = _vertex + _attributes._width + 1;
                _triangles[_tris + 2] = _vertex + 1;
                _triangles[_tris + 3] = _vertex + 1;
                _triangles[_tris + 4] = _vertex + _attributes._width + 1;
                _triangles[_tris + 5] = _vertex + _attributes._width + 2;
                _vertex++;
                _tris += 6;
            }
            _vertex++;
        }
    }

    private void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.uv = _uvs;
        _mesh.RecalculateNormals();
    }
}
