using _4RTools.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace _4RTools.Utils
{
    public static class ClientUpdater
    {
        public static void StartUpdate()
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
            }
        }

        private static string LoadResourceServerFile()
        {
            return Resources._4RTools.ETCResource.supported_servers;
        }

        private static void LoadServers(List<ClientDTO> clients)
        {
            foreach (ClientDTO clientDTO in clients)
            {
                try
                {
                    ClientListSingleton.AddClient(new Client(clientDTO));
                }
                catch { }
            }
        }
    }
}
