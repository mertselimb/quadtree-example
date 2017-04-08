using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;

using System.Collections;


namespace prolab2_2
{
    public partial class Form1 : Form
    {
        private Bitmap buffer;//Declare the buffer at class level for painting the panel
        private Timer timer; //Declare the timer at class level
        public Form1()
        {
            InitializeComponent();
            panel1_Resize(this, null);//Resizing the panel
            
            quadtree.quad firstQuad = new quadtree.quad(0, 0, 512, 512,null);//Drawing the first quad
            quadtree.quadPool.Add(firstQuad);//Adding the first quad
            timer = new Timer { Interval = 1000 }; //Interval is the amount of time in millis before it fires
            timer.Tick += OnTick;//

        }
        private void panel1_Resize(object sender, EventArgs e)
        {
            // Resize the buffer, if it is growing
            if (buffer == null ||
                buffer.Width < panel1.Width ||
                buffer.Height < panel1.Height)
            {
                Bitmap newBuffer = new Bitmap(panel1.Width, panel1.Height);
                if (buffer != null)
                    using (Graphics bufferGrph = Graphics.FromImage(newBuffer))
                        bufferGrph.DrawImageUnscaled(buffer, Point.Empty);
                buffer = newBuffer;
            }
        }//Resize the panel

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(buffer, Point.Empty);
            drawPanel(sender , e);
        }//Paint the buffer


        public void drawDot(int x , int y , Color e)
        {
            // Draw rectangle into the buffer
            using (Graphics bufferGrph = Graphics.FromImage(buffer))
            {
                bufferGrph.DrawRectangle(new Pen(e, 1), x-15, y-50, 2, 2);
            }

            // Invalidate the panel. This will lead to a call of 'panel1_Paint'
            panel1.Invalidate();
        }//Drawing dots only visually
        public void drawPlus(int x, int y , Color e , quadtree.quad quad)
        {
            // Draw line into the buffer
            using (Graphics bufferGrph = Graphics.FromImage(buffer))
            {

                bufferGrph.DrawLine(new Pen(e), x - 15, quad.MinY, x-15,  quad.MaxY);
                bufferGrph.DrawLine(new Pen(e), quad.MinX, y - 50, quad.MaxX, y - 50);
            }

            // Invalidate the panel. This will lead to a call of 'panel1_Paint'
            panel1.Invalidate();
        }//Drawing plusses only visually
        public void drawPanel(object sender, PaintEventArgs e)
        {
 
            Pen blackPen = new Pen(Color.Black, 3);

                e.Graphics.DrawLine(blackPen, 0, 1, 512, 1);
                e.Graphics.DrawLine(blackPen, 0, 510, 512, 510);
                e.Graphics.DrawLine(blackPen, 1, 0, 1, 512);
                e.Graphics.DrawLine(blackPen, 500, 0, 500, 512);

        }//drawing first quad manually

        private void panel1_Click(object sender, EventArgs e)
        {
            int x = Cursor.Position.X, y = Cursor.Position.Y;
            quadtree.quad quad = insertNode(x, y);
            createPlus(x, y,quad);
            Random  rnd = new Random();
            int color = rnd.Next(1, 5);
            if (color == 1)
            {
                drawDot(x, y , Color.Black);
                drawPlus(x, y, Color.Black, quad);
            }
            if (color == 2)
            {
                drawDot(x, y , Color.Red);
                drawPlus(x, y, Color.Red, quad);
            }
            if (color == 3)
            {
                drawDot(x, y , Color.Green);
                drawPlus(x, y , Color.Green, quad);
            }
            if (color == 4)
            {
                drawDot(x, y, Color.Blue);
                drawPlus(x, y, Color.Blue, quad);
            }
            if (color == 5)
            {
                drawDot(x, y,Color.Purple);
                drawPlus(x, y, Color.Purple, quad);
            }

            

        }//listening for user clicks and creating nodes and plusses eventually quad in create plus class
        public quadtree.quad insertNode(int x, int y)//find near and most small quad for using it as a border 
        {
            quadtree.quad smallest = new quadtree.quad(0, 0, 10000, 10000, null);//making it enourmous so it can't be selected
            foreach (quadtree.quad item in quadtree.quadPool)//searching for smalles in quadPool()
            {
                if (item.MaxX > x && item.MinX < x && item.MinY < y && item.MaxY > y)
                {
                    if ((item.MaxX - item.MinX) * (item.MaxY - item.MinY) < (smallest.MaxX - smallest.MinX) * (smallest.MaxY - smallest.MinY)&&item.split==false) { 
                        smallest.MaxX = item.MaxX;
                        smallest.MinX = item.MinX;
                        smallest.MinY = item.MinY;
                        smallest.MaxY = item.MaxY;
                        smallest.parent = item.parent;
                    }
                }
            }
            quadtree.nodes node = new quadtree.nodes(x, y, smallest, smallest.parent);//creating node
            quadtree.nodePool.Add(node);//adding node
            smallest.split = true;//so a node cant have more than 1 plus
            splitWhom(smallest);//so a node cant have more than 1 plus
            return smallest;
        }
        public int length(ArrayList array)
        {
            int answer=0 ;

            for (int i = 0; array[i]==null; i++)
            {
                answer++;
            }
            return answer;
        }//for easy acces to arraylist length

