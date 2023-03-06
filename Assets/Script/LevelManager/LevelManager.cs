using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;
    public List<GameObject> levels;

    [Header("Pieces")]
    public List<LevelPieceBase> levelPiecesStart;
    public List<LevelPieceBase> levelPieces;
    public List<LevelPieceBase> levelPiecesEnd;
    public int piecesStartNumber = 2;
    public int piecesNumber = 10;
    public int piecesEndNumber = 1;
    public float timeBetweenPieces = .3f;

    [SerializeField] private int _index;
    private GameObject _currentLevel;

    private List<LevelPieceBase> _spawnedPieces;

    private void Awake()
    {
        //SpawnNextLevel();
        CreateLevelPieces();
    }

    private void SpawnNextLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;

            if (_index >= levels.Count)
            {
                ResetLevelIndex();
            }
        }

        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }

    #region
    private void CreateLevelPieces()
    {
        StartCoroutine(CreateLevelPieceCoroutine());
    }

    private void CreateLevelPiece(List<LevelPieceBase> list)
    {
        var piece = list[Random.Range(0, list.Count)];
        var spawnedPiece = Instantiate(piece, container);

        if (_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[^1];

            spawnedPiece.transform.position = lastPiece.endPiece.position;
        }

        _spawnedPieces.Add(spawnedPiece);
    }

    IEnumerator CreateLevelPieceCoroutine()
    {
        _spawnedPieces = new List<LevelPieceBase>();


        for (int i = 0; i < piecesStartNumber; i++)
        {
            CreateLevelPiece(levelPiecesStart);
            yield return new WaitForSeconds(timeBetweenPieces);
        }

        for (int i = 0; i < piecesNumber; i++)
        {
            CreateLevelPiece(levelPieces);
            yield return new WaitForSeconds(timeBetweenPieces);
        }

        for (int i = 0; i < piecesEndNumber; i++)
        {
            CreateLevelPiece(levelPiecesEnd);
            yield return new WaitForSeconds(timeBetweenPieces);
        }
    }

    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            SpawnNextLevel();
        }
    }
}
