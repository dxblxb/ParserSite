using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParserSite
{
    public partial class MainForm : Form
    {
        public string[] matches, resultMatches;
        public int curMatch = 0;
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
            RTBLog.Clear();
            label1.Text = matches.Length.ToString();
            for (int i = 0; i<matches.Length; ++i)
            {
                RTBLog.AppendText(matches[i]);
                RTBLog.AppendText("\n---------------------------\n");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Browser.Url = $"https://www.scoreboard.com/ru/match/{matches[curMatch]}/#match-summary";
            string link = $"https://www.scoreboard.com/ru/match/{matches[curMatch]}/#match-summary";
            string page = ParserSites.GetPage(link);
            RTBLog.Clear();
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
                List<MatchState> result = new List<MatchState>();
                //resultMatches = ParserSites.GetMatch(Browser.GetHtml());
                result = ParserSites.GetMatch(Browser.GetHtml());
                for (int i = 0; i < result.Count(); ++i)
                {
                    RTBLog.AppendText(result.First().Date);
                    RTBLog.AppendText("\n---------------------------\n");
                    RTBLog.AppendText(result.First().CommandHome);
                    RTBLog.AppendText("\n---------------------------\n");
                    RTBLog.AppendText(result.First().CommandAway);
                    RTBLog.AppendText("\n---------------------------\n");
                }
            }
        }

        private void Browser_DownloadCompleted(object sender, EO.WebBrowser.DownloadEventArgs e)
        {
            
        }
    }
}
