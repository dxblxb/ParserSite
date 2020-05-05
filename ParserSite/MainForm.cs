﻿using System;
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
        public MainForm()
        {
            InitializeComponent();
            Browser.Url = "https://www.scoreboard.com/ru/football/england/premier-league-2018-2019/results/";
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            TBUrl.Text = "https://ru.soccerway.com/teams/england/leicester-city-fc/682/matches/";
            //Browser.Url = TBUrl.Text;
            //string page = ParserSites.GetPage(TBUrl.Text);
            string page = Browser.GetHtml();
            string[] matches = ParserSites.GetMatches(page);
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
            Browser.Url = "https://www.scoreboard.com/ru/match/tjScDhWo/#match-summary";
        }
    }
}
