using Blog.Model;
using Blog.IServices.BASE;

namespace Blog.IServices
{
    public interface IAdvertisementServices:IBaseServices<Advertisement>
    {
        int SUM(int i, int j);

        int Test();
    }
}
