using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public int _rows, _columns;
    public TextAsset _mapTxt;
    private MapParser _mapParser; 
    public GameObject _brick1;
    private LevelManager _levelManager;
    
    // Use this for initialization
	void Start () {
		       
	}
    /// <summary>
    ///  Initialize the object, create the mapParser and get the level manager.   
    /// </summary>
    /// <param name="lm"> LevelManager </param>
    public void Init(LevelManager lm)
    {
        _levelManager = lm;
        _mapParser = new MapParser();
    }
    /// <summary>
    ///Create the level given a txt file
    ///and return the board with the objects to be managed
    /// </summary>
    /// <returns></returns>
    public BasicTile[,] CreateLevel() {
       

        Tile[,] grid = new Tile[_rows, _columns];
        BasicTile[,] ObjectGrid = new BasicTile[_rows, _columns];
        grid = _mapParser.ParseText(_mapTxt.text);//Parse the txt file into a matrix to be readed
        Vector2 brickPos = new Vector2(0,0);

        for (uint i = 0; i < grid.GetLength(0); i++) {
            for(uint j = 0; j < grid.GetLength(1); j++){
                
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
                        Brick b2 = Object.Instantiate(_brick1, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Brick>();
                        if (b2 != null)
                            b2.Init(grid[i, j].life * 2, new Vector2(i, 10 - j), gameObject, _levelManager);
                        ObjectGrid[i, 10 - j] = b2;
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    default:
                        break;                   

                }



            }
        }
        return ObjectGrid;       

    }

}
