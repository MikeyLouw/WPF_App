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
    class Projects
    {
        public int PK { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public bool Is_Billable { get; set; }
        public bool Is_Active { get; set; }

        public static ObservableCollection<Projects> SetProjects()
        {
            var Project = new ObservableCollection<Projects>();
            try
            {
                //create webrequest to api.
                var apiURL = ConfigurationManager.AppSettings["Projects_Service"].ToString();
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
                    int pk;
                    pk = 0;
                    int.TryParse(jsonString[x]["pk"].ToString(), out pk);

                    DateTime start_date;
                    start_date = DateTime.MinValue;
                    DateTime.TryParse(jsonString[x]["start_date"].ToString(), out start_date);

                    string end_dateNUll;
                    DateTime end_date;
                    end_date = DateTime.MinValue;
                    DateTime.TryParse(jsonString[x]["end_date"].ToString(), out end_date);
                    if (end_date == DateTime.MinValue)
                    {
                        end_dateNUll = "(None)";
                    }
                    else
                    {
                        end_dateNUll = end_date.ToString();
                    }

                    bool is_billable, is_active;
                    is_active = false;
                    is_billable = false;
                    bool.TryParse(jsonString[x]["is_billable"].ToString(), out is_billable);
                    bool.TryParse(jsonString[x]["is_active"].ToString(), out is_active);

                    Project.Add(new Projects() { PK = pk, Title = jsonString[x]["title"].ToString(), Description = jsonString[x]["description"].ToString(), Start_Date = start_date.ToString(), End_Date = end_date.ToString(), Is_Active = is_active, Is_Billable = is_billable });
                }
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 10);
            }
            return Project;
        }
    }
}
