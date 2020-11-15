using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ShowRoom.Data;
using ShowRoom.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ShowRoom
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddDbContext<ShowRoomContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ShowRoomContext")));

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ShowRoomContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Main}/{action=Index}/{id?}");
            });

            RunDataInitialization(context);
        }

        private void RunDataInitialization(ShowRoomContext context)
        {
            if (!context.Account.Any())
            {
                context.Account.Add(new Models.Account()
                {
                    FullName = "Admin",
                    Email = "admin@gmail.com",
                    Password = "admin1234",
                    ConfirmPassword = "admin1234",
                    Gender = "Male",
                    Type = Models.userType.Admin,
                });

                context.Account.Add(new Models.Account()
                {
                    FullName = "Bdika",
                    Email = "bdika@gmail.com",
                    Password = "bdika1234",
                    ConfirmPassword = "bdika1234",
                    Gender = "Male",
                    Type = Models.userType.Customer,
                });
            }

            if (!context.Clothings.Any())
            {
                context.Clothings.AddRange(new List<Models.Clothings>()
                {
                    new Models.Clothings()
                    {
                        Brand = "Zara",
                        Category = "1",
                        Description = "A very beautiful and durable T-shirt created from recycable materials. Vegan Friendly.",
                        ImageUrl = @"images\Clothes\shirt.jpeg",
                        Name = "SS20 Women Fashion Awards Show T-Shirt",
                        Price = 169.90
                    },
                    new Models.Clothings()
                    {
                        Brand = "Balenciaga",
                        Category = "5",
                        Description = "Premium Leather Unisex Handbag",
                        ImageUrl = @"images\Clothes\accessories.jpeg",
                        Name = "SS21 SSENSE BALENCIAGA SHOW",
                        Price = 2390.90
                    },
                    new Models.Clothings()
                    {
                        Brand = "Heron Preston",
                        Category = "2",
                        Description = "Heron Preston x AAPE Collab SS20",
                        ImageUrl = @"images\Clothes\pants.jpeg",
                        Name = "Heron Preston x AAPE SS20 Shorts",
                        Price = 1490.90
                    },
                    new Models.Clothings()
                    {
                        Brand = "Yeezy",
                        Category = "3",
                        Description = "This inaugural colorway of Kanye West’s Yeezy Wave Runner 700 launched in November 2017.",
                        ImageUrl = @"images\Clothes\shoes.jpg",
                        Name = "Yeezy Boost 700 V1 Waverunner",
                        Price = 1299.90
                    },
                    new Models.Clothings()
                    {
                        Brand = "Asos",
                        Category = "4",
                        Description = "Basic dress.",
                        ImageUrl = @"images\Clothes\dress.jpeg",
                        Name = "Basic Essential Black Dress by Asos. SS19",
                        Price = 219.90
                    },
                    new Models.Clothings()
                    {
                        Brand = "Zara",
                        Category = "2",
                        Description = "Basic Men Jeans.",
                        ImageUrl = @"images\Clothes\Men\10.jpeg",
                        Name = "Basic Jeans by Zara. SS19",
                        Price = 219.90
                    },
                     new Models.Clothings()
                    {
                        Brand = "Asos",
                        Category = "1",
                        Description = "Basic Men T-Shirt.",
                        ImageUrl = @"images\Clothes\Men\2.jpeg",
                        Name = "Basic Men T-Shirt by Asos. SS19",
                        Price = 189.90
                    },
                     new Models.Clothings()
                    {
                        Brand = "Asos",
                        Category = "1",
                        Description = "Basic Men Brown Jacket.",
                        ImageUrl = @"images\Clothes\Men\1.jpeg",
                        Name = "Basic Men Brown Jacket by Asos. SS19",
                        Price = 169.90
                    },
                     new Models.Clothings()
                    {
                        Brand = "Asos",
                        Category = "2",
                        Description = "Basic Men Jeans.",
                        ImageUrl = @"images\Clothes\Men\11.jpeg",
                        Name = "Basic Men Jeans by Asos. SS19",
                        Price = 170.90
                    },
                     new Models.Clothings()
                    {
                        Brand = "Zara",
                        Category = "1",
                        Description = "Black Coat For Men.",
                        ImageUrl = @"images\Clothes\Men\13.jpeg",
                        Name = "Basic Black Coat For Men. SS19",
                        Price = 159.90
                    },
                     new Models.Clothings()
                    {
                        Brand = "Zara",
                        Category = "1",
                        Description = "Black Jeans Jacket.",
                        ImageUrl = @"images\Clothes\Men\12.jpeg",
                        Name = "Black Jeans Jacket By Zara. SS19",
                        Price = 149.90
                    },
                     new Models.Clothings()
                    {
                        Brand = "Asos",
                        Category = "1",
                        Description = "Red Coat For Men.",
                        ImageUrl = @"images\Clothes\Men\14.jpeg",
                        Name = "Red Coat For Men by Asos. SS19",
                        Price = 245.90
                    },
                     new Models.Clothings()
                    {
                        Brand = "Asos",
                        Category = "1",
                        Description = "Basic Loose Black Coat.",
                        ImageUrl = @"images\Clothes\Men\15.jpeg",
                        Name = "Basic Black Coat by Asos. SS19",
                        Price = 210.00
                    },
                     new Models.Clothings()
                    {
                        Brand = "Bershka",
                        Category = "1",
                        Description = "Basic Men White T-Shirt.",
                        ImageUrl = @"images\Clothes\Men\3.jpeg",
                        Name = "Basic Men T-Shirt by Bershka. SS19",
                        Price = 69.90
                    },
                     new Models.Clothings()
                    {
                        Brand = "Asos",
                        Category = "1",
                        Description = "Basic Men Two-tone Hoodie.",
                        ImageUrl = @"images\Clothes\Men\4.jpeg",
                        Name = "Basic Men Two-tone Hoodie by Asos. SS19",
                        Price = 129.90
                    },
                     new Models.Clothings()
                    {
                        Brand = "Asos",
                        Category = "1",
                        Description = "Basic Men Hoodie.",
                        ImageUrl = @"images\Clothes\Men\5.jpeg",
                        Name = "Basic Men Hoodie by Asos. SS19",
                        Price = 129.90
                    },
                     new Models.Clothings()
                    {
                        Brand = "Zara",
                        Category = "2",
                        Description = "Basic Men Pink Pants.",
                        ImageUrl = @"images\Clothes\Men\6.jpeg",
                        Name = "Basic Men Pink Pants by Zara. SS19",
                        Price = 79.90
                    },
                     new Models.Clothings()
                    {
                        Brand = "Asos",
                        Category = "1",
                        Description = "Blue Coat For Men.",
                        ImageUrl = @"images\Clothes\Men\7.jpeg",
                        Name = "Blue Coat For Men by Asos. SS19",
                        Price = 169.90
                    },
                     new Models.Clothings()
                    {
                        Brand = "Bershka",
                        Category = "2",
                        Description = "Blue Jeans For Men.",
                        ImageUrl = @"images\Clothes\Men\8.jpeg",
                        Name = "Blue Jeans For Men By Bershka. SS19",
                        Price = 210.00
                    },
                     new Models.Clothings()
                    {
                        Brand = "Asos",
                        Category = "2",
                        Description = "Basic Men Jeans.",
                        ImageUrl = @"images\Clothes\Men\9.jpeg",
                        Name = "Basic Men Jeans by Asos. SS19",
                        Price = 179.90
                    },
                     new Models.Clothings()
                    {
                        Brand = "Nike",
                        Category = "3",
                        Description = "UniSex Nike Shoes.",
                        ImageUrl = @"images\Clothes\Men\Shoe1.jpeg",
                        Name = "UniSex Nike Shoes by Nike. SS19",
                        Price = 330.90
                    },
                     new Models.Clothings()
                    {
                        Brand = "Asos",
                        Category = "3",
                        Description = "Shoes For Men.",
                        ImageUrl = @"images\Clothes\Men\Shoe2.jpeg",
                        Name = "Shoes For Men by Asos. SS19",
                        Price = 270.90
                    },
                    // new Models.Clothings()
                    //{
                    //    Brand = "Asos",
                    //    Category = "3",
                    //    Description = "Unisex Shoes.",
                    //    ImageUrl = @"images\Clothes\Men\Shoe3.jpeg",
                    //    Name = "White Green Shoes by Asos. SS19",
                    //    Price = 229.90
                    //},
                    new Models.Clothings()
                    {
                        Brand = "Asos",
                        Category = "4",
                        Description = "Black Dress.",
                        ImageUrl = @"images\Clothes\Women\1.jpeg",
                        Name = "Black Dress For Women by Asos. SS19",
                        Price = 59.90
                    },
                });
            }

            context.SaveChanges();
        }
    }
}