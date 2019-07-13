using System;
using MPrintSolutionsServiceBL;

namespace MprintSolutionsWindowService
{
    class Program
    {
        static void Main(string[] args)
        {
            

            var response = AdvertisersBL();
            //var responce = ProductfeedBL();

        }

        private static string AdvertisersBL()
        {
            return MPrintSolutionsServiceBL.AdvertisersBL.DownloadAdertisers();

        }

        private static string ProductfeedBL()
        {
            return MPrintSolutionsServiceBL.ProductFeedBL.DownloadProductFeed();
        }
                
    }
}
