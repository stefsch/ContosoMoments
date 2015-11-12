﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using ContosoMoments.Common.Models;
using Microsoft.Azure.Mobile.Server.Tables;

namespace ContosoMoments.MobileServer.Models
{

    public class MobileServiceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        //
        // To enable Entity Framework migrations in the cloud, please ensure that the 
        // service name, set by the 'MS_MobileServiceName' AppSettings in the local 
        // Web.config, is the same as the service name when hosted in Azure.

        private const string connectionStringName = "Name=MS_TableConnectionString";

        public MobileServiceContext() : base(connectionStringName)
        {

           // Database.SetInitializer(new MigrateDatabaseToLatestVersion<MobileServiceContext, Migrations.Configuration>());
//            var migrator = new System.Data.Entity.Migrations.DbMigrator(new Migrations.Configuration());
//migrator.Update();
          //  Database.SetInitializer(new BasicDBInitializer());
        }

        public DbSet<Image> Images { get; set; }

       

        public System.Data.Entity.DbSet<Album> Albums { get; set; }

        public System.Data.Entity.DbSet<User> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
               new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                   "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
            base.OnModelCreating(modelBuilder);
        }
    }

    public class ContosoMomentsDBInitializer : DropCreateDatabaseAlways<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            var defaultAlbum = new Album
            {
                Id = "11111111-1111-1111-1111-111111111111",
                AlbumName = "Default Album",
                User = new User
                {
                    UserName = "Demo User",
                    Id = "11111111-1111-1111-1111-111111111111",
                    IsEnabled = true
                }
            };

            context.Albums.Add(defaultAlbum);
            context.SaveChanges();
            base.Seed(context);
        }
    }


}
