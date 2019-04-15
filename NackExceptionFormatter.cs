//////////////////////////////////////////////////////////////////////
//                                                                  //
//                                                                  //
//  Methode voor het uitvoeren van het formateren op alle excepties.//
//                                                                  //
//  VARIABLE UITLEG:                                                //
//  startPos is de startpositie van de text die een exceptie        //
//  aangeeft.                                                       //
//  endPos is de eindpositie van de text die een exceptie aangeeft  //
//  result is de subtext die weggehaald dient te worden.            //
//                                                                  //
//  Het format van de variabeles startpos, endpos en result is:     //
//  variableName + caseNummer                                       //
//                                                                  //
//                                                                  //
//////////////////////////////////////////////////////////////////////  

using System;

namespace nacks
{
    public class NackExceptionFormatter
    {
        public NackExceptionFormatter()
        {

        }

        public string formatExceptions(int id, string errorString)
        {
            switch(id)
            {
                case 1:
                    int startPos = errorString.IndexOf("Message identifier: ");
                    int endPos = errorString.IndexOf(" is");
                    string result = errorString.Substring(startPos, endPos - startPos);
                    errorString = errorString.Replace(result, "Message identifier");
                    return errorString;
                case 2:
                    int startPos2 = errorString.IndexOf("The phone number");
                    int endPosIs = errorString.IndexOf("is");
                    int endPosWas = errorString.IndexOf("was");

                    if(startPos2 < endPosIs)
                    {
                        string result2 = errorString.Substring(startPos2, endPosIs - startPos2);
                        errorString = errorString.Replace(result2, "The phone number ");
                    }
                    else if(startPos2 < endPosWas)
                    {
                        string result2 = errorString.Substring(startPos2, endPosWas - startPos2);
                        errorString = errorString.Replace(result2, "The phone number ");
                    }
                    return errorString;
                case 3:
                    int startPos3 = errorString.IndexOf("is invalid");
                    int endPos3 = errorString.Length;
                    string result3 = errorString.Substring(startPos3, endPos3 - startPos3);
                    errorString = errorString.Replace(result3, "is invalid");
                    return errorString;
                case 4:
                    int startPos4 = errorString.IndexOf("operator");
                    int endPos4 = errorString.Length;
                    string result4 = errorString.Substring(startPos4, endPos4 - startPos4);
                    return errorString;
                case 5:
                    int startPos5 = errorString.IndexOf("Original Operators ");
                    int endPos5 = errorString.IndexOf(" found in");
                    string result5 = errorString.Substring(startPos5, endPos5 - startPos5);
                    errorString = errorString.Replace(result5, "Original Operators");
                    return errorString;
                case 6:
                    int startPos6 = errorString.IndexOf("The recipient");
                    int endPos6 = errorString.IndexOf("needs to differ");
                    string result6 = errorString.Substring(startPos6, endPos6 - startPos6);
                    errorString = errorString.Replace(result6, "The recipient");
                    startPos6 = errorString.IndexOf("from the donor");
                    endPos6 = errorString.Length;
                    result6 = errorString.Substring(startPos6, endPos6 - startPos6);
                    errorString = errorString.Replace(result6, " from the donor");
                    return errorString;
                case 7:
                    int startPos7 = errorString.IndexOf("found for identifier:");
                    int endpos7 = errorString.Length;
                    string result7 = errorString.Substring(startPos7, endpos7 - startPos7);
                    errorString = errorString.Replace(result7, "found for identifier");
                    return errorString;
                case 8:
                    int startPos8 = errorString.IndexOf("not serviced by the specified operator");
                    int endpos8 = errorString.Length;
                    string result8 = errorString.Substring(startPos8, endpos8 - startPos8);
                    errorString = errorString.Replace(result8, "not serviced by the specified operator");
                    return errorString;
                case 9:
                    int startPos9 = errorString.IndexOf("the porting start date");
                    int endpos9 = errorString.Length;
                    string result9 = errorString.Substring(startPos9, endpos9 - startPos9);
                    errorString = errorString.Replace(result9, "the porting start date");
                    return errorString;
                case 10:
                    int startPos10 = errorString.IndexOf("The response code: ");
                    int endPos10 = errorString.IndexOf("is not valid");
                    string result10 = errorString.Substring(startPos10, endPos10 - startPos10);
                    errorString = errorString.Replace(result10, "The response code 02");
                    startPos10 = errorString.IndexOf("request status code");
                    endPos10 = errorString.Length;
                    result10 = errorString.Substring(startPos10, endPos10 - startPos10);
                    errorString = errorString.Replace(result10, "request status code AREQ");
                    return errorString;
                case 11:
                    int startPos11 = errorString.IndexOf("The phone number:");
                    int endPos11 = errorString.IndexOf(" for IVR");
                    string result11 = "";
                    if(startPos11 < endPos11)
                    {
                        result11 = errorString.Substring(startPos11, endPos11 - startPos11);
                        errorString = errorString.Replace(result11, "The phone number");
                    }
                    else
                    {
                        endPos11 = errorString.IndexOf("is currently");
                        result11 = errorString.Substring(startPos11, endPos11 - startPos11);
                        errorString = errorString.Replace(result11, "The phone number");
                    }
                    return errorString;
                case 12:
                    int startPos12 = errorString.IndexOf("The authorisation number");
                    int endPos12 = errorString.IndexOf("is not part of");
                    string result12 = errorString.Substring(startPos12, endPos12 - startPos12);
                    errorString = errorString.Replace(result12, "The authorisation number");
                    return errorString;
            }

            return errorString;
        }
    }
}