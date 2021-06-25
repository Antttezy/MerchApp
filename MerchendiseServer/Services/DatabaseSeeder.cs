using Domain.Core.Models;
using Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace MerchendiseServer.Services
{
    public class DatabaseSeeder
    {
        private readonly IRepository<Coordinator> coordinators;
        private readonly IEncryptor encryptor;
        private readonly ILogger<DatabaseSeeder> logger;

        public static Coordinator DefaultAccount { get; } = new Coordinator
        {
            FirstName = "Антон",
            SecondName = "Кумачев",
            Login = "antkumachev",
            Password = "88888888"
        };

        public DatabaseSeeder(IRepository<Coordinator> coordinators, IEncryptor encryptor, ILogger<DatabaseSeeder> logger)
        {
            this.coordinators = coordinators;
            this.encryptor = encryptor;
            this.logger = logger;
        }

        public void Seed()
        {
            if (!coordinators.All().Any())
            {
                try
                {
                    logger.LogInformation("Started seeding");
                    DefaultAccount.Password = encryptor.Encrypt(DefaultAccount.Password);
                    coordinators.Add(DefaultAccount);
                    logger.LogInformation("Seeding completed");
                }
                catch (Exception e)
                {
                    logger.LogError("Error while seeding a database: " + e.Message);
                }
            }
            else
            {
                logger.LogInformation("Database already seeded");
            }
        }
    }
}
