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
    public class Mat : MonoBehaviour
    {
       
        Renderer rend;
        private int m;
        CheckersBoardInterface cbi;
        GameObject[] mat;
        // Start is called before the first frame update
        void Start()
        {
            mat = GameObject.FindGameObjectsWithTag("mat");
            cbi = GameObject.FindGameObjectWithTag("pieceMovement").GetComponent<CheckersBoardInterface>();

            rend = GetComponent<Renderer>();
            rend.enabled = true;
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        public void SetKings()
        {
            foreach (GameObject sq in cbi.squares)
            {
                string temp = sq.name;
                string x = temp.Substring(0, 1);
                string y = temp.Substring(1, 1);
                int X = int.Parse(x);
                int Y = int.Parse(y);
                foreach(Piece pc in cbi.allPieces)
                {
                    foreach (GameObject ma in mat)
                    {

                        if ((sq.transform.position - ma.transform.position - pc.transform.position).magnitude < 0.05f && Y == 0 && pc.color == 0)
                        {
                            m = 0;
                        }
                        else if ((sq.transform.position - ma.transform.position).magnitude < 0.05f && Y == 7 && pc.color == 1)
                        {
                            m = 1;
                        }

                }
                }
                
            }
        }
    }
}


