using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string Calculate()
        {
            string data = HttpContext.Current.Request["request"];
            List<Subject> subjects = JsonConvert.DeserializeObject<List<Subject>>(data);
            int minMarks =  subjects.Min(y => y.marks);
            int maxMarks = subjects.Max(y => y.marks);

            Subject SubjectWithMinMarks = subjects.First(x => x.marks == minMarks);
            Subject SubjectWithMaxMarks = subjects.First(x => x.marks == maxMarks);

            decimal subjectMarks = subjects.Count();
            decimal totalMarks = subjectMarks * 100;
            decimal totalmarks = subjects.Sum(x => x.marks);
            decimal percent = (totalmarks / totalMarks) * 100;

            Result result = new Result()
            {
                MinimumSubject = SubjectWithMinMarks,
                MaximumSubject = SubjectWithMaxMarks,
                Percentage = percent
            };

            return JsonConvert.SerializeObject(result);
        }



    }
    internal class StripMethodAttribute : Attribute
    {
        private ResponseFormat json;
        private bool UseHttpGet;

        public StripMethodAttribute(ResponseFormat json, bool UseHttpGet)
        {
            this.json = json;
            this.UseHttpGet = UseHttpGet;
        }
    }
}

