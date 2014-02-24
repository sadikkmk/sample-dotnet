using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSample.Models;
using Nest;

namespace MvcSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IElasticClient _elasticClient;

        public HomeController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        //
        // GET: /

        public ActionResult Index(string q, string cat)
        {
            var query = _elasticClient.Search<Post>(s =>
                                                        {
                                                            s.QueryString(q);
                                                            s.FacetTerm(f =>
                                                                        f.OnField(p =>
                                                                                  p.Category));

                                                            if (!string.IsNullOrEmpty(cat))
                                                            {
                                                                s.Filter(f =>
                                                                         f.Term(p =>
                                                                                p.Category, cat));
                                                            }

                                                            return s;
                                                        });

            return View(new SearchResults(query, q, cat));
        }
    }
}
