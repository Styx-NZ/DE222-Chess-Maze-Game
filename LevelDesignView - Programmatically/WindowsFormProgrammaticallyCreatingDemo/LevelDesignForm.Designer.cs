namespace LevelDesignView
{
    partial class LevelDesignForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            textBox1 = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            textBox1.Location = new System.Drawing.Point(372, 453);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new System.Drawing.Size(169, 82);
            textBox1.TabIndex = 16;
            textBox1.Text = "Click \"Create Parts\" to create a range of pieces to place on the board.                           White Pieces are player pieces";
            // 
            // LevelDesignForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(593, 599);
            Controls.Add(textBox1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "LevelDesignForm";
            Text = "Programmatically Creating Demo";
            Load += LevelDesignForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox textBox1;
    }
}