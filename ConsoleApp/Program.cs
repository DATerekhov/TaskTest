using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();

            var sites = new List<string>()
            {
                "https://medium.com/@naveenrtr/introduction-to-functional-programming-with-c-b167f15221e1",
                "https://www.manning.com/books/functional-programming-in-c-sharp",
                "https://hackernoon.com/is-c-7-starting-to-look-like-a-functional-language-d4326b427aaa",
                "https://www.codeproject.com/Articles/375166/Functional-programming-in-Csharp",
                "https://www.dotnetcurry.com/csharp/1384/functional-programming-fsharp-for-csharp-developers",
                "https://www.telerik.com/blogs/functions-as-data-functional-programming-in-c",
                "https://weblogs.asp.net/dixin/functional-csharp-fundamentals",
                "https://blog.submain.com/csharp-functional-programming/",
                "https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Functional-Programming-in-CSharp"
            };

            //firstMethod
            //var res = new List<int>();
            //Task.WaitAll(sites.Select(x =>
            //{
            //    return Task.Run(() =>
            //    {
            //        var r = GetSiteResponseLength(x);
            //        res.Add(r);
            //    });
            //}).ToArray());
            //foreach (var item in res)
            //{
            //    Console.WriteLine(item.ToString());
            //}

            //secondMethod
            var res = GetSiteResponseLengthAsync(sites).Result;
            foreach (var item in res)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("Time: " + watch.ElapsedMilliseconds);
            Console.ReadKey();
        }

        private static int GetSiteResponseLength(string site)
        {
            var request = WebRequest.Create(site);
            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream);
            var strSiteResponse = reader.ReadToEnd();

            return strSiteResponse.Length;
        }

        private static async Task<int[]> GetSiteResponseLengthAsync(List<string> sites)
        {
            var tasks = new List<Task<int>>();

            foreach (var site in sites)
            {
                tasks.Add(Task.Run(() => GetSiteResponseLength(site)));
            }

            return await Task.WhenAll(tasks);
        }
    }
}
