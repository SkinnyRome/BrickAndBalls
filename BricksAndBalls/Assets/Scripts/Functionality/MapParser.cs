using System;
using System.Collections.Generic;
/// <summary>
/// Struct that defines a Tile. With a type and a life.
/// </summary>
public struct Tile {
    public int type;
    public int life;
}
/// <summary>
/// Extern class that parse a text and create a matrix of the struct Tile.
/// </summary>
public class MapParser  {

    
    private List<Tile> _typeList;//Auxiliar list to read the types of the text.
    public MapParser() {
        _typeList = new List<Tile>();
    }

    /// <summary>
    /// Parse a text and create a matrix of the struct Tile.
    /// </summary>
    /// <param name="text">The text to be read</param>
    /// <returns>A matrix of struct Tiles</returns>
    public Tile[,] ParseText(string text) {

        Tile[,] _grid; //The grid to return.

        string[] auxText;
        auxText = text.Split(new char[] { '\r' });
        auxText = text.Split(new char[] { '\n' });
        int tam = auxText.Length;

        string[] typeText;
        string[] lifeText;
       
        bool matrixEnded = false;
        int j = 3;
        int rows = 0;
        //First we parse the type to know how many tiles we have.
        while (!matrixEnded) {

            typeText = auxText[j].Split(new char[] { '\r' });
            string[] typeLine;
            typeLine = typeText[0].Split(',');//Content of a line, each one is a tile type.
            rows++;

            for (int k = 0; k < 11; k++)
            {

                Tile t;
                if (k == 10)
                {
                    string[] lastLineType;
                    

                    lastLineType = typeLine[k].Split('.');
                    if (lastLineType[lastLineType.Length - 1] == "") {//End of parse. We split the '.' so a "" is left.
                        matrixEnded = true;
                    }
                    
                    int type = int.Parse(lastLineType[0]);
                   
                    t.type = type;
                    t.life = 0;//Initialize
                    _typeList.Add(t);
                    


                } //last row
                else
                {

                    int type = int.Parse(typeLine[k]);
                    
                    t.type = type;
                    t.life = 0;
                    _typeList.Add(t);
                    
                }
            }
                       
            j++;
            
        }//End of type parse

        //Now we can create the matrix and parse the life.

        _grid = new Tile[11, rows];
        Tile[] typeArray = _typeList.ToArray();
        int index = 0;
        for (int r = rows - 1; r >= 0; r--, index++) {


            lifeText = auxText[rows + r + 6].Split(new char[] { '\r' });
            string[] lifeLine;
            lifeLine = lifeText[0].Split(',');

            for (int k = 0; k < 11; k++)
            {
                Tile t = typeArray[k + r * 11];

                if (k == 10)
                {
                    
                    string[] lastLineLife;

                   
                    lastLineLife = lifeLine[k].Split('.');
                   
                    int life = int.Parse(lastLineLife[0]);

                    t.life = life;
                    _grid[k, index] = t;


                } //last row
                else
                {

                    
                    int life = int.Parse(lifeLine[k]);

                    t.life = life;
                    _grid[k, index] = t;


                }
            }

        

        }//End of life parse

        return _grid;
    }



}
