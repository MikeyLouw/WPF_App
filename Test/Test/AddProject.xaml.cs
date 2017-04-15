using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Utilities;

namespace TangentTest
{
    /// <summary>
    /// Interaction logic for AddProject.xaml
    /// </summary>
    public partial class AddProject : Window
    {
        public AddProject()
        {
            InitializeComponent();

            //populate comboboxes.
            Billable.Items.Add("");
            Billable.Items.Add("true");
            Billable.Items.Add("false");

            Active.Items.Add("");
            Active.Items.Add("true");
            Active.Items.Add("false");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //save project to api.
                var Title = title.Text;
                var Description = description.Text;
                var StartDate = startdate.Text;
                var EndDate = enddate.Text;
                var isBillable = Billable.SelectedValue.ToString();
                var isActive = Active.SelectedValue.ToString();


                //creeatean send to the api.
                var apiURL = ConfigurationManager.AppSettings["Projects_Service"].ToString();
                var request = (HttpWebRequest)WebRequest.Create(apiURL);

                var postData = "title=" + Title;
                postData += "&description=" + Description;
                postData += "&start_date=" + StartDate.Replace(@"/", "-");
                postData += "&end_date=" + EndDate.Replace(@"/", "-");
                postData += "&is_billable=" + isBillable;
                postData += "&is_active=" + isActive;

                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.Headers[HttpRequestHeader.Authorization] = "Token " + Application.Current.Properties["Token"].ToString();

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                this.Close();
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 0);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            title.Text = "";
            description.Text = "";
            Billable.SelectedIndex = 0;
            Active.SelectedIndex = 0;
            this.Close();
        }

        
    }
}
