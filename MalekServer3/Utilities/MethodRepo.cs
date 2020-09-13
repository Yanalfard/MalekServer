using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Web.Mvc;
using MalekServer3.Models.ViewModel;
using Newtonsoft.Json;
namespace MalekServer3.Utilities
{
    public static class MethodRepo
    {

        public static bool CheckRechapcha(FormCollection form)
        {
            string urlToPost = "https://www.google.com/recaptcha/api/siteverify";
            //< !--Site-- >
            string secretKey = "6LevZMIZAAAAAEo3AIKVdQNkbobs8eHHzxOMQNdl"; // change this
            //< !--localhost-- >
            //string secretKey = "6Ld_XsIZAAAAAL8yHVP2OmPE0LYumkEuPWDq1-rS"; // change this //
            string gRecaptchaResponse = form["g-recaptcha-response"];

            var postData = "secret=" + secretKey + "&response=" + gRecaptchaResponse;

            // send post data
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlToPost);
            request.Method = "POST";
            request.ContentLength = postData.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(postData);
            }

            // receive the response now
            string result = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }

            // validate the response from Google reCaptcha
            var captChaesponse = JsonConvert.DeserializeObject<reCaptchaResponse>(result);
            return captChaesponse.Success;
            //!!!
            //return true;
        }


    }
}