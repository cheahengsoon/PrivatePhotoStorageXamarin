using SQLite;

namespace PrivatePhotoStorageXamarin.Services.Database
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
