using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using AutoMapper;
using CDYNews.Model.Models;
using CDYNews.Service;
using CDYNews.Web.Infrastructure.Core;
using CDYNews.Web.Infrastructure.Extensions;
using CDYNews.Web.Models;

namespace CDYNews.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        IPostCategoryService _postCategoryService;
        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            _postCategoryService = postCategoryService;
        }
        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage requestMessage, PostCategoryViewModel postCategoryViewModel)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (!ModelState.IsValid)
                {
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    PostCategory postCategory = new PostCategory();
                    postCategory.UpdatePostCategory(postCategoryViewModel);
                    postCategory.CreatedDate = DateTime.Now;
                    postCategory.CreatedBy = User.Identity.Name;
                    _postCategoryService.Add(postCategory);
                    _postCategoryService.Save();
                    var responseData = Mapper.Map<PostCategory, PostCategoryViewModel>(postCategory);
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return responseMessage;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage requestMessage, PostCategoryViewModel postCategoryViewModel)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (!ModelState.IsValid)
                {
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    PostCategory postCategory = _postCategoryService.GetById(postCategoryViewModel.ID);
                    postCategory.UpdatePostCategory(postCategoryViewModel);
                    postCategory.UpdatedDate = DateTime.Now;
                    postCategory.UpdatedBy = User.Identity.Name;
                    _postCategoryService.Update(postCategory);
                    _postCategoryService.Save();
                    var responseData = Mapper.Map<PostCategory, PostCategoryViewModel>(postCategory);
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return responseMessage;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage requestMessage, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var totalRow = 0;
                var model = _postCategoryService.GetAll(keyword);
                totalRow = model.Count();
                var query = model.OrderByDescending(s => s.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(query);

                var paginationSet = new PaginationSet<PostCategoryViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalPage = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    TotalCount = totalRow
                };
                var response = requestMessage.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }
        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var model = _postCategoryService.GetAll();
                var responseData = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(model);
                var response = requestMessage.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage requestMessage, int id)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (!ModelState.IsValid)
                {
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldData = _postCategoryService.Delete(id);
                    _postCategoryService.Save();
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, oldData);
                }
                return responseMessage;
            });
        }
        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage requestMessage, string listId)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (!ModelState.IsValid)
                {
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var ids = new JavaScriptSerializer().Deserialize<List<int>>(listId);
                    foreach (var id in ids)
                    {
                        _postCategoryService.Delete(id);
                    }
                    _postCategoryService.Save();

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, ids.Count);
                }
                return responseMessage;
            });
        }
        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage requestMessage, int id)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var model = _postCategoryService.GetById(id);
                var responseData = Mapper.Map<PostCategory, PostCategoryViewModel>(model);
                var response = requestMessage.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
    }
}
