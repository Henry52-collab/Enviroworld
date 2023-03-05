using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static string[] piece_tags;
    public GameObject White_Square;
    public GameObject Vertical_Line;
    public GameObject Horizantal_Line;
    public GameObject Purple_Square;
    public GameObject Green_Square;
    public GameObject Square_Pieces;

    public int lengthMax=10;    
    public int boardBottomCornerx=-5;
    public int boardBottomCornery=-5;
    public int pieceRowMax=3;
    public int pieceColumnMax=3;

    public int leftBorderShop=7;
    public int lowerBorderShop=-5;

    //spaces out the pieces in the shop, distance is bottom of lower piece to bottom of next piece
    public int verticalInShopInterval=5;

    //prebuilt boards will be stored in this
    //int values: 0 inidactes empty, 1 indicates a plain square, 2=combo square, cover all of these for a bonus, prob money
    private int[][] boardLVL1=new int[][]{new int[]{1,2,0,1,1,1,1,0},new int[]{1,1,1,1,1,0,1},new int[]{1,1,1,0,2,1,1},new int[]{1,1,1,0,0,1,1},new int[]{0,1,1,0,2,1,1,1},new int[]{1,1,1,0,1,1,0,1}};
    private int[][] piece1LVL1=new int[][]{new int[]{0,0,0},new int[]{0,1,1},new int[]{1,1,0}};
    private int[][] piece2LVL1=new int[][]{new int[]{0,0,1},new int[]{1,1,1},new int[]{1,0,0}};
    //private int[][] piece3LVL1=new int[][]{new int[]{0,1,1,1},new int[]{1,1,1,0},new int[]{1,1,0,0},new int[]{0,1,0,0}};
    //for now two pieces, eco piece and non eco piece

    private int[][][] piecesLVL1;
    private int[][] boardLVL2=new int[][]{};
    private int[][][] allBoards;
    private int[][][][] allPiecesAllLevels;
    
    private static GameObject[][] pieces;
    private static float[][] deltaXY=new float[2][];
    private static List<GameObject> linesV=new List<GameObject>();
    private static List<GameObject> linesH=new List<GameObject>();
    private static float[][] deltaLineXY=new float[4][];
    
    private int nextTagNum=0;
    
    public static void setDelta(Vector2 mousePosition,string tag){
        deltaXY[0]=new float[getLength(pieces)];
        deltaXY[1]=new float[getLength(pieces)];
        deltaLineXY[0]=new float[linesH.Count];
        deltaLineXY[1]=new float[linesH.Count];
        deltaLineXY[2]=new float[linesV.Count];
        deltaLineXY[3]=new float[linesV.Count];
        int i=0;      
        foreach(GameObject[] piece in pieces){
            foreach(GameObject square in piece){
                if(square.tag.Equals(tag)){         
                    deltaXY[0][i]=mousePosition.x-square.transform.position.x;
                    deltaXY[1][i]=mousePosition.y-square.transform.position.y;               
                    i++;
                }            
            }
        }
        
        i=0;
        foreach(GameObject line in linesH){
            if(line.tag.Equals(tag)){
                deltaLineXY[0][i]=mousePosition.x-line.transform.position.x;
                deltaLineXY[1][i]=mousePosition.y-line.transform.position.y;                
                i++;
            }          
        }
        i=0;
        foreach(GameObject line in linesV){
            if(line.tag.Equals(tag)){
                deltaLineXY[2][i]=mousePosition.x-line.transform.position.x;
                deltaLineXY[3][i]=mousePosition.y-line.transform.position.y;    
                i++;
            }          
        }
        
    }

    public static void dragPiece(Vector2 mousePosition,string tag){  
        int i=0;      
        foreach(GameObject[] piece in pieces){
            foreach(GameObject square in piece){
                if(square.tag.Equals(tag)){                    
                    square.transform.position=new Vector2((float)System.Math.Ceiling(mousePosition.x+deltaXY[0][i]),(float)System.Math.Ceiling(mousePosition.y+deltaXY[1][i]));                
                    i++;
                }            
            }
        }
        i=0;
        float half=(float)0.5;
        foreach(GameObject line in linesH){
            if(line.tag.Equals(tag)){
                line.transform.position=new Vector2((float)System.Math.Ceiling(mousePosition.x+deltaLineXY[0][i]),(float)System.Math.Ceiling(mousePosition.y+deltaLineXY[1][i]-half)+half); 
                i++;
            }          
        }
        i=0;        
        foreach(GameObject line in linesV){
            if(line.tag.Equals(tag)){
                line.transform.position=new Vector2((float)System.Math.Ceiling(mousePosition.x+deltaLineXY[2][i]-half)+half,(float)System.Math.Ceiling(mousePosition.y+deltaLineXY[3][i])); 
                i++;
            }          
        }
    }

    // Start is called before the first frame update
    void Start()
    {        
        allBoards=new int[][][]{boardLVL1,boardLVL2};
        piecesLVL1=new int[][][]{piece1LVL1,piece2LVL1};        
        allPiecesAllLevels=new int[][][][]{piecesLVL1};
        lengthMax=boardLVL1.Length;
        pieces=new GameObject[piecesLVL1.Length][];
        BuildBoard(0);    
        BuildPieces(allPiecesAllLevels[0]);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //assuming each block has length 1, so in theory by shifting there location by 1 each time they should line up perfectly
    //adding the lines in only the right spots is tricky, I could just surround each block in lines, and allow overlap
    //I think for the scope of this project that's fine
    public void BuildBoard(int level){       
        for(int i=0;i<lengthMax;i++){
            for (int j = 0; j < lengthMax; j++) {
                int x=i+boardBottomCornerx;
                int y=j+boardBottomCornery;
                surroundBlockwithLines(x,y);
                GameObject block;
                switch (blockAt(i,j,level)){
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
        float half=(float)0.5;
        for (int i = 0; i < 2; i++){
            GameObject line=Instantiate(Horizantal_Line);
            line.transform.position=new Vector2(x, y+i-half);           
        }

        for (float i = 0; i < 2; i++){
            GameObject line=Instantiate(Vertical_Line);
            line.transform.position=new Vector2(x+i-half,y);
        }
    }

    private void surroundBlockwithLines(int x, int y, string tag){
        float half=(float)0.5;
        for (int i = 0; i < 2; i++){
            linesH.Add(Instantiate(Horizantal_Line));
            linesH[linesH.Count-1].transform.position=new Vector2(x, y+i-half);
            linesH[linesH.Count-1].tag=tag;           
        }

        for (float i = 0; i < 2; i++){
            linesV.Add(Instantiate(Vertical_Line));
            linesV[linesV.Count-1].transform.position=new Vector2(x+i-half,y);
            linesV[linesV.Count-1].tag=tag;
        }
    }


    public void BuildPieces(int[][][] piecesForAllLevels){        
        foreach(int[][] piece in piecesForAllLevels){
            pieces[nextTagNum]=new GameObject[numBlocksinPiece(piece)];
            BuildPiece(piece);
        }
        setPiecesInShop();
    }

    //builds a piece, as long as movement is handled in such a way thet their relative distance remains the same
    //the array should look like a piece
    private void BuildPiece(int[][] piece){
        int k=0;
        string nextTag="piece"+nextTagNum;        
        for (int i = 0; i < pieceRowMax; i++){
            for (int j = 0; j < piece[i].Length; j++) {                    
                if (piece[i][j]==1){                   
                    pieces[nextTagNum][k]= Instantiate(Square_Pieces);
                    pieces[nextTagNum][k].transform.position = new Vector2(i,j);
                    pieces[nextTagNum][k].tag=nextTag;
                    surroundBlockwithLines(i,j,nextTag);
                    k++;
                }
                
            }
        }
        nextTagNum++;
    }

    private void setPiecesInShop(){
        int i=0;
        foreach(GameObject[] piece in pieces){
            foreach(GameObject square in piece){
                square.transform.position = new Vector2(square.transform.position.x+leftBorderShop, square.transform.position.y+lowerBorderShop+i*verticalInShopInterval);
            }
            foreach(GameObject line in linesH){
                if(piece[0].tag.Equals(line.tag)){
                    line.transform.position=new Vector2(line.transform.position.x+leftBorderShop, line.transform.position.y+lowerBorderShop+i*verticalInShopInterval);
                }
            }
            foreach(GameObject line in linesV){
                if(piece[0].tag.Equals(line.tag)){
                    line.transform.position=new Vector2(line.transform.position.x+leftBorderShop, line.transform.position.y+lowerBorderShop+i*verticalInShopInterval);
                }
            }
            i++;
        }
    }

    private int numBlocksinPiece(int[][] piece){
        int result=0;
        foreach(int[] row in piece){
            foreach(int square in row){
                if (square>0){
                    result++;
                }
            }
        }
        return result;
    }

    private static int getLength(GameObject[][] array2D){
        int result=0;
        foreach(GameObject[] piece in pieces){
            result+=piece.Length;
        }
        return result;
    }
}
