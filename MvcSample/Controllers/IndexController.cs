using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSample.Models;
using Nest;

namespace MvcSample.Controllers
{
    public class IndexController : Controller
    {
        private readonly IElasticClient _elasticClient;

        public IndexController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        //
        // GET: /Index/New
        public ActionResult New()
        {
            return View(new Post());
        }

        //
        // POST: /Index/New

        [HttpPost]
        public ActionResult New(Post post)
        {
            var response = _elasticClient.Index(post, new IndexParameters { Refresh = true });

            if (!response.ConnectionStatus.Success)
            {
                ModelState.AddModelError(string.Empty, response.ConnectionStatus.Result);

                return View(post);
            }

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Index/Edit
        public ActionResult Edit(string id)
        {
            var model = _elasticClient.Get<Post>(id);

            if (model == null)
            {
                throw new HttpException(404, "Post not found");
            }

            return View(model);
        }

        //
        // POST: /Index/Edit

        [HttpPost]
        public ActionResult Edit(Post post)
        {
            _elasticClient.Index(post, new IndexParameters { Refresh = true });

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Index/Delete
        public ActionResult Delete(string id)
        {
            var post = _elasticClient.Get<Post>(id);

            if (post == null)
            {
                throw new HttpException(404, "Post not found");
            }

            var result = _elasticClient.Delete(post, new DeleteParameters
                                                         {
                                                             Refresh = true
                                                         });

            if (!result.ConnectionStatus.Success)
            {
                throw new HttpException(500, string.Format("Failed to delete post bacause of '{0}'.", result.ConnectionStatus.Result));
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
