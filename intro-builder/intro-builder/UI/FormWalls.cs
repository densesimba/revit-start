using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using eConFaire.RevitBuilder.Intro.Model;

namespace eConFaire.RevitBuilder.Intro
{

    public partial class FormWalls : Form
    {

        int countClick = 0;
        public FormWalls()
        {
            InitializeComponent();

            App.rooms.Clear();
            App.joinPair.Clear();
            App.wallLines.Clear();

        }


        private void btn_createWall_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_roomName.Text))
            {
                MessageBox.Show(" Numele nu poate fi gol.");
                return;
            }

            Regex regex = null;

            regex = new Regex("^([a-zA-Z0-9])*$");

            if (regex.IsMatch(txt_roomName.Text))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Numele nu poate contine caractere speciale.");
            }
        }

        private void numRoomsNumber_ValueChanged(object sender, EventArgs e)
        {
            if (numRoomsNumber.Value > 1)
            {
                btn_createWall.Visible = false;
                btnAdaugaIncapere.Visible = true;
                numRoomsNumber.Enabled = false;

            }
        }

        private void btnAdaugaIncapere_Click(object sender, EventArgs e)
        {
            countClick++;

            if (countClick == numRoomsNumber.Value)
            {
                btnAdaugaIncapere.Visible = false;
                btn_createWall.Visible = true;
            }

            Room newRoom = new Room();

            if (App.rooms.Keys.Count > 0)
            {
                newRoom.index = App.rooms.Keys.Max() + 1;
            }
            newRoom.LengthX = num_LenX.Value;
            newRoom.LengthY = num_LenY.Value;
            newRoom.Name = txt_roomName.Text;


            App.rooms.Add(newRoom.index, newRoom);

            //num_LenX.Value = 0;
            //num_LenY.Value = 0;
            //txt_roomName.Text = "";

        }

        private void FormWalls_Load(object sender, EventArgs e)
        {

        }
    }
}
