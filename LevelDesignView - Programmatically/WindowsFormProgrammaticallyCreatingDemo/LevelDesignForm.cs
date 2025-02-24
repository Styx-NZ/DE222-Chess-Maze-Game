using System;
using System.Drawing;
using System.Windows.Forms;
using LevelDesignNS;
using FilerNS;
using System.Drawing.Text;
using System.Collections.Generic;
using System.Linq;

namespace LevelDesignView
{
    public partial class LevelDesignForm : Form
    {
        private string _clickedText;
        private ChessMazeLevel chessMazeLevel;
        private Part _lastClickedPart = Part.Empty;
        private List<ComboBox> dynamicComboBoxes = new List<ComboBox>();
        private List<Button> dynamicButtons = new List<Button>();

        public LevelDesignForm()
        {
            InitializeComponent();

            MessageBox.Show("This is a chess maze game level designer \n Please make sure there is only 1 player piece \n (this is the piece you start on) \n Your objective is to create a level where you can use valid chess moves to get to the goal (The X)");

            AddLabel("Level Height", 468, 158);
            AddLabel("Level Width", 472, 222);
            AddComboBox("comboHeight", 420, 176);
            AddComboBox("comboWidth", 420, 240);
            MakeControlButton("createLevel", "Create a Level", 441, 285);
            MakeControlButton("saveLevel", "Save Level", 441, 340);
            MakeControlButton("createParts", "Create Parts", 441, 411);
            SetClicks();

        }

        public void MakeControlButton(string name, string text, int x, int y)
        {
            Button button = new Button();
            button.Name = name;
            button.Text = text;
            button.Size = new Size(100, 36);
            button.Location = new Point(x, y);
            button.Anchor = AnchorStyles.Right;

            dynamicButtons.Add(button);
            this.Controls.Add(button);
        }

        public void AddComboBox(string name, int x , int y)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Name = name;
            comboBox.Size = new Size(121,23);
            comboBox.Location = new Point(x,y);
            comboBox.Anchor = AnchorStyles.Right;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            List<int> sizeOptions = new List<int>() { 3, 4, 5, 6, 7, 8 };
            foreach (int i in sizeOptions)
            {
                comboBox.Items.Add(i);
            }
            comboBox.SelectedIndex = 0;

            dynamicComboBoxes.Add(comboBox); // Add the ComboBox to the list
            this.Controls.Add(comboBox);
        }

        public void AddLabel(string name, int x, int y)
        {
            Label label = new Label();

            label.Name = $"{name}{x}{y}";
            label.Text = name;
            label.Font = new Font("Arial", 9);
            label.AutoSize = true;
            label.Location = new Point(x, y);
            label.Anchor = AnchorStyles.Right;

            this.Controls.Add(label);
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
            btnNew.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

            // button location
            int margin = 10;
            btnNew.Location = new Point(margin + btnNew.Height * row, margin + btnNew.Width * column);

            //add new button to controls property
            this.Controls.Add(btnNew);
        }

        // event handler
        protected void WhoClicked(object sender, EventArgs e)
        {
            Button btnWho = sender as Button;

            Text = btnWho.Name;

            if (btnWho.Name.StartsWith("btn"))
            {
                // Get the row and column from the button name
                string[] nameParts = btnWho.Name.Split('_');
                if (nameParts.Length == 3 && int.TryParse(nameParts[1], out int column) && int.TryParse(nameParts[2], out int row))
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
            else if (btnWho.Name == "createLevel")
            {
                Button1_Click();
            }
            else if (btnWho.Name == "createParts")
            {
                Button2_Click();
            }
            else if (btnWho.Name == "saveLevel")
            {
                Button3_Click();
            }
            else
            {
                Enum.TryParse(btnWho.Name.Substring(0), out _lastClickedPart);
            }
        }

        protected void SetClicks()
        {
            foreach (Control c in Controls)
            {
                if (c is Button)
                {
                    Button who = c as Button;
                    who.Click -= WhoClicked;
                    who.Click += WhoClicked;
                }
            }
        }

        private int GetComboBoxValue(string comboBoxName)
        {
            ComboBox comboBox = dynamicComboBoxes.Find(c => c.Name == comboBoxName);
            if (comboBox != null)
            {
                return Convert.ToInt32(comboBox.SelectedItem);
            }
            return 0; // Default value if ComboBox is not found
        }

        private void Button1_Click()
        {
            //create instance
            chessMazeLevel = new ChessMazeLevel();

            //create level
            int selectedWidth = GetComboBoxValue("comboWidth");
            int selectedHeight = GetComboBoxValue("comboHeight");
            chessMazeLevel.CreateLevel(selectedWidth, selectedHeight);

            //display level
            DisplayChessboard();
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

        private void Button2_Click()
        {
            ClearPartRange();
            int columnNumber = 0;
            int rowNumber = 9;
            foreach (Part part in Enum.GetValues(typeof(Part)))
            {
                if (part != Part.PlayerOnEmpty && part != Part.Goal)//Player will never be an empty piece and we dont place goal
                {
                    if (columnNumber == 5)
                    {
                        rowNumber++;
                        columnNumber = 0;
                    }
                    char displayChar = (char)part;//get part value as a char
                    string buttonText = displayChar.ToString();//convert char to a string
                    //MakeAButton("iptBtn_", buttonText, columnNumber, rowNumber);
                    MakePartButton(part, GetPartSymbol(part), columnNumber, rowNumber);
                    columnNumber += 1;
                }
            }
            SetClicks();
        }
        private void ClearPartRange()
        {
            foreach (Control control in Controls.OfType<Button>().Where
                (c => !c.Name.StartsWith("btn") && 
                !c.Name.StartsWith("button") && 
                !dynamicButtons.Contains(c)).ToList())
            {
                Controls.Remove(control);

            }
        }

        private void Button3_Click()
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

        private void LevelDesignForm_Load(object sender, EventArgs e)
        {

        }
    }
}
