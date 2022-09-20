using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBWinForm 
{
    public partial class Form1 : Form
    {
        //Для нестатического поля требуется ссылка на обьект, по этому это тута вот
        //Надо узнать как исправить
        SQLiteWorckbench SQWorck = new SQLiteWorckbench();
        TechnicalFunctions functions = new TechnicalFunctions();

        public Form1()
        {
            InitializeComponent();
        }
        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchButtonAction();
        }
        private void SearchBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropMenuAction();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            CloseButtonAction();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteButtonAction();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddButtonAction();
        }

        private void CloseButtonAction()
        {
            CloseButton.Visible = false;
            AddButton.Visible = true;
            SearchButton.Visible = true;
            SearchBox.Visible = true;
            SearchBox.Text = "";
            TitleTextBox.Visible = false;
            TitleTextBox.Text = "";
            ContentTextBox.Visible = false;
            ContentTextBox.Text = "";
            DeleteButton.Visible = false;
            IDtext.Text = "";
        }
        private void SearchButtonAction()
        {
            SearchBox.Items.Clear();
            List<string> resultOfSearcing = SQWorck.SearchInDBOnServer(SearchBox.Text);
            
            for (int i = 0; i < resultOfSearcing.Count; i++)
            {
                SearchBox.Items.Insert(i,resultOfSearcing[i]);
            }
            if (SearchBox.Items.Count>1)
            SearchBox.DroppedDown = true;
        }
        private void DropMenuAction()
        {
            CloseButton.Visible = true;
            AddButton.Visible = false;
            SearchButton.Visible = false;
            SearchBox.Visible = false;
            TitleTextBox.Visible = true;
            ContentTextBox.Visible = true;
            DeleteButton.Visible = true;

            int numberOfRowWithID = SearchBox.SelectedIndex;
            if (numberOfRowWithID != 0 || numberOfRowWithID % 3 != 0)
            {
                for (int i = SearchBox.SelectedIndex; i != -1;)
                {
                    if (i % 3 == 0)
                    {
                        numberOfRowWithID = i;
                        break;
                    }
                    else
                    {
                        i--;
                    }
                }
            }
            IDtext.Text = (string)SearchBox.Items[numberOfRowWithID];
            /*
            string[] stringWithIindex = functions.Split((string)SearchBox.Items[numberOfRowWithID], ' ');
            SQWorck.LoadDateFromDBByID(int.Parse(stringWithIindex[0]), ContentTextBox, TitleTextBox);
            */

            SQWorck.LoadDateFromDBByID(int.Parse(functions.Split((string)SearchBox.Items[numberOfRowWithID], ' ').First()), ContentTextBox, TitleTextBox);
        }
        private void AddButtonAction()
        {
            if (CloseButton.Visible == true && AddButton.Visible == true)
            {
                SQWorck.AddToDB(TitleTextBox.Text, ContentTextBox.Text );
                CloseButtonAction();
                SearchBox.Text = "Запись добавлена.";
            }
            else
            {
                CloseButton.Visible = true;
                SearchButton.Visible = false;
                SearchBox.Visible = false;
                TitleTextBox.Visible = true;
                ContentTextBox.Visible = true;
                TitleTextBox.Text = "";
                ContentTextBox.Text = "";
            }
        }
        private void DeleteButtonAction()
        {
            SQWorck.DeliteFromDB(int.Parse(functions.Split(IDtext.Text, ' ').First()));
            CloseButtonAction();
            SearchBox.Text = "Запись удалена.";
        }

    }
}
