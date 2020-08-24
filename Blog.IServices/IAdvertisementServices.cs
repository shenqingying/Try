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
    }
}
