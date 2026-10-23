using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }
        public void Save();
    }
}
