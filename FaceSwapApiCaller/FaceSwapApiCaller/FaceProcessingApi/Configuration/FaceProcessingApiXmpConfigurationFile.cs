namespace FaceSwapApiCaller.FaceProcessingApi.Configuration;

using System.Xml;

public class FaceProcessingApiXmpConfigurationFile
{
    public FaceProcessingApiXmpConfigurationFile(string filename)
    {
        _filename = filename;
    }

    private string _filename;

    public bool Exist()
    {
        return File.Exists(_filename);
    }
    
    public void Remove()
    {
        File.Delete(_filename);
    }

    public void Write(FaceProcessingApiConfiguration configuration)
    {
        var xmlDocument = new XmlDocument();
        
        var apiUrlsNode = xmlDocument.CreateElement("ApiUrls");
        xmlDocument.AppendChild(apiUrlsNode);
        
        var faceSwapApiUrlAttribute = xmlDocument.CreateAttribute("faceSwapApiUrl");
        faceSwapApiUrlAttribute.Value = configuration.FaceSwapApiUrl;
        apiUrlsNode.Attributes.Append(faceSwapApiUrlAttribute);
        
        var stableDiffusionApiUrlAttribute = xmlDocument.CreateAttribute("stableDiffusionApiUrl");
        stableDiffusionApiUrlAttribute.Value = configuration.StableDiffusionApiUrl;
        apiUrlsNode.Attributes.Append(stableDiffusionApiUrlAttribute);
        
        xmlDocument.Save(_filename);
    }
    
    public FaceProcessingApiConfiguration Read()
    {
        using var reader = XmlReader.Create(_filename);
        while (reader.Read())
        {
            // if (reader.NodeType == XmlNodeType.Element && reader.Name == "ApiUrls")
            // {
            var faceSwapApiUrl = reader.GetAttribute("faceSwapApiUrl");
            var stableDiffusionApiUrl = reader.GetAttribute("stableDiffusionApiUrl");
            
            // Console.WriteLine("faceSwapApiUrl: {0}", faceSwapApiUrl);
            // Console.WriteLine("stableDiffusionApiUrl: {0}", stableDiffusionApiUrl);
            
            return new FaceProcessingApiConfiguration(faceSwapApiUrl, stableDiffusionApiUrl);
            // }
        }
        
        throw new Exception("Configuration don't contains expected data");
    }
}