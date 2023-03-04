using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject White_Square;
    public GameObject Vertical_Line;
    public GameObject Horizantal_Line;
    public GameObject Purple_Square;
    public GameObject Green_Square;
    public GameObject Square_Pieces;
    public int rowMax=10;
    public int columnMax=10;

    public int pieceRowMax=4;
    public int pieceColumnMax=4;

    public int leftBorderShop=30;
    public int lowerBorderShop=0;

    //spaces out the pieces in the shop, distance is bottom of lower piece to bottom of next piece
    public int verticalInShopInterval=2;

    //prebuilt boards will be stored in this
    //int values: 0 inidactes empty, 1 indicates a plain square, 2=combo square, cover all of these for a bonus, prob money
    private int[][] boardLVL1=new int[][]{};
    private int[][] boardLVL2=new int[][]{};
    private int[][][] allBoards;

    private GameObject[] pieces;

    // Start is called before the first frame update
    void Start()
    {
        allBoards=new int[][][]{boardLVL1,boardLVL2};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //assuming each block has length 1, so in theory by shifting there location by 1 each time they should line up perfectly
    //adding the lines in only the right spots is tricky, I could just surround each block in lines, and allow overlap
    //I think for the scope of this project that's fine
    public void BuildBoard(int level){        
        for(int y=0;y<rowMax;y++){
            for (int x = 0; x < columnMax; x++) {
                surroundBlockwithLines(x,y);
                GameObject block;
                switch (blockAt(x,y,level)){
                    case 0:
                        block=Instantiate(White_Square);
                        break;

                    case 1:
                        block=Instantiate(Purple_Square);
                        surroundBlockwithLines(x,y);
                        break;

                    default:
                        block=Instantiate(Green_Square);
                        surroundBlockwithLines(x,y);
                        break;
                    
                }                 
                block.transform.position = new Vector2(x,y);
            }                  
        }
    }

    private int blockAt(int x, int y, int level){
           return allBoards[level][x][y];
    }

    //makes 4 lines surrounding the block given by the x and y address
    private void surroundBlockwithLines(int x, int y){
        for (int i = 0; i < 2; i++){
            GameObject line=Instantiate(Horizantal_Line);
            line.transform.position=new Vector2(x+i,y);
        }

        for (int i = 0; i < 2; i++){
            GameObject line=Instantiate(Horizantal_Line);
            line.transform.position=new Vector2(x+i,y);
        }
    }


    public void BuildPieces(bool[][][] piecesForAllLevels){
        foreach(bool[][] piece in piecesForAllLevels){
            BuildPiece(piece);
        }
        setPiecesInShop();
    }

    //builds a piece, as long as movement is handled in such a way thet their relative distance remains the same
    //the array should look like a piece
    private void BuildPiece(bool[][] piece){
        int k=0;
        for (int i = 0; i < pieceRowMax; i++){
            for (int j = 0; j < pieceColumnMax; j++) {                    
                if (piece[i][j]){
                    pieces[k]=Instantiate(Square_Pieces);
                    pieces[k].transform.position = new Vector2(i,j);
                }
                k++;
            }
        }
    }

    private void setPiecesInShop(){
        int i=0;
        foreach(GameObject piece in pieces){
            piece.transform.position = new Vector2(piece.transform.position.x+leftBorderShop,piece.transform.position.y+lowerBorderShop+i*verticalInShopInterval);
            i++;
        }
    }
}
