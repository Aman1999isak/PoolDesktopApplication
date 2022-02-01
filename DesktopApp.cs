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
using AForge.Video;
using AForge.Video.DirectShow;

namespace PoolDesktopApp
{
    public partial class DesktopApp : Form
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        // Startpage startpage = new Startpage();

        // Counter for å se om alle ballene av dens slag er i hullet, for å vite om en kan putte svart
        public int counterHalf = 0;
        public int counterSolid = 0;

        // Lager objekter av alle ballene
        Ball ball1, ball2, ball3, ball4, ball5, ball6, ball7, ball8, 
             ball9, ball10, ball11, ball12, ball13, ball14, ball15;

        // Lager objekter av spillere
        Player player1;
        Player player2;

        // Liste med baller som skal itereres gjennom
        List<Ball> solidBalls = new List<Ball>();
        List<Ball> halfBalls = new List<Ball>();

        // Variabler for å holde styr på hvem som har hvilke kuler, samt hvem sin tur det er
        public string player1Name;
        public string player2Name;
        public string p1BallType;
        public string p2BallType;
        public bool p1Turn = false;
        public bool p2Turn = false;
        public bool p1Half = false;
        public bool p1Solid = false;
        public bool p2Half = false;
        public bool p2Solid = false;
        public bool p1Lost = false;
        public bool p2Lost = false;

        // Objekt av stoppeklokke for timer
        private Stopwatch stopwatch;


