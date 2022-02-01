using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoolDesktopApp
{
    public partial class Startpage : Form
    {

        // Deklarerer tomme variabler som skal fylles ut og sendes videre
        public static string p1Name = "";
        public static string p2Name = "";
        public static string ballTypeP1 = "";
        public static string ballTypeP2 = "";

        // Boolsk variabel for å sjekke om navn er oppgitt
        public bool nameOkay = false;


        public Startpage()
        {
            InitializeComponent();
            cboSelectBall.SelectedIndex = 0;
        }

        // Metode som setter navn
        public void SetName()
        {
            p1Name = txtP1Name.Text;
            p2Name = txtP2Name.Text;

            // Sjekker om navnefelt er fyllt ut
            if (p1Name == "" || p2Name == "")
            {
                nameOkay = false;
                MessageBox.Show("Mangler navn");
            }
            else
            {
                nameOkay = true;
            }
        }

        // Metode som setter balltype etter valg i drop down lista
        public void SetBallType()
        {
            ballTypeP1 = cboSelectBall.Text;
            if (ballTypeP1 == "Solid")
            {
                ballTypeP2 = "Half";
            }
            else
            {
                ballTypeP2 = "Solid";
            }
        }

        // Click-event som starter spillet, og sender brukeren videre til hovedsiden
        private void btnStart_Click(object sender, EventArgs e)
        {
            SetName();
            SetBallType();
            if (nameOkay == true)
            {
                DesktopApp desktopApp = new DesktopApp();
                desktopApp.Show();
                this.Hide();
            }
        }

        private void Startpage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
        }
    }
}
