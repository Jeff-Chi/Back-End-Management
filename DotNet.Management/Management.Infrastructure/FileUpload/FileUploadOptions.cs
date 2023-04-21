namespace Management.Infrastructure.FileUpload
{
    public class FileUploadOptions
    {
        public const string SectionName = "FileUpload";
        public string PhysicalStoragePath { get; set; } = string.Empty;
        public string StoreRootDirName { get; set; } = string.Empty;
        public string RootDirName { get; set; } = string.Empty;
        public string TempDirName { get; set; } = string.Empty;
        public string NormalDirName { get; set; } = string.Empty;
        public string RemoteServiceUrl { get; set; } = string.Empty;
        public List<FileUploadTypeSetting> TypeSettings { get; set; } = new List<FileUploadTypeSetting>();
    }
}
