using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Configuration;
using System.Net;
using System.IO;
using Utilities;

namespace TangentTest
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var password = txtPassword.Password;
                var username = txtUserName.Text;

                //create webrequest to api.
                var apiURL = ConfigurationManager.AppSettings["User_Service"].ToString();
                var request = (HttpWebRequest)WebRequest.Create(apiURL);

                var postData = "username=" + username;
                postData += "&password=" + password;

                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                var jsonString = Newtonsoft.Json.Linq.JObject.Parse(responseString);

                //save token to session variable.
                if (jsonString["token"].ToString() != null)
                {
                    Application.Current.Properties["Token"] = jsonString["token"].ToString();

                    //if success open main.
                    try
                    {
                        MainWindow OpenMain = new MainWindow();
                        OpenMain.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    //write to logger.

                    //give failed login to user.

                }
            }
            catch (Exception ex)
            {
                //wron password color.
                SolidColorBrush MyColor = new SolidColorBrush();
                MyColor.Color = Colors.Red;
                
                lblpassword.Foreground = MyColor;
                lblusername.Foreground = MyColor;

                Logger.error(ex.Message, 1);
            }
        }
    }
}
