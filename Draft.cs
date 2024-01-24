using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms_Practice
{
    public enum PanelColours
    {
        Black,
        Blue,
        Red,
        Orange, 
        Green
    }

    public partial class Draft : Form
    {
        public Draft()
        {
            InitializeComponent();
        }

        private static bool isLeftClicked = false;
        private static bool isCtrlPressed = false;
        private static bool isPanelSelected = false;
        private static int coordinateX = 0, coordinateY = 0;
        private static int releaseX = 0, releaseY = 0;
        private static Point screenPoint, clientPoint;
        private static int colorCounter = 0;
        private static Control rectangle, selectedRectangle;
        private static List<Control> selectedRectangleCollection = new List<Control>();

        private void Draft_MouseDown(object sender, MouseEventArgs e)
        {
            isPanelSelected = false;

            rectangle = new Panel();

            if (sender is Panel)
            {
                Panel p = sender as Panel;
                screenPoint = p.PointToScreen(new Point(e.X, e.Y));
                clientPoint = PointToClient(screenPoint);
                coordinateX = clientPoint.X;    coordinateY = clientPoint.Y;
            }
            else
            {
                coordinateX = e.X; coordinateY = e.Y;
            }

            rectangle.Location = new Point(coordinateX, coordinateY);
            rectangle.BackColor = Color.Red;
            rectangle.Name = "button1";
            rectangle.Size = new System.Drawing.Size(75, 23);
            rectangle.TabIndex = 0;
            rectangle.BringToFront();
            this.Controls.Add(rectangle);
            isLeftClicked = true;
            colorCounter++;
        }

        private void Draft_Load(object sender, EventArgs e)
        {

        }

        private void Draft_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.ControlKey)
            {
                isCtrlPressed = true;
            }
        }

        private void Draft_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                foreach(Control C in selectedRectangleCollection)
                {
                    C.Dispose();
                }
            }
            if (e.KeyData == Keys.ControlKey)
            {
                isCtrlPressed = false;
            }
        }

        private void Draft_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Panel)
            {
                Panel p = sender as Panel;
                screenPoint = p.PointToScreen(new Point(e.X, e.Y));
                clientPoint = PointToClient(screenPoint);
                releaseX = clientPoint.X;   releaseY = clientPoint.Y;
            }
            else
            {
                releaseX = e.X; releaseY = e.Y;
            }

            if (isLeftClicked)
            {
                if (releaseX - coordinateX < 0 && releaseY - coordinateY < 0)
                {
                    rectangle.Location = new Point(releaseX, releaseY);
                    rectangle.Size = new Size(coordinateX - releaseX,coordinateY - releaseY);
                }
                else if (releaseX - coordinateX > 0 && releaseY - coordinateY < 0)
                {
                    rectangle.Location = new Point(coordinateX, releaseY);
                    rectangle.Size = new Size(releaseX - coordinateX, coordinateY - releaseY);
                }
                else if (releaseX - coordinateX < 0 && releaseY - coordinateY > 0)
                {
                    rectangle.Location = new Point(releaseX, coordinateY);
                    rectangle.Size = new Size(coordinateX - releaseX, releaseY - coordinateY);
                }
                else
                {
                    rectangle.Size = new Size(releaseX - coordinateX, releaseY - coordinateY);
                }
                rectangle.BringToFront();

            }
        }

        private void Draft_MouseUp(object sender, MouseEventArgs e)
        {
            if(!isPanelSelected)
            {
                rectangle.Click += RemovePanel;
                rectangle.MouseDown += Draft_MouseDown;
                rectangle.MouseUp += Draft_MouseUp;
                rectangle.MouseMove += Draft_MouseMove;
                isLeftClicked = false;
            }
        }

        private void RemovePanel(object sender, EventArgs e)
        {
            colorCounter++;
            selectedRectangle = sender as Control;
            isLeftClicked = false;
            isPanelSelected = true;
            if(isCtrlPressed)
            {
                selectedRectangleCollection.Add(selectedRectangle);
            }
            else
            {
                foreach(Control C in selectedRectangleCollection)
                {
                    C.BackColor = Color.Red;
                }
                selectedRectangleCollection = new List<Control>();
                selectedRectangleCollection.Add(selectedRectangle);
            }
            selectedRectangle.BackColor = Color.Green;
        }
    }
}
