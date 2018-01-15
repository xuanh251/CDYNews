using CDYNews.Data.Repositories;
using CDYNews.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDYNews.Service
{
    public interface ICommonServices
    {
        IEnumerable<PostTag> getListPostTagOfSelectedPost(int postId);
    }
    public class CommonSerivces : ICommonServices
    {
        private IPostRepository _postRepository;
        private IPostTagRepository _postTagRepository;
        public CommonSerivces(IPostRepository postRepository, IPostTagRepository postTagRepository)
        {
            _postRepository = postRepository;
            _postTagRepository = postTagRepository;
        }

        public IEnumerable<PostTag> getListPostTagOfSelectedPost(int postId)
        {
            IEnumerable<Post> _post = _postRepository.GetAll();
            IEnumerable<PostTag> _postTag = _postTagRepository.GetAll();
            var result = (from a in _post
                          from b in _postTag.Where(s => s.PostID == a.ID)
                          where a.ID == postId
                          select b).ToList();
            return result;
            
        }
    }
}
