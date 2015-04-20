using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class Video
    {
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }



    }
    public class ListController : Controller
    {
        //
        // GET: /List/

        public ActionResult Index()
        {


            return View();
        }

        [HttpGet]
        public JsonResult Search(string key)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyA0GO6WEDb4p9y7AfsCzIYip55QSU2kI-A",
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = key; // Replace with your search term.
            searchListRequest.MaxResults = 50;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = searchListRequest.ExecuteAsync().Result;

            List<Video> videos = new List<Video>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add(new Video
                        {
                            Title = searchResult.Snippet.Title,
                            Thumbnail = searchResult.Snippet.Thumbnails.Default.Url,
                            Description = searchResult.Snippet.Description,
                           
                        });
                        break;

                    case "youtube#channel":
                        channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        break;

                    case "youtube#playlist":
                        playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                        break;
                }
            }

            return Json(new
            {
                videos = videos
            }, JsonRequestBehavior.AllowGet);
        }


        //
        // GET: /List/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /List/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /List/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /List/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /List/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /List/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /List/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
