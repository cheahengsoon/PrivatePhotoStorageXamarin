using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivatePhotoStorageXamarin.Services
{
    public interface ISaveImage
    {
        string CopyPhotoTo(string photoPath);
        string GetPath();
    }
}
