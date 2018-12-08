using FakeBook.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace FakeBook.Controllers
{
    public class PostsController : ApiController
    {
        private FakeBookEntities db = new FakeBookEntities();

        // GET: api/Posts
        public IList<CustomPost> GetAlList()
        {
            IList<CustomPost> customPostList = new List<CustomPost>();
            IList<Post> PostList = db.Posts.ToList();

            foreach (var post in PostList)
            {
                var usr = db.AspNetUsers.Find(post.FriendId);
                if (usr != null)
                {
                    customPostList.Add(new CustomPost
                    {
                        ID = post.ID,
                        Text = post.Text,
                        Bold = post.Bold,
                        RandomColor = post.RandomColor,
                        FriendId = usr.LastName + " " + usr.FirstName,
                        OwnerId = post.OwnerId
                    });
                }
                else
                {
                    customPostList.Add(new CustomPost
                    {
                        ID = post.ID,
                        Text = post.Text,
                        Bold = post.Bold,
                        RandomColor = post.RandomColor,
                        FriendId = string.Empty,
                        OwnerId = post.OwnerId
                    });
                }
         
            }

            return customPostList;

        }

        // GET: api/Posts/5
        [Route("api/Posts/{id}")]
        public IHttpActionResult Get(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // GET: api/Posts/5
        [Route("api/Posts/own/{ownerId}")]
        public IList<Post> GetAllOwnersPosts(string ownerId)
        {
            IList<Post> postList = db.Posts.Where(p => p.OwnerId == ownerId).ToList();

            foreach (var post in postList)
            {
                var friend = db.AspNetUsers.Find(post.FriendId);
                if (friend != null)
                {
                    post.FriendId = friend.LastName + " " + friend.FirstName;
                }
            }

            if (!postList.Any())
            {
                return new List<Post>();
            }

            return postList;
        }

        // GET: api/Posts/5/5
        [Route("api/Posts/{ownerId}/{postId}")]
        public IHttpActionResult GetOwnersPost(string ownerId, int postId)
        {
            Post post = db.Posts.Find(postId);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/Posts/5            
        [Route("api/Posts/upd/{postId}")]
        public IHttpActionResult PutUpdatePost(int postId, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (postId != post.ID)
            {
                return BadRequest();
            }

            db.Entry(post).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(postId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(post);
        }

        // POST: api/Posts
        public IHttpActionResult PostPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.Posts.Add(post);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = post.ID }, post);
        }

        // DELETE: api/Posts/5      
        [Route("api/Posts/delete/{id}")]
        public IHttpActionResult DeletePostById(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            db.Posts.Remove(post);
            db.SaveChanges();

            return Ok(post);
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostExists(int id)
        {
            return db.Posts.Count(e => e.ID == id) > 0;
        }
    }
}