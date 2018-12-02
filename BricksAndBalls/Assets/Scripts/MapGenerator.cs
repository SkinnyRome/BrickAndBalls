using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {


    public TextAsset _mapTxt;
    private MapParser _mapParser;
    
    // Use this for initialization
	void Start () {
		_mapParser = new MapParser();
        CreateLevel();
	}


    public void CreateLevel() {
        Debug.Log(_mapTxt.text[0]);

        //_mapParser.ParseText()


        Tile[,] grid = new Tile[11, 11];
        grid = _mapParser.ParseText(_mapTxt.text);


        //Todo crear el nivel en unity, ya esta la matrix con la info de vida y tipo. su posicion va en funciond de la x,y de la matrix

    }

}
