using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FractalsCS
{
    public partial class ColorPaletteDialog : Form
    {
        private List<Color> _colorPalette = null;

        private ComboBox _sizesComboBox = new ComboBox();
        private Color[] _colors = new Color[16];
        private int _counter = 0;
        private int _counterRand = 0;
        private bool _IsRandom = false;

        private string[] _sizes = new string[]
        {
            "8", "16", "32", "64", "128", "256", "512", "1024", "2048", "4096"
        };

        public ColorPaletteDialog() //Choose colours setup
        {
            InitializeComponent();

            string[] paletteType = new string[]
            {
                "Set Colours",
                "Use Generic Colours",
                "Use Random Colours",
                "Use Colour Gradient",
                "Use Triple Colour Gradient"
            };
            comboBox.Items.Clear();
            comboBox.Items.AddRange(paletteType);
            comboBox.SelectedIndex = 2;
        }

        public List<Color> ColorPalette
        {
            get { return _colorPalette; }
        }

        private void btn_add_Click(object sender, EventArgs e)//for button_add to add more colours for calculations.
        {
            createATextBox();
        }

        private void createATextBox()
        {
            TextBox createTextBoxes = new TextBox();
            createTextBoxes.BorderStyle = BorderStyle.FixedSingle;
            createTextBoxes.Size = new Size(100, 20);
            if (comboBox.Text == "Use Triple Colour Gradient" && _IsRandom == true )
            {

                    createTextBoxes.BackColor = _colors[_counterRand];
                _counterRand++;

            }
            else
            {

                    createTextBoxes.BackColor = Color.White;

            }

            createTextBoxes.MouseClick += shared_MouseClick;
            flowLayoutPanel.Controls.Add(createTextBoxes);
        }

        private void shared_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox txtbx = (TextBox)sender;
            if (Control.ModifierKeys == Keys.Shift)
            {
                flowLayoutPanel.Controls.Remove(txtbx);
            }
            else
            {
                colorDialog1.Color = txtbx.BackColor;
                DialogResult dialogResult = colorDialog1.ShowDialog(this);

                if (dialogResult == DialogResult.OK)
                {
                    _colors[_counter] = colorDialog1.Color;
                    _counter++;
                    txtbx.BackColor = colorDialog1.Color;
                }
            }
        }

        private void btn_Done_Click(object sender, EventArgs e)
        {
            switch (comboBox.Text)
            {
                case "Set Colours":
                    _colorPalette = ColorPaletteFactory.GetColorPalette1(_colors);
                    break;

                case "Use Colour Gradient":
                    _colorPalette = ColorPaletteFactory.GenerateColorGradientPalette(_colors[0], _colors[1], Convert.ToInt32(_sizesComboBox.Text));
                    break;

                case "Use Triple Colour Gradient":
                    Color[] cFirst = new Color[3];
                    Color[] cSecond = new Color[3];
                    for (int i = 0; i < 3; i++)
                    {
                        cFirst[i] = _colors[i];
                        cSecond[i] = _colors[i + 3];
                    }
                    _colorPalette = ColorPaletteFactory.GenerateTHREEColorGradientPalettes(cFirst, cSecond, Convert.ToInt32(_sizesComboBox.Text));
                    break;

                default:
                    //Do nothing
                    break;
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            flowLayoutPanel.Controls.Clear();
            switch (comboBox.Text)
            {

                case "Set Colours":
                    createATextBox();
                    btn_Done.Visible = true;
                    btn_add.Visible = true;
                    break;

                case "Use Generic Colours":
                    btn_Done.Visible = false;
                    btn_add.Visible = false;
                    _colorPalette = ColorPaletteFactory.GetColorPalette2();
                    break;

                case "Use Random Colours":
                    btn_Done.Visible = false;
                    btn_add.Visible = false;
                    _colorPalette = ColorPaletteFactory.GetRandomColorPalette();
                    break;

                case "Use Colour Gradient":
                    btn_add.Visible = false;
                    btn_Done.Visible = true;
                    for (int i = 0; i < 2; i++)
                    {
                        createATextBox();
                    }
                    createACombobox();
                    break;

                case "Use Triple Colour Gradient":

                    Button btn = new Button();
                    btn.Location = new Point(btnOK.Location.X - 50, btnOK.Location.Y);
                    btn.Size = new Size(40, 20);
                    btn.Text = "Random";
                    btn.MouseClick += btn_MouseClick;
                    this.Controls.Add(btn);

                    btn_add.Visible = false;
                    btn_Done.Visible = true;
                    for (int i = 0; i < 6; i++)
                    {
                        createATextBox();
                    }
                    createACombobox();
                    break;

                default:
                    //Do nothing
                    break;
            }
        }

        private void createACombobox()
        {
            _sizesComboBox.Items.Clear();
            _sizesComboBox.MaxDropDownItems = 9;
            _sizesComboBox.Items.AddRange(_sizes);
            flowLayoutPanel.Controls.Add(_sizesComboBox);
        }

        private void btn_MouseClick(object sender, MouseEventArgs e)
        {
            _counterRand = 0;
            flowLayoutPanel.Controls.Clear();
            _IsRandom = true;
            Random rand = new Random();
            int[] randomNumber = new int[3];
            for (int i = 0; i < 16; i++)
            {
                for (int f = 0; f < 3; f++)
                {
                    randomNumber[f] = rand.Next(0, 255);
                }
                _colors[i] = Color.FromArgb(randomNumber[0], randomNumber[1], randomNumber[2]);
            }
            for (int i = 0; i < 16; i++)
            {
                createATextBox();
            }
            createACombobox();
        }
    }
}