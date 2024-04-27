using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMaze : MonoBehaviour
{
    [SerializeField][Range(3, 100)] private int _size = 5;
    [SerializeField][Range(5, 10)] private int _wallSize = 5;
    [SerializeField][Range(0, 10)] private int _weight = 5;
    [SerializeField] private GameObject _ground;
    [SerializeField] private GameObject _roof;
    [SerializeField] private GameObject _verticalWallPrefab, _horizontalWallPrefab;

    private const int N = 1, S = 2, E = 4, W = 8;
    private int[,] _grid;
    private GameObject[,] _verticalWalls, _horizontalWalls;
    // Start is called before the first frame update
    void Start()
    {
        _verticalWalls = new GameObject[_size + 1, _size + 1];
        _horizontalWalls = new GameObject[_size + 1, _size + 1];
        GenerateGrid();
        DrawGrid();
        CarveGrid();
        DrawFloor();

        // _ground.transform.localScale = new Vector3(_size * _wallSize, 1, _size * _wallSize);
        // _ground.transform.position = new Vector3(-_size*0.5f, 0, -_size*0.5f);

        _roof.transform.localScale = new Vector3(_size * 5, 1, _size * 5);
        _roof.transform.position = new Vector3(-_size * 0.5f, _verticalWallPrefab.transform.localScale.y, -_size * 0.5f);
    }

    private void CarveGrid()
    {
        for (int row = 0; row < _size; row++)
        {
            for (int cell = 0; cell < _size; cell++)
            {
                if (_grid[cell, row] == N) _horizontalWalls[cell, row + 1].SetActive(false);
                if (_grid[cell, row] == E) _verticalWalls[cell + 1, row].SetActive(false);
            }
        }
    }

    IEnumerator Waiter()
    {
        for (int row = 0; row < _size; row++)
        {
            for (int cell = 0; cell < _size; cell++)
            {
                yield return new WaitForSeconds(1);
                if (_grid[cell, row] == N) _horizontalWalls[cell, row + 1].SetActive(false);
                if (_grid[cell, row] == E) _verticalWalls[cell + 1, row].SetActive(false);
            }
        }
        print("Finished");
    }

    private void DrawGrid()
    {
        for (int i = 0; i <= _size; i++)
        {
            for (int j = 0; j <= _size; j++)
            {
                if (i < _size)
                {
                    float vWallSize = _verticalWallPrefab.transform.localScale.z;

                    float xOffset = -(_size * vWallSize) / 2;
                    float zOffset = -(_size * vWallSize) / 2;

                    var go = Instantiate(_verticalWallPrefab, new Vector3(-vWallSize / 2 + j * _wallSize + xOffset, _wallSize / 2, i * vWallSize + zOffset), Quaternion.identity);
                    go.SetActive(true);
                    go.name = "v" + i + j;
                    go.tag = "wall";
                    go.transform.parent = transform;
                    _verticalWalls[j, i] = go;
                }

                if (j < _size)
                {
                    float hWallSize = _horizontalWallPrefab.transform.localScale.x;

                    float xOffset = -(_size * hWallSize) / 2;
                    float zOffset = -(_size * hWallSize) / 2;

                    var go = Instantiate(_horizontalWallPrefab, new Vector3(j * hWallSize + xOffset, _wallSize / 2, -(hWallSize / 2) + i * hWallSize + zOffset), Quaternion.identity);
                    go.SetActive(true);
                    go.name = "h" + i + j;
                    go.tag = "wall";
                    go.transform.parent = transform;
                    _horizontalWalls[j, i] = go;
                }
            }
        }
    }

    private void GenerateGrid()
    {
        _grid = new int[_size, _size];
        float randomNumber;
        int carvingDirection;

        for (int row = 0; row < _size; row++)
        {
            for (int cell = 0; cell < _size; cell++)
            {
                randomNumber = Random.Range(0, 100);

                if (randomNumber > 50) carvingDirection = N;
                else carvingDirection = E;
                if (cell == _size - 1)
                {
                    if (row < _size - 1) carvingDirection = N;
                    else carvingDirection = W;
                }
                else if (row == _size - 1)
                {
                    if (cell < _size - 1) carvingDirection = E;
                    else carvingDirection = -1;
                }
                _grid[cell, row] = carvingDirection;
            }
        }
    }

    private void DrawFloor()
    {
        for (int x = 0; x < _size; _size++)
        {
            Instantiate(_ground, new Vector3(x*_wallSize, 0, 0), Quaternion.identity);
        }
    }
}
