using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableMySql;

namespace DataSetConvert
{
    public partial class MainForm : Form
    {
        public Table pool;
        public MainForm()
        {
            InitializeComponent();
            Table.setConnection("mysql.id190489746-0.myjino.ru", "3306", "046039000_taramp", "l6gnHxy87VW", "id190489746-0_edl");
            pool = new Table("pool");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            grid.DataSource = pool.dbTable;
        }
    }
}
