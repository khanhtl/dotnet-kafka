using CQRS.Core.Events;
using Post.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Query.Infrastructure.Converters
{
    internal class EventJsonConverter : JsonConverter<BaseEvent>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(BaseEvent));
        }
        public override BaseEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(!JsonDocument.TryParseValue(ref reader, out var doc))
            {
                throw new JsonException($"Fail to parse {nameof(JsonDocument)}");
            }

            if(!doc.RootElement.TryGetProperty("Type", out var type))
            {
                throw new JsonException("Could not detect the type discriminator property");
            }

            var typeDiscriminator = type.GetString();
            var json = doc.RootElement.GetRawText();
            return typeDiscriminator switch
            {
                nameof(PostCreatedEvent) => JsonSerializer.Deserialize<PostCreatedEvent>(json, options),
                nameof(MessageUpdatedEvent) => JsonSerializer.Deserialize<MessageUpdatedEvent>(json),
                nameof(PostLikedEvent) => JsonSerializer.Deserialize<PostLikedEvent>(json),
                nameof(CommentAddedEvent) => JsonSerializer.Deserialize<CommentAddedEvent>(json),
                nameof(CommentUpdatedEvent) => JsonSerializer.Deserialize<CommentUpdatedEvent>(json),
                nameof(CommentRemovedEvent) => JsonSerializer.Deserialize<CommentRemovedEvent>(json),
                nameof(PostRemovedEvent) => JsonSerializer.Deserialize<PostRemovedEvent>(json),
                _ => throw new JsonException($"{typeDiscriminator} is not support yet!")
            };

        }

        public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
