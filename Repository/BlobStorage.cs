
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace StorageAccount.Repository
{
    public class BlobStorage
    {
        static string connectionString="DefaultEndpointsProtocol=https;AccountName=renukaiotstorage;AccountKey=i8z+3idrXyQthIdXSGpDudjPF8kTqdGuIPImAPcs7/qIKT305TBmhhqVCrBKoafajZ4bfH3vESlu+AStE4kfZw==;EndpointSuffix=core.windows.net";
        public static async Task CreateBlob(string blobName)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                await container.CreateAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task DeleteBlob(string blobName)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                await container.DeleteAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task DeleteBlobContent(string blobName,string file)
        {
             if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                await container.DeleteBlobAsync(file);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        // public static async Task<BlobProperties> UpdateBlobContent(string blobName,string file)
        // {
        //      if(string.IsNullOrEmpty(blobName))
        //     {
        //         throw new ArgumentNullException("enter blob name");
        //     }
        //     try
        //     {
        //         string fileName=Path.GetTempFileName();
        //         BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
        //         BlobClient blob=container.GetBlobClient(file);
        //         await blob.UploadAsync(fileName);
        //         BlobProperties prop=await blob.GetPropertiesAsync();
        //         return prop;
        //     }
        //     catch(Exception ex)
        //     {
        //         throw ex;
        //     }
        // }
          public static async Task UpdateBlobContent(string blobName,string fileName)
        {
             if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                BlobClient blob=container.GetBlobClient(fileName);
                var filePath = @"C:\Users\M1080951\Desktop\StorageAccount\BlobTest\rentRecipt.pdf";
                using FileStream uploadFileStream = File.OpenRead(filePath);
                await blob.UploadAsync(uploadFileStream,true);
                uploadFileStream.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<BlobProperties> getBlobContent(string blobName,string file)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                BlobClient blob=container.GetBlobClient(file);
                BlobProperties prop=await blob.GetPropertiesAsync();
                return prop;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<List<string>> GetBlob(string blobName,string file)
        {
           if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                List<string> names=new List<string>();
                await foreach(BlobItem a in container.GetBlobsAsync())
                {
                    names.Add(a.Name);
                }
                return names;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<BlobProperties> DownloadBlob(string blobName,string file)
        {
            try
            {
                string path=@"C:\Users\M1080951\Desktop\StorageAccount\Downloads\"+file;
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                BlobClient client=container.GetBlobClient(file);
                await client.DownloadToAsync(path);
                BlobProperties prop=await client.GetPropertiesAsync();
                return prop;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}