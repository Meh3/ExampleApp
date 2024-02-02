using ErpApp.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Domain;

public partial record TrackDescriptiveData
{
    public string Code { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string? Description { get; init; }


    public TrackDescriptiveData(string code, string name, string? description = null)
    {

        Code = AlphanumericAndMin3CharactersRegex().Match(code).Success
            ? code
            : throw new DomainValidationException("Code must be alphanumeric and at least 3 characters long.");

        Name = name.Length < 2 || string.IsNullOrWhiteSpace(name)
            ? throw new DomainValidationException("Name must be at least 2 characters long and not be empty.")
            : name;

        Description = description is not null && string.IsNullOrEmpty(description)
            ? throw new DomainValidationException("Description can't be empty if provided.")
            : description;
    }

    [GeneratedRegex("^[a-zA-Z0-9]{3,}$")]
    private static partial Regex AlphanumericAndMin3CharactersRegex();
}