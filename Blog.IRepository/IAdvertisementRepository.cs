using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Blog.IRepository.Base;
using Blog.Model;

namespace Blog.IRepository
{
    public interface IAdvertisementRepository: IBaseRepository<Advertisement>

    {
        int SUM(int i, int j);
        //int Add(Advertisement model);
        //bool Delete(Advertisement model);
        //bool Update(Advertisement model);
        //List<Advertisement> Query(Expression<Func<Advertisement, bool>> wherExpression);
    }
}
