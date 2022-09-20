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
            //CMD.CommandText = "select * from Content where Title like '%" + (string)searcResult.Text + "%' or Content like '%" + (string)searcResult.Text + "%'";
            CMD.CommandText = "select *,substr(Content,1,20) from Content where Title like '%" + (string)searcResult.Text + "%' or Content like '%" + (string)searcResult.Text + "%'";
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
                    searcResult.Items.Insert(rowCounterForSearcResult, (string)dbReturnetData["substr(Content,1,20)"] + "...");
                    rowCounterForSearcResult++;
                }
            }
            else
            {
                searcResult.Text = "Нет такго";
            }
            dbReturnetData.Close();
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
            dbReturnetData.Close();
            BreakTheConnectionToDB();
        }
    
        
        public List<string> SearchInDBOnServer(string searchQuery)
        {
            List<string> searchResult = new List<string>();
            ConnectToDB();
            SQLiteCommand CMD = DB.CreateCommand();
            CMD.CommandText = "select *,substr(Content,1,20) from Content where Title like '%" + searchQuery + "%' or Content like '%" + searchQuery + "%'";
            SQLiteDataReader dbReturnetData = CMD.ExecuteReader();
            if (dbReturnetData.HasRows)
            {
                while (dbReturnetData.Read())
                {
                    searchResult.Add(dbReturnetData["ID"].ToString() + " id");
                    searchResult.Add((string)dbReturnetData["Title"]);
                    searchResult.Add((string)dbReturnetData["substr(Content,1,20)"] + "...");
                }
            }
            else
            {
                searchResult.Add("Нет такго");
            }
            dbReturnetData.Close();
            BreakTheConnectionToDB();
            return searchResult;
        }
        public void AddToDB( string title, string content)
        {
            ConnectToDB();
            SQLiteCommand CMD = DB.CreateCommand();
            CMD.CommandText = "insert into Content(Title, Content) values ('" + title + "', '" + content + "')";
            CMD.ExecuteNonQuery();
            BreakTheConnectionToDB();
        }
        public void DeliteFromDB(int ID)
        {
            ConnectToDB();
            SQLiteCommand CMD = DB.CreateCommand(); 
            CMD.CommandText = "delete from Content where ID like '" +ID + "'";
            CMD.ExecuteNonQuery();
            BreakTheConnectionToDB();
        }
    }
}
