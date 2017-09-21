using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using MsgPack.Serialization;



namespace Sample 
{
    /// <summary>
    ///		A simple sample code for basic serialization/deserialization.
    /// </summary>
    
    public class MessagePackTest : MonoBehaviour
    {
        
        public static void SerializeThenDeserialize()
        {
            // They are object for just description. 
            var targetObject =
                new PhotoEntry
                {
                    Id = 123,
                    Title = "My photo",
                    Date = DateTime.Now,
                    Image = new byte[] { 1, 2, 3, 4 },
                    Comment = "This is test object to be serialize/deserialize using MsgPack."
                };
            targetObject.Tags.Add("Sample");
            targetObject.Tags.Add("Excellent");
            var stream = new MemoryStream();

            // 1. Create serializer instance.
            var serializer = MessagePackSerializer.Get<PhotoEntry>();

            // 2. Serialize object to the specified stream.
            serializer.Pack(stream, targetObject);

            // Set position to head of the stream to demonstrate deserialization.
            stream.Position = 0;

            // 3. Deserialize object from the specified stream.
            var deserializedObject = serializer.Unpack(stream);

            // Test deserialized value.
            UnityEngine.Debug.LogError("Same object:"+ System.Object.ReferenceEquals(targetObject, deserializedObject));
            UnityEngine.Debug.LogError("Same Id:"+ (targetObject.Id == deserializedObject.Id));
            UnityEngine.Debug.LogError("Same Title? {0}" + (targetObject.Title == deserializedObject.Title));
            // Note that MsgPack defacto-standard is Unix epoc in milliseconds precision, so micro- and nano- seconds will be lost. See sample 04 for workaround.
            UnityEngine.Debug.LogError("Same Date? {0}" + (targetObject.Date.ToString("YYYY-MM-DD HH:mm:ss.fff") == deserializedObject.Date.ToString("YYYY-MM-DD HH:mm:ss.fff")));
            // Image and Comment tests are ommitted here.
            // Collection elements are deserialzed.
            UnityEngine.Debug.LogError("Items count: {0}" + (deserializedObject.Tags.Count));
        }
    }

    // Note: If you want to interop with other platform using SerializationMethod.Array (default), you should use [MessagePackMember]. See Sample06 for details.
    public class PhotoEntry
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public byte[] Image { get; set; }
        private readonly List<string> _tags = new List<string>();
        // Note that non-null read-only collection members are OK (of course, collections themselves must not be readonly.)
        public IList<string> Tags { get { return this._tags; } }
    }
}