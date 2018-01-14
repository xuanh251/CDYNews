using CDYNews.Data.Infrastructure;
using CDYNews.Data.Repositories;
using CDYNews.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDYNews.Service
{
    public interface IPostCategoryService
    {
        PostCategory Add(PostCategory postCategory);

        void Update(PostCategory postCategory);

        PostCategory Delete(int id);

        IEnumerable<PostCategory> GetAll();
        IEnumerable<PostCategory> GetAll(string keyword);

        IEnumerable<PostCategory> GetAllByParentId(int? parentId=null);

        PostCategory GetById(int id);

        void Save();
    }

    public class PostCategoryService : IPostCategoryService
    {
        private IPostCategoryRepository _postCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public PostCategoryService(IPostCategoryRepository postCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._postCategoryRepository = postCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public PostCategory Add(PostCategory postCategory)
        {
            return _postCategoryRepository.Add(postCategory);
        }

        public PostCategory Delete(int id)
        {
            return _postCategoryRepository.Delete(id);
        }

        public IEnumerable<PostCategory> GetAll()
        {
            var model= _postCategoryRepository.GetAll();
            return model;
        }

        public IEnumerable<PostCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _postCategoryRepository.GetMulti(s => s.Name.Contains(keyword) || s.Description.Contains(keyword) || s.Alias.Contains(keyword));
            }
            else
            {
                return _postCategoryRepository.GetAll();
            }
        }

        public IEnumerable<PostCategory> GetAllByParentId(int? parentId=null)
        {
            return _postCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
        }

        public PostCategory GetById(int id)
        {
            return _postCategoryRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(PostCategory postCategory)
        {
            _postCategoryRepository.Update(postCategory);
        }
    }
}
