using System;
using WinSCP;
using System.IO;
using System.Text;

namespace nacks
{

    class FtpConnection
    {
        private string localPath = "C:/temp/myTest.txt";
        // Set up session options
        SessionOptions sessionOptions = new SessionOptions
        {
            Protocol = Protocol.Sftp,
            HostName = "placeholder",
            UserName = "placeholder",
            Password = "placeholder",
            SshHostKeyFingerprint = "placeholder",
            PortNumber = 22,
            
        };

        

        public void downloadNacks()
        {
            try
            {
                // using (FileStream fs = File.Create(localPath))
                // {
                //     Byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                //     // Add some information to the file.
                //     fs.Write(info, 0, info.Length);
                // }

                // // Open the stream and read it back.
                // using (StreamReader sr = File.OpenText(localPath))
                // {
                //     string s = "";
                //     while ((s = sr.ReadLine()) != null)
                //     {
                //         Console.WriteLine(s);
                //     }
                // }

                sessionOptions.AddRawSettings("FtpUseMlsd", "1");

                using (Session session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;
                    // transferOptions.FilePermissions = null;
                    transferOptions.PreserveTimestamp = false;
                    // transferOptions.ResumeSupport.State = TransferResumeSupportState.Off;

                    // Your code
                    TransferOperationResult transfer;
                    transfer = session.GetFiles("placeholder", localPath, false, transferOptions);
                    transfer.Check();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}