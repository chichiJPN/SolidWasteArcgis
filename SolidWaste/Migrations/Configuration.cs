namespace SolidWaste.Migrations
{
    using SolidWaste.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<SolidWaste.Models.SolidWasteDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SolidWaste.Models.SolidWasteDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
               //context.People.AddOrUpdate(
               //   p => p.FullName,
               //   new Person { FullName = "Andrew Peters" },
               //   new Person { FullName = "Brice Lambson" },
               //   new Person { FullName = "Rowan Miller" }
               // );
            var districts = new List<District>
            {
                new District {  Name = "Pamplemousses", 
                                image = "district/Pamplemousses.jpg", 
                                marker_x_coordinate = 57.559, 
                                marker_y_coordinate = -20.077,
                                zoomed_x_coordinate = 57.559,
                                zoomed_y_coordinate = -20.112,
                                search_xmin = 57.491,
                                search_xmax = 57.641,
                                search_ymin = -20.004,
                                search_ymax = -20.210,
                                FID = 5
                             },
                new District {  Name = "Riviere du Rempart", 
                                image = "district/Pamplemousses.jpg", 
                                marker_x_coordinate = 57.650, 
                                marker_y_coordinate = -20,
                                zoomed_x_coordinate = 57.650,
                                zoomed_y_coordinate = -20,
                                search_xmin = 57.491,
                                search_xmax = 57.641,
                                search_ymin = -20.004,
                                search_ymax = -20.210,
                                FID = 8
                            },
                new District {  Name = "Flacq", 
                                image = "district/Pamplemousses.jpg", 
                                marker_x_coordinate = 57.701, 
                                marker_y_coordinate = -20.210,
                                zoomed_x_coordinate = 57.701,
                                zoomed_y_coordinate = -20.210,
                                search_xmin = 57.491,
                                search_xmax = 57.641,
                                search_ymin = -20.004,
                                search_ymax = -20.210,
                                FID = 2
                             },
                new District {  Name = "Moka", 
                                image = "district/Pamplemousses.jpg", 
                                marker_x_coordinate = 57.573, 
                                marker_y_coordinate = -20.240,
                                zoomed_x_coordinate = 57.573,
                                zoomed_y_coordinate = -20.240,
                                search_xmin = 57.491,
                                search_xmax = 57.641,
                                search_ymin = -20.004,
                                search_ymax = -20.210,

                                FID = 4
                             },
                new District {  Name = "Grand Port", 
                                image = "district/Pamplemousses.jpg", 
                                marker_x_coordinate = 57.643, 
                                marker_y_coordinate = -20.384,
                                zoomed_x_coordinate = 57.643,
                                zoomed_y_coordinate = -20.384,
                                search_xmin = 57.491,
                                search_xmax = 57.641,
                                search_ymin = -20.004,
                                search_ymax = -20.210,

                                FID = 3
                            },
                new District { 
                                Name = "Plaines Wilhems", 
                                image = "district/Pamplemousses.jpg", 
                                marker_x_coordinate = 57.500, 
                                marker_y_coordinate = -20.305,
                                zoomed_x_coordinate = 57.500,
                                zoomed_y_coordinate = -20.305,
                                search_xmin = 57.491,
                                search_xmax = 57.641,
                                search_ymin = -20.004,
                                search_ymax = -20.210,

                                FID = 6
                            },
                new District {  Name = "Savanne", 
                                image = "district/Pamplemousses.jpg", 
                                marker_x_coordinate = 57.484, 
                                marker_y_coordinate = -20.431,
                                zoomed_x_coordinate = 57.484,
                                zoomed_y_coordinate = -20.431,
                                search_xmin = 57.491,
                                search_xmax = 57.641,
                                search_ymin = -20.004,
                                search_ymax = -20.210,

                                FID = 3
                             },
                new District {  Name = "Black River", 
                                image = "district/Pamplemousses.jpg", 
                                marker_x_coordinate = 57.402,
                                marker_y_coordinate = -20.273,
                                zoomed_x_coordinate = 57.402,
                                zoomed_y_coordinate = -20.273,
                                search_xmin = 57.491,
                                search_xmax = 57.641,
                                search_ymin = -20.004,
                                search_ymax = -20.210,

                                FID = 9
                            },
                new District { 
                                Name = "Port Louis", 
                                image = "district/Pamplemousses.jpg", 
                                marker_x_coordinate = 57.505, 
                                marker_y_coordinate = -20.150,
                                zoomed_x_coordinate = 57.505,
                                zoomed_y_coordinate = -20.150,
                                search_xmin = 57.491,
                                search_xmax = 57.641,
                                search_ymin = -20.004,
                                search_ymax = -20.210,

                                FID = 7
                            }
            };

            districts.ForEach(s =>context.Districts.AddOrUpdate(p => p.Name, s));

            context.SaveChanges();

            var municipalities = new List<Municipality>
            {
                new Municipality {DistrictID = districts.Single(s => s.Name == "Black River").DistrictID,Name = "Albion"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Amaury"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Amaury"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Amitié-Gokhoola"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Anse La Raie"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Arsenal", population = 169,boundary = "[[57.528,-20.051],[57.528,-20.101],[57.628,-20.101],[57.628,-20.051]]"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Savanne").DistrictID,Name = "Baie du Cap"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Baie du Tombeau",boundary = "[[57.528,-20.051],[57.528,-20.101],[57.628,-20.101],[57.628,-20.051]]"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Black River").DistrictID,Name = "Bambous"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Bambous Virieux"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Bananes"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Beau Vallon"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Bel Air Rivière Sèche"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Savanne").DistrictID,Name = "Bel Ombre"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Belle Vue Maurel"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Savanne").DistrictID,Name = "Bénarès"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Savanne").DistrictID,Name = "Bois Chéri"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Bois des Amourettes"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Bon Accueil"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Brisée Verdière"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Brisée Verdière"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Savanne").DistrictID,Name = "Britannia"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Calebasses",boundary = "[[57.528,-20.051],[57.528,-20.101],[57.628,-20.101],[57.628,-20.051]]"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Camp de Masque"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Camp de Masque Pavé"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Savanne").DistrictID,Name = "Camp Diable"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Camp Ithier"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "Camp Thorel"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Cap Malheureux"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Black River").DistrictID,Name = "Cascavelle"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Plaines Wilhems").DistrictID,Name = "Cascavelle"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Black River").DistrictID,Name = "Case Noyale"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Centre de Flacq"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Black River").DistrictID,Name = "Chamarel"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Savanne").DistrictID,Name = "Chamarel"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Savanne").DistrictID,Name = "Chamouny"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Savanne").DistrictID,Name = "Chemin Grenier"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Clémencia"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Cluny"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Congomah"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Cottage"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Crève Coeur"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "D'Épinay"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "Dagotière"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "Dubreuil"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Dubreuil"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Écroignard"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "Espérance"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Espérance Trébuchet"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Black River").DistrictID,Name = "Flic en Flac"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Fond du Sac"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Goodlands"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Grand Baie"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Grand Baie"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Grand Bel Air"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Savanne").DistrictID,Name = "Grand Bois"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Grand Gaube"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Grand Sable"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Black River").DistrictID,Name = "Grande Black River"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Grande Rivière Sud Est"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Black River").DistrictID,Name = "Gros Cailloux"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "L'Avenir"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "L'Escalier"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Savanne").DistrictID,Name = "L'Escalier"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Black River").DistrictID,Name = "La Gaulette"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "La Laura-Malenga"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Lalmatie"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Laventure"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Le Hochet"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Black River").DistrictID,Name = "Le Morne"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "The Vale"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Mahébourg"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Mapou"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Mapou"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Mare Chicose"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Mare d'Albert"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Mare La Chaux"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Mare Tabac"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Médine Camp de Masque"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "Médine Camp de Masque"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "Melrose"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Plaines Wilhems").DistrictID,Name = "Midlands"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "Moka"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Plaines Wilhems").DistrictID,Name = "Moka"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "Montagne Blanche"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Montagne Blanche"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Montagne Longue"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Morcellement Saint André"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "New Grove"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Notre Dame"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "Nouvelle Découverte"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Nouvelle France"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Olivia"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "Pailles"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Pamplemousses"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Petit Bel Air"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Petit Raffray"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Black River").DistrictID,Name = "Petite Rivière"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Piton"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Piton"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Plaine des Papayes"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Plaine des Roches"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Plaine des Roches"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Plaine Magnien"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Pointe aux Piments"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Poste de Flacq"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Poudre d'Or"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Poudre d'Or Hamlet"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "Providence"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "Quartier Militaire"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Quatre Cocos"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Quatre Soeurs"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Queen Victoria"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Black River").DistrictID,Name = "Richelieu"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Savanne").DistrictID,Name = "Rivière des Anguilles"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Rivière des Créoles"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Rivière du Poste"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Savanne").DistrictID,Name = "Rivière du Poste"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Riviere du Rempart"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Roche Terre"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Roches Noires"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Rose Belle"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Savanne").DistrictID,Name = "Saint Aubin"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Saint Hubert"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Saint Julien Village"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Saint Julien d'Hotman"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "Saint Julien d'Hotman"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "Saint Pierre"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Sébastopol"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Savanne").DistrictID,Name = "Souillac"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Savanne").DistrictID,Name = "Surinam"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Black River").DistrictID,Name = "Tamarin"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Terre Rouge"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Triolet"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Trois Boutiques"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Trou aux Biches"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Flacq").DistrictID,Name = "Trou d'Eau Douce"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Union Park"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Vale"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Moka").DistrictID,Name = "Verdun"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Grand Port").DistrictID,Name = "Vieux Grand Port"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Pamplemousses").DistrictID,Name = "Ville Bague"},
                new Municipality {DistrictID = districts.Single(s => s.Name == "Riviere du Rempart").DistrictID,Name = "Ville Bague"}
            };
            municipalities.ForEach(s => context.Municipalities.AddOrUpdate(p => new { p.DistrictID, p.Name }, s));
            context.SaveChanges();

            SeedMembership();
        }

        private void SeedMembership()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Admin")) {
                roles.CreateRole("Admin");
            }

            if (membership.GetUser("junichi", false) == null) {
                membership.CreateUserAndAccount("junichi", "junichi");
            }

            if (!roles.GetRolesForUser("junichi").Contains("Admin")) {
                roles.AddUsersToRoles(new[] { "junichi" }, new[] { "Admin" });
            }
        }
    }
}
