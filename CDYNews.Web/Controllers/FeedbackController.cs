using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using CDYNews.Data.Infrastructure;
using CDYNews.Data.Repositories;
using CDYNews.Model.Models;
using CDYNews.Service;
using CDYNews.Web.Infrastructure.Core;
using CDYNews.Web.Infrastructure.Extensions;
using CDYNews.Web.Models;
using CDYNews.Data;

namespace CDYNews.Web.Controllers
{
    public class FeedbackController : ApiController
    {
        // GET: api/Feedback
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Feedback/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Feedback
        public void Post([FromBody]FeedbackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                Feedback feedback = new Feedback();
                feedback.UpdateFeedback(feedbackViewModel);
                feedback.Status = true;
                feedback.CreatedDate = DateTime.Now;
                CDYNewsDbContext db = new CDYNewsDbContext();
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
            }
            else
            {

            }
            
            //var responseData = Mapper.Map<Feedback, FeedbackViewModel>(feedback);
        }

        // PUT: api/Feedback/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Feedback/5
        public void Delete(int id)
        {
        }
    }
}
