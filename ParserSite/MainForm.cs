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
                MatchState result = new MatchState();
                //resultMatches = ParserSites.GetMatch(Browser.GetHtml());
                result=ParserSites.GetMatch(Browser.GetHtml());
                string json = JsonSerializer.Serialize<MatchState> (result);

                // Получить объект приложения Excel.
                Excel.Application excel_app = new Excel.Application();

                // Сделать Excel видимым (необязательно).
                excel_app.Visible = true;

                // Откройте книгу.
                Excel.Workbook workbook = excel_app.Workbooks.Open(
                    @"C:\Users\Проспект\Source\Repos\ParserSite\ParserSite\bin\Debug\DataSet.xlsx",
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

                // Посмотрим, существует ли рабочий лист.
                string sheet_name = DateTime.Now.ToString("MM-dd-yy");

                Excel.Worksheet sheet = FindSheet(workbook, sheet_name);
                if (sheet == null)
                {
                    // Добавить лист в конце.
                    sheet = (Excel.Worksheet)workbook.Sheets.Add(
                        Type.Missing, workbook.Sheets[workbook.Sheets.Count],
                        1, Excel.XlSheetType.xlWorksheet);
                    sheet.Name = DateTime.Now.ToString("MM-dd-yy");
                }

                // Добавить некоторые данные в отдельные ячейки.
                sheet.Cells[1, 1] = "A";
                sheet.Cells[1, 2] = "B";
                sheet.Cells[1, 3] = "C";

                // Делаем этот диапазон ячеек жирным и красным.
                Excel.Range header_range = sheet.get_Range("A1", "C1");
                header_range.Font.Bold = true;
                header_range.Font.Color =
                    System.Drawing.ColorTranslator.ToOle(
                        System.Drawing.Color.Red);
                header_range.Interior.Color =
                    System.Drawing.ColorTranslator.ToOle(
                        System.Drawing.Color.Pink);

                // Добавьте некоторые данные в диапазон ячеек.
                int[,] values =
                {
        { 2,  4,  6},
        { 3,  6,  9},
        { 4,  8, 12},
        { 5, 10, 15},
    };
                Excel.Range value_range = sheet.get_Range("A2", "C5");
                value_range.Value2 = values;

                // Сохраните изменения и закройте книгу.
                workbook.Close(true, Type.Missing, Type.Missing);

                // Закройте сервер Excel.
                excel_app.Quit();

                MessageBox.Show("Done");


                //for (int i = 0; i < result.Count(); ++i)
                //{
                //    
                //    RTBLog.AppendText(json);
                //    //RTBLog.AppendText("\n---------------------------\n");
                //    //RTBLog.AppendText(result.First().CommandHome);
                //    //RTBLog.AppendText("\n---------------------------\n");
                //    //RTBLog.AppendText(result.First().CommandAway);
                //    //RTBLog.AppendText("\n---------------------------\n");
                //    //RTBLog.AppendText(result.First().ReplacePlayers.First()[0]);
                //    //RTBLog.AppendText("\n---------------------------\n");
                //}
            }
        }
        private Excel.Worksheet FindSheet(Excel.Workbook workbook, string sheet_name)
        {
            foreach (Excel.Worksheet sheet in workbook.Sheets)
            {
                if (sheet.Name == sheet_name) return sheet;
            }

            return null;
        }
        private void Browser_DownloadCompleted(object sender, EO.WebBrowser.DownloadEventArgs e)
        {
            
        }
    }
}
