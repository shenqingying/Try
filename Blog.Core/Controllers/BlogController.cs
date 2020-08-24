using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.IServices;
using Blog.Model;
using Blog.Services.Blog.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        readonly IAdvertisementServices _advertisementServices;
        private readonly ILogger<BlogController> _logger;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="blogArticleServices"></param>
        /// <param name="logger"></param>
        public BlogController(IAdvertisementServices blogArticleServices, ILogger<BlogController> logger)
        {
            _advertisementServices = blogArticleServices;
            _logger = logger;
        }


        // GET: api/<BlogController>
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="id">参数id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Advertisement>> Get(int id)
        {
          //  IAdvertisementServices advertisementServices = new AdvertisementServices();

            return await _advertisementServices.Query(d => d.Id == id);
        }
    }
}
