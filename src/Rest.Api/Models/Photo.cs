using System;

namespace Rest.Api.Models
{
    public class Photo
    {
        public Guid PhotoId { get; private set; }
        public string Name { get; private set; }
        public byte[] Bytes { get; private set; }
        public string ContentType { get; private set; }
        public float Height { get; private set; }
        public float Width { get; private set; }

        public Photo(string name)
        {
            Name = name;
        }

        public void Update(byte[] bytes, string contentType)
        {
            Bytes = bytes;
            ContentType = contentType;
        }

        public void Update(float height, float width)
        {
            Height = height;
            Width = width;
        }
    }
}
