using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Utilities;

namespace TangentTest.DataSources
{
    class Tasks
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime Due_Date { get; set; }
        public decimal Estimated_Hours { get; set; }
        public int Project { get; set; }

        public static ObservableCollection<Tasks> SetTasks()
        {
            var Task = new ObservableCollection<Tasks>();
            try
            {              
                //create webrequest to api.
                var apiURL = ConfigurationManager.AppSettings["Task_Service"].ToString();
                var request = (HttpWebRequest)WebRequest.Create(apiURL);

                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers[HttpRequestHeader.Authorization] = "Token " + Application.Current.Properties["Token"].ToString();

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                var jsonString = JsonConvert.DeserializeObject<JArray>(responseString);


                //populate the dataset.
                int length = jsonString.Count;
                for (int x = 0; x < length; x++)
                {
                    //try parse the data.
                    int id;
                    id = 0;
                    int.TryParse(jsonString[x]["id"].ToString(), out id);

                    string title = jsonString[x]["title"].ToString();

                    DateTime due_date;
                    due_date = DateTime.MinValue;
                    DateTime.TryParse(jsonString[x]["due_date"].ToString(), out due_date);

                    decimal estimated_hours = 0;
                    decimal.TryParse(jsonString[x]["estimated_hours"].ToString(), out estimated_hours);

                    int project = 0;
                    int.TryParse(jsonString[x]["project"].ToString(), out project);

                    Task.Add(new Tasks() {ID=id, Due_Date = due_date, Estimated_Hours = estimated_hours, Project = project, Title = title });
                }
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 10);
            }
            return Task;
        }
    }
}
