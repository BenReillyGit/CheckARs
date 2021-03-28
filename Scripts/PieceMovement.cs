using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using System.Linq;

 namespace CheckersAI
{
    
public class PieceMovement : MonoBehaviour {

    // Use this for initialization
    // this is the piece that is selected

    // 0=white 1=black turn
    PieceMovement pieceMovement;
   
    ChessSquare chessSquare;
    public int playerTurn=0;
    //SquareCoords square;
    //this is the piece that is selected by clicking on it
	public GameObject selectedPiece;
    public GameObject lastSelectedPiece;

    // the time variable for the movement
	public float timeToMove=0.5f;
    
    //the event system of the game--> need to be disabled when pieces are moving
    public EventSystem mod;

    //ChessSquare SquareColor;
    //CheckersBoard CheckersScript;
    //Square AISquare;

    public GameObject[] squares;

    
    //public Move move1;

    //when a piece is captureed it goes here
    public Transform whitecaptures_pos;
    public Transform blackcaptures_pos;

    //register the number of pieces
    public int nbWhitecaptures;
    public int nbBlackcaptures;

    //all pieces of the set
    public Piece[] allPieces;
    public Piece[] whitePieces, blackPieces;

    public GameObject[] allSquares;
   // public ChessSquare[] blackSquares;
   // public ChessSquare[] whiteSquares;

    //images to set turn
    public Text turnWhiteText,turnBlackText;
    //public Piece kingPieceW;
    //public Piece kingPieceB;

    public float th = 0.008f;
    CheckersBoard board;
    
  

    //private string colour;

    

    void Start ()
	{

        //get the squares
        //squares = GameObject.FindGameObjectsWithTag("chessSquare");
        //GameObject[] squares = GameObject.FindGameObjectsWithTag("chessSquare");
        //allSquares = new GameObject[squares.Length];
        
       // CheckersBoard checkerboard = squares.GetComponent<CheckersBoard>();
        //blackSquares = new ChessSquare[aq.Length/2];
        //whiteSquares = new ChessSquare[aq.Length/2];
        //for(int i = 0; i < aq.Length; i++) {
        //    allSquares[i] = aq[i].GetComponent<ChessSquare>();
        //    //Debug.Log(allSquares[i]);
        //    if(Equals(allSquares[i].colour, "black")) {
        //        blackSquares[j] = allSquares[i];
        //        j++;
        //        //Debug.Log(blackSquares[j]);
        //    }
        //}
        //foreach(ChessSquare sq in blackSquares) {
        //    Debug.Log(sq.name);
        //}
        
        //Move m;
       
        
        //Debug.Log(board);
        
        
        
       
        //get all the pieces
        //GameObject[] go=GameObject.FindGameObjectsWithTag("Piece");
        //allPieces = new Piece[go.Length];
        //whitePieces=new Piece[go.Length/2];
        //blackPieces= new Piece[go.Length/2];
        
        //int jj = 0;
        //int kk = 0;
        ////determine white and black pieces
        //for (int ii=0;ii<go.Length;ii++)
        //{
        //    allPieces[ii] = go[ii].GetComponent<Piece>();


        //    if (allPieces[ii].color==0)
        //    {
        //        whitePieces[jj] = allPieces[ii];
        //        jj++;
        //    }
        //    if (allPieces[ii].color == 1)
        //    {
        //        blackPieces[kk] = allPieces[ii];
        //        kk++;
        //    }
        //}
       
        //blackSquareCoords();
        
        //CheckPiece();
        //CheckSquares();
        //getCoords();
        disablePieces(1);
    }

    
        //coords of all black squares
        void blackSquareCoords() {
        List<int> blackSqList = new List<int>();

        /*foreach(GameObject bq in blackSquares) {
            string blackSq = bq.name;
            string X = blackSq.Substring(0,1);
            string Y = blackSq.Substring(1);
            int x = int.Parse(X);
            int y = int.Parse(Y);
            blackSqList.Add(x);
            blackSqList.Add(y);
        }*/

        ListToArray(blackSqList);
    }
    public void ListToArray(List<int> coords) {
        int[] arrayCoords = coords.ToArray();

        int[,] ArrayCoords = new int[32,2];

        int index = 0;

        for(int i = 0; i < 32; i++) {
            for( int j = 0; j < 2; j++) {
                ArrayCoords[i,j] = arrayCoords[index];
                index++;
            }
        }

        for(int i = 0; i < 32; i++) {
            Debug.Log("( " + ArrayCoords[i,0] + ", " + ArrayCoords[i,1] + " )");
        }
    }  

     void CheckSquares()
   {        
       
        
          
           // CheckPiece(sq);
           
        
   }

    //returns coords of all pieces;
   public void CheckPiece()
   {    
        //var board = new CheckersBoard(); 
       /*foreach(ChessSquare sq in blackSquares)
        {
            List<string> coords = new List<string>();
       
           foreach(Piece pc in allPieces)
            {
                if((((pc.transform.position) - ((sq.transform.position))).magnitude < th))
                {
                    if(pc.color == 0) {
                        sq.state = ChessSquare.SquareState.White;
                        coords.Add(sq.name + " " + pc.name + " " + sq.state);
                    }
                    if(pc.color == 1) {
                        sq.state = ChessSquare.SquareState.Black;
                        coords.Add(sq.name + " " + pc.name + " " + sq.state);
                    }
                    //Debug.Log(pc.name + " is on " + sq.name + "square state " + sq.state);
                    
                }
            }
        
            string[] pieceCoords = coords.ToArray();
           
            for(int i=0; i < pieceCoords.Length; i++) {
                string temp = pieceCoords[i];
                string x = temp.Substring(0,1);
                string y = temp.Substring(1,1);
                
                int X = int.Parse(x);
                int Y = int.Parse(y);
                //Debug.Log("X: " + X + " Y: " + Y);
            }
        }*/
        
   }
   public void getCoords() {
        List<int> coords = new List<int>();
         
        foreach (GameObject sq in squares)
        {
            foreach (Piece pc in allPieces)
            {
                if((((pc.transform.position) - ((sq.transform.position))).magnitude < th))
                {
                    string coord = sq.name;
                    string X = coord.Substring(0,1);
                    string Y = coord.Substring(1);

                    int x = int.Parse(X);
                    int y = int.Parse(Y);

                    coords.Add(x);
                    coords.Add(y);
                }
            }
        }
        foreach(int coord in coords) {
            Debug.Log(coord);
        }
        //ListToArray(coords);
    }

