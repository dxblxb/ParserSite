using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ParserSite
{
    class TableMySql
    {
        MySqlConnection connection = new MySqlConnection("server=mysql.id190489746-0.myjino.ru;port=3306;username=046039000_taramp;password=l6gnHxy87VW;database=id190489746-0_edl");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }

        public string sqlStr;
        public string tableName;
        public DataTable dbTable = new DataTable();
        public bool connectionResult;


        public TableMySql(string tableName)
        {
            this.tableName = tableName;
            this.sqlStr = "SELECT * FROM " + this.tableName;
            this.connectionResult = this.refresh();
        }

        public bool refresh()
        {
            //MessageBox.Show("Обновлено!");
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand(this.sqlStr, this.getConnection());

            adapter.SelectCommand = command;
            try
            {
                this.openConnection();
            }
            catch
            {
                MessageBox.Show("Нет соединения с сервером!");
                return false;
            }
            finally
            {
                this.closeConnection();
            }

            this.dbTable.Clear();
            try
            {
                adapter.Fill(this.dbTable);
            }
            catch
            {
                MessageBox.Show("Ошибка соединения с сервером!");
                return false;
            }
            return true;

        }

        public bool addRow(string[] colName, object[] colData)
        {
            string sqlAddStr = "INSERT INTO " + this.tableName + " (";
            int colCount = colName.Length;

            for (int i = 0; i < colCount; ++i)
            {
                sqlAddStr += colName[i];
                if (i == colCount - 1) { sqlAddStr += ")"; } else { sqlAddStr += ", "; };
            }

            sqlAddStr += " VALUES (";

            for (int i = 0; i < colCount; ++i)
            {
                sqlAddStr += "@parm" + i.ToString();
                if (i == colCount - 1) { sqlAddStr += ")"; } else { sqlAddStr += ", "; };
            }



            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand(sqlAddStr, this.getConnection());

            for (int i = 0; i < colCount; ++i)
            {
                if (colData[i].GetType() == typeof(string))
                    command.Parameters.Add("@parm" + i, MySqlDbType.VarChar).Value = colData[i];
                if (colData[i].GetType() == typeof(int))
                    command.Parameters.Add("@parm" + i, MySqlDbType.Int32).Value = colData[i];
                if (colData[i].GetType() == typeof(bool))
                    command.Parameters.Add("@parm" + i, MySqlDbType.Int16).Value = colData[i];
                if (colData[i].GetType() == typeof(float))
                    command.Parameters.Add("@parm" + i, MySqlDbType.Float).Value = colData[i];
                if (colData[i].GetType() == typeof(DateTime))
                    command.Parameters.Add("@parm" + i, MySqlDbType.DateTime).Value = colData[i];
            }

            try
            {
                this.openConnection();
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения. Нет соединения с сервером!");
                return false;
            }
            finally
            {
            }

            command.ExecuteNonQuery();

            this.closeConnection();

            this.refresh();
            return true;
        }
        public bool updateRow(int idRow, string[] colName, object[] colData)
        {
            string sqlUpdateStr = "UPDATE " + this.tableName + " SET ";
            int colCount = colName.Length;

            for (int i = 0; i < colCount; ++i)
            {
                sqlUpdateStr += colName[i] + " = @parm" + i;
                if (i == colCount - 1) { sqlUpdateStr += " "; } else { sqlUpdateStr += ", "; };
            }
            sqlUpdateStr += "WHERE id = " + idRow.ToString();

            //MessageBox.Show(sqlUpdateStr);

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand(sqlUpdateStr, this.getConnection());

            for (int i = 0; i < colCount; ++i)
            {
                if (colData[i].GetType() == typeof(string))
                    command.Parameters.Add("@parm" + i, MySqlDbType.VarChar).Value = colData[i];
                if (colData[i].GetType() == typeof(int))
                    command.Parameters.Add("@parm" + i, MySqlDbType.Int32).Value = colData[i];
                if (colData[i].GetType() == typeof(bool))
                    command.Parameters.Add("@parm" + i, MySqlDbType.Int16).Value = colData[i];
                if (colData[i].GetType() == typeof(float))
                    command.Parameters.Add("@parm" + i, MySqlDbType.Float).Value = colData[i];
                if (colData[i].GetType() == typeof(DateTime))
                    command.Parameters.Add("@parm" + i, MySqlDbType.DateTime).Value = colData[i];
            }
            try
            {
                this.openConnection();
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения. Нет соединения с сервером!");
                return false;
            }
            finally
            {
            }


            command.ExecuteNonQuery();

            this.closeConnection();

            this.refresh();
            return true;
        }
        public bool deleteRow(string colName, object colData)
        {
            string sqlDelStr = "DELETE FROM " + this.tableName + " WHERE " + colName + " = @param";

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand(sqlDelStr, this.getConnection());

            if (colData.GetType() == typeof(string))
                command.Parameters.Add("@param", MySqlDbType.VarChar).Value = colData;
            if (colData.GetType() == typeof(int))
                command.Parameters.Add("@param", MySqlDbType.Int32).Value = colData;
            if (colData.GetType() == typeof(bool))
                command.Parameters.Add("@param", MySqlDbType.Int16).Value = colData;
            if (colData.GetType() == typeof(float))
                command.Parameters.Add("@param", MySqlDbType.Float).Value = colData;
            if (colData.GetType() == typeof(DateTime))
                command.Parameters.Add("@param", MySqlDbType.DateTime).Value = colData;

            try
            {
                this.openConnection();
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения. Нет соединения с сервером!");
                return false;
            }
            finally
            {
            }

            command.ExecuteNonQuery();

            this.closeConnection();

            this.refresh();
            return true;
        }
    }
}
