using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Models
{
   public class DatabaseContext:DbContext
    {
        static DatabaseContext()
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogGroup> BlogGroups { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<ServiceGroup> ServiceGroups { get; set; }
        public DbSet<GalleryGroup> GalleryGroups { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<TextItem> TextItems { get; set; }
        public DbSet<TextItemType> TextItemTypes { get; set; }
        public DbSet<ContactUsForm> ContactUsForms { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        public System.Data.Entity.DbSet<Models.ServiceTopic> ServiceTopics { get; set; }
    }
}
