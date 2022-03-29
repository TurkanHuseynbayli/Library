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
    public partial class ADD : Form
    {
        public ADD()
        {
            InitializeComponent();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var BookForm = new BookForm
            {
                Name = txtName.Text,
               
            };

            var json = JsonConvert.SerializeObject(BookForm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();

            var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            client.BaseAddress = new Uri("https://localhost:7223/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            client.BaseAddress = new Uri("https://localhost:7223/");
            var result = await client.PostAsync("api/Book", content);

            string resultContent = await result.Content.ReadAsStringAsync();

            if (((int)result.StatusCode) == 200)
            {
                var result2 = await client.GetAsync("api/Book");
                string resultContent2 = await result2.Content.ReadAsStringAsync();
                var info = JsonConvert.DeserializeObject<List<BookForm>>(resultContent2);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = info;
            }

            else
            {
                MessageBox.Show("Only the Admin can add it!!!");

            }
        }

        private async void ADD_Load(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            client.BaseAddress = new Uri("https://localhost:7223/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var result = await client.GetAsync("api/Book");
            

            string resultContent = await result.Content.ReadAsStringAsync();
          

            var info = JsonConvert.DeserializeObject<List<BookForm>>(resultContent);
          

            dataGridView1.DataSource = info;

            this.dataGridView1.AllowUserToAddRows = false;
        }
    }
}
