//using Microsoft.Extensions.Options;
//using OnlineStationary.Config;
//using OnlineStationary.Interfaces.IRepositories;

//namespace OnlineStationary.Implementations.Repositories
//{
//    public class FileRepository : IFileRepository
//    {
//        private readonly StorageConfiguration _config;
//        public FileRepository(IOptions<StorageConfiguration> config)
//        {
//             _config = config.Value;
//        }
//        public string UploadFile(IFormFile? file)
//        {
//            if(file == null)
//            {
//                return null;
//            }
//            var a = file.ContentType.Split('/');
//            var fileName = $"IMG{a[0]}{Guid.NewGuid().ToString().Substring(6, 5)}.{a[0]}";

//            var b = _config.Path;
//            if (!Directory.Exists(b))
//            {
//                Directory.CreateDirectory(b);
//            }

//            var c = Path.Combine(b, fileName);
//            using (var d = new FileStream(c, FileMode.Create)) { 
//                file.CopyTo(d);
//            }
//            return fileName;
//        }
//    }
//}
