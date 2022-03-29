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
    public partial class Location : Form
    {
        public Location()
        {
            InitializeComponent();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var item = new
            {
                locations = txtLocations.Text,
                Description=txtDescription.Text,

                //LocationName2 = txtLocation.Text,
              //  LocationName = comboBox1.Text,
            };

            

            var json = JsonConvert.SerializeObject(item);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var token = ProtectToken.Decrypt(ProtectToken.PrToken);
            client.BaseAddress = new Uri("https://localhost:7223/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            client.BaseAddress = new Uri("https://localhost:7223/");
            var result = await client.PostAsync("api/Location", content);
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
                var result2 = await client.GetAsync("api/Location");
                string resultContent2 = await result2.Content.ReadAsStringAsync();
                var info = JsonConvert.DeserializeObject<List<LocationForm>>(resultContent2);

                //dataGridView1.DataSource = null;
                //dataGridView1.DataSource = info;
            }

            else
            {
                MessageBox.Show("Only the Admin can add it!!!");

            }
        }
    }
}
