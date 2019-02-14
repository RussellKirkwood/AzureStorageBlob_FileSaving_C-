
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;


public class AzureStorageHelper
    {            

            public string determineAttachmentExtension(string ContentType)
            {
                string AttachmentExtension = ContentType.Substring(ContentType.IndexOf('/') + 1).ToLower();

                if (AttachmentExtension == "jpeg")
                {
                    AttachmentExtension = "jpg";
                }

                if (AttachmentExtension == "tiff")
                {
                    AttachmentExtension = "tif";
                }

                return AttachmentExtension;
            }

            public string determineAttachmentExtensionByURL(string URL)
            {
                string AttachmentExtension = URL.Substring(URL.IndexOf('.') + 1).ToLower();

                if (AttachmentExtension == "jpeg")
                {
                    AttachmentExtension = "jpg";
                }

                if (AttachmentExtension == "tiff")
                {
                    AttachmentExtension = "tif";
                }

                return AttachmentExtension;
            }

            public string determineAttachmentFileName(string Uri)
            {
                string AttachmentFileName = Uri.Substring(Uri.LastIndexOf('/') + 1).ToLower();

                return AttachmentFileName;
            }


        public ResourceData UploadtoAzureByte(byte[] fileByteData, string ResourceGlobalID)
            {

            var fileStreamData= new MemoryStream(fileByteData);
            var ResourceData = new ResourceData();
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureStorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("testblobcontainer");

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            CloudBlockBlob blob = container.GetBlockBlobReference(ResourceGlobalID + ".pdf");

            // Create / Overwrite the "myblob" blob with contents from a file.            
            blob.UploadFromStream(fileStreamData);

            ResourceData.ResourceGlobalID = ResourceGlobalID;
            ResourceData.ResourceURL1Primary = blob.StorageUri.PrimaryUri.ToString();
            ResourceData.ResourceURL1Secondary = blob.StorageUri.SecondaryUri.ToString();            

            return ResourceData;

            }
        }
