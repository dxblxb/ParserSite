using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using WindowsInput;

namespace ParserSite
{
    public partial class MainForm : Form
    {
        public string[] matches, resultMatches;
        public int curMatch = 0;
        public int id = 1;
        TableMySql mainTable = new TableMySql("pool");
        TableMySql replaceTable = new TableMySql("replaysplayers");
        public MainForm()
        {
            InitializeComponent();
            Browser.Url = "https://www.scoreboard.com/ru/football/england/premier-league-2018-2019/results/";
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            //TBUrl.Text = "https://ru.soccerway.com/teams/england/leicester-city-fc/682/matches/";
            //Browser.Url = TBUrl.Text;
            //string page = ParserSites.GetPage(TBUrl.Text);
            string page = Browser.GetHtml();
            matches = ParserSites.GetMatches(page);
            //Browser.DocumentText = ParserSites.table;
            label1.Text = matches.Length.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Browser.Url = $"https://www.scoreboard.com/ru/match/{matches[curMatch]}/#match-summary";
            string link = $"https://www.scoreboard.com/ru/match/{matches[curMatch]}/#match-summary";
            string page = ParserSites.GetPage(link);
            //RTBLog.Text = page;
            //RTBLog.Text = Browser.GetHtml();
            //string[] matche = ParserSites.p(page);
            ++curMatch;
            //detailMS__incidentsHeader stage-12
        }

        private void Browser_LoadCompleted(object sender, EO.WebBrowser.LoadCompletedEventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (curMatch > 0)
            {
                MatchState result = new MatchState();
                result = ParserSites.GetMatch(Browser.GetHtml());

                string[] colName = {
                    "date",
                    "orbitr",
                    "trainerhome",
                    "traineraway",
                    "commandhome",
                    "commandaway",
                    "matchscorehome",
                    "matchscoreaway",
                    "totalfirsttimehome",
                    "totalfirsttimeaway",
                    "homeplayer1",
                    "homeplayer2",
                    "homeplayer3",
                    "homeplayer4",
                    "homeplayer5",
                    "homeplayer6",
                    "homeplayer7",
                    "homeplayer8",
                    "homeplayer9",
                    "homeplayer10",
                    "homeplayer11",
                    "homeplayer12",
                    "homeplayer13",
                    "homeplayer14",
                    "homeplayer15",
                    "homeplayer16",
                    "homeplayer17",
                    "homeplayer18",
                    "awayplayer1",
                    "awayplayer2",
                    "awayplayer3",
                    "awayplayer4",
                    "awayplayer5",
                    "awayplayer6",
                    "awayplayer7",
                    "awayplayer8",
                    "awayplayer9",
                    "awayplayer10",
                    "awayplayer11",
                    "awayplayer12",
                    "awayplayer13",
                    "awayplayer14",
                    "awayplayer15",
                    "awayplayer16",
                    "awayplayer17",
                    "awayplayer18",
                };
                object[] colData = {
                    Convert.ToDateTime(result.Date),
                    result.Stadion,
                    result.Orbitr,
                    result.TrainerHome,
                    result.TrainerAway,
                    result.CommandHome,
                    result.CommandAway,
                    result.MatchScoreHome,
                    result.MatchScoreAway,
                    result.TotalFirstTimeHome,
                    result.TotalFirstTimeAway,
                    result.HomePlayers[0],
                    result.HomePlayers[1],
                    result.HomePlayers[2],
                    result.HomePlayers[3],
                    result.HomePlayers[4],
                    result.HomePlayers[5],
                    result.HomePlayers[6],
                    result.HomePlayers[7],
                    result.HomePlayers[8],
                    result.HomePlayers[9],
                    result.HomePlayers[10],
                    result.HomePlayers[11],
                    result.HomePlayers[12],
                    result.HomePlayers[13],
                    result.HomePlayers[14],
                    result.HomePlayers[15],
                    result.HomePlayers[16],
                    result.HomePlayers[17],
                    result.AwayPlayers[0],
                    result.AwayPlayers[1],
                    result.AwayPlayers[2],
                    result.AwayPlayers[3],
                    result.AwayPlayers[4],
                    result.AwayPlayers[5],
                    result.AwayPlayers[6],
                    result.AwayPlayers[7],
                    result.AwayPlayers[8],
                    result.AwayPlayers[9],
                    result.AwayPlayers[10],
                    result.AwayPlayers[11],
                    result.AwayPlayers[12],
                    result.AwayPlayers[13],
                    result.AwayPlayers[14],
                    result.AwayPlayers[15],
                    result.AwayPlayers[16],
                    result.AwayPlayers[17],
                };
                mainTable.addRow(colName,colData);
                
                if (result.ReplacePlayers.Count() > 0)
                {
                    foreach (string[] tmp in result.ReplacePlayers)
                    {
                        string[] name =
                        {
                            "parentid",
                            "time",
                            "playerout",
                            "playerin",
                            "command",
                        };
                        object[] data =
                        {
                            id,
                            tmp[1],
                            tmp[2],
                            tmp[3],
                            tmp[0],
                        };
                        replaceTable.addRow(name, data);
                    }
                }
                if (result.ReplacePlayers.Count() > 0)
                {
                    foreach (string[] tmp in result.ReplacePlayers)
                    {
                        string[] name =
                        {
                            "parentid",
                            "time",
                            "playerout",
                            "playerin",
                            "command",
                        };
                        object[] data =
                        {
                            id,
                            tmp[1],
                            tmp[2],
                            tmp[3],
                            tmp[0],
                        };
                        replaceTable.addRow(name, data);
                    }
                }
                ++id;
            }
        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
        //[System.Runtime.InteropServices.DllImport("user32.dll",CharSet = System.Runtime.InteropServices.CharSet.Auto,CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        //public static extern void mouse_event(uint dwFlags,
        //int dx,
        //int dy,
        //int dwData,
        //int dwExtraInfo);

        ////Нормированные абсолютные координаты
        //private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        ////Нажатие на левую кнопку мыши
        //private const int MOUSEEVENTF_LEFTDOWN = 0x0002;

        ////Поднятие левой кнопки мыши
        //private const int MOUSEEVENTF_LEFTUP = 0x0004;

        ////перемещение указателя мыши
        //private const int MOUSEEVENTF_MOVE = 0x0001;

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Есть контакт!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ////Координаты на экране:
            //int X = 10000;
            //int Y = 10000;

            ////Перемещение курсора на указанные координаты
            //mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE,
            //System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width,
            //System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height, X, Y);

            ////Выполнение первого клика левой клавишей мыши
            //mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);

            ////Выполнение второго клика левой клавишей мыши
            /////mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            MainTable.DataSource = mainTable.dbTable;
        }

        private void Browser_DownloadCompleted(object sender, EO.WebBrowser.DownloadEventArgs e)
        {
            
        }
    }
}
