using System.Text.Json.Serialization;

namespace PeiFeira.Communication.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRoleDto
{
    Admin = 0,
    Aluno = 1,
    Professor = 2,
    Coordenador = 3,
}
