﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class frmAddDetails : Form
    {
        
        public frmAddDetails()
        {
            InitializeComponent();
        }
        public ToDoList item = new ToDoList();
        public bool cancel = false;
        private void btnDone_Click(object sender, EventArgs e)
        {
            if(txtDetails.Text!="")
            {
                item.Date = dtDate.Value.ToShortDateString();
                item.Details = txtDetails.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("You can leave category empty");
            }
           

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cancel = true;
            this.Close();
        }
    }
}
