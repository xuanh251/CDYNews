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
    public interface IErrorService
    {
        ErrorLog Create(ErrorLog error);

        void Save();
    }

    public class ErrorLogService : IErrorService
    {
        private IErrorLogRepository _errorRepository;
        private IUnitOfWork _unitOfWork;

        public ErrorLogService(IErrorLogRepository errorLogRepository, IUnitOfWork unitOfWork)
        {
            this._errorRepository = errorLogRepository;
            this._unitOfWork = unitOfWork;
        }

        public ErrorLog Create(ErrorLog error)
        {
            return _errorRepository.Add(error);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
