using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ToDoList
{
    public partial class Form1 : Form
    {
      
        
        public Form1()
        {
            InitializeComponent();
        }
       
        private List<ToDoList> lists = null;
        public InvItemDB file = new InvItemDB("ToDoListInventory");
        public string parsed;
        private string secondFilePath;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            FillItemListBox();
            frmAddDetails details = new frmAddDetails();
            details.ShowDialog();
            ToDoList item = null;
            item = details.item;
            if(!details.cancel)
            {
                lbList.Items.Add(item.displayText());
                lists.Add(item);
                file.SaveItems(lists, true);
            }
          
            


        }
        private void FillItemListBox()
        {
            lbList.Items.Clear();
            lbList.Items.Add("Date" + "\t\t" + "Category");
            // Add code here that loads the list box with the items in the list.
            lists = file.GetItems(true);
            foreach (ToDoList item in lists)
            {
                lbList.Items.Add(item.displayText());
               
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            int i = lbList.SelectedIndex;
            if (i != -1)
            {

                // the deletion and then removes the item from the list,
                //MessageBox.Show(parsed);
                File.Delete(@"..\..\" + parsed + ".xml");
                lbList.Items.RemoveAt(i);
                // saves the list of products, and refreshes the list box 
                lists.Remove(lists[i - 1]);
                file.SaveItems(lists, true);
                FillItemListBox();
            }
            else
            {
                MessageBox.Show("You must select an item");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

            const string path = @"..\..\ToDoListInventory.xml";
            if (!File.Exists(path))
            {
                FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter outputFile=new StreamWriter(file);
                outputFile.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?> \n<ToDoLists />");
                outputFile.Close();
            }
           

            FillItemListBox();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            int selected = lbList.SelectedIndex;
            if (selected!=-1)
            {
                frmExpanded expand = new frmExpanded();
                //figure a way out to sperate the date from the details
                string data = lbList.GetItemText(lbList.SelectedItem);
                int i;
                //getting the third /
                i = data.IndexOf("/", 3);
                //adding the date
                string subString = data.Substring(i+6);
                parsed = subString.Replace("\t", "");
                
               
                expand.lblDetail.Text = parsed;
                expand.secondFile = new InvItemDB(parsed);
                secondFilePath = expand.secondFile.getPath();
                expand.Show();
            }
            else
            {
                MessageBox.Show("You must select an item");
            }
            
        }

     
    }
}