        public quadtree.plus createPlus(int X , int Y , quadtree.quad quad)
        {
            int x = X-15, y = Y-50;//panel lines shouldnt interfier with our coordinates so negate them
            quadtree.plus plus = new quadtree.plus();
            plus.x = x;
            plus.y = y;
            //creating quads
            plus.Quads[0] = new quadtree.quad(x, y, quad.MaxX, quad.MaxY, plus);//1.side
            plus.Quads[1] = new quadtree.quad(quad.MinX, y, x, quad.MaxY, plus);//2.side
            plus.Quads[2] = new quadtree.quad(quad.MinX, quad.MinY, x, y, plus);//3.side
            plus.Quads[3] = new quadtree.quad(x, quad.MinY, quad.MaxX, y, plus);//4.side
            plus.parent = quad;
            for (int i = 0; i < 4; i++)//add to quadPool
            {
                quadtree.quadPool.Add(plus.Quads[i]);
            }

            return plus;
        }//creating plusses

        public void randomNode_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();//we are using this to give random nubers
            int x = rnd.Next(10,508), y = rnd.Next(10, 508);//getting random coordinates
            quadtree.quad quad = insertNode(x, y);//creating node in the quad and getting it here
            createPlus(x, y, quad);
            //coloring the drawing process
            int color = rnd.Next(1, 5);
            if (color == 1)
            {
                drawDot(x, y, Color.Black);
                drawPlus(x, y, Color.Black, quad);
            }
            if (color == 2)
            {
                drawDot(x, y, Color.Red);
                drawPlus(x, y, Color.Red, quad);
            }
            if (color == 3)
            {
                drawDot(x, y, Color.Green);
                drawPlus(x, y, Color.Green, quad);
            }
            if (color == 4)
            {
                drawDot(x, y, Color.Blue);
                drawPlus(x, y, Color.Blue, quad);
            }
            if (color == 5)
            {
                drawDot(x, y, Color.Purple);
                drawPlus(x, y, Color.Purple, quad);
            }


        }//for random click qw use this button

        public void splitWhom(quadtree.quad quad)
        {
            foreach (quadtree.quad item in quadtree.quadPool)
            {
                if ( quad.MinX==item.MinX && quad.MinY == item.MinY && quad.MaxX == item.MaxX && quad.MaxY == item.MaxY && quad.parent == item.parent && quad.split == item.split)
                {
                    item.split = true;
                }
            }
        }//finding which  quad we are using and making it cant be split again
            
        public void deletoNode(int x, int y)
        {
            quadtree.quad smallest = new quadtree.quad(0, 0, 10000, 10000, null);
            foreach (quadtree.quad item in quadtree.quadPool)
            {
                if (item.MaxX > x && item.MinX < x && item.MinY < y && item.MaxY > y)
                {
                    if ((item.MaxX - item.MinX) * (item.MaxY - item.MinY) < (smallest.MaxX - smallest.MinX) * (smallest.MaxY - smallest.MinY) && item.split == false)
                    {
                        smallest.MaxX = item.MaxX;
                        smallest.MinX = item.MinX;
                        smallest.MinY = item.MinY;
                        smallest.MaxY = item.MaxY;
                        smallest.parent = item.parent;
                    }
                }
            }
            using (Graphics bufferGrph = Graphics.FromImage(buffer))//deleting from visual panel
            {
                bufferGrph.DrawRectangle(new Pen(Color.White, 1), smallest.MinX, smallest.MinY, smallest.MaxX-smallest.MinX, smallest.MaxY - smallest.MinY);
            }

            panel1.Invalidate();
            foreach (quadtree.quad item in quadtree.quadPool)//deleting from quadPool with enourmous numbers
            {
                if (item.MaxX > x && item.MinX < x && item.MinY < y && item.MaxY > y){
                    item.MaxX = 100000;
                    item.MaxY = 100000;
                }
            }
        }//deleting nodes
        public void searchForQuad() {

            }

        private void deleteNode_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Pick a position after clicking OK", "OK", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                timer.Start();
            }
            timer.Stop(); //Don't forget to stop the timer, or it'll continue to tick
            Point coordinates = Cursor.Position;
            deletoNode(coordinates.X, coordinates.Y);
        }//activating delete process
        private void OnTick(object sender, EventArgs eventArgs)
        {
            timer.Stop(); //Don't forget to stop the timer, or it'll continue to tick
            Point coordinates = Cursor.Position;
            MessageBox.Show("Coordinates are: " + coordinates);
        }//timer process started

        private void clear_Click(object sender, EventArgs e)//restarting so everything will be erased
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (quadtree.nodes item in quadtree.nodePool)
            {
                MessageBox.Show(item.X + " " +item.Y);
            }
        }
    }


}
