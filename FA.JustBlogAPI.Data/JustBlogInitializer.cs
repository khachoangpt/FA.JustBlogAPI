using FA.JustBlogAPI.Data;
using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.Models.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FA.JustBlog.Data
{
    public class JustBlogInitializer : DropCreateDatabaseIfModelChanges<JustBlogContext>
    {
        protected override void Seed(JustBlogContext context)
        {

            InitializeIdentity(context);

            var categories = new List<Category>()
            {
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Travel",
                    UrlSlug = "travel",
                    Description = "Travel Blog"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Recipe",
                    UrlSlug = "recipe",
                    Description = "Recipe Blog"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Tips",
                    UrlSlug = "tips",
                    Description = "Tips Blog"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Life Style",
                    UrlSlug = "life-style",
                    Description = "Life Style Blog"
                }
            };

            var tag1 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "travel",
                UrlSlug = "travel",
                Description = "Travel Tag"
            };

            var tag2 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "food",
                UrlSlug = "food",
                Description = "food Tag"
            };

            var tag3 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "recipe",
                UrlSlug = "recipe",
                Description = "recipe Tag"
            };

            var tag4 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "tips",
                UrlSlug = "tips",
                Description = "tips Tag"
            };

            var tag5 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "study",
                UrlSlug = "study",
                Description = "study Tag"
            };

            var tag6 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "life style",
                UrlSlug = "life-style",
                Description = "life style Tag"
            };

            var tag7 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "setup",
                UrlSlug = "setup",
                Description = "setup Tag"
            };

            var posts = new List<Post>
            {
                new Post
                {
                    Id = Guid.NewGuid(),
                    Title = "What to do when you have to wait",
                    UrlSlug = "what-to-do-when-you-have-to-wait",
                    ShortDescription = "Wait for the next delayed flight in 3 hours. Wait on the train for 12 hours before arrival. Wait for someone who is never going to...",
                    ImageUrl = "blog-1.jpg",
                    PostContent = "Wait for the next delayed flight in 3 hours. Wait on the train for 12 hours before arrival. Wait for someone who is never going to text you back. Wait for something that is never going to fall in your mouth. Wait for a story where you could become the main character. Wait for the inevitable conclusion that awaits everyone.",
                    Published = true,
                    PostedOn = DateTime.Now,
                    Modified = DateTime.Now,
                    RateCount = 10,
                    TotalRate = 46,
                    ViewCount = 112,
                    Category = categories.Single(x => x.Name == categories[0].Name),
                    Tags = new List<Tag>{tag1, tag2, tag3}
                },
                new Post
                {
                    Id = Guid.NewGuid(),
                    Title = "The Incredible Rise of North Korea’s Hacking Army",
                    UrlSlug = "the-incredible-rise-of-north-koreas-hacking-army",
                    ShortDescription = "The country’s cyber forces have raked in billions of dollars for the regime by pulling off schemes ranging from A.T.M. heists to cryptocurrency...",
                    ImageUrl = "blog-2.jpg",
                    PostContent = "The superior read instructions from a thin manual: early the next morning, a Sunday, they should go to any 7-Eleven and use their white card at the store’s A.T.M. They could not use a regular bank A.T.M., or one in another convenience store. The gangsters should each withdraw a hundred thousand yen at a time (about nine hundred dollars) but make no more than nineteen transactions per machine. If anybody made twenty withdrawals from a single A.T.M., his card would be blocked. Withdrawals could start at 5 a.m. and continue until 8 a.m. The volunteers were told to choose the Japanese language when prompted—an indication, Shimomura realized, that the cards were foreign. After making nineteen withdrawals, they should wait an hour before visiting another 7-Eleven. They could keep ten per cent of the cash. The rest would go to the bosses. Finally, each volunteer was told to memorize a pin.",
                    Published = true,
                    PostedOn = DateTime.Now,
                    Modified = DateTime.Now,
                    RateCount = 11,
                    TotalRate = 41,
                    ViewCount = 147,
                    Category = categories.Single(x => x.Name == categories[3].Name),
                    Tags = new List<Tag>{tag1, tag4, tag3}
                },
                new Post
                {
                    Id = Guid.NewGuid(),
                    Title = "A Tay on Wheels",
                    UrlSlug = "a-tay-on-wheels",
                    ShortDescription = "Motorbikes in Hanoi. Alright, so it’s been a while since my last (and first and only) post. I could come up with any number of excuses...",
                    ImageUrl = "blog-3.jpg",
                    PostContent = "Yes, the traffic is annoying and crazy. I often count my lucky stars for not knowing a lot of dirty language in Vietnamese, because I’d be spouting it a lot. At people that absolutely need to pass me on the left 2 meters before they take a right turn. At people who feel that while driving a motorbike is the perfect time for them to check their Facebook comments. At people who seem to think their horn creates a force field, so they keep it blaring while they speed through tiny openings in traffic with no regard for anyone else or even themselves.",
                    Published = true,
                    PostedOn = DateTime.Now,
                    Modified = DateTime.Now,
                    RateCount = 13,
                    TotalRate = 31,
                    ViewCount = 125,
                    Category = categories.Single(x => x.Name == categories[1].Name),
                    Tags = new List<Tag>{tag5, tag2, tag3}
                },
                new Post
                {
                    Id = Guid.NewGuid(),
                    Title = "How Art Could Save The World",
                    UrlSlug = "how-art-could-save-the-world",
                    ShortDescription = "It’s extremely difficult to define “art” in modern times. As of recent, postmodernism has taken a hold of how we express our artistic...",
                    ImageUrl = "blog-4.jpg",
                    PostContent = "We are naturally drawn to statements that could resonate with us and our place in society. The existential and political angst the artist faces in modern times as they’re conveying ideas, through conventional means of art. The recent Joker movie is a prime example of how a fictional character could be representative and even reflective of certain group(s) of people, in this case, the mentally ill. The character Arthur Fleck (presumably based on respectively the two classic Martin’s characters Travis Bickle and Rupert Pupkin) should give us a clear, albeit twisted image of a modern tragedy: He’s a mentally ill loner who’s also got a medical condition where he can’t control his laugh. He’s misunderstood, isolated and mistreated by society, who harbors dangerous perspectives about life and about himself.",
                    Published = true,
                    PostedOn = DateTime.Now,
                    Modified = DateTime.Now,
                    RateCount = 13,
                    TotalRate = 31,
                    ViewCount = 125,
                    Category = categories.Single(x => x.Name == categories[2].Name),
                    Tags = new List<Tag>{tag1, tag5, tag3}
                },
                new Post
                {
                    Id = Guid.NewGuid(),
                    Title = "The Times Of Our Lives.",
                    UrlSlug = "the-times-of-our-lives",
                    ShortDescription = "Life It's been a while since my last journal. I was so caught up with exam and my Helpx trip in Finland, it had been the only thing...",
                    ImageUrl = "blog-5.jpg",
                    PostContent = "It's been a while since my last journal. I was so caught up with exam and my Helpx trip in Finland, it had been the only thing in my mind for days. I had never been so far up North until that trip to Uusi-Värtsilä, if I had a Russian visa at the time, I could go to Russia in a couple of hours. Uusi-Värtsilä is an abandoned village, it has less than 200 inhabitants, no public transport and the nearest supermarket is 7 km away. Yes, I was literally in the middle of nowhere. I enjoyed so much being in the nature in Finland. Water is never far away. Dense forests always await somewhere nearby. The first time I was in an open bog, I thought I was teleported in another planet. The working tasks in Dennis' place were interesting and pretty relaxing as well. We harvested the wild garlics, went picking wild berries and mushrooms in the forest, made the traditional Finnish bread, took care of the green house garden and the house. One minus point though the sleeping arrangements there were chaotic, we had to share our rooms and my roommate was a self-centered character. I rarely found my privacy in the house so it drained my energy sometimes. When my roommate left, we all felt like we just woke up from a nightmare.",
                    PostedOn = DateTime.Now,
                    Modified = DateTime.Now,
                    RateCount = 5,
                    TotalRate = 20,
                    ViewCount = 100,
                    Category = categories.Single(x => x.Name == categories[1].Name),
                    Tags = new List<Tag>{tag2, tag3}
                },
                new Post
                {
                    Id = Guid.NewGuid(),
                    Title = "Space colonization: What are hindering us?",
                    UrlSlug = "space-colonization-what-are-hindering-us",
                    ShortDescription = "https://www.facebook.com/notes/nguy%E1%BB%85n-t%C3%A0i-long/space-colonization-what-are-hindering-us/1112384025483978 Ever since...",
                    ImageUrl = "blog-6.jpg",
                    PostContent = "Ever since Neil Armstrong imprinted his footsteps on the Lunar surface, bringing new laurels to international space exploration endeavors and effectively ending the Space Race, humanity has dreamed even more of one day settling down in space and conquering the stars. Enormous amounts of financial budget coupled with thousands of hours of research efforts throughout the years by global space programs and organizations (most prominently NASA) are gradually and laboriously bringing the human race closer to fulfillment of this centuries-long ambition. Yet, insurmountable physical challenges exist, and with them comes the uncertainty of long-term human settlement on other planets. What lies waiting for us ahead in the vastness of space? What are the requirements and how do we settle down on those planets? In fact, does the term space colonization really mean that humans are really able to conquer the entirety of the 93-billion-light-year observable universe?",
                    Published = true,
                    PostedOn = DateTime.Now,
                    Modified = DateTime.Now,
                    RateCount = 11,
                    TotalRate = 23,
                    ViewCount = 111,
                    Category = categories.Single(x => x.Name == categories[0].Name),
                    Tags = new List<Tag>{tag6, tag3}
                }
            };

            var comments = new List<Comment>
            {
                new Comment
                {
                    Id = Guid.NewGuid(),
                    Name = "Liang",
                    Email = "liang@gmail.com",
                    CommentHeader = "Lorem Ipsum is simply",
                    CommentText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it",
                    CommentTime = DateTime.Now,
                    Post = posts.Single(p => p.Title == posts[0].Title)
                },
                new Comment
                {
                    Id = Guid.NewGuid(),
                    Name = "Jonty Andrews",
                    Email = "jontyandrews@gmail.com",
                    CommentHeader = "Contrary to popular belief",
                    CommentText = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites",
                    CommentTime = DateTime.Now,
                    Post = posts.Single(p => p.Title == posts[1].Title)
                },
                new Comment
                {
                    Id = Guid.NewGuid(),
                    Name = "Sarah Tim",
                    Email = "sarahtim@gmail.com",
                    CommentHeader = "There are many variations of passages",
                    CommentText = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure",
                    CommentTime = DateTime.Now,
                    Post = posts.Single(p => p.Title == posts[3].Title)
                },
                new Comment
                {
                    Id = Guid.NewGuid(),
                    Name = "Samso Nagaro",
                    Email = "samsonagaro@gmail.com",
                    CommentHeader = "It uses a dictionary of over 200 Latin words",
                    CommentText = "It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.",
                    CommentTime = DateTime.Now,
                    Post = posts.Single(p => p.Title == posts[4].Title)
                },
                new Comment
                {
                    Id = Guid.NewGuid(),
                    Name = "Corey oates",
                    Email = "coreyoates@gmail.com",
                    CommentHeader = "Lorem ipsum dolor sit amet",
                    CommentText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                    CommentTime = DateTime.Now,
                    Post = posts.Single(p => p.Title == posts[5].Title)
                },
                new Comment
                {
                    Id = Guid.NewGuid(),
                    Name = "Samoya Johns",
                    Email = "samoyajohns@gmail.com",
                    CommentHeader = "Lorem ipsum dolor sit amet",
                    CommentText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it",
                    CommentTime = DateTime.Now,
                    Post = posts.Single(p => p.Title == posts[0].Title)
                },
            };

            context.Categories.AddRange(categories);
            context.Posts.AddRange(posts);
            context.Comments.AddRange(comments);
            context.SaveChanges();
        }

        public static void InitializeIdentity(JustBlogContext db)
        {
            var userManager = new UserManager<User>(new UserStore<User>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            const string name = "admin@example.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleResult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new User { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}