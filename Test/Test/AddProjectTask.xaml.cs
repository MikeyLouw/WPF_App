using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for AddProjectTask.xaml
    /// </summary>
    public partial class AddProjectTask : Window
    {
        public AddProjectTask()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //save task to api.
                var title = Title.Text;
                var duedate = Due_date.Text;
                var estimatedHours = Estimated_Hours.Text;


                //creeatean send to the api.
                var apiURL = ConfigurationManager.AppSettings["Task_Service"].ToString();
                var request = (HttpWebRequest)WebRequest.Create(apiURL);

                var postData = "title=" + title;
                postData += "&estimated_hours=" + estimatedHours;
                postData += "&due_date=" + duedate.Replace(@"/", "-");
                postData += "&project=" + Application.Current.Properties["Project_PK"].ToString();

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
            Title.Text = "";
            Estimated_Hours.Text = "";
            this.Close();
        }
    }
}
