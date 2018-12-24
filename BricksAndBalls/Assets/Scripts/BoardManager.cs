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
    public void fall(){
        for (int i = 0; i < _board.GetLength(0); i++) {
            for (int j = 0; j < _board.GetLength(1); j++) {

                if (_board[i, j] != null) {
                    _board[i, j].fall();
                    _board[i,j - 1] = _board[i, j];
                    _board[i, j] = null;
                }
            }
        }

    }

    public void hitRow(int row)
    {
        for(int i = 0; i < _board.GetLength(0); i++)
        {
            if (_board[i, row] != null)
            {
                _board[i, row].DecreaseLife(1);
            }
        }
    }

    public void hitColumn(int column)
    {
        for (int i = 0; i < _board.GetLength(1); i++)
        {
            if (_board[column, i] != null)
            {
                _board[column, i].DecreaseLife(1);
            }
        }
    }
}


