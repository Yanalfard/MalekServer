using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace MalekServer3.Controllers
{
    public class uploadController : ApiController
    {
        public Task<HttpResponseMessage> uploadfile()
        {
            List<string> savefilepath = new List<string>();

            //if (!Request.Content.IsMimeMultipartContent())
            //{
            //    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            //}

            string rootpath = HttpContext.Current.Server.MapPath("~/Resources/Images/");
            var provider = new MultipartFileStreamProvider(rootpath);
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith(t =>
                {
                    if (t.IsCanceled || t.IsFaulted)
                    {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                    }
                    foreach (MultipartFileData item in provider.FileData)
                    {
                        try
                        {
                            string name = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                            // string newfilename = Guid.NewGuid() + Path.GetExtension(name);
                            //string s = Guid.NewGuid().ToString();
                            //string sv = Path.GetExtension(name);
                            string datetime = DateTime.Now.ToString().Replace('/', '-').Replace(':', '-');
                            string newfilename = "_" + Guid.NewGuid() + "_" + datetime + Path.GetExtension(name);
                            File.Move(item.LocalFileName, Path.Combine(rootpath, newfilename));
                            Uri baseuri = new Uri(Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty));
                            string fileRelativePath = "~/Resources/Images/" + newfilename;
                            Uri filefullpath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));
                            //savefilepath.Add(filefullpath.ToString());
                            savefilepath.Add(newfilename.ToString());
                        }
                        catch (Exception)

                        {
                            throw;
                        }

                    }
                    return Request.CreateResponse(HttpStatusCode.Created, savefilepath);
                });

            return task;

        }

    }
}