        // Timer event som oppdaterer labelen med timer, formatert til å vise minutter og sekunder
        private void tmrGameTime_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = string.Format("{0:mm\\:ss}", stopwatch.Elapsed);
        }

        public DesktopApp()
        {
            InitializeComponent();
            Init();
            Turn();

        }


        // Initialiserer spillet ved å legge til ballene i respektive lister, 
        // initialisere spillere, pluss starte stoppeklokka
        public void Init()
        {
            initPlayer();
            addBalls();

            stopwatch = new Stopwatch();
            stopwatch.Start();

            solidBalls.Add(ball1);
            solidBalls.Add(ball2);
            solidBalls.Add(ball3);
            solidBalls.Add(ball4);
            solidBalls.Add(ball5);
            solidBalls.Add(ball6);
            solidBalls.Add(ball7);
            solidBalls.Add(ball8);

            halfBalls.Add(ball9);
            halfBalls.Add(ball10);
            halfBalls.Add(ball11);
            halfBalls.Add(ball12);
            halfBalls.Add(ball13);
            halfBalls.Add(ball14);
            halfBalls.Add(ball15);
            halfBalls.Add(ball8);
        }

        // Følgende tre blokker henter live video fra kamera, og viser det i applikasjonen
        private void DesktopApp_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filter in filterInfoCollection)
            {
                cboCamera.Items.Add(filter.Name);
            }
            cboCamera.SelectedIndex = 0;
            videoCaptureDevice = new VideoCaptureDevice();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboCamera.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrane;
            videoCaptureDevice.Start();
        }

        private void VideoCaptureDevice_NewFrane(object sender, NewFrameEventArgs eventArgs)
        {
            pBoxMainGame.Image = (Bitmap)eventArgs.Frame.Clone();
        }


        // Metode for å initialisere spillere
        public void initPlayer()
        {
            player1Name = Startpage.p1Name;
            player2Name = Startpage.p2Name;
            p1BallType = Startpage.ballTypeP1;
            p2BallType = Startpage.ballTypeP2;

            player1 = new Player(0, p1BallType, player1Name);
            player2 = new Player(1, p2BallType, player2Name);

            lblP1.Text = player1.Name;
            lblP2.Text = player2.Name;
        }


        // Metode for å initialisere baller, og legge dem til riktig spiller
        public void addBalls()
        {
            // Legger verdier til objektene av ballene
            #region Add balls as objects
            ball1 = new Ball("yellowSolid", true, "images/yellowWhole.png");
            ball2 = new Ball("blueSolid", true, "images/blueWhole.png");
            ball3 = new Ball("redSolid", true, "images/redWhole.png");
            ball4 = new Ball("purpleSolid", true, "images/purpleWhole.png");
            ball5 = new Ball("orangeSolid", true, "images/orangeWhole.png");
            ball6 = new Ball("greenSolid", true, "images/greenWhole.png");
            ball7 = new Ball("brownSolid", true, "images/brownWhole.png");
            ball8 = new Ball("black", true, "images/black.png");
            ball9 = new Ball("yellowHalf", true, "images/yellowHalf.png");
            ball10 = new Ball("blueHalf", true, "images/blueHalf.png");
            ball11 = new Ball("redHalf", true, "images/redHalf.png");
            ball12 = new Ball("purpleHalf", true, "images/purpleHalf.png");
            ball13 = new Ball("orangeHalf", true, "images/orangeHalf.png");
            ball14 = new Ball("greenHalf", true, "images/greenHalf.png");
            ball15 = new Ball("brownHalf", true, "images/brownHalf.png");
            #endregion

            // Switch case for å vite hvilken spiller som har halve eller hele kuler, og legge kulene i rett
            // sett med pictureboxes
            switch (player1.BallType)
            {
                case "Half":
                    txtTest.Text += player1.Name + " har halve kuler" + "\r\n";
                    p1Ball1.Load(ball9.ImageString);
                    p1Ball2.Load(ball10.ImageString);
                    p1Ball3.Load(ball11.ImageString);
                    p1Ball4.Load(ball12.ImageString);
                    p1Ball5.Load(ball13.ImageString);
                    p1Ball6.Load(ball14.ImageString);
                    p1Ball7.Load(ball15.ImageString);
                    p1Half = true;
                    break;
                case "Solid":
                    txtTest.Text += player1.Name + " har hele kuler" + "\r\n";
                    p1Ball1.Load(ball1.ImageString);
                    p1Ball2.Load(ball2.ImageString);
                    p1Ball3.Load(ball3.ImageString);
                    p1Ball4.Load(ball4.ImageString);
                    p1Ball5.Load(ball5.ImageString);
                    p1Ball6.Load(ball6.ImageString);
                    p1Ball7.Load(ball7.ImageString);
                    p1Solid = true;
                    break;
            }

            switch (player2.BallType)
            {
                case "Half":
                    txtTest.Text += player2.Name + " har halve kuler" + "\r\n";
                    p2Ball1.Load(ball9.ImageString);
                    p2Ball2.Load(ball10.ImageString);
                    p2Ball3.Load(ball11.ImageString);
                    p2Ball4.Load(ball12.ImageString);
                    p2Ball5.Load(ball13.ImageString);
                    p2Ball6.Load(ball14.ImageString);
                    p2Ball7.Load(ball15.ImageString);
                    p2Half = true;
                    break;
                case "Solid":
                    txtTest.Text += player2.Name + " har hele kuler" + "\r\n";
                    p2Ball1.Load(ball1.ImageString);
                    p2Ball2.Load(ball2.ImageString);
                    p2Ball3.Load(ball3.ImageString);
                    p2Ball4.Load(ball4.ImageString);
                    p2Ball5.Load(ball5.ImageString);
                    p2Ball6.Load(ball6.ImageString);
                    p2Ball7.Load(ball7.ImageString);
                    p2Solid = true;
                    break;
            }
        }

        // Metode som sjekker halve kuler, og hvilke kuler som er igjen på bordet
        public void CheckHalf()
        {
            counterHalf = 0;

            foreach (var Ball in halfBalls)
            {

                switch (Ball.IsOnTable)
                {
                    case true:
                        break;
                    case false:
                        Ball.ImageString = ("images/control.png");

                        BallType();

                        if (Ball.IsOnTable == false)
                        {
                            counterHalf += 1;

                            if (counterHalf >= 7)
                            {
                                if (p1Half == true && p2Solid == true)
                                {
                                    p1Ball8.Load("images/black.png");
                                }

                                else if (p2Half == true && p1Solid == true)
                                {
                                    p2Ball8.Load("images/black.png");
                                }
                            }
                            txtTest.Text = counterHalf.ToString();
                        }
                        break;
                }
            }
        }

        // Metode som sjekker hele kuler, og hvilke kuler som er igjen på bordet
        public void CheckSolid()
        {
            counterSolid = 0;

            foreach (var Ball in solidBalls)
            {
                switch (Ball.IsOnTable)
                {
                    case true:
                        break;
                    case false:
                        Ball.ImageString = ("images/control.png");

                        BallType();

                        if (Ball.IsOnTable == false)
                        {
                            counterSolid += 1;

                            if (counterSolid >= 7)
                            {
                                if (p1Solid == true && p2Half == true)
                                {
                                    p1Ball8.Load("images/black.png");
                                }

                                else if (p2Solid == true && p1Half == true)
                                {
                                    p2Ball8.Load("images/black.png");
                                }
                            }
                        }
                        break;
                }
            }
        }

        // Metode for å sjekke hvem som har hvilke baller, og legge riktig baller til riktig spiller
        public void BallType()
        {
            switch (player1.BallType)
            {
                case "Solid":
                    p1Ball1.Load(ball1.ImageString);
                    p1Ball2.Load(ball2.ImageString);
                    p1Ball3.Load(ball3.ImageString);
                    p1Ball4.Load(ball4.ImageString);
                    p1Ball5.Load(ball5.ImageString);
                    p1Ball6.Load(ball6.ImageString);
                    p1Ball7.Load(ball7.ImageString);
                    break;

                case "Half":
                    p1Ball1.Load(ball9.ImageString);
                    p1Ball2.Load(ball10.ImageString);
                    p1Ball3.Load(ball11.ImageString);
                    p1Ball4.Load(ball12.ImageString);
                    p1Ball5.Load(ball13.ImageString);
                    p1Ball6.Load(ball14.ImageString);
                    p1Ball7.Load(ball15.ImageString);
                    break;
            }

            switch (player2.BallType)
            {
                case "Solid":
                    p2Ball1.Load(ball1.ImageString);
                    p2Ball2.Load(ball2.ImageString);
                    p2Ball3.Load(ball3.ImageString);
                    p2Ball4.Load(ball4.ImageString);
                    p2Ball5.Load(ball5.ImageString);
                    p2Ball6.Load(ball6.ImageString);
                    p2Ball7.Load(ball7.ImageString);
                    break;

                case "Half":
                    p2Ball1.Load(ball9.ImageString);
                    p2Ball2.Load(ball10.ImageString);
                    p2Ball3.Load(ball11.ImageString);
                    p2Ball4.Load(ball12.ImageString);
                    p2Ball5.Load(ball13.ImageString);
                    p2Ball6.Load(ball14.ImageString);
                    p2Ball7.Load(ball15.ImageString);
                    break;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            CheckBoxesSolid();
            CheckBoxesHalf();
            CheckBoxBlack();
            CheckSolid();
            CheckHalf();
            CheckBlack();
            CheckResult();
        }


        // Midlertidig løsning med checkboxes for å fjerne kuler fra bordet
        public void CheckBoxesSolid()
        {
            if (cbYellowSolid.Checked == false)
            {
                ball1.IsOnTable = false;
            }

            if (cbBlueSolid.Checked == false)
            {
                ball2.IsOnTable = false;
            }

            if (cbRedSolid.Checked == false)
            {
                ball3.IsOnTable = false;
            }

            if (cbPurpleSolid.Checked == false)
            {
                ball4.IsOnTable = false;
            }

            if (cbOrangeSolid.Checked == false)
            {
                ball5.IsOnTable = false;
            }

            if (cbGreenSolid.Checked == false)
            {
                ball6.IsOnTable = false;
            }

            if (cbBrownSolid.Checked == false)
            {
                ball7.IsOnTable = false;
            }
        }

        public void CheckBoxesHalf()
        {
            if (cbYellowHalf.Checked == false)
            {
                ball9.IsOnTable = false;
            }

            if (cbBlueHalf.Checked == false)
            {
                ball10.IsOnTable = false;
            }

            if (cbRedHalf.Checked == false)
            {
                ball11.IsOnTable = false;
            }

            if (cbPurpleHalf.Checked == false)
            {
                ball12.IsOnTable = false;
            }

            if (cbOrangeHalf.Checked == false)
            {
                ball13.IsOnTable = false;
            }

            if (cbGreenHalf.Checked == false)
            {
                ball14.IsOnTable = false;
            }

            if (cbBrownHalf.Checked == false)
            {
                ball15.IsOnTable = false;
            }
        }

        public void CheckBoxBlack()
        {
            if (cbBlack.Checked == false)
            {
                ball8.IsOnTable = false;
            }
        }


        // Metode for å gi cuen over til neste spiller
        public void Turn()
        {
            string turn = "";
            p1Turn = !p1Turn;
            if (p1Turn == true)
            {
                p2Turn = false;
                //turn = "P1 turn";
                //txtTest.Text += turn;
                pBoxCueP1.Load("images/CueF.png");
                pBoxCueP2.Load("images/control.png");
            }

            else
            {
                p2Turn = true;
                //turn = "P2 turn";
                //txtTest.Text += turn;
                pBoxCueP1.Load("images/control.png");
                pBoxCueP2.Load("images/Cue.png");
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Turn();
        }

        // Metode som sjekker om svart er puttet, og om det evt er flere baller av en sort på bordet
        // noe som betyr at den andre vinner
        public void CheckBlack()
        {
            if (halfBalls[7].IsOnTable == false)
            {
                if (counterHalf < 7)
                {
                    if (p1Half == true && p1Turn == true)
                    {
                        p1Lost = true;
                    }
                    else if(p2Half == true && p2Turn == true)
                    {
                        p2Lost = true;
                    }
                    
                }

                else if (counterHalf >= 7)
                {
                    if (p1Half == true && p1Turn == true)
                    {
                        p2Lost = true;
                    }
                    else if (p2Half == true && p2Turn == true)
                    {
                        p1Lost = true;
                    }

                }

            }
            if (solidBalls[7].IsOnTable == false)
            {
                if (counterSolid < 7)
                {
                    if (p1Solid == true && p1Turn == true)
                    {
                        p1Lost = true;
                    }
                    else if (p2Solid == true && p2Turn == true)
                    {
                        p2Lost = true;
                    }
                }

                else if (counterSolid >= 7)
                {
                    if (p1Solid == true && p1Turn == true)
                    {
                        p2Lost = true;
                    }
                    else if (p2Solid == true && p2Turn == true)
                    {
                        p1Lost = true;
                    }
                }
            }
        }

        public void CheckResult()
        {
            if (p1Lost == true)
            {
                stopwatch.Stop();
                MessageBox.Show(player2.Name + " vinner!");
            }

            else if (p2Lost == true)
            {
                stopwatch.Stop();
                MessageBox.Show(player1.Name + " vinner!");
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            videoCaptureDevice.Stop();
            Application.Exit();
        }
    }
}

