namespace FractalsCS
{
    partial class MainForm
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
            this.picMandelbrot = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openAtOriginalPlaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openAtFilesPlaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurrentAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertCurrentToBitmapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFractalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.restartThisColourPaletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.picMandelbrot)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picMandelbrot
            // 
            this.picMandelbrot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picMandelbrot.Location = new System.Drawing.Point(22, 31);
            this.picMandelbrot.Name = "picMandelbrot";
            this.picMandelbrot.Size = new System.Drawing.Size(600, 600);
            this.picMandelbrot.TabIndex = 0;
            this.picMandelbrot.TabStop = false;
            this.picMandelbrot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMandelbrot_MouseDown);
            this.picMandelbrot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMandelbrot_MouseMove);
            this.picMandelbrot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picMandelbrot_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(644, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openAtOriginalPlaceToolStripMenuItem,
            this.openAtFilesPlaceToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveCurrentAsToolStripMenuItem,
            this.restartToolStripMenuItem,
            this.convertCurrentToBitmapToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.openToolStripMenuItem.Text = "Open At &Current Place";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openAtOriginalPlaceToolStripMenuItem
            // 
            this.openAtOriginalPlaceToolStripMenuItem.Name = "openAtOriginalPlaceToolStripMenuItem";
            this.openAtOriginalPlaceToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.openAtOriginalPlaceToolStripMenuItem.Text = "Open At &Original Place";
            this.openAtOriginalPlaceToolStripMenuItem.Click += new System.EventHandler(this.openAtOriginalPlaceToolStripMenuItem_Click);
            // 
            // openAtFilesPlaceToolStripMenuItem
            // 
            this.openAtFilesPlaceToolStripMenuItem.Name = "openAtFilesPlaceToolStripMenuItem";
            this.openAtFilesPlaceToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.openAtFilesPlaceToolStripMenuItem.Text = "Open At &File\'s Place";
            this.openAtFilesPlaceToolStripMenuItem.Click += new System.EventHandler(this.openAtFilesPlaceToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.saveAsToolStripMenuItem.Text = "Save Original &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsOriginalToolStripMenuItem_Click);
            // 
            // saveCurrentAsToolStripMenuItem
            // 
            this.saveCurrentAsToolStripMenuItem.Name = "saveCurrentAsToolStripMenuItem";
            this.saveCurrentAsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.saveCurrentAsToolStripMenuItem.Text = "Save &Current As";
            this.saveCurrentAsToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentAsToolStripMenuItem_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.restartToolStripMenuItem.Text = "&Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // convertCurrentToBitmapToolStripMenuItem
            // 
            this.convertCurrentToBitmapToolStripMenuItem.Name = "convertCurrentToBitmapToolStripMenuItem";
            this.convertCurrentToBitmapToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.convertCurrentToBitmapToolStripMenuItem.Text = "&Convert Current to PNG";
            this.convertCurrentToBitmapToolStripMenuItem.Click += new System.EventHandler(this.convertCurrentToBitmapToolStripMenuItem_Click_1);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFractalToolStripMenuItem1,
            this.restartThisColourPaletteToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // newFractalToolStripMenuItem1
            // 
            this.newFractalToolStripMenuItem1.Name = "newFractalToolStripMenuItem1";
            this.newFractalToolStripMenuItem1.Size = new System.Drawing.Size(213, 22);
            this.newFractalToolStripMenuItem1.Text = "&New Colour Palette";
            this.newFractalToolStripMenuItem1.Click += new System.EventHandler(this.newFractalToolStripMenuItem1_Click);
            // 
            // restartThisColourPaletteToolStripMenuItem
            // 
            this.restartThisColourPaletteToolStripMenuItem.Name = "restartThisColourPaletteToolStripMenuItem";
            this.restartThisColourPaletteToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.restartThisColourPaletteToolStripMenuItem.Text = "&Restart This Colour Palette";
            this.restartThisColourPaletteToolStripMenuItem.Click += new System.EventHandler(this.restartThisColourPaletteToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 662);
            this.Controls.Add(this.picMandelbrot);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picMandelbrot)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picMandelbrot;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem openAtOriginalPlaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openAtFilesPlaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertCurrentToBitmapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFractalToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem restartThisColourPaletteToolStripMenuItem;
    }
}

