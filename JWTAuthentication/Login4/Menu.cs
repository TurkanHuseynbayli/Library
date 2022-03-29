using Login4.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login4
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void autorsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aDDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ADD aDD = new ADD();
            aDD.ShowDialog();
            
        }

        private async void menuStrip3_Click(object sender, EventArgs e)
        {
            

            HttpClient client = new HttpClient();
            var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            client.BaseAddress = new Uri("https://localhost:7223/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var result = await client.GetAsync("api/Book");
            var result2 = await client.GetAsync("api/Author");

            string resultContent = await result.Content.ReadAsStringAsync();
            string resultContent2 = await result2.Content.ReadAsStringAsync();
            ToolStripMenuItem[] items = new ToolStripMenuItem[2];


           
           var info = JsonConvert.DeserializeObject<List<BookForm>>(resultContent);
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new ToolStripMenuItem();
                items[i].Name = info[i].Name;
                //items[i].Name = "dynamicItem" + i.ToString();
                //items[i].Tag = "specialDataHere";
                //items[i].Text = "Visible Menu Text Here";
                //items[i].Click += new EventHandler(MenuItemClickHandler);
            }
            // menuStrip3.DropDownItems.AddRange(items);
            //  myMenu.DropDownItems.AddRange(items);
            //var info2 = JsonConvert.DeserializeObject<List<AuthorForm>>(resultContent2);

            // dataGridView1.DataSource = info;

            //   this.dataGridView1.AllowUserToAddRows = false;

            //AuthorForm form = new AuthorForm();
            //ComboBox combo=new ComboBox();
            ////  combo.DropDownStyle =  DropDownStyle.DropDownList;
            //combo.SelectedText = "Name";
            //combo.ValueMember = "id";
            //combo.DisplayMember = "Name";
            //comboBox1 = new ComboBox();
            //menuStrip3 
            //comboBox1.ValueMember = "id";
            //comboBox1.DisplayMember = "Name";
            //comboBox1.DataSource = info2;




        }

        private async void Menu_Load(object sender, EventArgs e)
        {

            //HttpClient client = new HttpClient();
            //var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            //client.BaseAddress = new Uri("https://localhost:7223/");
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            //var result = await client.GetAsync("api/Book");
            //var result2 = await client.GetAsync("api/Author");

            //string resultContent = await result.Content.ReadAsStringAsync();
            //string resultContent2 = await result2.Content.ReadAsStringAsync();

            //var info = JsonConvert.DeserializeObject<List<BookForm>>(resultContent);
            //var info2 = JsonConvert.DeserializeObject<List<AuthorForm>>(resultContent2);

            ////dataGridView1.DataSource = info;

            ////this.dataGridView1.AllowUserToAddRows = false;

            ////AuthorForm form = new AuthorForm();
            ////ComboBox combo=new ComboBox();
            //////  combo.DropDownStyle =  DropDownStyle.DropDownList;
            ////combo.SelectedText = "Name";
            ////combo.ValueMember = "id";
            ////combo.DisplayMember = "Name";
            ////comboBox1 = new ComboBox();

            ////comboBox1.ValueMember = "id";
            ////comboBox1.DisplayMember = "Name";
            ////comboBox1.DataSource = info2;
            ////// combo.Items.Add(info2);
            ////combo.DataSource = info2;


            //this.menuStrip1 = new MenuStrip();
            //this.MainMenuStrip = menuStrip1;
            //this.Controls.Add(this.menuStrip1);
            //this.menuStrip1.BackColor = Color.DimGray;
            //this.menuStrip1.ForeColor = Color.LightSkyBlue;
            //this.menuStrip1.Dock = DockStyle.Top;

            ////this.menuItemFile = (ToolStripMenuItem)this.menu.Items.Add("&File");
            ////this.menuItemEdit = (ToolStripMenuItem)this.menu.Items.Add("&Edit");

            //this.toolStripComboBox2 = new ToolStripComboBox();
            //this.toolStripComboBox2.Items.Add("connect");
            //this.toolStripComboBox2.Items.Add("disconnect");
            //this.toolStripComboBox2.SelectedItem = "connect";
            //this.toolStripComboBox2.Items.Add(this.toolStripComboBox2);

            ////this.menuItemSearch = new ToolStripTextBox();
            ////this.menuItemSearch.Text = "Поиск";
            ////this.menuItemSearch.ForeColor = Color.DimGray;
            ////this.menu.Items.Add(this.menuItemSearch);

            ////this.fileItemOpen = this.menuItemFile.DropDownItems.Add("Open");
            ////this.fileItemSave = this.menuItemFile.DropDownItems.Add("Save");
            ////this.fileItemClose = this.menuItemFile.DropDownItems.Add("Close");

            ////this.editItemUndo = this.menuItemEdit.DropDownItems.Add("Undo");
            ////this.editItemRedo = this.menuItemEdit.DropDownItems.Add("Redo");
            ////this.menuItemEdit.DropDownItems.Add(new ToolStripSeparator());
            ////this.editItemCopy = this.menuItemEdit.DropDownItems.Add("Copy");
            ////this.editItemPast = this.menuItemEdit.DropDownItems.Add("Past");
            
            //this.menuStrip1.Show();
        }

        private async void Menu_Load_1(object sender, EventArgs e)
        {

            HttpClient client = new HttpClient();
            var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            client.BaseAddress = new Uri("https://localhost:7223/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

           
            var result2 = await client.GetAsync("api/Author");

         
            string resultContent2 = await result2.Content.ReadAsStringAsync();

           
            var info2 = JsonConvert.DeserializeObject<List<AuthorForm>>(resultContent2);

            foreach (var item in info2)
            {
                toolStripComboBox2.Items.Add(item.Name);
                //comboBox1.Items.Add(item.Name);
                

            }
            /*

                        this.menuStrip3 = new MenuStrip();
                        this.MainMenuStrip = menuStrip3;*/


            this.menuStrip3.Show();

            

            toolStripComboBox2.ComboBox.ValueMember = "id";
            toolStripComboBox2.ComboBox.DisplayMember = "Name";
            toolStripComboBox2.ComboBox.DataSource = info2;
        }

       
    }
}
