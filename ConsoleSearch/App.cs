﻿using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleSearch
{
    public class App
    {
        public void Run()
        {
            HttpClient api = new HttpClient();
            api.BaseAddress = new Uri("http://loadbalancer-1");
            //SearchLogic mSearchLogic = new SearchLogic(new Database());
            Console.WriteLine("Console Search");
            
            while (true)
            {
                Console.WriteLine("enter search terms - q for quit");
                string input = Console.ReadLine() ?? string.Empty;
                if (input.Equals("q")) break;

                Task<string> task = api.GetStringAsync("/Load/Search?terms=" + input + "&numberOfResults=10");
                task.Wait();
                string resultString = task.Result;

                SearchResult result = JsonConvert.DeserializeObject<SearchResult>(resultString);

                foreach (var ignored in result.IgnoredTerms)
                {
                    Console.WriteLine(ignored + " Was Ignored");
                }

                foreach (var resultDocument in result.Documents)
                {
                    Console.WriteLine(resultDocument.Id + ": " + resultDocument.Path + " - number of terms found: " + resultDocument.NumberOfOccurences);
                }

                Console.WriteLine("Found " + result.Documents.Count + " in " + result.ElapsedMilliseconds + " ms");
            }
        }
    }
}
