using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Blog.IRepository;
using Blog.IRepository.Base;
using Blog.IServices;
using Blog.Model;
using Blog.Repository;
using Blog.Services.BASE.Blog.Core.Services.BASE;

namespace Blog.Services
{
    namespace Blog.Core.Services
    {
        public class AdvertisementServices : BaseServices<Advertisement>,  IAdvertisementServices
        {
             IAdvertisementRepository dal;

             public AdvertisementServices()
             {

             }
             public AdvertisementServices(IAdvertisementRepository dal)
             {
                 this.dal = dal;
                 base.baseDal = dal;
             }
             public int SUM(int i, int j)
             {
                 return dal.SUM(i, j);
             }
             public int Test()
             {
                 return 1;
             }
        }
    }
}
