using System.IO;
using System.Xml.Serialization;

public sealed class XmlSaver<T> where T : new()
{
	public void Save(string filePathWithExtension, T serializedObject)
	{
		var serializer = new XmlSerializer(typeof(T));
		var fileStream = new FileStream(filePathWithExtension, FileMode.Create, FileAccess.Write);
		serializer.Serialize(fileStream, serializedObject);
		fileStream.Close();
	}

	public T Upload(string filePathWithExtension)
	{
		var serializer = new XmlSerializer(typeof(T));
		if (File.Exists(filePathWithExtension))
		{
			var fileStream = new FileStream(filePathWithExtension, FileMode.Open, FileAccess.Read);
			var deserializedObject = (T)serializer.Deserialize(fileStream);
			fileStream.Close();
			return deserializedObject;
		}
		else
		{
			return new T();
		}
	}
}
