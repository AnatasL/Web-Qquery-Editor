using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;
using NLog.Filters;
using NLog.Internal;
using Web_Service.Models;
using Web_Service.Services;


namespace Web_Service.Controllers
{
    public class QueryController : Controller
    {
        public List<Models.Task> task = new List<Models.Task>();

        private readonly DbRepository _dbRepository;

        public QueryController(DbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public async Task<IActionResult> Index()
        {
            var s = await _dbRepository.SelectTasksAsync();


            return View("Create", s);
        }

        //Fill in table with all db records 
        public async Task<IActionResult> Create()
        {
            var s = await _dbRepository.SelectTasksAsync();
            return View("Create", s);
        }


        [HttpPost]
        public async Task<IActionResult> Select(Query xmlq)
        {

            XmlSerializer xsSubmit = new XmlSerializer(typeof(Query));
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, xmlq);
                    xml = sww.ToString();
                }
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            doc.Save(@"./Models/Where.xml");

            var s = await _dbRepository.SelectTasksAsync();


            return View("Create",s);

        }


        [HttpGet]
        public async Task<IActionResult> Generate()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(@"./Models/Where.xml");

            XmlNodeList xnl = xml.SelectNodes("//Group/id_group | //Group/AndOr | //Group/Field | //Group/Operator | //Group/Value ");

            string q = "";
            int i = 0, g = 0;

            foreach (XmlElement elem in xnl)
            {
                switch (elem.Name)
                {
                    case "id_group":
                        if (elem.InnerText == "1" && g != 0) g++;
                        else if (elem.InnerText == "1" && g == 0) { q = $"{q}  (";  g++; }
                        else if (elem.InnerText == "0" && g != 0) { q = $"{q}  )"; g = 0; }
                        break;

                    case "AndOr":

                        q = $"{q} {elem.InnerText} (";
                        i++;

                        break;


                    case "Field":

                        q = $"{q} {elem.InnerText}";
                        break;

                    case "Operator":

                        q = $"{q} {elem.InnerText}";
                        break;

                    case "Value":
                        if (i != 0)
                        {
                            q = $"{q} '{elem.InnerText}')";
                            i--;
                        }
                        else q = $"{q} '{elem.InnerText}'";

                        break;
                }
            }
            if (q == "") return View("Create", task);

            var s = await _dbRepository.QueryBuilderAsync(q); 

            return View("Create",s);
        }


    }

}
