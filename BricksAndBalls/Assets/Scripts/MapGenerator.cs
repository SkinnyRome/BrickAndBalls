using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public uint _rows, _columns;
    private uint _offset;
    private TextAsset _mapTxt;
    private MapParser _mapParser; 
    public GameObject _brickTile;
    public GameObject _triangle;
    public GameObject _horizontalRayTile;
    public GameObject _verticalRayTile;
    public GameObject _boardManager;
    public GameObject _additionalBallsTile;
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
        _offset = 2;
        _mapParser = new MapParser();
    }
    /// <summary>
    ///Create the level given a txt file
    ///and return the board with the objects to be managed
    /// </summary>
    /// <returns></returns>
    public BasicTile[,] CreateLevel(string m) {

        
        _mapTxt = (TextAsset)Resources.Load("Maps/" + m);

         
        Tile[,] grid = _mapParser.ParseText(_mapTxt.text);//Parse the txt file into a matrix to be readed
        BasicTile[,] ObjectGrid = new BasicTile[grid.GetLength(0), grid.GetLength(1) + _offset];
        Vector2 brickPos = new Vector2(0,0);

        for (uint i = 0; i < grid.GetLength(0); i++) {
            for(uint j = 0; j < grid.GetLength(1); j++){
                uint index = j + _offset;

                switch (grid[i,j].type) {
                    case 0:
                        break;
                    case 1:
                        //Normal Brick
                        Brick b = Object.Instantiate(_brickTile, new Vector3(0,0,0), Quaternion.identity).GetComponent<Brick>();
                        if(b != null)
                            b.Init(grid[i,j].life, new Vector2(i, index), _boardManager, _levelManager);
                        ObjectGrid[i,index] = b;
                        break;
                    case 2:
                        //Double life Brick
                        Brick b2 = Object.Instantiate(_brickTile, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Brick>();
                        if (b2 != null)
                            b2.Init(grid[i, j].life * 2, new Vector2(i, index), _boardManager, _levelManager);
                        ObjectGrid[i, index] = b2;
                        break;
                    //Triangle Tiles
                    case 3:
                        Brick t3 = Object.Instantiate(_triangle, new Vector3(0, 0, 0), Quaternion.Euler(0,0,0)).GetComponent<Brick>();
                        if (t3 != null)
                            t3.Init(grid[i, j].life, new Vector2(i, index), _boardManager, _levelManager);
                        ObjectGrid[i, index] = t3;
                        break;                        
                    case 4:
                        Brick t4 = Object.Instantiate(_triangle, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 90)).GetComponent<Brick>();
                        if (t4 != null)
                            t4.Init(grid[i, j].life, new Vector2(i, index), _boardManager, _levelManager);
                        ObjectGrid[i, index] = t4;
                        break;
                    case 5:
                        Brick t5 = Object.Instantiate(_triangle, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 180)).GetComponent<Brick>();
                        if (t5 != null)
                            t5.Init(grid[i, j].life, new Vector2(i, index), _boardManager, _levelManager);
                        ObjectGrid[i, index] = t5;
                        break;
                    case 6:
                        Brick t6 = Object.Instantiate(_triangle, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 270)).GetComponent<Brick>();
                        if (t6 != null)
                            t6.Init(grid[i, j].life, new Vector2(i, index), _boardManager, _levelManager);
                        ObjectGrid[i, index] = t6;
                        break;
                    case 7:
                        //Horizontal Ray
                        RayTile r1 = Object.Instantiate(_horizontalRayTile, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<RayTile>();
                        if (r1 != null)
                            r1.Init(new Vector2(i, index), _boardManager, _levelManager);
                        ObjectGrid[i, index] = r1;
                        break;
                    case 8:
                        //Vertial Ray
                        RayTile r2 = Object.Instantiate(_verticalRayTile, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<RayTile>();
                        if (r2 != null)
                            r2.Init(new Vector2(i, index), _boardManager, _levelManager);
                        ObjectGrid[i, index] = r2;
                        break;
                    case 21:
                        AditionalBallTile ab1 = Object.Instantiate(_additionalBallsTile, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<AditionalBallTile>();
                        if (ab1 != null)
                            ab1.Init(new Vector2(i, index), _boardManager, _levelManager, 1);
                        ObjectGrid[i, index] = ab1;
                        break;
                    case 22:
                        AditionalBallTile ab2 = Object.Instantiate(_additionalBallsTile, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<AditionalBallTile>();
                        if (ab2 != null)
                            ab2.Init(new Vector2(i, index), _boardManager, _levelManager, 2);
                        ObjectGrid[i, index] = ab2;
                        break;
                    case 23:
                        AditionalBallTile ab3 = Object.Instantiate(_additionalBallsTile, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<AditionalBallTile>();
                        if (ab3 != null)
                            ab3.Init(new Vector2(i, index), _boardManager, _levelManager, 3);
                        ObjectGrid[i, index] = ab3;
                        break;
                    default:
                        break;                   

                }



            }
        }
        return ObjectGrid;       

    }

}
