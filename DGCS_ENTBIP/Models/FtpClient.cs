using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;

namespace DGCS_ENTBIP.Models
{


    public class FtpClient
    {
        private string ftpServer;
        private string userName;
        private string password;

        public FtpClient(string server, string user, string pass)
        {
            ftpServer = server;
            userName = user;
            password = pass;
        }

        public void UploadFile(string localFilePath, string remoteFilePath)
        {
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(userName, password);

                try
                {
                    client.UploadFile(ftpServer + remoteFilePath, localFilePath);
                    Console.WriteLine("Arquivo enviado com sucesso!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }
            }
        }


        public bool TestConnection()
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpServer);
                request.Credentials = new NetworkCredential(userName, password);
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine("Conexão FTP bem-sucedida.");
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro na conexão FTP: " + e.Message);
                return false;
            }
        }
    }

}