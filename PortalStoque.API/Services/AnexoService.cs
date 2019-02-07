using System;
using WinSCP;

namespace PortalStoque.API.Services
{
    public class AnexoService
    {
        public static string TransferFile(string executablepath, string filepath)
        {
            try
            {
                // Setup session options
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Scp,
                    HostName = Properties.Settings.Default.SankhyaURL.Substring(0, 11).Replace(":", ""),
                    UserName = "sportal",
                    Password = "5p0rtaL@)!&",
                    SshHostKeyFingerprint = "ssh-rsa 2048 60:0d:d3:e8:a1:f3:93:3a:4e:d5:24:89:a9:93:2e:d1"
                };

                using (Session session = new Session())
                {
                    // Connect
                    session.ExecutablePath = executablepath;
                    session.Open(sessionOptions);

                    // Upload files
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;

                    TransferOperationResult transferResult;
                    transferResult = session.PutFiles(filepath, "/home/mgeweb/modelos/Sistema/Anexos/BHBPMAtividade/", false, transferOptions);

                    // Throw on any error
                    transferResult.Check();

                    // Print results
                    foreach (TransferEventArgs transfer in transferResult.Transfers)
                    {

                    }
                }

                return "success";
            }
            catch (Exception e)
            {
                return "failed";
            }
        }

        public static bool GetFile(string executablepath, string filepath, string pChaveArquivo)
        {
            try
            {
                // Setup session options
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Scp,
                    HostName = Properties.Settings.Default.SankhyaURL.Substring(0, 11).Replace(":", ""),
                    UserName = "sportal",
                    Password = "5p0rtaL@)!&",
                    SshHostKeyFingerprint = "ssh-rsa 2048 60:0d:d3:e8:a1:f3:93:3a:4e:d5:24:89:a9:93:2e:d1"
                };

                using (Session session = new Session())
                {
                    // Connect
                    session.ExecutablePath = executablepath;
                    session.Open(sessionOptions);

                    // Upload files
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;

                    TransferOperationResult transferResult;
                    var caminho = string.Format("/home/mgeweb/modelos/Sistema/Anexos/BHBPMAtividade/{0}*", pChaveArquivo);
                    transferResult = session.GetFiles(caminho, filepath, false, transferOptions);

                    // Throw on any error
                    transferResult.Check();

                    // Print results
                    foreach (TransferEventArgs transfer in transferResult.Transfers)
                    {
                        //Console.WriteLine("Download of {0} succeeded", transfer.FileName);
                    }

                    if (transferResult.Transfers.Count > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}