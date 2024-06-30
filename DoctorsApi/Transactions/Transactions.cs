using DoctorsApi.Helpers;
using DoctorsApi.Models;
using MongoDB.Driver;


namespace DoctorsApi.Transactions
{
    public class Registries
    {
        private static string _connectionString = "mongodb://root:admin@localhost:27017/?authSource=admin";
        private static string _dataBase = "DoctorsFile";
        private static string _collection = "Doctors";

        public async void UpdateDoctorsProfile(DoctorModel doctor)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase(_dataBase);
            var collection = db.GetCollection<DoctorModel>(_collection);

            var filter = Builders<DoctorModel>.Filter.Eq("userId", doctor.userId);

            await collection.ReplaceOneAsync(filter, doctor, new ReplaceOptions { IsUpsert = true });

            string subjectMessage = "Profile updated successfully!!!";
            string bodyMessage = @$"Your profile was updated, please keep your information up to date";
            RabbitSender messager = new RabbitSender();
            messager.MessageRabbit(doctor.email, subjectMessage, bodyMessage);
        }

        public async void DeleteDoctorsProfile(DoctorModel doctor)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase(_dataBase);
            var collection = db.GetCollection<DoctorModel>(_collection);

            var filter = Builders<DoctorModel>.Filter.Eq("userId", doctor.userId);

            await collection.DeleteOneAsync(filter);

            string subjectMessage = "Profile deleted successfully!!!";
            string bodyMessage = @$"Your profile was deleted, hope you get back soon!!";
            RabbitSender messager = new RabbitSender();
            messager.MessageRabbit(doctor.email, subjectMessage, bodyMessage);
        }
    }
}
