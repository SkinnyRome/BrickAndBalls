

public struct Tile {
    public int type;
    public int life;
}

public class MapParser  {

    private Tile[,] _grid;

    public MapParser() {
        _grid = new Tile[11,11];
    }


    public Tile[,] ParseText(string text) {


        //string[] typeText = "";
        //string[] lifeText = "";
        string[] auxText;
        auxText = text.Split(new char[] { '\r' });
        auxText = text.Split(new char[] { '\n' });
        int tam = auxText.Length;

        string[] typeText;
        string[] lifeText;
        //3 y 17
        //repasar para ver si esta bien hecho
        for (int j = 3; j < 14; j++) {
           
            typeText = auxText[j].Split(new char[] { '\r' });
            lifeText = auxText[j + 14].Split(new char[] { '\r' });
            string[] typeLine;
            string[] lifeLine;
            typeLine = typeText[0].Split(',');
            lifeLine = lifeText[0].Split(',');
            

           
            for (int k = 0; k < 11; k++) {

                Tile t;
                if (k == 10)
                {
                    string[] lastLineType;
                    string[] lastLineLife;

                    lastLineType = typeLine[k].Split('.');
                    lastLineLife = lifeLine[k].Split('.');
                    int type = int.Parse(lastLineType[0]);
                    int life = int.Parse(lastLineType[0]);
                    t.type = type;
                    t.life = life;
                    _grid[k, j - 3] = t;


                } //ultima fila
                else {
                   
                    int type = int.Parse(typeLine[k]);
                    int life = int.Parse(lifeLine[k]);
                    t.type = type;
                    t.life = life;
                    _grid[k, j - 3] = t;

                }    
              
                  
                    

            }

               
            
        }

        return _grid;
    }



}
