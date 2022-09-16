using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace DBWinForm
{
    class SQLiteWorckbench
    {
        private SQLiteConnection DB;

        private void ConnectToDB()
        {
            DB = new SQLiteConnection("Data Source=TestDB.db");
            DB.Open();
        }
        private void BreakTheConnectionToDB()
        {
            DB.Close();
        }

        public bool SearchInDB(ComboBox searcResult)
        {
            bool state = false;
            searcResult.Items.Clear();

            ConnectToDB();

            SQLiteCommand CMD = DB.CreateCommand();
            CMD.CommandText = "select * from Content where Title like '%" + (string)searcResult.Text + "%' or Content like '%" + (string)searcResult.Text + "%'";
            SQLiteDataReader dbReturnetData = CMD.ExecuteReader();
            if (dbReturnetData.HasRows)
            {
                state = true;
                int rowCounterForSearcResult = 0;
                while (dbReturnetData.Read())
                {
                    searcResult.Items.Insert(rowCounterForSearcResult, dbReturnetData["ID"].ToString() + " id");
                    rowCounterForSearcResult++;
                    searcResult.Items.Insert(rowCounterForSearcResult, (string)dbReturnetData["Title"]);
                    rowCounterForSearcResult++;
                    searcResult.Items.Insert(rowCounterForSearcResult, (string)dbReturnetData["Content"]);
                    rowCounterForSearcResult++;
                }
            }
            else
            {
                searcResult.Text = "Нет такго";
            }
            BreakTheConnectionToDB();
            return state;
        }

        public void LoadDateFromDBByID(int ID, TextBox contentOutput, TextBox titleOutput)
        {
            ConnectToDB();
            SQLiteCommand CMD = DB.CreateCommand();
            CMD.CommandText = "select * from Content where ID like '" + ID + "'";
            SQLiteDataReader dbReturnetData = CMD.ExecuteReader();
            dbReturnetData.Read();
            titleOutput.Text = (string)dbReturnetData["Title"];
            contentOutput.Text = (string)dbReturnetData["Content"];
            BreakTheConnectionToDB();
        }
        public void LoadDateFromDB(string text)
        {
            ConnectToDB();

            SQLiteCommand CMD = DB.CreateCommand();
            CMD.CommandText = "select * from Users";
            SQLiteDataReader dbReturnetData = CMD.ExecuteReader();
            if (dbReturnetData.HasRows)
            {
                while (dbReturnetData.Read())
                {
                    text += "Логин: " + dbReturnetData["Title"] + " Пароль: " + dbReturnetData["Password"] + " \r\n";
                }
            }
            else
            {
                text = "null";
            }

            BreakTheConnectionToDB();
        }
    }
}
