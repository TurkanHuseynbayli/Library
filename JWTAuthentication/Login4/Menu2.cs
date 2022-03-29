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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login4
{
    public partial class Menu2 : Form
    {
        public Menu2()
        {
            InitializeComponent();
            dataGridView1.CellClick += DataGridView1_CellClick;
            dataGridView2.CellClick += dataGridView2_CellClick;
            dataGridView3.CellClick += dataGridView3_CellClick;
        }
        AuthorForm authorForm;
        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                var id = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                var FullNames = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
               // var Surnames = dataGridView1.Rows[e.RowIndex].Cells[2].Value;
                var Ages = dataGridView1.Rows[e.RowIndex].Cells[2].Value;

                authorForm = new AuthorForm()
                {
                    Id = (int)id,
                    Name = (string)FullNames,
                   // Surname = (string)Surnames,
                    Age = (int)Ages,
                };

                var json = JsonConvert.SerializeObject(authorForm);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var token = ProtectToken.Decrypt(ProtectToken.PrToken);
                client.BaseAddress = new Uri("https://localhost:7223/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.BaseAddress = new Uri("https://localhost:7223/");

                var result = await client.PutAsync($"api/Author/{id}", content);
                string resultContent = await result.Content.ReadAsStringAsync();

              
                if (((int)result.StatusCode) == 200)
                {
                    await LoadDataAsync();

                    // MessageBox.Show("Updated!");

                    //var result2 = await client.GetAsync("api/Author");

                    //string resultContent2 = await result2.Content.ReadAsStringAsync();

                    //var info = JsonConvert.DeserializeObject<List<AuthorForm>>(resultContent2);
                    //dataGridView1.DataSource = info;
                }

                else
                {

                    MessageBox.Show("Only the Manager can update it!!!");
                }

            }

            if (e.ColumnIndex == 4)
            {
                var id = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                HttpClient client = new HttpClient();
                var token = ProtectToken.Decrypt(ProtectToken.PrToken);
                client.BaseAddress = new Uri("https://localhost:7223/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.BaseAddress = new Uri("https://localhost:7223/");

                var result = await client.DeleteAsync($"api/Author/{id}");
                string resultContent = await result.Content.ReadAsStringAsync();
               
                if (((int)result.StatusCode) == 200)
                {
                    await LoadDataAsync();
                }

                else
                {
                    MessageBox.Show("Only the Manager can delete it!!!");
                }

            }

        }

        private async void Menu2_Load(object sender, EventArgs e)
        {
            BookDGV();
            dataGridViewButtonColumn1.Visible = false;
            dataGridViewButtonColumn2.Visible = false;
            btnAdd.Visible = false;
            await LoadDataAsync();
            await LoadDataAsync2();
            await LoadDataAsync3();
        }

        private async Task LoadDataAsync()
        {
            HttpClient client = new HttpClient();
            var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            client.BaseAddress = new Uri("https://localhost:7223/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var result = await client.GetAsync("api/Author");
            string resultContent = await result.Content.ReadAsStringAsync();
            var info = JsonConvert.DeserializeObject<List<AuthorForm>>(resultContent);

            dataGridView1.DataSource = info;
            this.dataGridView1.AllowUserToAddRows = false;

            //foreach (var item in info)
            //{
            //    toolStripComboBox1.Items.Add(item.Name);
            //    //  comboBox1.Items.Add(item.Name);
            //}
        }

        List<BookDataSource> bookList;
        private async Task LoadDataAsync2()
        {
            HttpClient client = new HttpClient();
            var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            client.BaseAddress = new Uri("https://localhost:7223/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var result = await client.GetAsync("api/Book");
            string resultContent = await result.Content.ReadAsStringAsync();
            var info = JsonConvert.DeserializeObject<List<BookForm>>(resultContent);

            bookList = new List<BookDataSource>();

            foreach (var item in info)
            {
                var bookDataSource = new BookDataSource
                {
                    Id = item.Id,
                    Name = item.Name,
                    Locations = item.Location.Locations,
                    Authors=item.Author.Name,  
                   
                };

                bookList.Add(bookDataSource);
                //comboBox1.Items.Add(item.Name);
            }


            dataGridView2.DataSource = bookList;
            this.dataGridView2.AllowUserToAddRows = false;

            

            //foreach (var item in info)
            //{
            //    toolStripComboBox1.Items.Add(item.Name);
            //    //  comboBox1.Items.Add(item.Name);
            //}
        }

        private async Task LoadDataAsync3()
        {
            HttpClient client = new HttpClient();
            var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            client.BaseAddress = new Uri("https://localhost:7223/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var result = await client.GetAsync("api/Location");
            string resultContent = await result.Content.ReadAsStringAsync();

            var info = JsonConvert.DeserializeObject<List<LocationForm>>(resultContent);

            List<BookDataSource> bookList = new List<BookDataSource>();

            foreach (var item in info)
            {
                var bookDataSource = new BookDataSource
                {
                    Id = item.Id,
                    // Name = item.Name,
                    Locations = item.Locations,
                    Description=item.Description,
                  
                };

                bookList.Add(bookDataSource);
            }

            dataGridView3.DataSource = bookList;
            this.dataGridView3.AllowUserToAddRows = false;

            //foreach (var item in info)
            //{
            //    toolStripComboBox1.Items.Add(item.Name);
            //    //  comboBox1.Items.Add(item.Name);
            //}
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var AuthorForm = new AuthorForm
            {

                //Name = txtName.Text,
                //Surname = txtSurname.Text,
                //Age = Convert.ToInt32(txtAge.Text),

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

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = info;
            }

            else
            {
                MessageBox.Show("Only the Manager can add it!!!");

            }
        }
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            //var AuthorForm = new AuthorForm
            //{
            //    //Id = Convert.ToInt32(txtId.Text),
            //    //Name = txtName.Text,
            //    //Surname = txtSurname.Text,
            //    //Age = Convert.ToInt32(txtAge.Text),
            //};

            //var json = JsonConvert.SerializeObject(AuthorForm);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            //HttpClient client = new HttpClient();
            //var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            //client.BaseAddress = new Uri("https://localhost:7223/");
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
         //   client.BaseAddress = new Uri("https://localhost:7223/");

           // var result = await client.PutAsync($"api/Author/{txtId.Text}", content);

            //string resultContent = await result.Content.ReadAsStringAsync();


            ////MessageBox.Show(resultContent);
            //if (((int)result.StatusCode) == 200)
            //{

            //    // MessageBox.Show("Updated!");

            //    var result2 = await client.GetAsync("api/Author");

            //    string resultContent2 = await result2.Content.ReadAsStringAsync();

            //    var info = JsonConvert.DeserializeObject<List<AuthorForm>>(resultContent2);
            //    dataGridView1.DataSource = info;
            //}

            //else
            //{

            //    MessageBox.Show("Only the Manager can update it!!!");
            //}
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            client.BaseAddress = new Uri("https://localhost:7223/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            client.BaseAddress = new Uri("https://localhost:7223/");

            //var result = await client.DeleteAsync($"api/Author/{txtId.Text}");

            //string resultContent = await result.Content.ReadAsStringAsync();



            ////MessageBox.Show(resultContent);

            //if (((int)result.StatusCode) == 200)
            //{

            //    //MessageBox.Show("Deleted!");
            //    var result2 = await client.GetAsync("api/Author");

            //    string resultContent2 = await result2.Content.ReadAsStringAsync();

            //    var info = JsonConvert.DeserializeObject<List<AuthorForm>>(resultContent2);
            //    dataGridView1.DataSource = info;

            //}

            //else
            //{
            //    MessageBox.Show("Only the Manager can delete it!!!");
            //}
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
           
            //txtId.Text = row.Cells[0].Value.ToString();
            //txtName.Text = row.Cells[1].Value.ToString();
            //txtSurname.Text = row.Cells[2].Value.ToString();
            //txtAge.Text = row.Cells[3].Value.ToString();
        }
        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
           // groupBox2.Visible = true;  
        }
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
           // groupBox2.Visible = true;
        }
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
        }
        
        private async void button2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Author auth = new Author();
            //    auth.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            Author auth = new Author();
            if (auth.ShowDialog() == DialogResult.Cancel)
            {
                await LoadDataAsync();
            }
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
          // MessageBox.Show("a");
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //MessageBox.Show("j");
           
        }
        private async void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {


            
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void BookDGV()
        {
            groupBox2.Visible = true;
            dataGridView2.Visible = true;
            dataGridView2.Left = 10;
            button1.Visible = false;
            button1.Left = 410;
            button1.Top = 80;
            dataGridView1.Visible = false;
            button2.Visible = false;
            dataGridView3.Visible = false;
            btnAdd.Visible = false;
           
        }
        private void sHOWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            client.BaseAddress = new Uri("https://localhost:7223/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            var jti = tokenS.Claims.Where(x => x.Type == @"http://schemas.microsoft.com/ws/2008/06/identity/claims/role").First().Value;

            if (jti == "Admin")
            {
                groupBox2.Visible = true;
                dataGridView2.Visible = true;
                dataGridView2.Left = 10;
                button1.Visible = true;
                button1.Left = 410;
                button1.Top = 60;
                dataGridView1.Visible = false;
                button2.Visible = false;
                dataGridView3.Visible = false;
                btnAdd.Visible = false;
                dataGridView2.DataSource = bookList;
                dataGridViewButtonColumn1.Visible = true;
                dataGridViewButtonColumn2.Visible = true;
            }
            else
            {
                groupBox2.Visible = false;
                dataGridView2.Visible = false;
                dataGridView2.Left = 10;
                button1.Visible = true;
                button1.Left = 410;
                button1.Top = 80;
                dataGridView1.Visible = false;
                button2.Visible = false;
                dataGridView3.Visible = false;
                btnAdd.Visible = false;
                //dataGridView2.DataSource = bookList;
                //dataGridViewButtonColumn1.Visible = true;
                //dataGridViewButtonColumn2.Visible = true;
            }

        }

        private void sHOWToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            client.BaseAddress = new Uri("https://localhost:7223/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            var jti = tokenS.Claims.Where(x => x.Type == @"http://schemas.microsoft.com/ws/2008/06/identity/claims/role").First().Value;

            if (jti == "Manager")
            {
                groupBox2.Visible = true;
                dataGridView1.Visible = true;
                button2.Visible = true;
                button2.Left = 550;
                dataGridView2.Visible = false;
                button1.Visible = false;
                dataGridView3.Visible = false;
                btnAdd.Visible = false;
            }
            else
            {
                groupBox2.Visible = false;
                dataGridView1.Visible = false;
                button2.Visible = false;
                button2.Left = 550;
                dataGridView2.Visible = false;
                button1.Visible = false;
                dataGridView3.Visible = false;
                btnAdd.Visible = false;
            }

        }

        private async void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           if (e.ColumnIndex == 4)
            {
                var id = dataGridView2.Rows[e.RowIndex].Cells[0].Value;
                var Names = dataGridView2.Rows[e.RowIndex].Cells[1].Value;
                var Locations = dataGridView2.Rows[e.RowIndex].Cells[2].Value;
                var Authors = dataGridView2.Rows[e.RowIndex].Cells[3].Value;
                var item = new
                {
                    Id = (int)id,
                    Name = (string)Names,
                    LocationName =(string) Locations,
                    AuthorName = (string) Authors,
                    
                };

                //BookForm BookForm = new BookForm()
                //{
                //    Id = (int)id,
                //    Name = (string)Names,


                //};

                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var token = ProtectToken.Decrypt(ProtectToken.PrToken);
                client.BaseAddress = new Uri("https://localhost:7223/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.BaseAddress = new Uri("https://localhost:7223/");

                var result = await client.PutAsync($"api/Book/{id}", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                
                //MessageBox.Show(resultContent);
                if (((int)result.StatusCode) == 200)
                {
                    await LoadDataAsync2();
                }

                else
                {
                    MessageBox.Show("Only the Admin can update it!!!");
                }

            }
           
           if (e.ColumnIndex == 5)
            {
                var id = dataGridView2.Rows[e.RowIndex].Cells[0].Value;

                HttpClient client = new HttpClient();
                var token = ProtectToken.Decrypt(ProtectToken.PrToken);
                client.BaseAddress = new Uri("https://localhost:7223/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.BaseAddress = new Uri("https://localhost:7223/");

                var result = await client.DeleteAsync($"api/Book/{id}");
                string resultContent = await result.Content.ReadAsStringAsync();

                //MessageBox.Show(resultContent);
               if (((int)result.StatusCode) == 200)
                {
                    await LoadDataAsync2();
                }

                //else
                //{

                //    MessageBox.Show("Only the Admin can delete it!!!");
                //}

            }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            Book book = new Book();
            if (book.ShowDialog() == DialogResult.Cancel)
            {
                await LoadDataAsync2();
            }
        }

        private void sHOWToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            client.BaseAddress = new Uri("https://localhost:7223/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            var jti = tokenS.Claims.Where(x => x.Type == @"http://schemas.microsoft.com/ws/2008/06/identity/claims/role").First().Value;

            if (jti == "Admin")
            {
                groupBox2.Visible = true;
                dataGridView1.Visible = false;
                button2.Visible = false;
                dataGridView2.Visible = false;
                button1.Visible = false;
                dataGridView3.Visible = true;
                dataGridView3.Top = 25;
                btnAdd.Visible = true;
                btnAdd.Top = 65;
            }
            else
            {
                groupBox2.Visible = false;
                dataGridView1.Visible = false;
                button2.Visible = false;
                dataGridView2.Visible = false;
                button1.Visible = false;
                dataGridView3.Visible = true;
                dataGridView3.Top = 25;
                btnAdd.Visible = false;
            }
        }

        private void bOOKSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private async void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                var id = dataGridView3.Rows[e.RowIndex].Cells[0].Value;
                var Locations = dataGridView3.Rows[e.RowIndex].Cells[1].Value;
                var Description = dataGridView3.Rows[e.RowIndex].Cells[2].Value;
                var item = new
                {
                    Id = (int)id,
                    Locations = Locations,
                    Description = Description,  
                };

                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var token = ProtectToken.Decrypt(ProtectToken.PrToken);
                client.BaseAddress = new Uri("https://localhost:7223/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.BaseAddress = new Uri("https://localhost:7223/");

                var result = await client.PutAsync($"api/Location/{id}", content);
                string resultContent = await result.Content.ReadAsStringAsync();

                if (((int)result.StatusCode) == 200)
                {
                    await LoadDataAsync3();
                }

                else
                {
                    MessageBox.Show("Only the Admin can update it!!!");
                }

            }

            if (e.ColumnIndex == 4)
            {
                var id = dataGridView3.Rows[e.RowIndex].Cells[0].Value;

                HttpClient client = new HttpClient();
                var token = ProtectToken.Decrypt(ProtectToken.PrToken);
                client.BaseAddress = new Uri("https://localhost:7223/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.BaseAddress = new Uri("https://localhost:7223/");

                var result = await client.DeleteAsync($"api/Location/{id}");
                string resultContent = await result.Content.ReadAsStringAsync();

                //MessageBox.Show(resultContent);
                if (((int)result.StatusCode) == 200)
                {
                    await LoadDataAsync3();

                }

                //else
                //{

                //    MessageBox.Show("Only the Admin can delete it!!!");
                //}

            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            Location location = new Location();
            if (location.ShowDialog() == DialogResult.Cancel)
            {
                await LoadDataAsync3();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            string result = ((ComboBox)sender).SelectedItem.ToString();
          
            dataGridView2.DataSource = bookList.Where(x=>x.Name == result).ToList();
            BookDGV();
            dataGridViewButtonColumn1.Visible = false;
            dataGridViewButtonColumn2.Visible = false;

        }
       
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
          //  string result=txtSearch.Text;
            if(e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    dataGridView2.DataSource = bookList.Where(x => x.Name.ToLower().Contains(txtSearch.Text.ToLower()) && x.Authors.ToLower().Contains(txtAuthorSearch.Text.ToLower()) && x.Locations.ToLower().Contains(txtLocations.Text.ToLower())).ToList();
                    BookDGV();
                    dataGridViewButtonColumn1.Visible = false;
                    dataGridViewButtonColumn2.Visible = false;
                }
                
                else
                {
                  dataGridView2.Visible = false;
                  MessageBox.Show("No Records Found", "Search Result", MessageBoxButtons.OK,
                     MessageBoxIcon.Exclamation);
                }
            }
            
        }
          
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    var result = bookList.Where(x => x.Authors.ToLower().Contains(txtAuthorSearch.Text.ToLower()) && x.Name.ToLower().Contains(txtSearch.Text.ToLower()) && x.Locations.ToLower().Contains(txtLocations.Text.ToLower())).ToList();
                    dataGridView2.DataSource = result;
                    BookDGV();
                    dataGridViewButtonColumn1.Visible = false;
                    dataGridViewButtonColumn2.Visible = false;
                }
                else
                {
                    dataGridView2.Visible = false;
                    MessageBox.Show("No Records Found", "Search Result", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                }
            }
        }

        private void txtLocations_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtLocations.Text))
                {
                    var result = bookList.Where(x => x.Locations.ToLower().Contains(txtLocations.Text.ToLower()) && x.Authors.ToLower().Contains(txtAuthorSearch.Text.ToLower()) && x.Name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                    dataGridView2.DataSource = result;
                    BookDGV();
                    dataGridViewButtonColumn1.Visible = false;
                    dataGridViewButtonColumn2.Visible = false;
                }
                
                else
                {
                    dataGridView2.Visible = false;
                    MessageBox.Show("No Records Found", "Search Result", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                }
            }
        }

        private void txtLocations_TextChanged(object sender, EventArgs e)
        {

        }

        //private void txtAuthorSearch_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}
    }
}
