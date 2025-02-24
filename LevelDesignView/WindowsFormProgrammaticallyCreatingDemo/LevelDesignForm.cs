using System;
using System.Drawing;
using System.Windows.Forms;
using LevelDesignNS;
using FilerNS;
using System.Drawing.Text;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LevelDesignView
{
    public partial class LevelDesignForm : Form
    {
        //private string _clickedText;
        private ChessMazeLevel chessMazeLevel;
        private Part _lastClickedPart = Part.Empty;

        public LevelDesignForm()
        {
            InitializeComponent();
            MessageBox.Show("This is a chess maze game level designer \n Please make sure there is only 1 player piece \n (this is the piece you start on) \n Your objective is to create a level where you can use valid chess moves to get to the goal (The X)");
            button12.Enabled = false;
        }

        public void MakeGridButton(string name, string text, int row, int column)
        {
            Button btnNew = new();

            //button properties
            btnNew.Name = $"{name}{column}_{row}";
            btnNew.Height = 50;
            btnNew.Width = 50;
            btnNew.Font = new Font("Arial", 20);
            btnNew.Text = text;
            btnNew.Visible = true;
            btnNew.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // button location
            int margin = 10;
            btnNew.Location = new Point(margin + btnNew.Height * row, margin + btnNew.Width * column);

            //add new button to controls property
            this.Controls.Add(btnNew);
        }

        //Event handler
        protected void WhoClicked(object sender, EventArgs e)
        {
            Button btnWho = sender as Button;

            Text = btnWho.Name;

            if (btnWho.Name.StartsWith("btn"))
            {
                // Get the row and column from the button name
                string[] nameParts = btnWho.Name.Split('_');
                if (nameParts.Length == 3 && 
                    int.TryParse(nameParts[1], out int column) && 
                    int.TryParse(nameParts[2], out int row))
                {
                    // update the boardGrid
                    chessMazeLevel.AddPiece(column, row, _lastClickedPart);

                    if (chessMazeLevel.ValidPosition(column, row))
                    {
                        btnWho.Text = GetPartSymbol(_lastClickedPart);
                    }
                    else
                    {
                        MessageBox.Show("Please don't place a piece on the goal.");
                    }
                }
            }
        }

        //set up event detection
        protected void SetClicks()
        {
            foreach (Control c in Controls)
            {
                if (c is Button)
                {
                    Button who = c as Button;
                    who.Click += WhoClicked;
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("we are starting");
            //create instance
            chessMazeLevel = new ChessMazeLevel();

            //create level
            chessMazeLevel.CreateLevel(comboWidth.SelectedIndex + 3, comboHeight.SelectedIndex + 3);//index starts at 0 with the value of 3

            //display level
            DisplayChessboard();

            button2.Enabled = true;
            button12.Enabled = true;
        }

        private void DisplayChessboard()
        {
            ClearChessboard();
            int width = chessMazeLevel.GetLevelWidth();
            int height = chessMazeLevel.GetLevelHeight();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Part part = chessMazeLevel.GetPartAtIndex(x, y);
                    char displayChar = (char)part;//get part value as a char
                    string buttonText = displayChar.ToString();//convert char to a string
                    MakeGridButton("btn_", GetPartSymbol(part), x, y);
                }
            }
            SetClicks();
        }

        private void ClearChessboard()
        {
            foreach (Control control in Controls.OfType<Button>().Where(c => c.Name.StartsWith("btn_")).ToList())
            {
                Controls.Remove(control);

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            _lastClickedPart = Part.Empty;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            _lastClickedPart = Part.King;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _lastClickedPart = Part.Rook;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _lastClickedPart = Part.Bishop;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _lastClickedPart = Part.Knight;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _lastClickedPart = Part.PlayerOnKing;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            _lastClickedPart = Part.PlayerOnRook;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            _lastClickedPart = Part.PlayerOnBishop;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            _lastClickedPart = Part.PlayerOnKnight;
        }

        private string GetPartSymbol(Part part)
        {
            //Symbols the Parts will be represented as
            //Player pieces should be white
            switch (part)
            {
                case Part.Empty://empty could be E or _
                    return " ";
                case Part.King:
                    return "♚";
                case Part.Rook:
                    return "♜";
                case Part.Bishop:
                    return "♝";
                case Part.Knight:
                    return "♞";
                case Part.PlayerOnEmpty://player will never be an empty piece
                    return "e";
                case Part.PlayerOnKing:
                    return "♔";
                case Part.PlayerOnRook:
                    return "♖";
                case Part.PlayerOnBishop:
                    return "♗";
                case Part.PlayerOnKnight:
                    return "♘";
                case Part.Goal:
                    return "⛌";
                default:
                    return "?";
            }
        }

        public void MakePartButton(Part part, string text, int row, int column)
        {
            Button btnNew = new();

            //button properties
            btnNew.Name = $"{part}";
            btnNew.Height = 50;
            btnNew.Width = 50;
            btnNew.Font = new Font("Arial", 20);
            btnNew.Text = text;
            btnNew.Visible = true;

            // button location
            int margin = 10;
            btnNew.Location = new Point(margin + btnNew.Height * row, margin + btnNew.Width * column);

            //add new button to controls property
            this.Controls.Add(btnNew);
        }
        private void LevelDesignForm_Load(object sender, EventArgs e)
        {
            List<int> sizeOptions = new List<int>() { 3, 4, 5, 6, 7, 8 };
            foreach (int i in sizeOptions)
            {
                comboHeight.Items.Add(i);
                comboWidth.Items.Add(i);
            }
            comboHeight.SelectedIndex = 0;
            comboWidth.SelectedIndex = 0;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string validMessage = chessMazeLevel.CheckValid();

            if (validMessage == "Level is valid.")
            {
                chessMazeLevel.SaveMe();
            }
            else
            {
                MessageBox.Show(validMessage);
            }
        }
    }
}
