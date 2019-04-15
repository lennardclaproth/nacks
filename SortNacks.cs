using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace nacks
{
    class SortNacks
    {
        public SortNacks(int nackId)
        {
            string sourcePath= "placeholder";
            string folderPathInports2 = "nack_inports2_prd/";
            string folderPathInports2Gateway = "nack_inports2gateway_prod/";
            string folderPathGhana = "nack_ghana/";
            string nackOmgeving = "";
            int totalFilesMoved = 0;
            int totalFoldersCreated = 0;
            int totalExceptions = 0;
            string logMessage;
            List<string> log = new List<string>();
            List<string> exLog = new List<string>();
            List<FolderCount> numFileFolder = new List<FolderCount>();

            if(nackId == 2)
            {
                sourcePath += folderPathInports2;
                nackOmgeving = "Inports2";
            }
            if(nackId == 3)
            {
                sourcePath += folderPathInports2Gateway;
                nackOmgeving = "Inports2 gateway";
            }

            log.Add("----Date of script execution " + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "----");

            foreach(var dirFile in Directory.GetFiles(sourcePath + "nog_te_categoriseren"))
            {
                string[] fileContent = File.ReadAllLines(dirFile);
                bool fileMoved;
                bool folderCreated;
                bool ex;
                
                checkFolder(formatFileName(fileContent, Path.GetFileName(dirFile)), Path.GetFileName(dirFile), out fileMoved, out folderCreated, out ex, out logMessage, numFileFolder, sourcePath);

                if(fileMoved)
                {
                    totalFilesMoved += 1;
                }
                if(folderCreated)
                {
                    totalFoldersCreated += 1;
                }
                if(ex)
                {
                    totalExceptions += 1;
                    exLog.Add(logMessage);
                }
                log.Add(logMessage);
            }
            log.Add("Files moved: " + totalFilesMoved + "; Folders created: " + totalFoldersCreated + "; Exceptions: " + totalExceptions + ";");
            File.AppendAllLines(sourcePath + "log_file/log.txt", log);
            SendEmail email = new SendEmail(numFileFolder, totalFilesMoved, totalFoldersCreated, totalExceptions, exLog, nackOmgeving, sourcePath);  
            // foreach(var item in log)
            // {
            //     Console.WriteLine(item);
            // } 
        }

        private static string formatFileName(string[] fileContent, string fileName)
        {
            foreach(string line in fileContent)
            {
                NackExceptionFormatter exceptionFormatter = new NackExceptionFormatter();

                if(line.Contains("Errordescription:"))
                {
                    string errorString = line;
                    string folderName;
                    errorString = Regex.Replace(errorString, @"\s+", " ");
                    errorString = errorString.Replace("Errordescription: ", "");

                    if(errorString.Contains("Message identifier: "))
                    {
                        errorString = exceptionFormatter.formatExceptions(1, errorString);
                    }
                    if(errorString.Contains("The phone number "))
                    {
                        errorString = exceptionFormatter.formatExceptions(2, errorString);
                    }
                    if(errorString.Contains("element is invalid"))
                    {
                        errorString = exceptionFormatter.formatExceptions(3, errorString);
                    }
                    if(errorString.Contains("specified operator"))
                    {
                        errorString = exceptionFormatter.formatExceptions(4, errorString);
                    }
                    if(errorString.Contains("Multiple Original Operators "))
                    {
                        errorString = exceptionFormatter.formatExceptions(5, errorString);
                    }
                    if(errorString.Contains("The recipient ") && errorString.Contains("the donor"))
                    {
                        errorString = exceptionFormatter.formatExceptions(6, errorString);
                    }
                    if(errorString.Contains("found for identifier:"))
                    {
                        errorString = exceptionFormatter.formatExceptions(7, errorString);
                    }
                    if(errorString.Contains("not serviced by the specified operator"))
                    {
                        errorString = exceptionFormatter.formatExceptions(8, errorString);
                    }
                    if(errorString.Contains("the porting start date"))
                    {
                        errorString = exceptionFormatter.formatExceptions(9, errorString);
                    }
                    if(errorString.Contains("The response code:"))
                    {
                        errorString = exceptionFormatter.formatExceptions(10, errorString);
                    }
                    if(errorString.Contains("The phone number:"))
                    {
                        errorString = exceptionFormatter.formatExceptions(11, errorString);
                    }
                    if(errorString.Contains("The authorisation number"))
                    {
                        errorString = exceptionFormatter.formatExceptions(12, errorString);
                    }

                    int startPos = 0;
                    int endPos = fileName.IndexOf("_");
                    folderName = fileName.Substring(startPos, endPos - startPos) + " - " + errorString;
                    return folderName;
                }
            }
            return null;
        }

        private static void checkFolder(string folderName, string fileName, out bool fileMoved, out bool folderCreated, out bool ex, out string logMessage, List<FolderCount> numFileFolder, string sourcePath)
        {
            fileMoved = false;
            folderCreated = false;
            ex = false;
            logMessage = "";

            try
            {
                if(Directory.Exists(sourcePath + "NACKS/" + folderName))
                {
                    File.Copy(sourcePath + "nog_te_categoriseren/" + fileName, sourcePath +"NACKS/" + folderName + "/" + fileName, true);
                    fileMoved = true;
                    logMessage = fileName + " Moved to existing folder: " + folderName;

                    if(!numFileFolder.Exists(x => x.folderName == folderName))
                    {
                        numFileFolder.Add(new FolderCount(1, folderName));
                    }
                    else{
                        numFileFolder.FirstOrDefault(x => x.folderName == folderName).aantal ++;
                    }
                }
                else
                {
                    Directory.CreateDirectory(sourcePath + "NACKS/" + folderName);
                    File.Copy(sourcePath + "nog_te_categoriseren/" + fileName, sourcePath + "NACKS/" + folderName + "/" + fileName, true);
                    logMessage = fileName + " Moved to new folder, folder name: " + folderName;
                    folderCreated = true;

                    if(!numFileFolder.Exists(x => x.folderName == folderName))
                    {
                        numFileFolder.Add(new FolderCount(1, folderName));
                    }
                    else{
                        numFileFolder.FirstOrDefault(x => x.folderName == folderName).aantal ++;
                    }
                }
            }
            catch(Exception e)
            {
                logMessage = e.ToString();
                ex = true;
            }
        }
    }
}