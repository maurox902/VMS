using Microsoft.EntityFrameworkCore;
using VMS.Repository;

namespace VMS.Web
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
            services.AddDbContextPool<VMSDatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("VMSDatabaseContext")));

            services.AddDistributedMemoryCache();
            // SQL Server, Redis, NCache

            services.AddRazorPages();

            services.AddHttpContextAccessor();
            services.AddResponseCaching();


            //services.AddControllersWithViews(options =>
            //{
            //    options.CacheProfiles.Add("Basic", new CacheProfile()
            //    {
            //        Duration = 10,
            //        VaryByHeader = "User-Agent"
            //    });
            //    options.CacheProfiles.Add("NoCaching",
            //        new CacheProfile() { NoStore = true, Location = ResponseCacheLocation.None });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseResponseCaching();
            app.UseRouting();
            app.UseAuthorization();
            //app.MapRazorPages();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
