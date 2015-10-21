using System;
using System.IO;
using PrivatePhotoStorageXamarin.iOS.Service.Database;
using PrivatePhotoStorageXamarin.Services.Database;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteTouch))]
namespace PrivatePhotoStorageXamarin.iOS.Service.Database
{
    public class SQLiteTouch : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "TodoSQLite2.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            var libraryPath = documentsPath;//Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);
            // Create the connection
            var conn = new SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}
