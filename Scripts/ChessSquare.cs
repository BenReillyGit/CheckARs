using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CheckersAI
{
    public class ChessSquare : MonoBehaviour
    {

        // Use this for initialization

        // this it the scrpt that manages the piece movement

        // this is the over material used when focusing on the square
        public Material overMat;


        CheckersBoardInterface cbi;


        // pointer functions is used to call the pepare to click
        //public bool canMove;
        public GameObject squareToGoTo;
        public Piece pieceToGoTo;
        public float th = 0.05f;
        GameObject[] squares;
        public string colour;



        void Start()
        {

            squares = GameObject.FindGameObjectsWithTag("chessSquare");

            cbi = GameObject.FindGameObjectWithTag("pieceMovement").GetComponent<CheckersBoardInterface>();
            gameObject.GetComponent<Renderer>().material = overMat;
            gameObject.GetComponent<Renderer>().enabled = false;
            squareToGoTo = null;
            pieceToGoTo = null;

        }


        // Update is called once per frame
        void Update()
        {
        }

        //*****************
        // call to the movement function in this case
        //*******************
        public void onClickSquare(string name)
        {
            if(cbi.board.Turn == Color.White)
            {
                foreach (GameObject squ in squares)
                {
                    foreach (Piece pc in cbi.allPieces)
                    {
                        if (squ.name.Equals(name))
                        {
                            if (squareToGoTo != null)
                            {
                                pieceToGoTo = pc;
                                cbi.ManualMovePiece(squareToGoTo, pc);
                                //FindMove(OriginSquare(), squareToGoTo);
                                squareToGoTo = null;
                                pieceToGoTo = null;
                                
                            }
                        }
                    }
                }
            }
        }
        
        public void active(GameObject sq)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            squareToGoTo = sq;
            //canMove = true;
            //pieceToCapture = pc;
            //foreach (Piece pc in cbi.allPieces)
            //{
            //    if ((pc.transform.position - sq.transform.position).magnitude < th)
            //    {
            //        Debug.Log(sq.name);
            //    }
            //}
        }

        public void onExitSquare()
        {
            gameObject.GetComponent<Renderer>().enabled = false;
           // cbi.GetBestMoveWhite();
            
            //canMove = false;
            // pieceToCapture = null;



        }
    }
}

