using MySql.Data.MySqlClient;
using System.Data;
using System;

namespace TableMySql
{
    public class Table
    {
        public static MySqlConnection connection;
        public string sqlStr;
        public string tableName;
        public DataTable dbTable = new DataTable();
        public string connectionResult;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        public Table(string tableName)
        {
            this.tableName = tableName;
            this.sqlStr = "SELECT * FROM " + this.tableName;
            this.connectionResult = this.refresh();
        }

        #region connection

        /// <summary>
        /// Устанавлиет строку соиденения с базой данных MySql
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="database"></param>
        public static void setConnection(string server, string port, string username, string password, string database)
        {
            MySqlConnection connection = new MySqlConnection($"server={server};port={port};username={username};password={password};database={database}");
            Table.connection = connection;
        }

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
        #endregion
        /// <summary>
        /// Обновляет данные в таблице dbTable
        /// </summary>
        /// <returns>Возврашает ошибку string</returns>
        public string refresh()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand(this.sqlStr, this.getConnection());

            adapter.SelectCommand = command;
            try
            {
                this.openConnection();
            }
            catch
            {
                return "Нет соединения с сервером!";
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
                return "Ошибка соединения с сервером!";
            }
            return "Успешно";

        }
        /// <summary>
        /// Добавляет строку в таблицу dbTable
        /// </summary>
        /// <param name="colName"></param>
        /// <param name="colData"></param>
        /// <returns>Возврашает ошибку string</returns>
        public string addRow(string[] colName, object[] colData)
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
                return "Ошибка сохранения. Нет соединения с сервером!";
            }
            finally
            {
            }

            command.ExecuteNonQuery();

            this.closeConnection();

            this.refresh();
            return "Успешно";
        }
        /// <summary>
        /// Обновляет строку в таблице dbTable
        /// </summary>
        /// <param name="idRow"></param>
        /// <param name="colName"></param>
        /// <param name="colData"></param>
        /// <returns>Возврашает ошибку string</returns>
        public string updateRow(int idRow, string[] colName, object[] colData)
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
                return "Ошибка сохранения. Нет соединения с сервером!";
            }
            finally
            {
            }


            command.ExecuteNonQuery();

            this.closeConnection();

            this.refresh();
            return "Успешно!";
        }
        /// <summary>
        /// Удаляет строку в таблице
        /// </summary>
        /// <param name="colName"></param>
        /// <param name="colData"></param>
        /// <returns>Возврашает ошибку string</returns>
        public string deleteRow(string colName, object colData)
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
                return "Ошибка сохранения. Нет соединения с сервером!";
            }
            finally
            {
            }

            command.ExecuteNonQuery();

            this.closeConnection();

            this.refresh();
            return "Успешно";
        }
    }
}
