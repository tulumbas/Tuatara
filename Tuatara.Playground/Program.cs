using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuatara.Data.DB;
using Tuatara.Data.Xml;

namespace Tuatara.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlContextExtensions.XMLDATA_FOLDER = @"C:\Users\Stan\Documents\Visual Studio 2015\Projects\Tuatara\Data";
            //var z = new XmlResourceRepository();

            var db = new ResourceRepository();
            var items = db.GetAll();
            
        }
    }
}
