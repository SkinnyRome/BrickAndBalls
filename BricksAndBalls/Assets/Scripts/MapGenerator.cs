using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public uint _rows, _columns, _offset;
    public TextAsset _mapTxt;
    private MapParser _mapParser; 
    public GameObject _brick1;
    public GameObject _horizontalRay;
    public GameObject _verticalRay;
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
        BasicTile[,] ObjectGrid = new BasicTile[_rows, _columns + _offset];
        grid = _mapParser.ParseText(_mapTxt.text);//Parse the txt file into a matrix to be readed
        Vector2 brickPos = new Vector2(0,0);

        for (uint i = 0; i < grid.GetLength(0); i++) {
            for(uint j = 0; j < grid.GetLength(1); j++){
                uint index = 10 - j + _offset - 1;

                switch (grid[i,j].type) {
                    case 0:
                        break;
                        //Normal Brick
                    case 1:
                        Brick b = Object.Instantiate(_brick1, new Vector3(0,0,0), Quaternion.identity).GetComponent<Brick>();
                        if(b != null)
                            b.Init(grid[i,j].life, new Vector2(i, index), gameObject, _levelManager);
                        ObjectGrid[i,index] = b;
                        break;
                        //Double life Brick
                    case 2:
                        Brick b2 = Object.Instantiate(_brick1, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Brick>();
                        if (b2 != null)
                            b2.Init(grid[i, j].life * 2, new Vector2(i, index), gameObject, _levelManager);
                        ObjectGrid[i, index] = b2;
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        //Horizontal Ray
                        RayTile r1 = Object.Instantiate(_horizontalRay, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<RayTile>();
                        if (r1 != null)
                            r1.Init(new Vector2(i, index), gameObject, _levelManager);
                        ObjectGrid[i, index] = r1;
                        break;
                    case 7:
                        RayTile r2 = Object.Instantiate(_verticalRay, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<RayTile>();
                        if (r2 != null)
                            r2.Init(new Vector2(i, index), gameObject, _levelManager);
                        ObjectGrid[i, index] = r2;
                        break;
                        //Vertial Ray
                    case 8:
                        break;
                    default:
                        break;                   

                }



            }
        }
        return ObjectGrid;       

    }

}
