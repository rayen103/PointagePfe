using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.SocieteConfigurations;

public static class SocieteSeed
{
    public static List<Societe> Data =>
    [
        Societe.Create(
            societeId: new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0")),
            logoPath: null,
            nom: "CST",
            matriculeFiscal: "MF-CST-001",
            rne: "RNE-CST-001",
            capital: 0m,
            dateOverture: new DateTime(2024, 1, 1),
            telephone1: null,
            telephone2: null,
            fax1: null,
            fax2: null,
            email: "admin@cst.tn",
            adresse: null,
            codeSociete: "CST")
    ];
}
