namespace invProj.models.ExternalData;

public class Photo
{
    public Guid Id { get; set; }

    public string S3Key { get; set; } = null!; // e.g., "items/12345/photo1.jpg"
    public string FileName { get; set; } = null!; // original filename
    public string ContentType { get; set; } = null!; // image/jpeg
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    
    public Group? Group { get; set; }
}