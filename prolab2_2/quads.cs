using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace prolab2_2
{

    public class quadtree

    {
        public static ArrayList plusPool = new ArrayList();//pool for plus
        public static ArrayList quadPool = new ArrayList();//pool for quad
        public static ArrayList nodePool = new ArrayList();//pool for node
        public class plus{
            internal int x, y;
            internal quad parent;
            internal quad[] Quads = new quad[4];
            //internal plus[] plusses = new plus[4];
            public bool inIt(int x ,  int y)
            {
                bool answer=false;
                int child;
                for (int i = 0; i < Quads.Length; i++)
                {
                    Quads[i].inIt(x, y);
                }
                return answer;
         }
        }
        public class quad
        {
            //for border coordinates
            public float MinX;
            public float MinY;
            public float MaxX;
            public float MaxY;
            public plus parent;//for easy finding the parent plus
            public bool split = false;//for not using the same quad twice

            public quad(float minX, float minY, float maxX, float maxY , plus Parent )//getting data easier
            {
                MinX = minX;
                MinY = minY;
                MaxX = maxX;
                MaxY = maxY;
                parent = Parent;
            }
            public void getSplit()//for not using the same quad twice
            {
                split = true;
            }
            public bool inIt(int x , int y)//finding if the coordinates in a quad
            {
                bool answer = false;
                if (MinX<x && MinY<y && MaxX>x && MaxY>y )
                {
                    answer = true;
                }
                return answer;
            }

        }

        public class nodes {//creating nodes
            public int X;
            public int Y;
            public nodes(int x , int y) {
                 X = x;
                 Y = y;
            }
            public nodes(int x, int y , quad ownerQuad , plus ownerPlus){//getting data in easier
                int X = x;
                int Y = y;
                quad parentQuad = ownerQuad;
                plus parentPlus = ownerPlus;
            }

        }


    }

}

