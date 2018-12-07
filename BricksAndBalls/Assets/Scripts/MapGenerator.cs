using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {


    public TextAsset _mapTxt;
    private MapParser _mapParser; 
    public GameObject _brick1;
    private LevelManager _levelManager;
    
    // Use this for initialization
	void Start () {
		_mapParser = new MapParser();         
	}

    public void Init(LevelManager lm)
    {
        _levelManager = lm;
        _mapParser = new MapParser();
    }

    public BasicTile[,] CreateLevel() {
       

        Tile[,] grid = new Tile[11, 11];
        BasicTile[,] ObjectGrid = new BasicTile[11, 11];
        grid = _mapParser.ParseText(_mapTxt.text);
        Vector2 brickPos = new Vector2(0,0);

        for (uint i = 0; i < grid.GetLength(0); i++) {
            for(uint j = 0; j < grid.GetLength(1); j++){
                //brickPos.x = _InitPos.x + i;
                //brickPos.y = _InitPos.y + 10 - j;
                switch (grid[i,j].type) {
                    case 0:
                        break;
                    case 1:
                        Brick b = Object.Instantiate(_brick1, new Vector3(0,0,0), Quaternion.identity).GetComponent<Brick>();  
                        if(b != null)
                            b.Init(grid[i,j].life, new Vector2(i, 10 - j), gameObject, _levelManager);
                        ObjectGrid[i, 10 - j] = b;
                        break;
                    case 2:
                        break;
                    default:
                        break;                   

                }



            }
        }
        return ObjectGrid;       

    }

}
