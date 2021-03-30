using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CheckersAI
{
    class CheckersBoardInterface : MonoBehaviour
    {

        public Piece[] allPieces;
        private Piece[] whitePieces, blackPieces;
        ChessSquare chessSquare;
        private ChessSquare[] allSquares;
        public ChessSquare[] blackSquares;
        public GameObject[] go;
        public GameObject[] squares;
        public CheckersBoard board;
        public IEnumerable<Move> moves;
        public GameObject[] mat;
        public Material kingWhite;
        public Material kingBlack;




        void Start()
        {
            board = new CheckersBoard(CheckersBoard.GetInitialBoard());
            moves = board.GetMoves();

            mat = GameObject.FindGameObjectsWithTag("mat");
            
            squares = GameObject.FindGameObjectsWithTag("chessSquare");
            allSquares = new ChessSquare[squares.Length];
            blackSquares = new ChessSquare[squares.Length / 2];
            
            int j = 0;
            for (int i = 0; i < squares.Length; i++)
            {
                allSquares[i] = squares[i].GetComponent<ChessSquare>();
                //Debug.Log(allSquares[i]);
                if (Equals(allSquares[i].colour, "black"))
                {
                    blackSquares[j] = allSquares[i];
                    j++;
                    //Debug.Log(blackSquares[j]);
                }
            }
            mat = GameObject.FindGameObjectsWithTag("mat");
            //get all the pieces
            go = GameObject.FindGameObjectsWithTag("Piece");
            allPieces = new Piece[go.Length];
            whitePieces = new Piece[go.Length / 2];
            blackPieces = new Piece[go.Length / 2];

            int jj = 0;
            int kk = 0;
            //determine white and black pieces
            for (int ii = 0; ii < go.Length; ii++)
            {
                allPieces[ii] = go[ii].GetComponent<Piece>();


                if (allPieces[ii].color == 0)
                {
                    whitePieces[jj] = allPieces[ii];
                    jj++;
                }
                if (allPieces[ii].color == 1)
                {
                    blackPieces[kk] = allPieces[ii];
                    kk++;
                }
            }
           
        }
        public void Update()
        {
            if (board.Winner != Kind.None)
            {
                Application.Quit();
            }
        }


        // Use this for initialization
        // this is the piece that is selected

        // 0=white 1=black turn

        public int playerTurn = 0;

        //this is the piece that is selected by clicking on it
        public GameObject selectedPiece;
        public GameObject lastSelectedPiece;

        // the time variable for the movement
        public float timeToMove = 0.5f;

        //the event system of the game--> need to be disabled when pieces are moving
        public EventSystem mod;

        //when a piece is captureed it goes here
        public Transform whitecaptures_pos;
        public Transform blackcaptures_pos;

        //register the number of pieces
        public int nbWhitecaptures;
        public int nbBlackcaptures;


        public Text turnWhiteText, turnBlackText;

        public float th = 0.1f;
        public void getCoords()
        {
            List<int> coords = new List<int>();

            foreach (GameObject sq in squares)
            {
                foreach (Piece pc in allPieces)
                {
                    if ((((pc.transform.position) - ((sq.transform.position))).magnitude < th))
                    {
                        string coord = sq.name;
                        string X = coord.Substring(0, 1);
                        string Y = coord.Substring(1);

                        int x = int.Parse(X);
                        int y = int.Parse(Y);

                        coords.Add(x);
                        coords.Add(y);
                    }
                }
            }
            foreach (int coord in coords)
            {
                Debug.Log(coord);
            }
        }
        string destSquare;
        Piece moveingPiece;
        public void GetMoves()
        {
             
        }
        public void GetBestMoveWhite()
        {
           
            if(board.Turn == Color.Black)
            {
                //board = new CheckersBoard(CheckersBoard.GetInitialBoard());
                var player = new MctsPlayer();
                //GetMoves();

                var move = player.GetMove(board);

                board = board.ApplyMove(move);
                moves = board.GetMoves();
                if (move.IsJump == true)
                {
                    int x = move.JumpedSquare.X;
                    int y = move.JumpedSquare.Y;
                    foreach (GameObject sq in squares)
                    {
                        foreach (Piece pc in allPieces)
                        {
                            string temp = sq.name;
                            string xx = temp.Substring(0, 1);
                            string yy = temp.Substring(1, 1);
                            int X = int.Parse(xx);
                            int Y = int.Parse(yy);
                            if (x == X && y == Y)
                            {
                                if (((sq.transform.position) - (pc.transform.position)).magnitude < th)
                                {
                                    CapturePieceCorrutine(pc);
                                }
                            }

                        }
                    }
                }

                foreach (GameObject sq in squares)
                {
                    string temp = sq.name;
                    string x = temp.Substring(0, 1);
                    string y = temp.Substring(1, 1);
                    int X = int.Parse(x);
                    int Y = int.Parse(y);
                    if (X == move.Start.X && Y == move.Start.Y)
                    {
                        foreach (Piece pc in allPieces)
                        {
                            if (((sq.transform.position) - (pc.transform.position)).magnitude < th)
                            {
                                moveingPiece = pc;
                                string xx = move.ToString();
                                string destSquareX = xx.Substring(10, 1);
                                string destSquareY = xx.Substring(12, 1);
                                destSquare = destSquareX + destSquareY;
                                //Debug.Log(destSquare);
                            }
                        }
                    }
                }

                //Debug.Log(move);
                moveMyPiece();
            }
           

        }

    public void moveMyPiece()
        {
            foreach (GameObject sq in squares)
            {
                foreach (GameObject g in go)
                {
                    if (sq.name.Equals(destSquare) && moveingPiece.name.Equals(g.name))
                    {
                        disableTexts();
                        selectedPiece = g;
                        movePiece(sq, moveingPiece);
                        
                        changePlayer();
                       
                    }
                }
                
            }
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
        public void SetKings()
        {
            foreach (GameObject sq in squares)
            {
                foreach (Piece pc in allPieces)
                {
                    if((sq.transform.position - pc.transform.position).magnitude < th) 
                    {
                        foreach (GameObject m in mat)
                        {
                            string temp = sq.name;
                            string x = temp.Substring(0, 1);
                            string y = temp.Substring(1, 1);
                            int X = int.Parse(x);
                            int Y = int.Parse(y);
                            if ((sq.transform.position - m.transform.position).magnitude < th && Y == 0 && pc.color == 0)
                            {
                                m.GetComponent<Renderer>().material = kingWhite;
                            }
                            if ((sq.transform.position - m.transform.position).magnitude < th && Y == 7 && pc.color == 1)
                            {
                                m.GetComponent<Renderer>().material = kingBlack;
                            }
                        }
                    }
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
        public void ManualMovePiece(GameObject groundPoint, Piece piece)
        {
            foreach (GameObject sq in squares)
            {
                if ((sq.transform.position - selectedPiece.transform.position).magnitude < th)
                {
                    string temp = groundPoint.name;
                    string startXT = temp.Substring(0, 1);
                    string startYT = temp.Substring(1, 1);
                    string temp1 = sq.name;
                    string endXT = temp1.Substring(0, 1);
                    string endYT = temp1.Substring(1, 1);
                    int startX = int.Parse(startXT);
                    int startY = int.Parse(startYT);
                    int endX = int.Parse(endXT);
                    int endY = int.Parse(endYT);
                    foreach (Move mv in board.moves)
                    {
                        if (startX == mv.End.X && startY == mv.End.Y && endX == mv.Start.X && endY == mv.Start.Y)
                        {
                            if(mv.IsJump == true)
                            {
                                int x = mv.JumpedSquare.X;
                                int y = mv.JumpedSquare.Y;
                                foreach (GameObject squ in squares)
                                {
                                    foreach(Piece pc in allPieces)
                                    {
                                        string temp2 = squ.name;
                                        string xx = temp2.Substring(0, 1);
                                        string yy = temp2.Substring(1, 1);
                                        int X = int.Parse(xx);
                                        int Y = int.Parse(yy);
                                        if (x == X && y == Y)
                                        {
                                            if (((squ.transform.position) - (pc.transform.position)).magnitude < th)
                                            {
                                                CapturePieceCorrutine(pc);
                                            }
                                        }
                                    }
                                }
                            }
                            board = board.ApplyMove(mv);
                            moves = board.GetMoves();
                        }
                    }
                }
            }
            Debug.Log("move piece = " + selectedPiece.name + " to " + groundPoint.name);
            StartCoroutine(MoveToObjective(groundPoint.transform, piece));           
        }
        public void movePiece(GameObject groundPoint, Piece piecec)
        {
            Debug.Log("move piece = "+selectedPiece.name+ " to "+ groundPoint.name);
            StartCoroutine(MoveToObjective(groundPoint.transform, piecec));
        }

        //this calls the second corrutine to move the piece away from the board 
        public void CapturePieceCorrutine(Piece piece)
        {
            StartCoroutine(capturePiece(piece));
            Debug.Log("capture piece");
        }

        IEnumerator MoveToObjective(Transform tf, Piece capturedPiece)
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

                yield return new WaitForFixedUpdate();
            }

            lastSelectedPiece = selectedPiece;
            //// deactivate the piece
            selectedPiece.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
            selectedPiece.transform.position = tf.position;
            selectedPiece = null;

            //CAPTURNG LOGIC
            /*if (capturedPiece = null)
            {
                CapturePieceCorrutine(capturedPiece);
            }*/
            //else
            //{
            //    //enable event system
            mod.enabled = true;
            resetSquares();
            disableTexts();
            changePlayer();
            SetKings();
            GetBestMoveWhite();
           
            //}
        }
        public void changePlayer()
        {
            if (board.Turn == Color.Black)
            {
                turnBlackText.enabled = true;
            }
            else
            {
                turnWhiteText.enabled = true;
            }

            //enablePieces(playerTurn);
           // disablePieces(1 - playerTurn);
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
                sq.GetComponent<ChessSquare>().onExitSquare();
            }
        }


        IEnumerator capturePiece(Piece piece)
        {
            //disable event system
            mod.enabled = false;

            float elapsed2 = 0;

            Vector3 destination;
            Vector3 origin = piece.transform.position;

            if (piece.color == 0)
            {
                destination = whitecaptures_pos.position + ((float)nbWhitecaptures / 30) * whitecaptures_pos.right;

            }
            else
            {
                destination = blackcaptures_pos.position + ((float)nbBlackcaptures / 30) * blackcaptures_pos.right;

            }

            // linear movement
            while (elapsed2 <= timeToMove)
            {


                piece.transform.position = (destination - origin) * elapsed2 / timeToMove + origin;

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
            disableTexts();
            changePlayer();
           // changePlayer();
            SetKings();
           //GetBestMoveWhite();

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
                if (hit.distance > thDist)
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

