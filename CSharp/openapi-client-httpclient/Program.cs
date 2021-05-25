using openapi_client_pure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace openapi_client_httpclient
{
    class Program
    {
        static void Main(string[] args)
        {
            testTrackByBlRef();

            Console.WriteLine("Hello World!");
        }

        public async static void testTrackByBlRef()
        {
            using(var client = createClient())
            {
                var response = await client.GetAsync(getOpenapiBaseUri() + "/info/tracking/6103622780?numberType=bl");

                response.EnsureSuccessStatusCode();//用来抛异常的
                string responseBody = "";

                responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
            }
        }

        static HttpClient createClient()
        {
            HttpClient client = new HttpClient(new HttpClientHandler() { UseCookies = false });



            return client;
        }

        private HmacPureExecutor hmacPureExecutor = null;

        public static string OPENAPI_URL_BASE_PROD = "https://api.lines.coscoshipping.com/service";

        public static string getOpenapiBaseUri()
        {
            return OPENAPI_URL_BASE_PROD;
        }
        public HmacPureExecutor getHmacPureExecutor()
        {
            return hmacPureExecutor;
        }

        private String[] hostPatterns = {"api.lines.coscoshipping.com","api-pp.lines.coscoshipping.com",
                     "api-internal.lines.coscoshipping.com","api-internal-pp.lines.coscoshipping.com"};
        private List<HttpClient> clients = new List<HttpClient>();
    }
}
