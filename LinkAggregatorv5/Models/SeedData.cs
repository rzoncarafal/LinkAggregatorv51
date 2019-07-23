using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkAggregatorv5.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new LinkAggregatorv5Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LinkAggregatorv5Context>>()))
            {
                if (context.Link.Any())
                {
                    return;
                }

                var links = new List<Link>();

                context.Link.AddRange(
                    new Link
                    {
                        IdLink = 1, //W Internecie jest mnóstwo postów, że nie da się zrobić pola autonumber (najpopularniejsza opcja [DatabaseGenerated(DatabaseGeneratedOption.Identity)])
                        Description = "Onet",
                        LikeCount = 0,
                        LinkURL = "onet.pl",
                        UpdateDate = DateTime.Now,
                        AddDate = DateTime.Now
                    }
                );
                context.Link.AddRange(
                    new Link
                    {
                        IdLink = 2,
                        Description = "Wirtualna Polska",
                        LikeCount = 0,
                        LinkURL = "wp.pl",
                        UpdateDate = DateTime.Now,
                        AddDate = DateTime.Now
                    }
                );
                context.Link.AddRange(
                    new Link
                    {
                        IdLink = 3,
                        Description = "YouTube",
                        LikeCount = 0,
                        LinkURL = "youtube.com",
                        UpdateDate = DateTime.Now,
                        AddDate = DateTime.Now
                    }
                );
                context.Link.AddRange(
                    new Link
                    {
                        IdLink = 4,
                        Description = "Facebook",
                        LikeCount = 0,
                        LinkURL = "facebook.com",
                        UpdateDate = DateTime.Now,
                        AddDate = DateTime.Now
                    }
                );
                context.Link.AddRange(
                    new Link
                    {
                        IdLink = 5,
                        Description = "Interia",
                        LikeCount = 0,
                        LinkURL = "interia.pl",
                        UpdateDate = DateTime.Now,
                        AddDate = DateTime.Now
                    }
                );
                context.Link.AddRange(
                    new Link
                    {
                        IdLink = 6,
                        Description = "o2",
                        LikeCount = 0,
                        LinkURL = "o2.pl",
                        UpdateDate = DateTime.Now,
                        AddDate = DateTime.Now
                    }
                );
                context.Link.AddRange(
                    new Link
                    {
                        IdLink = 7,
                        Description = "Poczta Gmail",
                        LikeCount = 0,
                        LinkURL = "mail.google.com",
                        UpdateDate = DateTime.Now,
                        AddDate = DateTime.Now
                    }
                );
                context.Link.AddRange(
                    new Link
                    {
                        IdLink = 8,
                        Description = "Allegro",
                        LikeCount = 0,
                        LinkURL = "allegro.pl",
                        UpdateDate = DateTime.Now,
                        AddDate = DateTime.Now
                    }
                );
                context.Link.AddRange(
                    new Link
                    {
                        IdLink = 9,
                        Description = "Translator",
                        LikeCount = 0,
                        LinkURL = "translate.google.pl",
                        UpdateDate = DateTime.Now,
                        AddDate = DateTime.Now
                    }
                );
                context.Link.AddRange(
                    new Link
                    {
                        IdLink = 10,
                        Description = "Poranny",
                        LikeCount = 0,
                        LinkURL = "poranny.pl",
                        UpdateDate = DateTime.Now,
                        AddDate = DateTime.Now
                    }
                );
                context.Link.AddRange(
                    new Link
                    {
                        IdLink = 11,
                        Description = "Współczesna",
                        LikeCount = 0,
                        LinkURL = "wspolczesna.pl",
                        UpdateDate = DateTime.Now,
                        AddDate = DateTime.Now
                    }
                );
                context.Link.AddRange(
                    new Link
                    {
                        IdLink = 12,
                        Description = "tvn24",
                        LikeCount = 0,
                        LinkURL = "tvn24.pl",
                        UpdateDate = DateTime.Now,
                        AddDate = DateTime.Now
                    }
                );
                context.SaveChanges();
            }
        }
    }
}