using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestSharp;

namespace WebApplication1.Controllers
{
    public class StackoverflowController : ApiController
    {
        [HttpGet]
        [Route("api/questions")]
        public async Task<IHttpActionResult> GetQuestions()
        {
        
            var client = new RestClient("https://api.stackexchange.com/2.3");
            var request = new RestRequest("questions", Method.Get);
            request.AddParameter("order", "desc");

            request.AddParameter("sort", "activity");
            request.AddParameter("site", "stackoverflow");

            var response = await client.ExecuteAsync<QuestionResponse>(request);

            if (response.IsSuccessful)
            {
                return Ok(response.Data.Items);
            }
            else
            {
                return InternalServerError();
            }
        }
    }

    public class QuestionResponse
    {
        public List<Question> Items { get; set; }
    }

    public class Question
    {
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
    }
}