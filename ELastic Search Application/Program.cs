using System;
using ELastic_Search_Application;

namespace ELastic_Search_Application
{
    class Program
    {
        static void Main(string[] args)
        {
            ElasticSearch es = new ElasticSearch();
            es.CreateDummyData();

            //es.PerformTermQuery();
            //es.PerformMatchPhrase();
            //es.PerormFilter();
            Console.ReadLine();

        }
    }
}
