﻿using System.Text.Json.Serialization;
using System.Text.Json;

namespace ApiExample.Infrastructure.Serialization
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => DateOnly.ParseExact(reader.GetString()!, Format);

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(Format));
    }
}
