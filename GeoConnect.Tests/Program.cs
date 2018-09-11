using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Atlassian.Jira;
using RestSharp.Authenticators;
using System.Net;

namespace GeoConnect.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("https://geoconnect.atlassian.net/rest/api/2");
            var request = new RestRequest("issue/", Method.POST);

            client.Authenticator = new HttpBasicAuthenticator("manimanu452@gmail.com", "Pranay@12");

            var issue = new Issue
            {
                fields =
                    new Fields
                    {
                        description = "Issue Description",
                        summary = "Issue Summary",
                        project = new Project { key = "GEOC" },
                        issuetype = new IssueType { name = "Story" }
                    }
            };

            request.AddJsonBody(issue);

            var res = client.Execute<Issue>(request);

            if (res.StatusCode == HttpStatusCode.Created)
                Console.WriteLine("Issue: {0} successfully created", res.Data.key);
            else
                Console.WriteLine(res.Content);
        }
    }

    public class Issue
    {
        public string id { get; set; }
        public string key { get; set; }
        public Fields fields { get; set; }
    }

    public class Fields
    {
        public Project project { get; set; }
        public IssueType issuetype { get; set; }
        public string summary { get; set; }
        public string description { get; set; }
    }

    public class Project
    {
        public string id { get; set; }
        public string key { get; set; }
    }

    public class IssueType
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
