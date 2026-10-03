using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SIMS_WinFormsApp.Services.AI.Implementations
{
    internal static class AiToolArgumentValidator
    {
        public static bool TryParseAndValidate(
            string argumentsJson,
            string schemaJson,
            out JObject arguments,
            out string error)
        {
            arguments = null;
            error = string.Empty;

            try
            {
                var parsed = JToken.Parse(string.IsNullOrWhiteSpace(argumentsJson) ? "{}" : argumentsJson);
                arguments = parsed as JObject;
                if (arguments == null)
                {
                    error = "Tham số công cụ phải là một đối tượng JSON.";
                    return false;
                }

                var schema = JObject.Parse(schemaJson);
                var properties = schema["properties"] as JObject ?? new JObject();
                var required = schema["required"] as JArray ?? new JArray();
                bool allowAdditional = schema["additionalProperties"] != null
                    && schema["additionalProperties"].Value<bool>();

                foreach (var requiredName in required.Values<string>())
                {
                    if (arguments[requiredName] == null)
                    {
                        error = "Thiếu tham số bắt buộc: " + requiredName + ".";
                        return false;
                    }
                }

                foreach (var argument in arguments.Properties())
                {
                    var propertySchema = properties[argument.Name] as JObject;
                    if (propertySchema == null)
                    {
                        if (!allowAdditional)
                        {
                            error = "Tham số không được hỗ trợ: " + argument.Name + ".";
                            return false;
                        }
                        continue;
                    }

                    if (!MatchesType(argument.Value, (string)propertySchema["type"]))
                    {
                        error = "Kiểu dữ liệu của tham số " + argument.Name + " không hợp lệ.";
                        return false;
                    }

                    if (argument.Value.Type == JTokenType.String)
                    {
                        int minLength = (int?)propertySchema["minLength"] ?? 0;
                        int maxLength = (int?)propertySchema["maxLength"] ?? int.MaxValue;
                        int length = argument.Value.Value<string>().Length;
                        if (length < minLength || length > maxLength)
                        {
                            error = "Độ dài tham số " + argument.Name + " không hợp lệ.";
                            return false;
                        }
                    }

                    if (argument.Value.Type == JTokenType.Integer)
                    {
                        long value = argument.Value.Value<long>();
                        long minimum = (long?)propertySchema["minimum"] ?? long.MinValue;
                        long maximum = (long?)propertySchema["maximum"] ?? long.MaxValue;
                        if (value < minimum || value > maximum)
                        {
                            error = "Giá trị tham số " + argument.Name + " nằm ngoài giới hạn cho phép.";
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (JsonException)
            {
                error = "Tham số công cụ không phải JSON hợp lệ.";
                return false;
            }
            catch (FormatException)
            {
                error = "Schema của công cụ không hợp lệ.";
                return false;
            }
        }

        private static bool MatchesType(JToken value, string type)
        {
            if (value == null || string.IsNullOrWhiteSpace(type)) return false;

            switch (type.ToUpperInvariant())
            {
                case "STRING":
                    return value.Type == JTokenType.String;
                case "INTEGER":
                    return value.Type == JTokenType.Integer;
                case "NUMBER":
                    return value.Type == JTokenType.Integer || value.Type == JTokenType.Float;
                case "BOOLEAN":
                    return value.Type == JTokenType.Boolean;
                case "OBJECT":
                    return value.Type == JTokenType.Object;
                case "ARRAY":
                    return value.Type == JTokenType.Array;
                default:
                    return false;
            }
        }
    }
}
