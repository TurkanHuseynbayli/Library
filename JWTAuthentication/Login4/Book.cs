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
    public partial class Book : Form
    {
        public Book()
        {
            InitializeComponent();
        }
        Location location= new Location();

        private async Task LoadDataAsync()
        {
            HttpClient client = new HttpClient();
            var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            client.BaseAddress = new Uri("https://localhost:7223/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var result = await client.GetAsync("api/Author");
            string resultContent = await result.Content.ReadAsStringAsync();
            var info = JsonConvert.DeserializeObject<List<AuthorForm>>(resultContent);
            //   comboBox2.Items.Add(info);
            //   dataGridView1.DataSource = info;
            //  this.dataGridView1.AllowUserToAddRows = false;

            foreach (var item in info)
            {
               
                comboBox2.Items.Add(item.Name);
            }
        }


        private async Task LoadDataAsync2()
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
                    Description = item.Description,

                };

                bookList.Add(bookDataSource);
            }

            //dataGridView3.DataSource = bookList;
            //this.dataGridView3.AllowUserToAddRows = false;

            foreach (var item in info)
            {
                
                 comboBox1.Items.Add(item.Locations);
            }
        }
        private async void Book_Load(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            client.BaseAddress = new Uri("https://localhost:7223/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var result = await client.GetAsync("api/Book");
            string resultContent = await result.Content.ReadAsStringAsync();
            var info = JsonConvert.DeserializeObject<List<BookForm>>(resultContent);
     
             List<BookDataSource> bookList = new List<BookDataSource>();
            
            foreach (var item in info)
            {

               var  bookDataSource = new BookDataSource
                {
                    Id = item.Id,
                    //Name = item.Name,
                //    Locations = item.Location.Locations,
                   // Authors = item.Author.Name,

                };
                
                bookList.Add(bookDataSource);

            //    comboBox1.Items.Add(item.Location.Locations);
            //    comboBox2.Items.Add(item.Author.Name);
            }
            LoadDataAsync();
            LoadDataAsync2();
            //comboBox2.ValueMember = "Id";
            //comboBox2.DisplayMember = "FullName";
            //comboBox2.DataSource = info2;
            //foreach (var item in info2)
            //{
            //    var testBookDataSource = new TestBookDataSource
            //    {
            //        Id = item.Id,
            //        // Name = item.Name,
            //       // Locations = item.Locations,
            //      Name=item.Name

            //    };


            //    bookList.Add(testBookDataSource);


            //    comboBox2.Items.Add(Authors);
            //}


        }
        //public Location Locationss { get; set; }
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            //List<BookForm> bookList = new List<BookForm>();
            //var test = new TestBookDataSource
            //{
            
            //    // Name = txtName.Text,
            //   Locations = txtLocation.Text,
                                
            //};
            
            //   Locationss = test.Locations;
            ////List<TestBookDataSource> bookList = new List<TestBookDataSource>();
            //foreach (var item in bookList)
            //{
            //    item.Locations = txtLocation.Text;
            //}
            //var BookForm = new BookForm
            //{
            //    Name = txtName.Text,
            //    //Location=Locationss
            // //   Location=txtLocation.Location

            //};
            //bookList.Add(BookForm);
            //bookList.Add(test);


            var item = new
            {
                Name = txtName.Text,
               //LocationName2 = txtLocation.Text,
                LocationName = comboBox1.Text,
                AuthorName=comboBox2.Text,
                
            };

         

            var json = JsonConvert.SerializeObject(item);
          
            var content = new StringContent(json, Encoding.UTF8, "application/json");
         
            HttpClient client = new HttpClient();
            
            var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            client.BaseAddress = new Uri("https://localhost:7223/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            client.BaseAddress = new Uri("https://localhost:7223/");
            var result = await client.PostAsync("api/Book", content);
          //  var result3 = await client.PostAsync("api/Location", content);

            //var result3 = await client.PostAsync("api/Location", content);
            //string resultContent3 = await result3.Content.ReadAsStringAsync();
            //var info1 = JsonConvert.DeserializeObject<List<LocationForm>>(resultContent3);

            //List<TestBookDataSource> bookList = new List<TestBookDataSource>();

            //foreach (var item1 in info1)
            //{
            //    var testBookDataSource = new TestBookDataSource
            //    {
            //        Id = item1.Id,
            //        // Name = item.Name,
            //        Locations = item1.Locations,

            //    };


            //    bookList.Add(testBookDataSource);

               
            //}


            string resultContent = await result.Content.ReadAsStringAsync();


            if (((int)result.StatusCode) == 200)
            {
                var result2 = await client.GetAsync("api/Book");
                string resultContent2 = await result2.Content.ReadAsStringAsync();
                var info = JsonConvert.DeserializeObject<List<BookForm>>(resultContent2);

                //dataGridView1.DataSource = null;
                //dataGridView1.DataSource = info;
            }

            else
            {
                MessageBox.Show("Only the Admin can add it!!!");

            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
          //  DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            //txtId.Text = row.Cells[0].Value.ToString();
            //txtName.Text = row.Cells[1].Value.ToString();
           // AuthorForm form = new AuthorForm();
            // txtAuthor.Text = row.Cells[form.Name].Value.ToString();
             
           // txtAuthor.Text = row.Cells[2].Value.ToString();
           
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            //var BookForm = new BookForm
            //{
            //    Id = Convert.ToInt32(txtId.Text),
            //    Name = txtName.Text,
            //   // Author = txtAuthor.Text,
            //};

            //var json = JsonConvert.SerializeObject(BookForm);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            //HttpClient client = new HttpClient();
            //var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            //client.BaseAddress = new Uri("https://localhost:7223/");
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //client.BaseAddress = new Uri("https://localhost:7223/");
            
            //var result = await client.PutAsync($"api/Book/{txtId.Text}", content);

            //string resultContent = await result.Content.ReadAsStringAsync();


            ////MessageBox.Show(resultContent);
            //if (((int)result.StatusCode) == 200)
            //{
            //    // MessageBox.Show("Updated!");

            //    var result2 = await client.GetAsync("api/Book");
            //    string resultContent2 = await result2.Content.ReadAsStringAsync();
            //    var info = JsonConvert.DeserializeObject<List<BookForm>>(resultContent2);
            //   // dataGridView1.DataSource = info;
            //}

            //else
            //{
            //    MessageBox.Show("Only the Admin can update it!!!");
            //}
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            //HttpClient client = new HttpClient();
            //var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            //client.BaseAddress = new Uri("https://localhost:7223/");
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //client.BaseAddress = new Uri("https://localhost:7223/");

            //var result = await client.DeleteAsync($"api/Book/{txtId.Text}");

            //string resultContent = await result.Content.ReadAsStringAsync();


            //Response response = new Response();
            //if (resultContent.Contains("false"))
            //{

            //    response = JsonConvert.DeserializeObject<Response>(resultContent);
            //    var errorMessage = $"{response.Message}";
            //    MessageBox.Show(errorMessage);

            //}

            //MessageBox.Show(resultContent);

            //if (((int)result.StatusCode) == 200)
            //{

            //    //MessageBox.Show("Deleted!");
            //    var result2 = await client.GetAsync("api/Book");
            //    string resultContent2 = await result2.Content.ReadAsStringAsync();

            //    var info = JsonConvert.DeserializeObject<List<BookForm>>(resultContent2);
            //  //  dataGridView1.DataSource = info;
                
            //}

            //else
            //{
            //    MessageBox.Show("Only the Admin can delete it!!!");
            //}
        }

        private  void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            //AuthorForm form = (AuthorForm)comboBox1.SelectedItem;
            // BookForm form = (BookForm)comboBox1.SelectedItem;

            //Author author =  new Author();
            ////AuthorForm form = (AuthorForm)comboBox1.SelectedItem;

            //author.Show();

            //txtId.Text = form.Id.ToString();
            //txtName.Text = form.Name.ToString();




            //txtId.Text = form.Id.ToString();
            //txtName.Text = form.Name.ToString();
            //txtAuthor.Text = form.Author.ToString();

        }

        private async void comboBox1_Click(object sender, EventArgs e)
        {
            
            //Author author = new Author();
            ////AuthorForm form = (AuthorForm)comboBox1.SelectedItem;

            //author.Show();

            //  //var AuthorForm = new AuthorForm
            //  //{
            //  //    Id = Convert.ToInt32(txtId.Text),
            //  //    Name = txtName.Text,

            //  //};
            //  HttpClient client = new HttpClient();

            //  var result2 = await client.GetAsync("api/Author");


            //  string resultContent2 = await result2.Content.ReadAsStringAsync();


            //  var info2 = JsonConvert.DeserializeObject<List<AuthorForm>>(resultContent2);



            //  //  this.dataGridView1.AllowUserToAddRows = false;

            //  //AuthorForm form = new AuthorForm();
            //  //ComboBox combo=new ComboBox();
            //  ////  combo.DropDownStyle =  DropDownStyle.DropDownList;
            //  //combo.SelectedText = "Name";
            //  //combo.ValueMember = "id";
            //  //combo.DisplayMember = "Name";
            //  // comboBox1 = new ComboBox();

            //  comboBox1.ValueMember = "Id";
            //  comboBox1.DisplayMember = "Name";
            //  comboBox1.DataSource = info2;

            ////  comboBox1.Items.Add(AuthorForm.Surname);
        }

        private void comboBox1_MouseEnter(object sender, EventArgs e)
        {
            //Author author = new Author();


            //author.Show();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            //Author author = new Author();


            //author.Show();
        }

        private void comboBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //Author author = new Author();

           
            //    author.Show();
            
           
            //AuthorForm form = (AuthorForm)comboBox1.SelectedItem;

                  
        }

        private void comboBox1_DataSourceChanged(object sender, EventArgs e)
        {
            //Author author = new Author();
            ////AuthorForm form = (AuthorForm)comboBox1.SelectedItem;

            //author.Show();
        }

        private void comboBox1_DisplayMemberChanged(object sender, EventArgs e)
        {
            //Author author = new Author();
            ////AuthorForm form = (AuthorForm)comboBox1.SelectedItem;

            //author.Show();
        }

        private void comboBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Author author = new Author();

            author.Show();
        }

        private void comboBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Author author = new Author();


            //author.Show();
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //Author author = new Author();


            //author.Show();
        }

        private void comboBox1_MouseLeave(object sender, EventArgs e)
        {
            //Author author = new Author();


            //author.Show();
        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //AuthorForm form = (AuthorForm)comboBox1.SelectedItem;
            //BookForm form = (BookForm)comboBox1.SelectedItem;
            //LocationForm locationForm = (LocationForm)comboBox1.SelectedItem;
            //Author author = new Author();
            ////AuthorForm form = (AuthorForm)comboBox1.SelectedItem;

            //author.Show();

            //txtId.Text = form.Id.ToString();
            //txtName.Text = form.Name.ToString();




            //txtId.Text = form.Id.ToString();
            //txtName.Text = form.Name.ToString();
            //txtAuthor.Text = form.Author.ToString();
        }

        private void comboBox2_MouseDown(object sender, MouseEventArgs e)
        {
            //Author author = new Author();

            //author.Show();
        }
    }
}
