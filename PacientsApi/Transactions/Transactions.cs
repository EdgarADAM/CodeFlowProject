using PacientsApi.Models;
using PacientsApi.Helpers;
using MongoDB.Driver;


namespace Pacients.Transactions
{
    public class Registries
    {
        private static string _connectionString = "mongodb://root:admin@localhost:27017/?authSource=admin";
        private static string _dataBase = "PacientsFile";
        private static string _collection = "Pacients";

        public async void UpdatePacientsProfile(PacientModel pacient)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase(_dataBase);
            var collection = db.GetCollection<PacientModel>(_collection);

            var filter = Builders<PacientModel>.Filter.Eq("userId", pacient.userId);

            await collection.ReplaceOneAsync(filter, pacient, new ReplaceOptions { IsUpsert = true });

            string subjectMessage = "Profile updated successfully!!!";
            string bodyMessage = @$"Your profile was updated, please keep your information up to date";
            RabbitSender messager = new RabbitSender();
            messager.MessageRabbit(pacient.emailAddress, subjectMessage, bodyMessage);
        }

        public async void DeletePacientsProfile(PacientModel pacient)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase(_dataBase);
            var collection = db.GetCollection<PacientModel>(_collection);

            var filter = Builders<PacientModel>.Filter.Eq("userId", pacient.userId);

            await collection.DeleteOneAsync(filter);

            string subjectMessage = "Profile deleted successfully!!!";
            string bodyMessage = @$"Your profile was deleted, hope you get back soon!";
            RabbitSender messager = new RabbitSender();
            messager.MessageRabbit(pacient.emailAddress, subjectMessage, bodyMessage);
        }
    }
}
