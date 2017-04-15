using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utilities;
using TangentTest.DataSources;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows.Threading;
using System.Runtime.Remoting.Messaging;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Microsoft.AspNet.Http.Extensions;
using ImranB.SystemNetHttp;
using System.Globalization;

namespace TangentTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        public static DataSources.ProjectsDBSet NewProjectsDBSet = new DataSources.ProjectsDBSet();
        private static List<Projects> rows = new List<Projects>();
        private static List<Tasks> TaskRows = new List<Tasks>();


        public MainWindow()
        {
            InitializeComponent();     
            if (Application.Current.Properties["Token"] == null)
            {
                this.Close();
                Login BackToLogin = new Login();
                BackToLogin.Show();
            }
            else
            {

            }
        }

        private void ListBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Projects_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            populateTable();
            populateTasks();
            rows.Clear();
            TaskRows.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            populateTable();
            populateTasks();
            TangentTest.DataSources.ProjectsDBSet projectsDBSet = ((TangentTest.DataSources.ProjectsDBSet)(this.FindResource("projectsDBSet")));
        }

        public void populateTable()
        {
            projectDataGrid.ItemsSource = Projects.SetProjects();
        }

        public void populateTasks()
        {
            tasksDataGrid.ItemsSource = Tasks.SetTasks();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddProject modalWindow = new AddProject();
            modalWindow.ShowDialog();
        }


        //delete project
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult ConfirmDeletion = System.Windows.MessageBox.Show("Please Confirm", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);

                if (ConfirmDeletion == MessageBoxResult.Yes)
                {
                    //execute.
                    //Projects Deletion = projectDataGrid.SelectedItems as Projects;
                    
                    for (int x = 0; x < rows.Count; x++)
                    {
                        var pk = rows[x].PK;
                        var apiURL = ConfigurationManager.AppSettings["Projects_Service"].ToString() + pk + "/";
                        var request = (HttpWebRequest)WebRequest.Create(apiURL);

                        request.Method = "DELETE";
                        request.ContentType = "application/json";
                        request.Headers[HttpRequestHeader.Authorization] = "Token " + Application.Current.Properties["Token"].ToString();

                        var response = (HttpWebResponse)request.GetResponse();
                        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    }                    

                    populateTable();
                    populateTasks();
                    rows.Clear();
                    TaskRows.Clear();
                }
                else
                {
                    //no changes
                }
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 10);
            }            
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                Projects updatetaskProject = projectDataGrid.SelectedItem as Projects;

                Projects postData = new Projects { Title = updatetaskProject.Title, Description = updatetaskProject.Description, End_Date = updatetaskProject.End_Date.ToString().Replace(@"/", "-").Substring(0, 10), Is_Active = updatetaskProject.Is_Active, Is_Billable = updatetaskProject.Is_Billable, PK = updatetaskProject.PK, Start_Date = updatetaskProject.Start_Date.ToString().Replace(@"/", "-").Substring(0, 10) };

                //string desc, start, end;
                //bool isbil, isac;
                //desc = updatetaskProject.Description;
                //start = updatetaskProject.Start_Date.ToString().Replace(@"/", "-").Substring(0, 10);
                //end = updatetaskProject.End_Date.ToString().Replace(@"/", "-").Substring(0, 10);
                //isbil = Convert.ToBoolean(updatetaskProject.Is_Billable.ToString());
                //isac = Convert.ToBoolean(updatetaskProject.Is_Active.ToString());

                //var postOBJ = "title=" + postData.Title;
                //postOBJ += "&description=" + desc;
                //postOBJ += "&start_date=" + start;
                //postOBJ += "&end_date=" + end;
                //postOBJ += "&is_Billable" + isbil;
                //postOBJ += "&is_Active" + isac;
                Dictionary<string, string> Patch = new Dictionary<string, string>();
                Patch.Add("title", postData.Title);
                Patch.Add("description", postData.Description);
                Patch.Add("start_date", postData.Start_Date);
                Patch.Add("end_date", postData.End_Date);
                Patch.Add("is_Billable", postData.Is_Billable.ToString());
                Patch.Add("is_Active", postData.Is_Active.ToString());

                var pk = updatetaskProject.PK;
                var client = new HttpClient();
                var response = await HTTPMethods.PatchAsJsonAsync(client, ConfigurationManager.AppSettings["Projects_Service"].ToString() + pk + "/", Patch);

                var content = response.Content;
                var obj = await content.ReadAsByteArrayAsync();
                var str = System.Text.Encoding.Default.GetString(obj);
           
                if (response.IsSuccessStatusCode)
                {
                    Logger.info(response.ToString(), 1);
                }
                else
                {
                    Logger.info(response.ToString(), 11);
                }

                rows.Clear();
                TaskRows.Clear();
                populateTable();
                populateTasks();
            }
            catch (Exception ex)
            {
                Logger.error(ex.Message, 0);
            }
        }        

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                Projects addtaskProject = projectDataGrid.SelectedItem as Projects;
                if (rows.Count == 1)
                {
                    Application.Current.Properties["Project_PK"] = rows[0].PK.ToString();
                    AddProjectTask NewAdd = new AddProjectTask();
                    NewAdd.ShowDialog();
                }
                else
                {
                    //no project selected.
                }
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 10);
            }            
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Projects prjRow = projectDataGrid.SelectedItem as Projects;
                rows.Add(prjRow);
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 25);
            }            
        }

        private void Select_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                Projects prjRow = projectDataGrid.SelectedItem as Projects;
                rows.Remove(prjRow);
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 26);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult ConfirmDeletion = System.Windows.MessageBox.Show("Please Confirm", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);

                if (ConfirmDeletion == MessageBoxResult.Yes)
                {
                    //execute.
                    //Projects Deletion = projectDataGrid.SelectedItems as Projects;

                    for (int x = 0; x < TaskRows.Count; x++)
                    {
                        var pk = TaskRows[x].ID;
                        var apiURL = ConfigurationManager.AppSettings["Task_Service"].ToString() + pk + "/";
                        var request = (HttpWebRequest)WebRequest.Create(apiURL);

                        request.Method = "DELETE";
                        request.ContentType = "application/json";
                        request.Headers[HttpRequestHeader.Authorization] = "Token " + Application.Current.Properties["Token"].ToString();

                        var response = (HttpWebResponse)request.GetResponse();
                        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    }

                    populateTable();
                    populateTasks();
                    rows.Clear();
                    TaskRows.Clear();
                }
                else
                {
                    //no changes
                }
            }
            catch (Exception ex)
            {
                Logger.error(ex.Message, 10);
            }
        }

        private async void Button_Click_6(object sender, RoutedEventArgs e)
        {
            try
            {
                Tasks updatetaskProject = tasksDataGrid.SelectedItem as Tasks;

                Tasks postData = new Tasks { Due_Date = updatetaskProject.Due_Date, Estimated_Hours = updatetaskProject.Estimated_Hours, ID = updatetaskProject.ID, Project = updatetaskProject.Project, Title = updatetaskProject.Title.ToString() };

                Dictionary<string, string> PatchTask = new Dictionary<string, string>();
                PatchTask.Add("due_date", postData.Due_Date.ToString().Replace(@"/", "-").Substring(0, 10));
                PatchTask.Add("estimated_hours", postData.Estimated_Hours.ToString());
                PatchTask.Add("id", postData.ID.ToString());
                PatchTask.Add("title", postData.Title.ToString());
                PatchTask.Add("project", postData.Project.ToString());


                var pk = updatetaskProject.ID;
                var client = new HttpClient();
                var response = await HTTPMethods.PatchAsJsonAsync(client, ConfigurationManager.AppSettings["Task_Service"].ToString() + pk + "/", PatchTask);

                var content = response.Content;
                var obj = await content.ReadAsByteArrayAsync();
                var str = System.Text.Encoding.Default.GetString(obj);

                if (response.IsSuccessStatusCode)
                {
                    Logger.info(response.ToString(), 1);
                }
                else
                {
                    Logger.info(response.ToString(), 11);
                }

                rows.Clear();
                TaskRows.Clear();
                populateTable();
                populateTasks();
            }
            catch (Exception ex)
            {
                Logger.error(ex.Message, 0);
            }
        }

        private void Select_Unchecked_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Tasks tskRow = tasksDataGrid.SelectedItem as Tasks;
                TaskRows.Remove(tskRow);

                var inte = TaskRows.Count();
            }
            catch (Exception ex)
            {
                Logger.error(ex.Message, 25);
            }
        }

        private void Select_Checked_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Tasks tskRow = tasksDataGrid.SelectedItem as Tasks;
                TaskRows.Add(tskRow);
                var inte = TaskRows.Count();
            }
            catch (Exception ex)
            {
                Logger.error(ex.Message, 26);
            }
        }
    }

    public static class HTTPMethods
    {
        public static Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient client, string requestUri, T value)
        {
            try
            {
                var content = new ObjectContent<T>(value, new JsonMediaTypeFormatter());
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri) { Content = content };
                //add authorization header.
                //request.Content = new ObjectContent<T>(value, new JsonMediaTypeFormatter());
                request.Content.Headers.Add("Contant-Type", "application/x-www-form-urlencoded");
                request.Headers.Add("Authorization", Application.Current.Properties["Token"].ToString());

                return client.SendAsync(request);
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 10);
            }
            return null;
        }
    }
}
