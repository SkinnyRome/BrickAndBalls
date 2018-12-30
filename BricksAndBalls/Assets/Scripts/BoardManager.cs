using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {


    private LevelManager _levelManager;
    private BasicTile[,] _board;


	// Use this for initialization
	void Start () {
		
	}
	
    public void Init(LevelManager lm, BasicTile[,] grid){
        _levelManager = lm;
        _board = grid;
    }

    public LevelManager GetLevelManager()
    {
        return _levelManager;
    }

    public void Fall(){
        
        for (int i = 0; i < _board.GetLength(0); i++) {
            for (int j = 0; j < _board.GetLength(1); j++) {

                if (_board[i, j] != null) {
                    _board[i, j].Fall();
                    //TODO: controlar que no se pregunte por una posición que no existe (cuidado con el -1 en las ultimas filas)
                    _board[i,j - 1] = _board[i, j];
                    _board[i, j] = null;           
                    
                }
            }
        }


    }
    /// <summary>
    /// Check the first row of the grid to see if some bricks are left.
    /// </summary>
    /// <returns>Returns true if some tile who needs to be destroyed is still alive.</returns>
    public bool CheckFirstRow() {

        for (int i = 0; i < _board.GetLength(0); i++) {
            if (_board[i,0] != null) {

                if (_board[i, 0].NeedToBeDestroyed())
                    return true;
            }
        }

        return false;
    }

    public bool CheckWarningRow()
    {

        for (int i = 0; i < _board.GetLength(0); i++)
        {
            if (_board[i, 1] != null)
            {

                if (_board[i, 1].NeedToBeDestroyed())
                    return true;
            }
        }

        return false;
    }


    public bool LevelCompleted() {

        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {

                if (_board[i, j] != null)
                {
                    if (_board[i, j].NeedToBeDestroyed())
                        return false;
                }
            }
        }

        return true;
    }

    
    public void HitRow(int row)
    {
        for(int i = 0; i < _board.GetLength(0); i++)
        {
            if (_board[i, row] != null)
            {
                _board[i, row].DecreaseLife(1);
            }
        }
    }

    public void HitColumn(int column)
    {
        for (int i = 0; i < _board.GetLength(1); i++)
        {
            if (_board[column, i] != null)
            {
                _board[column, i].DecreaseLife(1);
            }
        }
    }

    public void CreateTileAtRandomPos(BasicTile.TILE_TYPE type, int n)
    {


        for (int l = 0; l < n; l++){
            bool tilePlaced = false;
            do
            {
                int i = Random.Range(0, _board.GetLength(0));
                int j = Random.Range(0, _board.GetLength(1));
                if(_board[i,j] == null)
                {
                    switch (type)
                    {
                        case BasicTile.TILE_TYPE.BRICK:
                            break;
                        case BasicTile.TILE_TYPE.HRAY:
                            RayTile rH = Object.Instantiate((GameObject)Resources.Load("Prefabs/HorizontalRay"), new Vector3(0, 0, 0), Quaternion.identity).GetComponent<RayTile>();
                            if (rH != null)
                            {
                                rH.Init(new Vector2(i, j), gameObject, _levelManager);
                                _board[i, j] = rH;

                            }
                            break;
                        case BasicTile.TILE_TYPE.VRAY:
                            RayTile rV = Object.Instantiate((GameObject)Resources.Load("Prefabs/VerticalRay"), new Vector3(0, 0, 0), Quaternion.identity).GetComponent<RayTile>();
                            if (rV != null)
                            {
                                rV.Init(new Vector2(i, j), gameObject, _levelManager);
                                _board[i, j] = rV;
                            }
                            break;
                    }

                    tilePlaced = true;
                }

            } while (!tilePlaced);
        }
    }
}


