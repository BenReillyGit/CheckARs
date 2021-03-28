using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//this enum holds the value/type of the piece
namespace CheckersAI
{
    public enum PieceType
    {
        checker
    }

    // this script allows to hold the piece values, considering its posible movement
    public class Piece : MonoBehaviour
    {

        // Use this for initialization

        // the renderer is changed to create a chosing effect
        public Renderer groundRend;

        //capured parameter
        public bool captured;

        // square references to calculate distance movement
        public Transform destinationSq, originSq;

        //the distance obtained from the two variables above
        public float distance;

        //the threshold used to determine wheter the piece is in possible movement
        public float th = 0.008f;

        //the color of the piece
        public int color;
        CheckersBoardInterface cbi;
        string destSquare;
        public Material[] material;
        //public Renderer rend;
        private int m;



        void Start()
        {
            //rend.enabled = false;
            //distance allowed for the movement
            if (destinationSq != null && originSq != null)
            {
                distance = ((destinationSq.position - originSq.position).magnitude);
            }
            else
            {
                distance = 0;
            }
            

            cbi = GameObject.FindGameObjectWithTag("pieceMovement").GetComponent<CheckersBoardInterface>();

        }

        

        public void onPieceExit()
        {
            if (cbi.selectedPiece != gameObject)
            {
                groundRend.enabled = false;
            }
        }
        public GameObject originSquare;

        //this is the function that allows piece selection   0 = white  1 = black
        public void onPieceClick(int lastselectedcolor)
        {
            cbi.resetSquares();
            if(cbi.board.Turn == Color.White)
            {
                // if there wasn't a selected gameobject
                if (cbi.selectedPiece == null)
                {

                    cbi.selectedPiece = transform.gameObject;
                    groundRend.enabled = true;
                    foreach (GameObject sq in cbi.squares)
                    {
                        if ((transform.gameObject.transform.position - sq.transform.position).magnitude < th)
                        {
                            originSquare = sq;
                            Debug.Log(sq.name);
                            foreach (Move mv in cbi.board.moves)
                            {

                                //Debug.Log(mv);
                                string temp = sq.name;
                                string x = temp.Substring(0, 1);
                                string y = temp.Substring(1, 1);
                                int X = int.Parse(x);
                                int Y = int.Parse(y);
                                if (X == mv.Start.X && Y == mv.Start.Y)
                                {
                                    //Debug.Log(mv);
                                    string xx = mv.ToString();
                                    string destSquareX = xx.Substring(10, 1);
                                    string destSquareY = xx.Substring(12, 1);
                                    destSquare = destSquareX + destSquareY;
                                    //Debug.Log(destSquare);
                                    HighLightSquare();

                                }
                            }
                        }

                    }

                    //stop execution in this case

                }

                // set lastest piece render to false (unselect) only of there is one selected and is not the same as clicked
                else
                {
                    cbi.selectedPiece.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
                    cbi.selectedPiece = transform.gameObject;
                    groundRend.enabled = true;
                    foreach (GameObject sq in cbi.squares)
                    {
                        if ((transform.gameObject.transform.position - sq.transform.position).magnitude < th)
                        {
                            Debug.Log(sq.name);
                            foreach (Move mv in cbi.board.moves)
                            {
                                //Debug.Log(mv);
                                string temp = sq.name;
                                string x = temp.Substring(0, 1);
                                string y = temp.Substring(1, 1);
                                int X = int.Parse(x);
                                int Y = int.Parse(y);
                                if (X == mv.Start.X && Y == mv.Start.Y)
                                {
                                    string xx = mv.ToString();
                                    string destSquareX = xx.Substring(10, 1);
                                    string destSquareY = xx.Substring(12, 1);
                                    destSquare = destSquareX + destSquareY;
                                    HighLightSquare();
                                }
                            }
                        }

                    }




                }
            }
           
        }

        public void HighLightSquare()
        {
            foreach (GameObject s in cbi.squares)
            {
                if (s.name.Equals(destSquare))
                {
                    s.GetComponent<ChessSquare>().active(s);
                }
            }
        }

        //cbi.disableTexts();
        //////////////////////////////////////////////////
        //the square selection mechanics is implemented here
        //////////////////////////////////////////////////

        /////////////
        //  CHECKER
        ////////////
        //        if (pieceTp == PieceType.checker)
        //        {
        //            // loop accross the squares
        //            foreach (GameObject sq in piecemovScript.squares)
        //            {
        //                //Debug.Log(sq.name);
        //                float distToSquare = ((transform.position - sq.transform.position).magnitude);
        //                float angle = 0;

        //                //check movement of the checker in function of the color
        //                if (color == 0)
        //                {
        //                    angle = Vector3.Angle(transform.forward, -(sq.transform.position - piecemovScript.selectedPiece.transform.position));

