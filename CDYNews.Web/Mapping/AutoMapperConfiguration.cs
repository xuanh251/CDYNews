using AutoMapper;
using CDYNews.Model.Models;
using CDYNews.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDYNews.Web.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            Mapper.CreateMap<Tag, TagViewModel>();
            Mapper.CreateMap<Feedback, FeedbackViewModel>();
        }
            
    }
}