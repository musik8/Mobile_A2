using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.IO;

namespace sourceTree
{
   public class PhotoManager
    {
        string PhotoPath;
        NetworkManager nm = new NetworkManager();
        public async Task<string> AddImageAsync()
        {
            string ImagePath = "";
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                ImagePath = await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {ImagePath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
            }
            catch (PermissionException pEx)
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
            return ImagePath;

        }

        public async Task<string> LoadPhotoAsync(FileResult photo)
        {
          
            if (photo == null)
            {
                PhotoPath = null;
                return "";
            }
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            return newFile;
           
        }

        public async Task<string> UploadImage(Topic obj)
        {

            if (obj.ImagePath != null)
            {
                byte[] b = System.IO.File.ReadAllBytes(obj.ImagePath);
                ImageModel img = new ImageModel(Convert.ToBase64String(b));
                return await nm.ImageUpload(img);
            }
            else
            {
                return obj.ImagePath;
            }
        }
            
    }
}
