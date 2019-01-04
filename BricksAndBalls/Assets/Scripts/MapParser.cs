using System;
using System.Collections.Generic;

public struct Tile {
    public int type;
    public uint life;
}

public class MapParser  {

    private Tile[,] _grid;
    private List<Tile> _typeList;
    public MapParser() {
        _typeList = new List<Tile>();
    }


    public Tile[,] ParseText(string text) {


        string[] auxText;
        auxText = text.Split(new char[] { '\r' });
        auxText = text.Split(new char[] { '\n' });
        int tam = auxText.Length;

        string[] typeText;
        string[] lifeText;
       
        bool matrixEnded = false;
        int j = 3;
        int rows = 0;
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
                   
                    uint life = uint.Parse(lastLineLife[0]);

                    t.life = life;
                    _grid[k, index] = t;


                } //last row
                else
                {

                    
                    uint life = uint.Parse(lifeLine[k]);

                    t.life = life;
                    _grid[k, index] = t;


                }
            }

        

        }//End of life parse

        return _grid;
    }



}
