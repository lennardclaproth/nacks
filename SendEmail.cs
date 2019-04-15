using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.IO;

namespace nacks
{
    class SendEmail
    {
        public SendEmail(List<FolderCount> numFileFolder, int totalFilesMoved, int totalFoldersCreated, int totalExceptions, List<string> exLog, string nackOmgeving, string sourcePath)
        {
            SmtpClient client = new SmtpClient();
            MailMessage mail = new MailMessage("placeholder", "placeholder");
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("placeholder","placeholder");
            client.Host = "placeholder";
            client.EnableSsl = true;
            mail.IsBodyHtml = true;
            mail.Subject = "Nacks Report";
            mail.Body = 
                "LS, <br/><br/> Hierbij de dagelijkse " + nackOmgeving + " nack report mail van: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + ".<br><br><table><tr><td>Totaal aantal bestanden verplaatst: " +
                totalFilesMoved + ";</td><td>Totaal aantal mappen aangemaakt: " + 
                totalFoldersCreated + ";</td></tr></table><br/>Totaal aantal excepties: " + totalExceptions + "; <br/>"
            ;

            foreach(var ex in exLog)
            {
                mail.Body += ex + "<br/><br/>";
            }

            mail.Body += "<br/> Hieronder staat het totaal aantal bestanden verplaatst naar de specifieke map.<br/><br/><table>";

            foreach(var item in numFileFolder)
            {
                mail.Body +="<tr><td>" + item.folderName + "</td> <td>aantal: " + item.aantal + "</td></tr>"; 
            }

            mail.Body += "</table><br/><br/>PortingXS";

            try{
                client.Send(mail);
            }
            catch(Exception e)
            {
                string logMessage = e.ToString();
                File.AppendAllText(sourcePath + "log_file/log_mail.txt", "----Date of script execution " + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "----\n");
                File.AppendAllText(sourcePath + "log_file/log_mail.txt", logMessage + "\n");
            }
        }
    }
}
