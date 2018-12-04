using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {


    public TextAsset _mapTxt;
    private MapParser _mapParser;
    public GameObject _mapGenerator;
    public GameObject _brick1;
    
    // Use this for initialization
	void Start () {
		_mapParser = new MapParser();
        CreateLevel();


	}


    public void CreateLevel() {
       

        Tile[,] grid = new Tile[11, 11];
        grid = _mapParser.ParseText(_mapTxt.text);
        Vector2 brickPos = new Vector2(0,0);

        for (int i = 0; i < grid.GetLength(0); i++) {
            for(int j = 0; j < grid.GetLength(1); j++){
                //brickPos.x = _InitPos.x + i;
                //brickPos.y = _InitPos.y + 10 - j;
                switch (grid[i,j].type) {
                    case 0:
                        break;
                    case 1:
                        GameObject b = Object.Instantiate(_brick1, new Vector3(0,0,0), Quaternion.identity);
                        b.transform.parent = _mapGenerator.transform;
                        b.GetComponent<Brick>().Init(grid[i,j].life, new Vector2(i, 10 - j));
                        break;
                    case 2:
                        break;
                    default:
                        break;                   

                }



            }
        }







        

    }

}
