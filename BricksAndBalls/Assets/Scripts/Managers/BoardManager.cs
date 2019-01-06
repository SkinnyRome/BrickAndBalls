using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// GameObject who stores and manages the grid of Tiles.
/// </summary>
public class BoardManager : MonoBehaviour {


    private LevelManager _levelManager;
    private BasicTile[,] _board;
    private int _visibleRow;

	/// <summary>
    /// Initialize the BoardManager.
    /// </summary>
    /// <param name="lm">The level manager of the scene.</param>
    /// <param name="grid">The grid created by the MapGenerator.</param>
    public void Init(LevelManager lm, BasicTile[,] grid){
        _levelManager = lm;
        _board = grid;
        _visibleRow = 12;
        UpdateBoard();
    }

    private void UpdateBoard()
    {
        //Deactivate the rows above the visible area.

        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                if (_board[i, j] != null)
                {
                    if (j > _visibleRow)
                        _board[i, j].Hide();

                }
            }
        }
    }

    private void CheckForShow(int j, BasicTile t) {
        if (t.GetRow() <= _visibleRow)
            t.Show();
        else
            t.Hide();
        
    }

    public LevelManager GetLevelManager()
    {
        return _levelManager;
    }

    public void Fall(){
        
        for (int i = 0; i < _board.GetLength(0); i++) {
            for (int j = 0; j < _board.GetLength(1); j++) {

                if (_board[i, j] != null) {

                    if (j < 1 && !_board[i, j].NeedToBeDestroyed())
                    {
                        //Erase the special tile.
                        Destroy(_board[i, j].gameObject);                                

                    }
                    else {
                        _board[i, j].Fall();
                        //TODO: controlar que no se pregunte por una posición que no existe (cuidado con el -1 en las ultimas filas)
                        _board[i, j - 1] = _board[i, j];
                        CheckForShow(j, _board[i, j]);
                        _board[i, j] = null;
                    }


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

    /// <summary>
    /// Hit a row of the grid indicated by 'row' or,  if 'destroy' its true, destroys it.
    /// </summary>
    /// <param name="row">The row to hit</param>
    /// <param name="destroy">Boolean to indicate if destroy or only hit</param>
    public void HitRow(uint row, bool destroy = false)
    {
        for(int i = 0; i < _board.GetLength(0); i++)
        {
            if (_board[i, row] != null)
            {
                if (destroy)
                    _board[i, row].Destroy(); 
                else
                    _board[i, row].DecreaseLife(1);
            }
        }
    }

    
    /// <summary>
    /// Hit a column of the grid indicated by 'column'
    /// </summary>
    /// <param name="column">The columnn to hit</param>
    public void HitColumn(uint column)
    {
        for (int i = 0; i < _board.GetLength(1); i++)
        {
            if (_board[column, i] != null)
            {
                _board[column, i].DecreaseLife(1);
            }
        }
    }

    /// <summary>
    /// Destroy the last row of the grid, that is the firs row, starting from the bottom, which have a brick;
    /// </summary>
    public void DestroyLastRow()
    {
        uint row = 0;
        int i = 0;
        int j = 0;
        bool flag = true;
        while (j < _board.GetLength(1)  && flag)
        {
            i = 0;
            while (i < _board.GetLength(0) && flag)
            {

                if (_board[i, j] != null && _board[i,j].GetTileType() == BasicTile.TILE_TYPE.BRICK)
                {
                    flag = false;
                    row = (uint)j;
                }
                i++;
            }
            j++;
        }
        if(!flag)
            HitRow(row, true);
      
    }

    /// <summary>
    /// Create 'n' Tiles of the type 'type' at free random positions in the grid
    /// </summary>
    /// <param name="type">The type of tile to instantiate</param>
    /// <param name="n">The number of tiles to instantiate</param>
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