        //                }
        //                else if (color == 1)
        //                {
        //                    angle = Vector3.Angle(transform.forward, (sq.transform.position - piecemovScript.selectedPiece.transform.position));

        //                }

        //                //movement is posible if can move diagonal and is not blocked 
        //                // or if there is a opponent piece at the diagonal square

        //                capturedPiece = GetOccupiedDiffCol(sq);

        //                if (((Mathf.Abs(distToSquare - (distance) * Mathf.Sqrt(2)) < th 
        //                && (Mathf.Abs(angle - 135) < 0.5f)) 
        //                && (capturedPiece == null && IsOccupied_sameCol(sq) == false)))
        //                {
        //                    //the square is activated and can be selected 
        //                    //set the captured piece
        //                    //Debug.Log("first one");
        //                    //sq.GetComponent<ChessSquare>().active(capturedPiece);
        //                }
        //                if(((Mathf.Abs((distToSquare) - (distance) * Mathf.Sqrt(2)) < th 
        //                && (capturedPiece != null && IsBlocked(sq) == false && captured==false)) 
        //                && (Mathf.Abs(angle - 135) < 0.5f )))
        //                {
        //                    //sq.GetComponent<ChessSquare>().active(capturedPiece);   
        //                }
        //                /*else if(Mathf.Abs((distToSquare) - distance * Mathf.Sqrt(2)) < th
        //                && capturedPiece != null && IsOccupied_sameCol(sq) == false && captured == false
        //                && (Mathf.Abs(angle - 45) < 0.5f)) 
        //                {
        //                   // Debug.Log("second one");
        //                    sq.GetComponent<ChessSquare>().active(capturedPiece); 
        //                }*/


        //            }

        //        }



        //    }


        //    //OCCOUPIED  FUNCTIONS
        //    public bool  IsOccupied_sameCol(GameObject sq)
        //    {
        //        bool occupied = false;

        //        foreach (Piece pc in piecemovScript.allPieces)
        //        {
        //                occupied = occupied || (((pc.transform.position) - sq.transform.position).magnitude < th 
        //                && color == pc.color);
        //        }

        //        return occupied;
        //    }


        //    public Piece GetOccupiedDiffCol(GameObject sq)
        //    {
        //        Piece occupied = null;

        //        foreach (Piece pc in piecemovScript.allPieces)
        //        {

        //            if((((pc.transform.position) - ((sq.transform.position))).magnitude < th && (1 - color) == pc.color))
        //            {
        //                occupied = pc;
        //            }

        //        }

        //        return occupied;
        //    }

        //    public bool IsBlocked(GameObject sq)
        //    {
        //        bool blocked = false;

        //        Vector3 height = 0.01f * transform.up;

        //        RaycastHit hit;

        //        //this is the theretical distance between points:

        //        Vector3 direction = (piecemovScript.selectedPiece.transform.position- sq.transform.position);
        //        float thDist = ((direction).magnitude);

        //        //need to enable colliders to check the raycasts of the opposite pieces
        //        //piecemovScript.enablePieces(1-piecemovScript.playerTurn);

        //        // Does the ray intersect any objects excluding the player layer
        //        if (Physics.Raycast(sq.transform.position+height, direction , out hit, Mathf.Infinity))
        //        {
        //            if(Mathf.Abs(thDist-hit.distance)<0.1f)
        //            {
        //                blocked = false;
        //            }
        //            else
        //            {
        //                blocked = true;
        //            }
        //        }

        //        //disable colliders again
        //        piecemovScript.disablePieces(1 - piecemovScript.playerTurn);

        //        return blocked;
        //    }

        //    // this part of the code detects collision between pieces
        //    void OnTriggerEnter (Collider col)
        //	{




        //        /*for(int i=0; i<8; i++)
        //        {
        //            for(int j=0; j<8; j++)
        //            {
        //                if(col.gameObject.tag != "Untagged" && col.gameObject.name == "chessSquare " + "(" + i + ")")
        //                {
        //                    coords[i,j] = "chessSquare " + "(" + i + ")";
        //                }
        //                else
        //                {
        //                    coords[i,j] = "No";
        //                }

        //            }
        //            Debug.Log(coords[i,j]);
        //        }*/

        //		if (col.gameObject.tag != "Untagged") {
        //			//Debug.Log ("Collision Has occur with: "+ col.gameObject.name);   
        //            /*for(int j=0; j<coords.Length; j++)
        //            {
        //                coords[j] = col.gameObject.name;
        //                Debug.Log("Coords " + j + " " + coords[j]);

        //            }*/
        //            //CheckSquares();


        //        }     
        //   }






    }
}



