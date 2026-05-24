using CollectManagement.Domain.Gouvernorats;
using CollectManagement.Domain.Gouvernorats.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.GouvernoratConfigurations;

public static class GouvernoratSeed
{
    public static List<Gouvernorat> Data =>
    [
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "11", "Tunis", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "12", "Ariana", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "13", "Ben Arous", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "14", "Manouba", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "21", "Nabeul", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "22", "Zaghouan", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "23", "Bizerte", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "31", "Béja", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "32", "Jendouba", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "33", "Le Kef", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "34", "Siliana", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "41", "Kairouan", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "42", "Kasserine", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "43", "Sidi Bouzid", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "51", "Sousse", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "52", "Monastir", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "53", "Mahdia", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "61", "Sfax", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "71", "Gafsa", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "72", "Tozeur", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "73", "Kebili", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "81", "Gabès", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "82", "Médenine", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0"))),
        Gouvernorat.Create(new GouvernoratId(Ulid.NewUlid()), "83", "Tataouine", true, new SocieteId(Ulid.Parse("01HC85BM5QVRW7ABRV33TR1GQ0")))
    ];
}
