using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class frmExpanded : Form
    {
        public frmExpanded()
        {
            InitializeComponent();
        }
        public InvItemDB secondFile;
        private List<ToDoList> lists = null;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FillItemListBox();
            frmExpandDetails details = new frmExpandDetails();
            details.ShowDialog();
            ToDoList item = null;
            item = details.item;
            if (!details.cancel)
            {
                lbList.Items.Add(item.Details);
                lists.Add(item);
                secondFile.SaveItems(lists, false);
            }
          
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            int i = lbList.SelectedIndex;
            if (i != -1)
            {

                // the deletion and then removes the item from the list
                
                lbList.Items.RemoveAt(i);
                // saves the list of products, and refreshes the list box 
                lists.Remove(lists[i - 1]);
              
                secondFile.SaveItems(lists, false);
                FillItemListBox();
                
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FillItemListBox()
        {
            lbList.Items.Clear();
            lbList.Items.Add("Details");
            // Add code here that loads the list box with the items in the list.
            lists = secondFile.GetItems(false);
            foreach (ToDoList item in lists)
            {
                lbList.Items.Add(item.Details);

            }
        }


        private void frmExpanded_Load(object sender, EventArgs e)
        {
            string path = secondFile.getPath();
            if (!File.Exists(path))
            {
                FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter outputFile = new StreamWriter(file);
                outputFile.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?> \n<ToDoLists />");
                outputFile.Close();
            }


            FillItemListBox();
        }
    }
}
