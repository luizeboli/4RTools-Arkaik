using _4RTools.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace _4RTools.Forms
{
    public partial class ClientUpdaterForm : Form
    {
        private System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();

        public ClientUpdaterForm()
        {
            var requestAccepts = httpClient.DefaultRequestHeaders.Accept;
            requestAccepts.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request"); //Set the User Agent to "request"
            InitializeComponent();
            StartUpdate();
        }

        private void StartUpdate()
        {
            List<ClientDTO> clients = new List<ClientDTO>();

            try
            {
                clients.AddRange(JsonConvert.DeserializeObject<List<ClientDTO>>(LoadResourceServerFile()));
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot load supported_servers file....");
            }
            finally
            {
                LoadServers(clients);
                new Container().Show();
                Hide();
            }
        }

        private string LoadResourceServerFile()
        {
            return Resources._4RTools.ETCResource.supported_servers;
        }

        private void LoadServers(List<ClientDTO> clients)
        {
            foreach (ClientDTO clientDTO in clients)
            {
                try
                {
                    ClientListSingleton.AddClient(new Client(clientDTO));
                    pbSupportedServer.Increment(1);
                }
                catch { }

            }
        }
    }
}
