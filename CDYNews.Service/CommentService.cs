using CDYNews.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDYNews.Service
{
    public interface ICommentService
    {
        IEnumerable<Comment> LoadComment(int postId);
        Comment AddComment();
    }
    public class CommentService : ICommentService
    {
        public Comment AddComment()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> LoadComment(int postId)
        {
            throw new NotImplementedException();
        }
    }
}