    //events to enable/disable colliders
    public void enablePieces(int col)
    {
        if (col == 0)
        {
            for (int ii = 0; ii < whitePieces.Length; ii++)
            {
                whitePieces[ii].GetComponent<Collider>().enabled = true;

            }
            
        }
        else
        {
            for (int ii = 0; ii < whitePieces.Length; ii++)
            {
                blackPieces[ii].GetComponent<Collider>().enabled = true;

            }
            

        }
    }
    public void disablePieces(int col)
    {
        if (col == 0)
        {
            for (int ii = 0; ii < whitePieces.Length; ii++)
            {
                whitePieces[ii].GetComponent<Collider>().enabled = false;

            }
            
        }
        else
        {
            for (int ii = 0; ii < whitePieces.Length; ii++)
            {
                blackPieces[ii].GetComponent<Collider>().enabled = false;

            }
           
        }
    }
    //call a corrutine to start movement
    public void movePiece (GameObject groundPoint, Piece piecec)
	{
		//Debug.Log("move piece = "+selectedPiece.name+ " to "+ groundPoint.name);
		StartCoroutine( moveToObjective(groundPoint.transform,piecec));
              
    }


    //this calls the second corrutine to move the piece away from the board 
    public void capturePieceCorrutine(Piece piece)
    {
        
        StartCoroutine(capturePiece(piece));

        Debug.Log("capture piece");
        
    }

    IEnumerator moveToObjective (Transform tf, Piece capturedPiece)
	{
        //disable event system
        mod.enabled = false;

        float elapsed = 0;
        Vector3 origin = (selectedPiece.transform.position);

		// linear movement
		while (elapsed <= timeToMove) 
		{
            //controls sliding movement of the piece
			selectedPiece.transform.position = (((tf.position)) - origin) * elapsed / timeToMove + origin;
			elapsed += Time.fixedDeltaTime;

			yield return new WaitForFixedUpdate ();
		}

        lastSelectedPiece = selectedPiece;
        // deactivate the piece
        selectedPiece.transform.GetChild (0).GetComponent<Renderer> ().enabled = false;
		selectedPiece.transform.position = tf.position;
		selectedPiece = null;
        
        //CAPTURNG LOGIC
        if(capturedPiece!=null)
        {
            capturePieceCorrutine(capturedPiece);
        }
        else
        {
            //enable event system

            mod.enabled = true;
            resetSquares();
            changePlayer();
        }
         CheckSquares();

    }
    public void changePlayer()
    {
        if (playerTurn == 0)
        {
            playerTurn = 1;
            turnBlackText.enabled = true;
        }
        else
        {
            playerTurn = 0;
            turnWhiteText.enabled = true;
        }

        enablePieces(playerTurn);
        disablePieces(1-playerTurn);
    }

    public void disableTexts()
    {
        turnWhiteText.enabled = false;
        turnBlackText.enabled = false;
    }

    public void resetSquares()
    {
        foreach (GameObject sq in squares)
        {
            //sq.GetComponent<ChessSquare>().onExitSquare();
        }
    }


    IEnumerator capturePiece(Piece piece)
    {
        //disable event system
        mod.enabled = false;

        float elapsed2 = 0;

        Vector3 destination;
        Vector3 origin =piece.transform.position;

        if (piece.color == 0)
        {
            destination = whitecaptures_pos.position + ((float)nbWhitecaptures /30 )* whitecaptures_pos.right;
            
        }
        else
        {
            destination = blackcaptures_pos.position + ((float)nbBlackcaptures /30) * blackcaptures_pos.right;
            
        }

        // linear movement
        while (elapsed2 <= timeToMove)
        {
            

            piece.transform.position = (destination-origin) * elapsed2 / timeToMove + origin;

            elapsed2 += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        //enable event system
        mod.enabled = true;


        //increment  captures
        if (piece.color == 0)
        {
            nbWhitecaptures++;
        }
        else
        {
            nbBlackcaptures++;
        }

        piece.captured = true;
       
        resetSquares();
        changePlayer();
        
    }

    public bool RaycastIsBlocked(Piece pc, Transform tf)
    {
        bool blocked = false;

        Vector3 height = 0.01f * transform.up;

        RaycastHit hit;

        //this is the theretical distance between points:

        Vector3 direction = tf.position - pc.transform.position;
        float thDist = (direction).magnitude;

        //need to enable colliders to check the raycasts of the opposite pieces
        //enablePieces(1 - playerTurn);

        // Does the ray intersect any objects excluding the player layer?
        if (Physics.Raycast(pc.transform.position + height, direction, out hit, Mathf.Infinity))
        {
            //Debug.Log("Hit_dist="+hit.distance+" dist="+thDist);
            if (hit.distance>thDist)
            {
                blocked = false;
            }
            else
            {
                blocked = true;
            }
        }

        //disable colliders again
        //disablePieces(1 - playerTurn);

        return blocked;
    }
}
}
  