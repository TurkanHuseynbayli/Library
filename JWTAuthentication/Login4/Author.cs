using Login4.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login4
{
    public partial class Author : Form
    {
        public Author()
        {
            InitializeComponent();
        }

        private async void Author_Load(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            client.BaseAddress = new Uri("https://localhost:7223/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);






            var result = await client.GetAsync("api/Author");

            string resultContent = await result.Content.ReadAsStringAsync();

            var info = JsonConvert.DeserializeObject<List<AuthorForm>>(resultContent);

            //dataGridView1.DataSource = info;

            //  this.dataGridView1.AllowUserToAddRows = false;
            //Book book= new Book();


            //Book.comboBox1.ValueMember = "Id";
            //comboBox1.DisplayMember = "Name";
            //comboBox1.DataSource = info;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {

                var AuthorForm = new AuthorForm
                {
                    Name = txtFullName.Text,
                    //Surname = txtSurname.Text,
                    Age = Convert.ToInt32(txtAge.Text),
                };

                var json = JsonConvert.SerializeObject(AuthorForm);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();

                var token = ProtectToken.Decrypt(ProtectToken.PrToken);
                client.BaseAddress = new Uri("https://localhost:7223/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.BaseAddress = new Uri("https://localhost:7223/");
                var result = await client.PostAsync("api/Author", content);

                string resultContent = await result.Content.ReadAsStringAsync();

                if (((int)result.StatusCode) == 200)
                {
                    var result2 = await client.GetAsync("api/Author");
                    string resultContent2 = await result2.Content.ReadAsStringAsync();
                    var info = JsonConvert.DeserializeObject<List<AuthorForm>>(resultContent2);


                    //dataGridView1.DataSource = null;
                    //dataGridView1.DataSource = info;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error", "Result", MessageBoxButtons.OK,
                //   MessageBoxIcon.Exclamation);
                MessageBox.Show(ex.Message,"Result", MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation);
            }


            //var AuthorForm = new AuthorForm
            //{
            //    Name = txtFullName.Text,
            //    //Surname = txtSurname.Text,
            //    Age = Convert.ToInt32(txtAge.Text),
            //};

            //var json = JsonConvert.SerializeObject(AuthorForm);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            //HttpClient client = new HttpClient();

            //var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            //client.BaseAddress = new Uri("https://localhost:7223/");
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //client.BaseAddress = new Uri("https://localhost:7223/");
            //var result = await client.PostAsync("api/Author", content);

            //string resultContent = await result.Content.ReadAsStringAsync();
        
            
            //if (((int)result.StatusCode) == 200)
            //{
            //    var result2 = await client.GetAsync("api/Author");
            //    string resultContent2 = await result2.Content.ReadAsStringAsync();
            //    var info = JsonConvert.DeserializeObject<List<AuthorForm>>(resultContent2);
                

            //    //dataGridView1.DataSource = null;
            //    //dataGridView1.DataSource = info;
            //}

            //else
            //{
            //    MessageBox.Show("Only the Manager can add it!!!");

            //}
        }
        
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            //var AuthorForm = new AuthorForm
            //{
            // //   Id = Convert.ToInt32(txtId.Text),
            //    Name = txtName.Text,
            //    Surname = txtSurname.Text,
            //    Age = Convert.ToInt32(txtAge.Text),
            //};

            //var json = JsonConvert.SerializeObject(AuthorForm);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            //HttpClient client = new HttpClient();
            //var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            //client.BaseAddress = new Uri("https://localhost:7223/");
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //client.BaseAddress = new Uri("https://localhost:7223/");

            //var result = await client.PutAsync($"api/Author/{txtId.Text}", content);

            //string resultContent = await result.Content.ReadAsStringAsync();


            ////MessageBox.Show(resultContent);
            //if (((int)result.StatusCode) == 200)
            //{

            //    // MessageBox.Show("Updated!");

            //    var result2 = await client.GetAsync("api/Author");

            //    string resultContent2 = await result2.Content.ReadAsStringAsync();

            //    var info = JsonConvert.DeserializeObject<List<AuthorForm>>(resultContent2);
            //    // dataGridView1.DataSource = info;

            //    this.Close();
            //}

            //else
            //{

            //    MessageBox.Show("Only the Manager can update it!!!");
            //}
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
           // HttpClient client = new HttpClient();
           // var token = ProtectToken.Decrypt(ProtectToken.PrToken);
           // client.BaseAddress = new Uri("https://localhost:7223/");
           // client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
           // client.BaseAddress = new Uri("https://localhost:7223/");

           //// var result = await client.DeleteAsync($"api/Author/{txtId.Text}");

           // string resultContent = await result.Content.ReadAsStringAsync();



           // //MessageBox.Show(resultContent);

           // if (((int)result.StatusCode) == 200)
           // {

           //     //MessageBox.Show("Deleted!");
           //     var result2 = await client.GetAsync("api/Author");

           //     string resultContent2 = await result2.Content.ReadAsStringAsync();

           //     var info = JsonConvert.DeserializeObject<List<AuthorForm>>(resultContent2);
           //    // dataGridView1.DataSource = info;

           // }

           // else
           // {
           //     MessageBox.Show("Only the Manager can delete it!!!");
           // }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
          //  DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            //txtId.Text = row.Cells[0].Value.ToString();
            //txtName.Text = row.Cells[1].Value.ToString();
            //txtSurname.Text = row.Cells[2].Value.ToString();
            //txtAge.Text = row.Cells[3].Value.ToString();
        }

        //private void textBox2_TextChanged(object sender, EventArgs e)
        //{

        //}
    }
}
