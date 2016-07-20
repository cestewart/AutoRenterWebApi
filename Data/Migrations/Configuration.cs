using System.Data.Entity.ModelConfiguration.Conventions;
using System.Runtime.Serialization;

namespace Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.AutoRenterDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            CommandTimeout = 500;
        }

        protected override void Seed(Data.AutoRenterDatabaseContext context)
        {
            try
            {
                context.States.AddOrUpdate(x => x.StateId,
                    new State {StateId = 1, Abbreviation = "AL", Name = "Alabama"},
                    new State {StateId = 2, Abbreviation = "AK", Name = "Alaska"},
                    new State {StateId = 3, Abbreviation = "AZ", Name = "Arizona"},
                    new State {StateId = 4, Abbreviation = "AR", Name = "Arkansas"},
                    new State {StateId = 5, Abbreviation = "CA", Name = "California"},
                    new State {StateId = 6, Abbreviation = "CO", Name = "Colorado"},
                    new State {StateId = 7, Abbreviation = "CT", Name = "Connecticut"},
                    new State {StateId = 8, Abbreviation = "DE", Name = "Delaware"},
                    new State {StateId = 9, Abbreviation = "FL", Name = "Florida"},
                    new State {StateId = 10, Abbreviation = "GA", Name = "Georgia"},
                    new State {StateId = 11, Abbreviation = "HI", Name = "Hawaii"},
                    new State {StateId = 12, Abbreviation = "ID", Name = "Idaho"},
                    new State {StateId = 13, Abbreviation = "IL", Name = "Illinois"},
                    new State {StateId = 14, Abbreviation = "IN", Name = "Indiana"},
                    new State {StateId = 15, Abbreviation = "IA", Name = "Iowa"},
                    new State {StateId = 16, Abbreviation = "KS", Name = "Kansas"},
                    new State {StateId = 17, Abbreviation = "KY", Name = "Kentucky"},
                    new State {StateId = 18, Abbreviation = "LA", Name = "Louisiana"},
                    new State {StateId = 19, Abbreviation = "ME", Name = "Maine"},
                    new State {StateId = 20, Abbreviation = "MD", Name = "Maryland"},
                    new State {StateId = 21, Abbreviation = "MA", Name = "Massachusetts"},
                    new State {StateId = 22, Abbreviation = "MI", Name = "Michigan"},
                    new State {StateId = 23, Abbreviation = "MN", Name = "Minnesota"},
                    new State {StateId = 24, Abbreviation = "MS", Name = "Mississippi"},
                    new State {StateId = 25, Abbreviation = "MO", Name = "Missouri"},
                    new State {StateId = 26, Abbreviation = "MT", Name = "Montana"},
                    new State {StateId = 27, Abbreviation = "NE", Name = "Nebraska"},
                    new State {StateId = 28, Abbreviation = "NV", Name = "Nevada"},
                    new State {StateId = 29, Abbreviation = "NH", Name = "New Hampshire"},
                    new State {StateId = 30, Abbreviation = "NJ", Name = "New Jersey"},
                    new State {StateId = 31, Abbreviation = "NM", Name = "New Mexico"},
                    new State {StateId = 32, Abbreviation = "NY", Name = "New York"},
                    new State {StateId = 33, Abbreviation = "NC", Name = "North Carolina"},
                    new State {StateId = 34, Abbreviation = "ND", Name = "North Dakota"},
                    new State {StateId = 35, Abbreviation = "OH", Name = "Ohio"},
                    new State {StateId = 36, Abbreviation = "OK", Name = "Oklahoma"},
                    new State {StateId = 37, Abbreviation = "OR", Name = "Oregon"},
                    new State {StateId = 38, Abbreviation = "PA", Name = "Pennsylvania"},
                    new State {StateId = 39, Abbreviation = "RI", Name = "Rhode Island"},
                    new State {StateId = 40, Abbreviation = "SC", Name = "South Carolina"},
                    new State {StateId = 41, Abbreviation = "SD", Name = "South Dakota"},
                    new State {StateId = 42, Abbreviation = "TN", Name = "Tennessee"},
                    new State {StateId = 43, Abbreviation = "TX", Name = "Texas"},
                    new State {StateId = 44, Abbreviation = "UT", Name = "Utah"},
                    new State {StateId = 45, Abbreviation = "VT", Name = "Vermont"},
                    new State {StateId = 46, Abbreviation = "VA", Name = "Virginia"},
                    new State {StateId = 47, Abbreviation = "WA", Name = "Washington"},
                    new State {StateId = 48, Abbreviation = "WV", Name = "West Virginia"},
                    new State {StateId = 49, Abbreviation = "WI", Name = "Wisconsin"},
                    new State {StateId = 50, Abbreviation = "WY", Name = "Wyoming"},
                    new State {StateId = 51, Abbreviation = "DC", Name = "District of Columbia"}
                    );

                context.Users.AddOrUpdate(x => x.UserId, 
                    new User
                    {
                        UserId = 1, Username = "cstewart", FirstName = "Chris", LastName = "Stewart", Email= "cstewart@fusionalliance.com", BrandingAdministrator = true, UserAdministrator = true, FleetAdministrator = true, HashOfPassword = "foo"}
                    );

                context.SaveChanges();

                context.Locations.AddOrUpdate(x => x.LocationId,
                    new Location
                    {
                        LocationId = 8277,
                        City = "Loring",
                        Name = "Loring Seaplane Base",
                        SiteId = "13Z",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8278,
                        City = "Nunapitchuk",
                        Name = "Nunapitchuk Airport",
                        SiteId = "16A",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8279,
                        City = "Kalakaket Creek",
                        Name = "Kalakaket Creek AS Airport",
                        SiteId = "1KC",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8280,
                        City = "Lime Village",
                        Name = "Lime Village Airport",
                        SiteId = "2AK",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8281,
                        City = "False Island",
                        Name = "False Island Seaplane Base",
                        SiteId = "2Z6",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8282,
                        City = "Golden Horn Lodge",
                        Name = "Golden Horn Lodge Seaplane Base",
                        SiteId = "3Z8",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8283,
                        City = "Atmautluak",
                        Name = "Atmautluak Airport",
                        SiteId = "4A2",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8284,
                        City = "Livengood",
                        Name = "Livengood Camp Airport",
                        SiteId = "4AK",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8285,
                        City = "Pedro Bay",
                        Name = "Pedro Bay Airport",
                        SiteId = "4K0",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8286,
                        City = "Ouzinkie",
                        Name = "Ouzinkie Airport",
                        SiteId = "4K5",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8287,
                        City = "Blaine",
                        Name = "Blaine Municipal Airport",
                        SiteId = "4W6",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "WA").StateId
                    },
                    new Location
                    {
                        LocationId = 8288,
                        City = "Hyder",
                        Name = "Hyder Seaplane Base",
                        SiteId = "4Z7",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8289,
                        City = "Tokeen",
                        Name = "Tokeen Seaplane Base",
                        SiteId = "57A",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8290,
                        City = "Aleknagik",
                        Name = "Aleknagik / New Airport",
                        SiteId = "5A8",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8291,
                        City = "Ketchikan",
                        Name = "Ketchikan Harbor Seaplane Base",
                        SiteId = "5KE",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8292,
                        City = "New York",
                        Name = "East 34th Street Heliport",
                        SiteId = "6N5",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NY").StateId
                    },
                    new Location
                    {
                        LocationId = 8293,
                        City = "New York",
                        Name = "New York Skyports Inc Seaplane Base",
                        SiteId = "6N7",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NY").StateId
                    },
                    new Location
                    {
                        LocationId = 8294,
                        City = "Yes Bay",
                        Name = "Yes Bay Lodge Seaplane Base",
                        SiteId = "78K",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8295,
                        City = "Tatitlek",
                        Name = "Tatitlek Airport",
                        SiteId = "7KA",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8296,
                        City = "Meyers Chuck",
                        Name = "Meyers Chuck Seaplane Base",
                        SiteId = "84K",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8297,
                        City = "North Whale Pass",
                        Name = "North Whale Seaplane Base",
                        SiteId = "96Z",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8298,
                        City = "Chuathbaluk",
                        Name = "Chuathbaluk Airport",
                        SiteId = "9A3",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8299,
                        City = "Levelock",
                        Name = "Levelock Airport",
                        SiteId = "9Z8",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8300,
                        City = "Saginaw Bay",
                        Name = "Saginaw Seaplane Base",
                        SiteId = "A23",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8301,
                        City = "Tuntutuliak",
                        Name = "Tuntutuliak Airport",
                        SiteId = "A61",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8302,
                        City = "Chignik Lake",
                        Name = "Chignik Lake Airport",
                        SiteId = "A79",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8303,
                        City = "Lazy Bay",
                        Name = "Alitak Seaplane Base",
                        SiteId = "ALZ",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8304,
                        City = "Girdwood",
                        Name = "Girdwood Airport",
                        SiteId = "AQY",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8305,
                        City = "Branson",
                        Name = "Branson Airport",
                        SiteId = "BBG",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MO").StateId
                    },
                    new Location
                    {
                        LocationId = 8306,
                        City = "Willmar",
                        Name = "Willmar Municipal -John L Rice Field",
                        SiteId = "BDH",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MN").StateId
                    },
                    new Location
                    {
                        LocationId = 8307,
                        City = "Baranof",
                        Name = "Warm Spring Bay Seaplane Base",
                        SiteId = "BNF",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8308,
                        City = "Washington",
                        Name = "Bolling Air Force Base",
                        SiteId = "BOF",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "DC").StateId
                    },
                    new Location
                    {
                        LocationId = 8309,
                        City = "Brookings",
                        Name = "Brookings Airport",
                        SiteId = "BOK",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "OR").StateId
                    },
                    new Location
                    {
                        LocationId = 8310,
                        City = "Gustavus",
                        Name = "Bartlett Cove Seaplane Base",
                        SiteId = "BQV",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8311,
                        City = "Blairsville",
                        Name = "Blairsville",
                        SiteId = "BSI",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "PA").StateId
                    },
                    new Location
                    {
                        LocationId = 8312,
                        City = "Boundary",
                        Name = "Boundary Airport",
                        SiteId = "BYA",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8313,
                        City = "Cedar Key",
                        Name = "George T Lewis Airport",
                        SiteId = "CDK",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "FL").StateId
                    },
                    new Location
                    {
                        LocationId = 8314,
                        City = "Craig",
                        Name = "Craig Seaplane Base",
                        SiteId = "CGA",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8315,
                        City = "Circle Hot Springs",
                        Name = "Circle Hot Springs Airport",
                        SiteId = "CHP",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8316,
                        City = "Crooked Creek",
                        Name = "Crooked Creek Airport",
                        SiteId = "CJX",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8317,
                        City = "Cordova",
                        Name = "Cordova Municipal Airport",
                        SiteId = "CKU",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8318,
                        City = "Chicken",
                        Name = "Chicken Airport",
                        SiteId = "CKX",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8319,
                        City = "Hollister",
                        Name = "Hollister Municipal Airport",
                        SiteId = "CVH",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8320,
                        City = "Chitina",
                        Name = "Chitina Airport",
                        SiteId = "CXC",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8321,
                        City = "Chatham",
                        Name = "Chatham Seaplane Base",
                        SiteId = "CYM",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8322,
                        City = "Chisana",
                        Name = "Chisana Airport",
                        SiteId = "CZN",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8323,
                        City = "Delta Junction",
                        Name = "Delta Junction Airport",
                        SiteId = "D66",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8324,
                        City = "Dahl Creek",
                        Name = "Dahl Creek Airport",
                        SiteId = "DCK",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8325,
                        City = "Decatur",
                        Name = "Decatur HI-Way Airfield",
                        SiteId = "DCR",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "IN").StateId
                    },
                    new Location
                    {
                        LocationId = 8326,
                        City = "Excursion Inlet",
                        Name = "Excursion Inlet Seaplane Base",
                        SiteId = "EXI",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8327,
                        City = "Cheyenne",
                        Name = "Francis E Warren Air Force Base",
                        SiteId = "FEW",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "WY").StateId
                    },
                    new Location
                    {
                        LocationId = 8328,
                        City = "Flat",
                        Name = "Flat Airport",
                        SiteId = "FLT",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8329,
                        City = "Gabbs",
                        Name = "Gabbs Airport",
                        SiteId = "GAB",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NV").StateId
                    },
                    new Location
                    {
                        LocationId = 8330,
                        City = "Greenfield",
                        Name = "Pope Field",
                        SiteId = "GFD",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "IN").StateId
                    },
                    new Location
                    {
                        LocationId = 8331,
                        City = "Goodnews",
                        Name = "Goodnews Airport",
                        SiteId = "GNU",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8332,
                        City = "Granite Mountain",
                        Name = "Granite Mountain Air Station",
                        SiteId = "GSZ",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8333,
                        City = "Gordonsville",
                        Name = "Gordonsville Municipal Airport",
                        SiteId = "GVE",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "VA").StateId
                    },
                    new Location
                    {
                        LocationId = 8334,
                        City = "Haycock",
                        Name = "Haycock Airport",
                        SiteId = "HAY",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8335,
                        City = "Fort Rucker Ozark",
                        Name = "Hanchey Army (Fort Rucker) Heliport",
                        SiteId = "HEY",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AL").StateId
                    },
                    new Location
                    {
                        LocationId = 8336,
                        City = "Fort Hunter Ligget Jolon",
                        Name = "Tusi AHP (Hunter Liggett) Heliport",
                        SiteId = "HGT",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8337,
                        City = "Tahneta Pass Lodge",
                        Name = "Tahneta Pass Airport",
                        SiteId = "HNE",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8338,
                        City = "Hollis",
                        Name = "Hollis Clark Bay Seaplane Base",
                        SiteId = "HYL",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8339,
                        City = "Cooper Landing",
                        Name = "Quartz Creek Airport",
                        SiteId = "JLA",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8340,
                        City = "Sausalito",
                        Name = "Commodore Center Heliport",
                        SiteId = "JMC",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8341,
                        City = "Washington",
                        Name = "Pentagon Army Heliport",
                        SiteId = "JPN",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "DC").StateId
                    },
                    new Location
                    {
                        LocationId = 8342,
                        City = "New York",
                        Name = "West 30th St. Heliport",
                        SiteId = "JRA",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NY").StateId
                    },
                    new Location
                    {
                        LocationId = 8343,
                        City = "New York",
                        Name = "Downtown-Manhattan/Wall St Heliport",
                        SiteId = "JRB",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NY").StateId
                    },
                    new Location
                    {
                        LocationId = 8344,
                        City = "Durango",
                        Name = "Animas Air Park",
                        SiteId = "00C",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CO").StateId
                    },
                    new Location
                    {
                        LocationId = 8345,
                        City = "Broadus",
                        Name = "Broadus Airport",
                        SiteId = "00F",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MT").StateId
                    },
                    new Location
                    {
                        LocationId = 8348,
                        City = "Rolla",
                        Name = "Rolla Downtown Airport",
                        SiteId = "K07",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MO").StateId
                    },
                    new Location
                    {
                        LocationId = 8443,
                        City = "Alpena",
                        Name = "Alpena County Regional Airport",
                        SiteId = "APN",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MI").StateId
                    },
                    new Location
                    {
                        LocationId = 8444,
                        City = "Jasper",
                        Name = "Marion County Brown Field",
                        SiteId = "APT",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "TN").StateId
                    },
                    new Location
                    {
                        LocationId = 8445,
                        City = "Apple Valley",
                        Name = "Apple Valley Airport",
                        SiteId = "APV",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8446,
                        City = "New Iberia",
                        Name = "Acadiana Regional Airport",
                        SiteId = "ARA",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "LA").StateId
                    },
                    new Location
                    {
                        LocationId = 8447,
                        City = "Ann Arbor",
                        Name = "Ann Arbor Municipal Airport",
                        SiteId = "ARB",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MI").StateId
                    },
                    new Location
                    {
                        LocationId = 8448,
                        City = "Walnut Ridge",
                        Name = "Walnut Ridge Regional Airport",
                        SiteId = "ARG",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AR").StateId
                    },
                    new Location
                    {
                        LocationId = 8449,
                        City = "Wharton",
                        Name = "Wharton Regional Airport",
                        SiteId = "ARM",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "TX").StateId
                    },
                    new Location
                    {
                        LocationId = 8450,
                        City = "Chicago/Aurora",
                        Name = "Aurora Municipal Airport",
                        SiteId = "ARR",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "IL").StateId
                    },
                    new Location
                    {
                        LocationId = 8451,
                        City = "Watertown",
                        Name = "Watertown International Airport",
                        SiteId = "ART",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NY").StateId
                    },
                    new Location
                    {
                        LocationId = 8452,
                        City = "Minocqua-Woodruff",
                        Name = "Lakeland-Noble F. Lee Memorial field",
                        SiteId = "ARV",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "WI").StateId
                    },
                    new Location
                    {
                        LocationId = 8453,
                        City = "Beaufort",
                        Name = "Beaufort County Airport",
                        SiteId = "ARW",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "SC").StateId
                    },
                    new Location
                    {
                        LocationId = 8454,
                        City = "Aspen",
                        Name = "Aspen-Pitkin Co/Sardy Field",
                        SiteId = "ASE",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CO").StateId
                    },
                    new Location
                    {
                        LocationId = 8455,
                        City = "Springdale",
                        Name = "Springdale Municipal Airport",
                        SiteId = "ASG",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AR").StateId
                    },
                    new Location
                    {
                        LocationId = 8456,
                        City = "Nashua",
                        Name = "Boire Field",
                        SiteId = "ASH",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NH").StateId
                    },
                    new Location
                    {
                        LocationId = 8457,
                        City = "Marshall",
                        Name = "Harrison County Airport",
                        SiteId = "ASL",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "TX").StateId
                    },
                    new Location
                    {
                        LocationId = 8458,
                        City = "Talladega",
                        Name = "Talladega Municipal Airport",
                        SiteId = "ASN",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AL").StateId
                    },
                    new Location
                    {
                        LocationId = 8459,
                        City = "Astoria",
                        Name = "Astoria Regional Airport",
                        SiteId = "AST",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "OR").StateId
                    },
                    new Location
                    {
                        LocationId = 8460,
                        City = "Ashland",
                        Name = "John F Kennedy Memorial Airport",
                        SiteId = "ASX",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "WI").StateId
                    },
                    new Location
                    {
                        LocationId = 8461,
                        City = "Ashley",
                        Name = "Ashley Municipal Airport",
                        SiteId = "ASY",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "ND").StateId
                    },
                    new Location
                    {
                        LocationId = 8462,
                        City = "Atlanta",
                        Name = "Hartsfield Jackson Atlanta International Airport",
                        SiteId = "ATL",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "GA").StateId
                    },
                    new Location
                    {
                        LocationId = 8463,
                        City = "Artesia",
                        Name = "Artesia Municipal Airport",
                        SiteId = "ATS",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NM").StateId
                    },
                    new Location
                    {
                        LocationId = 8464,
                        City = "Appleton",
                        Name = "Outagamie County Regional Airport",
                        SiteId = "ATW",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "WI").StateId
                    },
                    new Location
                    {
                        LocationId = 8465,
                        City = "Watertown",
                        Name = "Watertown Regional Airport",
                        SiteId = "ATY",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "SD").StateId
                    },
                    new Location
                    {
                        LocationId = 8466,
                        City = "Augusta",
                        Name = "Augusta State Airport",
                        SiteId = "AUG",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "ME").StateId
                    },
                    new Location
                    {
                        LocationId = 8467,
                        City = "Austin",
                        Name = "Austin Municipal Airport",
                        SiteId = "AUM",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MN").StateId
                    },
                    new Location
                    {
                        LocationId = 8468,
                        City = "Auburn",
                        Name = "Auburn Municipal Airport",
                        SiteId = "AUN",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8469,
                        City = "Auburn",
                        Name = "Auburn Opelika Robert G. Pitts Airport",
                        SiteId = "AUO",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AL").StateId
                    },
                    new Location
                    {
                        LocationId = 8470,
                        City = "Austin",
                        Name = "Austin Bergstrom International Airport",
                        SiteId = "AUS",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "TX").StateId
                    },
                    new Location
                    {
                        LocationId = 8471,
                        City = "Wausau",
                        Name = "Wausau Downtown Airport",
                        SiteId = "AUW",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "WI").StateId
                    },
                    new Location
                    {
                        LocationId = 8472,
                        City = "Asheville",
                        Name = "Asheville Regional Airport",
                        SiteId = "AVL",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NC").StateId
                    },
                    new Location
                    {
                        LocationId = 8473,
                        City = "Avon Park",
                        Name = "Avon Park Executive Airport",
                        SiteId = "AVO",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "FL").StateId
                    },
                    new Location
                    {
                        LocationId = 8474,
                        City = "Wilkes-Barre/Scranton",
                        Name = "Wilkes Barre Scranton International Airport",
                        SiteId = "AVP",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "PA").StateId
                    },
                    new Location
                    {
                        LocationId = 8475,
                        City = "Tucson",
                        Name = "Marana Regional Airport",
                        SiteId = "AVQ",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AZ").StateId
                    },
                    new Location
                    {
                        LocationId = 8476,
                        City = "Avalon",
                        Name = "Catalina Airport",
                        SiteId = "AVX",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8477,
                        City = "West Memphis",
                        Name = "West Memphis Municipal Airport",
                        SiteId = "AWM",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AR").StateId
                    },
                    new Location
                    {
                        LocationId = 8478,
                        City = "Algona",
                        Name = "Algona Municipal Airport",
                        SiteId = "AXA",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "IA").StateId
                    },
                    new Location
                    {
                        LocationId = 8479,
                        City = "Alexandria",
                        Name = "Chandler Field",
                        SiteId = "AXN",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MN").StateId
                    },
                    new Location
                    {
                        LocationId = 8480,
                        City = "Altus",
                        Name = "Altus Quartz Mountain Regional Airport",
                        SiteId = "AXS",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "OK").StateId
                    },
                    new Location
                    {
                        LocationId = 8481,
                        City = "Wapakoneta",
                        Name = "Neil Armstrong Airport",
                        SiteId = "AXV",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "OH").StateId
                    },
                    new Location
                    {
                        LocationId = 8482,
                        City = "Angel Fire",
                        Name = "Angel Fire Airport",
                        SiteId = "AXX",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NM").StateId
                    },
                    new Location
                    {
                        LocationId = 8483,
                        City = "Waycross",
                        Name = "Waycross Ware County Airport",
                        SiteId = "AYS",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "GA").StateId
                    },
                    new Location
                    {
                        LocationId = 8583,
                        City = "Wahpeton",
                        Name = "Harry Stern Airport",
                        SiteId = "BWP",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "ND").StateId
                    },
                    new Location
                    {
                        LocationId = 8484,
                        City = "Kalamazoo",
                        Name = "Kalamazoo Battle Creek International Airport",
                        SiteId = "AZO",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MI").StateId
                    },
                    new Location
                    {
                        LocationId = 8485,
                        City = "Marysville",
                        Name = "Beale Air Force Base",
                        SiteId = "BAB",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8486,
                        City = "Bossier City",
                        Name = "Barksdale Air Force Base",
                        SiteId = "BAD",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "LA").StateId
                    },
                    new Location
                    {
                        LocationId = 8487,
                        City = "Westfield/Springfield",
                        Name = "Barnes Municipal Airport",
                        SiteId = "BAF",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MA").StateId
                    },
                    new Location
                    {
                        LocationId = 8488,
                        City = "Columbus",
                        Name = "Columbus Municipal Airport",
                        SiteId = "BAK",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "IN").StateId
                    },
                    new Location
                    {
                        LocationId = 8489,
                        City = "Battle Mountain",
                        Name = "Battle Mountain Airport",
                        SiteId = "BAM",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NV").StateId
                    },
                    new Location
                    {
                        LocationId = 8490,
                        City = "Benson",
                        Name = "Benson Municipal Airport",
                        SiteId = "BBB",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MN").StateId
                    },
                    new Location
                    {
                        LocationId = 8491,
                        City = "Brady",
                        Name = "Curtis Field",
                        SiteId = "BBD",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "TX").StateId
                    },
                    new Location
                    {
                        LocationId = 8492,
                        City = "Bennettsville",
                        Name = "Marlboro County Jetport H.E. Avent Field",
                        SiteId = "BBP",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "SC").StateId
                    },
                    new Location
                    {
                        LocationId = 8493,
                        City = "Broken Bow",
                        Name = "Broken Bow Municipal Airport",
                        SiteId = "BBW",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NE").StateId
                    },
                    new Location
                    {
                        LocationId = 8494,
                        City = "Blacksburg",
                        Name = "Virginia Tech Montgomery Executive Airport",
                        SiteId = "BCB",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "VA").StateId
                    },
                    new Location
                    {
                        LocationId = 8495,
                        City = "Bryce Canyon",
                        Name = "Bryce Canyon Airport",
                        SiteId = "BCE",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "UT").StateId
                    },
                    new Location
                    {
                        LocationId = 8496,
                        City = "Boca Raton",
                        Name = "Boca Raton Airport",
                        SiteId = "BCT",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "FL").StateId
                    },
                    new Location
                    {
                        LocationId = 8497,
                        City = "Baudette",
                        Name = "Baudette International Airport",
                        SiteId = "BDE",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MN").StateId
                    },
                    new Location
                    {
                        LocationId = 8498,
                        City = "Blanding",
                        Name = "Blanding Municipal Airport",
                        SiteId = "BDG",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "UT").StateId
                    },
                    new Location
                    {
                        LocationId = 8499,
                        City = "Hartford",
                        Name = "Bradley International Airport",
                        SiteId = "BDL",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CT").StateId
                    },
                    new Location
                    {
                        LocationId = 8500,
                        City = "Bridgeport",
                        Name = "Igor I Sikorsky Memorial Airport",
                        SiteId = "BDR",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CT").StateId
                    },
                    new Location
                    {
                        LocationId = 8501,
                        City = "Boulder",
                        Name = "Boulder Municipal Airport",
                        SiteId = "BDU",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CO").StateId
                    },
                    new Location
                    {
                        LocationId = 8502,
                        City = "Bell Island",
                        Name = "Bell Island Hot Springs Seaplane Base",
                        SiteId = "KBE",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8503,
                        City = "Wichita",
                        Name = "Beech Factory Airport",
                        SiteId = "BEC",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "KS").StateId
                    },
                    new Location
                    {
                        LocationId = 8504,
                        City = "Bedford",
                        Name = "Laurence G Hanscom Field",
                        SiteId = "BED",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MA").StateId
                    },
                    new Location
                    {
                        LocationId = 8505,
                        City = "Benton Harbor",
                        Name = "Southwest Michigan Regional Airport",
                        SiteId = "BEH",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MI").StateId
                    },
                    new Location
                    {
                        LocationId = 8506,
                        City = "Bradford",
                        Name = "Bradford Regional Airport",
                        SiteId = "BFD",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "PA").StateId
                    },
                    new Location
                    {
                        LocationId = 8507,
                        City = "Scottsbluff",
                        Name = "Western Neb. Rgnl/William B. Heilig Airport",
                        SiteId = "BFF",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NE").StateId
                    },
                    new Location
                    {
                        LocationId = 8508,
                        City = "Seattle",
                        Name = "Boeing Field King County International Airport",
                        SiteId = "BFI",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "WA").StateId
                    },
                    new Location
                    {
                        LocationId = 8509,
                        City = "Bakersfield",
                        Name = "Meadows Field",
                        SiteId = "BFL",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8510,
                        City = "Mobile",
                        Name = "Mobile Downtown Airport",
                        SiteId = "BFM",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AL").StateId
                    },
                    new Location
                    {
                        LocationId = 8511,
                        City = "Bedford",
                        Name = "Virgil I Grissom Municipal Airport",
                        SiteId = "BFR",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "IN").StateId
                    },
                    new Location
                    {
                        LocationId = 8512,
                        City = "Borger",
                        Name = "Hutchinson County Airport",
                        SiteId = "BGD",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "TX").StateId
                    },
                    new Location
                    {
                        LocationId = 8513,
                        City = "Bainbridge",
                        Name = "Decatur County Industrial Air Park",
                        SiteId = "BGE",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "GA").StateId
                    },
                    new Location
                    {
                        LocationId = 8514,
                        City = "Binghamton",
                        Name = "Greater Binghamton/Edwin A Link field",
                        SiteId = "BGM",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NY").StateId
                    },
                    new Location
                    {
                        LocationId = 8515,
                        City = "Bangor",
                        Name = "Bangor International Airport",
                        SiteId = "BGR",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "ME").StateId
                    },
                    new Location
                    {
                        LocationId = 8516,
                        City = "Bar Harbor",
                        Name = "Hancock County-Bar Harbor Airport",
                        SiteId = "BHB",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "ME").StateId
                    },
                    new Location
                    {
                        LocationId = 8517,
                        City = "Birmingham",
                        Name = "Birmingham-Shuttlesworth International Airport",
                        SiteId = "BHM",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AL").StateId
                    },
                    new Location
                    {
                        LocationId = 8518,
                        City = "Block Island",
                        Name = "Block Island State Airport",
                        SiteId = "BID",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "RI").StateId
                    },
                    new Location
                    {
                        LocationId = 8519,
                        City = "Beatrice",
                        Name = "Beatrice Municipal Airport",
                        SiteId = "BIE",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NE").StateId
                    },
                    new Location
                    {
                        LocationId = 8520,
                        City = "Fort Bliss/El Paso",
                        Name = "Biggs Army Air Field (Fort Bliss)",
                        SiteId = "BIF",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "TX").StateId
                    },
                    new Location
                    {
                        LocationId = 8521,
                        City = "Bishop",
                        Name = "Eastern Sierra Regional Airport",
                        SiteId = "BIH",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8522,
                        City = "Billings",
                        Name = "Billings Logan International Airport",
                        SiteId = "BIL",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MT").StateId
                    },
                    new Location
                    {
                        LocationId = 8523,
                        City = "Bismarck",
                        Name = "Bismarck Municipal Airport",
                        SiteId = "BIS",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "ND").StateId
                    },
                    new Location
                    {
                        LocationId = 8524,
                        City = "Biloxi",
                        Name = "Keesler Air Force Base",
                        SiteId = "BIX",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MS").StateId
                    },
                    new Location
                    {
                        LocationId = 8525,
                        City = "Denver",
                        Name = "Rocky Mountain Metropolitan Airport",
                        SiteId = "BJC",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CO").StateId
                    },
                    new Location
                    {
                        LocationId = 8526,
                        City = "Bemidji",
                        Name = "Bemidji Regional Airport",
                        SiteId = "BJI",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MN").StateId
                    },
                    new Location
                    {
                        LocationId = 8527,
                        City = "Wooster",
                        Name = "Wayne County Airport",
                        SiteId = "BJJ",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "OH").StateId
                    },
                    new Location
                    {
                        LocationId = 8528,
                        City = "Breckenridge",
                        Name = "Stephens County Airport",
                        SiteId = "BKD",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "TX").StateId
                    },
                    new Location
                    {
                        LocationId = 8529,
                        City = "Baker City",
                        Name = "Baker City Municipal Airport",
                        SiteId = "BKE",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "OR").StateId
                    },
                    new Location
                    {
                        LocationId = 8530,
                        City = "Aurora",
                        Name = "Buckley Air Force Base",
                        SiteId = "BKF",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CO").StateId
                    },
                    new Location
                    {
                        LocationId = 8531,
                        City = "Cleveland",
                        Name = "Burke Lakefront Airport",
                        SiteId = "BKL",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "OH").StateId
                    },
                    new Location
                    {
                        LocationId = 8532,
                        City = "Blackstone",
                        Name = "Allen C Perkinson Blackstone Army Air Field",
                        SiteId = "BKT",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "VA").StateId
                    },
                    new Location
                    {
                        LocationId = 8533,
                        City = "Beckley",
                        Name = "Raleigh County Memorial Airport",
                        SiteId = "BKW",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "WV").StateId
                    },
                    new Location
                    {
                        LocationId = 8534,
                        City = "Brookings",
                        Name = "Brookings Regional Airport",
                        SiteId = "BKX",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "SD").StateId
                    },
                    new Location
                    {
                        LocationId = 8535,
                        City = "Bluefield",
                        Name = "Mercer County Airport",
                        SiteId = "BLF",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "WV").StateId
                    },
                    new Location
                    {
                        LocationId = 8536,
                        City = "Blythe",
                        Name = "Blythe Airport",
                        SiteId = "BLH",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8537,
                        City = "Bellingham",
                        Name = "Bellingham International Airport",
                        SiteId = "BLI",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "WA").StateId
                    },
                    new Location
                    {
                        LocationId = 8538,
                        City = "Belmar/Farmingdale",
                        Name = "Monmouth Executive Airport",
                        SiteId = "BLM",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NJ").StateId
                    },
                    new Location
                    {
                        LocationId = 8539,
                        City = "Emigrant Gap",
                        Name = "Blue Canyon Nyack Airport",
                        SiteId = "BLU",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8540,
                        City = "Belleville",
                        Name = "Scott AFB/Midamerica Airport",
                        SiteId = "BLV",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "IL").StateId
                    },
                    new Location
                    {
                        LocationId = 8541,
                        City = "Brigham City",
                        Name = "Brigham City Airport",
                        SiteId = "BMC",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "UT").StateId
                    },
                    new Location
                    {
                        LocationId = 8542,
                        City = "Bloomington",
                        Name = "Monroe County Airport",
                        SiteId = "BMG",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "IN").StateId
                    },
                    new Location
                    {
                        LocationId = 8543,
                        City = "Bloomington/Normal",
                        Name = "Central Illinois Regional Airport at Bloomington-Normal",
                        SiteId = "BMI",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "IL").StateId
                    },
                    new Location
                    {
                        LocationId = 8544,
                        City = "Berlin",
                        Name = "Berlin Regional Airport",
                        SiteId = "BML",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NH").StateId
                    },
                    new Location
                    {
                        LocationId = 8545,
                        City = "Beaumont",
                        Name = "Beaumont Municipal Airport",
                        SiteId = "BMT",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "TX").StateId
                    },
                    new Location
                    {
                        LocationId = 8546,
                        City = "Nashville",
                        Name = "Nashville International Airport",
                        SiteId = "BNA",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "TN").StateId
                    },
                    new Location
                    {
                        LocationId = 8547,
                        City = "Banning",
                        Name = "Banning Municipal Airport",
                        SiteId = "BNG",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8548,
                        City = "Barnwell",
                        Name = "Barnwell Regional Airport",
                        SiteId = "BNL",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "SC").StateId
                    },
                    new Location
                    {
                        LocationId = 8549,
                        City = "Burns",
                        Name = "Burns Municipal Airport",
                        SiteId = "BNO",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "OR").StateId
                    },
                    new Location
                    {
                        LocationId = 8550,
                        City = "Boone",
                        Name = "Boone Municipal Airport",
                        SiteId = "BNW",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "IA").StateId
                    },
                    new Location
                    {
                        LocationId = 8551,
                        City = "Boise",
                        Name = "Boise Air Terminal/Gowen field",
                        SiteId = "BOI",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "ID").StateId
                    },
                    new Location
                    {
                        LocationId = 8552,
                        City = "Boston",
                        Name = "General Edward Lawrence Logan International Airport",
                        SiteId = "BOS",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MA").StateId
                    },
                    new Location
                    {
                        LocationId = 8553,
                        City = "Bartow",
                        Name = "Bartow Municipal Airport",
                        SiteId = "BOW",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "FL").StateId
                    },
                    new Location
                    {
                        LocationId = 8554,
                        City = "Big Spring",
                        Name = "Big Spring Mc Mahon-Wrinkle Airport",
                        SiteId = "BPG",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "TX").StateId
                    },
                    new Location
                    {
                        LocationId = 8555,
                        City = "Big Piney",
                        Name = "Miley Memorial Field",
                        SiteId = "BPI",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "WY").StateId
                    },
                    new Location
                    {
                        LocationId = 8556,
                        City = "Mountain Home",
                        Name = "Ozark Regional Airport",
                        SiteId = "BPK",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AR").StateId
                    },
                    new Location
                    {
                        LocationId = 8557,
                        City = "Bowman",
                        Name = "Bowman Municipal Airport",
                        SiteId = "BPP",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "ND").StateId
                    },
                    new Location
                    {
                        LocationId = 8558,
                        City = "Beaumont/Port Arthur",
                        Name = "Southeast Texas Regional Airport",
                        SiteId = "BPT",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "TX").StateId
                    },
                    new Location
                    {
                        LocationId = 8559,
                        City = "Brunswick",
                        Name = "Brunswick Golden Isles Airport",
                        SiteId = "BQK",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "GA").StateId
                    },
                    new Location
                    {
                        LocationId = 8560,
                        City = "Brainerd",
                        Name = "Brainerd Lakes Regional Airport",
                        SiteId = "BRD",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MN").StateId
                    },
                    new Location
                    {
                        LocationId = 8561,
                        City = "Burlington",
                        Name = "Southeast Iowa Regional Airport",
                        SiteId = "BRL",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "IA").StateId
                    },
                    new Location
                    {
                        LocationId = 8562,
                        City = "Brownsville",
                        Name = "Brownsville South Padre Island International Airport",
                        SiteId = "BRO",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "TX").StateId
                    },
                    new Location
                    {
                        LocationId = 8563,
                        City = "Bardstown",
                        Name = "Samuels Field",
                        SiteId = "BRY",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "KY").StateId
                    },
                    new Location
                    {
                        LocationId = 8564,
                        City = "Bountiful",
                        Name = "Skypark Airport",
                        SiteId = "BTF",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "UT").StateId
                    },
                    new Location
                    {
                        LocationId = 8565,
                        City = "Battle Creek",
                        Name = "W K Kellogg Airport",
                        SiteId = "BTL",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MI").StateId
                    },
                    new Location
                    {
                        LocationId = 8566,
                        City = "Butte",
                        Name = "Bert Mooney Airport",
                        SiteId = "BTM",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MT").StateId
                    },
                    new Location
                    {
                        LocationId = 8567,
                        City = "Butler",
                        Name = "Butler County-K W Scholter Field",
                        SiteId = "BTP",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "PA").StateId
                    },
                    new Location
                    {
                        LocationId = 8568,
                        City = "Baton Rouge",
                        Name = "Baton Rouge Metropolitan Ryan Field",
                        SiteId = "BTR",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "LA").StateId
                    },
                    new Location
                    {
                        LocationId = 8569,
                        City = "Burlington",
                        Name = "Burlington International Airport",
                        SiteId = "BTV",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "VT").StateId
                    },
                    new Location
                    {
                        LocationId = 8570,
                        City = "Beatty",
                        Name = "Beatty Airport",
                        SiteId = "BTY",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NV").StateId
                    },
                    new Location
                    {
                        LocationId = 8571,
                        City = "Burwell",
                        Name = "Cram Field",
                        SiteId = "BUB",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NE").StateId
                    },
                    new Location
                    {
                        LocationId = 8572,
                        City = "Buffalo",
                        Name = "Buffalo Niagara International Airport",
                        SiteId = "BUF",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NY").StateId
                    },
                    new Location
                    {
                        LocationId = 8573,
                        City = "Butler",
                        Name = "Butler Memorial Airport",
                        SiteId = "BUM",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MO").StateId
                    },
                    new Location
                    {
                        LocationId = 8574,
                        City = "Burbank",
                        Name = "Bob Hope Airport",
                        SiteId = "BUR",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8575,
                        City = "Beaver Falls",
                        Name = "Beaver County Airport",
                        SiteId = "BVI",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "PA").StateId
                    },
                    new Location
                    {
                        LocationId = 8576,
                        City = "Bartlesville",
                        Name = "Bartlesville Municipal Airport",
                        SiteId = "BVO",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "OK").StateId
                    },
                    new Location
                    {
                        LocationId = 8577,
                        City = "Batesville",
                        Name = "Batesville Regional Airport",
                        SiteId = "BVX",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AR").StateId
                    },
                    new Location
                    {
                        LocationId = 8578,
                        City = "Beverly",
                        Name = "Beverly Municipal Airport",
                        SiteId = "BVY",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MA").StateId
                    },
                    new Location
                    {
                        LocationId = 8579,
                        City = "Brawley",
                        Name = "Brawley Municipal Airport",
                        SiteId = "BWC",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8580,
                        City = "Brownwood",
                        Name = "Brownwood Regional Airport",
                        SiteId = "BWD",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "TX").StateId
                    },
                    new Location
                    {
                        LocationId = 8588,
                        City = "Burley",
                        Name = "Burley Municipal Airport",
                        SiteId = "BYI",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "ID").StateId
                    },
                    new Location
                    {
                        LocationId = 8589,
                        City = "Fort Irwin/Barstow",
                        Name = "Bicycle Lake Army Air Field",
                        SiteId = "BYS",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8590,
                        City = "Bay City",
                        Name = "Bay City Municipal Airport",
                        SiteId = "BYY",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "TX").StateId
                    },
                    new Location
                    {
                        LocationId = 8591,
                        City = "Bozeman",
                        Name = "Gallatin Field",
                        SiteId = "BZN",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MT").StateId
                    },
                    new Location
                    {
                        LocationId = 8592,
                        City = "Coalinga",
                        Name = "New Coalinga Municipal Airport",
                        SiteId = "C80",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8593,
                        City = "Cadillac",
                        Name = "Wexford County Airport",
                        SiteId = "CAD",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MI").StateId
                    },
                    new Location
                    {
                        LocationId = 8594,
                        City = "Columbia",
                        Name = "Columbia Metropolitan Airport",
                        SiteId = "CAE",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "SC").StateId
                    },
                    new Location
                    {
                        LocationId = 8595,
                        City = "Craig",
                        Name = "Craig Moffat Airport",
                        SiteId = "CAG",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CO").StateId
                    },
                    new Location
                    {
                        LocationId = 8596,
                        City = "Akron",
                        Name = "Akron Canton Regional Airport",
                        SiteId = "CAK",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "OH").StateId
                    },
                    new Location
                    {
                        LocationId = 8597,
                        City = "Clayton",
                        Name = "Clayton Municipal Airpark",
                        SiteId = "CAO",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NM").StateId
                    },
                    new Location
                    {
                        LocationId = 8598,
                        City = "Caribou",
                        Name = "Caribou Municipal Airport",
                        SiteId = "CAR",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "ME").StateId
                    },
                    new Location
                    {
                        LocationId = 8599,
                        City = "Cumberland",
                        Name = "Greater Cumberland Regional Airport",
                        SiteId = "CBE",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MD").StateId
                    },
                    new Location
                    {
                        LocationId = 8600,
                        City = "Council Bluffs",
                        Name = "Council Bluffs Municipal Airport",
                        SiteId = "CBF",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "IA").StateId
                    },
                    new Location
                    {
                        LocationId = 8601,
                        City = "Colby",
                        Name = "Shalz Field",
                        SiteId = "CBK",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "KS").StateId
                    },
                    new Location
                    {
                        LocationId = 8602,
                        City = "Columbus",
                        Name = "Columbus Air Force Base",
                        SiteId = "CBM",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MS").StateId
                    },
                    new Location
                    {
                        LocationId = 8603,
                        City = "Coffman Cove",
                        Name = "Coffman Cove Seaplane Base",
                        SiteId = "KCC",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AK").StateId
                    },
                    new Location
                    {
                        LocationId = 8604,
                        City = "Upland",
                        Name = "Cable Airport",
                        SiteId = "CCB",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8605,
                        City = "Concord",
                        Name = "Buchanan Field",
                        SiteId = "CCR",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8606,
                        City = "Charles City",
                        Name = "Northeast Iowa Regional Airport",
                        SiteId = "CCY",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "IA").StateId
                    },
                    new Location
                    {
                        LocationId = 8607,
                        City = "Lyndonville",
                        Name = "Caledonia County Airport",
                        SiteId = "CDA",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "VT").StateId
                    },
                    new Location
                    {
                        LocationId = 8608,
                        City = "Cedar City",
                        Name = "Cedar City Regional Airport",
                        SiteId = "CDC",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "UT").StateId
                    },
                    new Location
                    {
                        LocationId = 8609,
                        City = "Camden",
                        Name = "Harrell Field",
                        SiteId = "CDH",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "AR").StateId
                    },
                    new Location
                    {
                        LocationId = 8610,
                        City = "Camden",
                        Name = "Woodward Field",
                        SiteId = "CDN",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "SC").StateId
                    },
                    new Location
                    {
                        LocationId = 8611,
                        City = "Chadron",
                        Name = "Chadron Municipal Airport",
                        SiteId = "CDR",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NE").StateId
                    },
                    new Location
                    {
                        LocationId = 8612,
                        City = "Childress",
                        Name = "Childress Municipal Airport",
                        SiteId = "CDS",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "TX").StateId
                    },
                    new Location
                    {
                        LocationId = 8613,
                        City = "Caldwell",
                        Name = "Essex County Airport",
                        SiteId = "CDW",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "NJ").StateId
                    },
                    new Location
                    {
                        LocationId = 8614,
                        City = "Wichita",
                        Name = "Cessna Aircraft Field",
                        SiteId = "CEA",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "KS").StateId
                    },
                    new Location
                    {
                        LocationId = 8615,
                        City = "Crescent City",
                        Name = "Jack Mc Namara Field Airport",
                        SiteId = "CEC",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "CA").StateId
                    },
                    new Location
                    {
                        LocationId = 8616,
                        City = "Springfield/Chicopee",
                        Name = "Westover ARB/Metropolitan Airport",
                        SiteId = "CEF",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "MA").StateId
                    },
                    new Location
                    {
                        LocationId = 8617,
                        City = "Clemson",
                        Name = "Oconee County Regional Airport",
                        SiteId = "CEU",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "SC").StateId
                    },
                    new Location
                    {
                        LocationId = 8618,
                        City = "Connersville",
                        Name = "Mettel Field",
                        SiteId = "CEV",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "IN").StateId
                    },
                    new Location
                    {
                        LocationId = 9048,
                        City = "Indiana",
                        Name = "Indiana County/Jimmy Stewart Fld/ Airport",
                        SiteId = "IDI",
                        StateId = context.States.FirstOrDefault(i => i.Abbreviation == "PA").StateId
                    }
                );

                context.SaveChanges();
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
                System.Diagnostics.Debug.WriteLine(exception.StackTrace);
            }
        }
    }
}
