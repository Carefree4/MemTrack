using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MemTrack
{
    public class AccessMemberXml
    {
        private readonly string _filePath;

        public AccessMemberXml(string filePath)
        {
            _filePath = filePath;
        }

        public void OverwriteXml(List<Member> members)
        {
            var serializer = new XmlSerializer(typeof (List<Member>));
            using (var fileStream = File.OpenWrite(_filePath))
            {
                serializer.Serialize(fileStream, members);
            }
        }

        public List<Member> ReadXml()
        {
            var members = new List<Member>();
            try
            {
                var serializer = new XmlSerializer(typeof (List<Member>));
                using (var fileStream = File.OpenRead(_filePath))
                {
                    members = (List<Member>) serializer.Deserialize(fileStream);
                }
            }
            catch (Exception e)
            {
                var err = new ErrorMessageWindow(e);
                OverwriteXml(members);
            }
            return members;
        }
    }
}