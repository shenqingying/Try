using System;
using System.Collections.Generic;
using System.Text;
using Blog.Model;
using System.Linq.Expressions;
using Blog.IServices.BASE;

namespace Blog.IServices
{
    public interface IAdvertisementServices:IBaseServices<Advertisement>
    {
        int SUM(int i, int j);

        int Test();
        //int Add(Advertisement model);
        //bool Delete(Advertisement model);
        //bool Update(Advertisement model);
        //List<Advertisement> Query(Expression<Func<Advertisement, bool>> wherExpression);
    }
}
