using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBWinForm //SQWorck.LoadDateFromDB(TitleTextBox);
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
            if (SQWorck.SearchInDB(SearchBox))
            {
                SearchBox.DroppedDown = true;
            }
            else
            {
                SearchBox.DroppedDown = false;
            }
        }
        private void SearchBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseButton.Visible = true;
            AddButton.Visible = false;
            SearchButton.Visible = false;
            SearchBox.Visible = false;
            TitleTextBox.Visible = true;
            ContentTextBox.Visible = true;

            if (SearchBox.SelectedIndex == 0 || SearchBox.SelectedIndex % 3 == 0)
            {
                string[] stringWithIindex = functions.Split((string)SearchBox.Items[SearchBox.SelectedIndex], ' ');
                SQWorck.LoadDateFromDBByID(int.Parse(stringWithIindex[0]), ContentTextBox, TitleTextBox);
            }
            else
            {
                int numberOfRowWithID = 0;
                for (int i = SearchBox.SelectedIndex; i != 0;)
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
                string[] stringWithIindex = functions.Split((string)SearchBox.Items[numberOfRowWithID], ' ');
                SQWorck.LoadDateFromDBByID(int.Parse(stringWithIindex[0]), ContentTextBox, TitleTextBox);
            }
            
            //SQWorck.SearcInDB(SearchBox, ContentTextBox, TitleTextBox);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            CloseButton.Visible = false;
            AddButton.Visible = true;
            SearchButton.Visible = true;
            SearchBox.Visible = true;
            SearchBox.Text = "";
            TitleTextBox.Visible = false;
            ContentTextBox.Visible = false;
            ContentTextBox.Text = "";
        }
    }
}
