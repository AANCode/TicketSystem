# TicketSystem

En enkel konsollapplikasjon for å administrere tickets/oppgaver.

## Beskrivelse

TicketSystem er et ticket-administreringssystem bygget med C# og .NET 10.0. Appen lar deg opprette, administrere og spore tickets med ulike statuser.

## Funksjoner

- **Opprett tickets** - Lag nye tickets med tittel og beskrivelse
- **Se tickets** - Vis alle tickets i systemet
- **Oppdater status** - Endre status på tickets (Åpen, Pågår, Lukket)
- **SQLite database** - Data lagres persistent i lokal database

## Teknologi

- **Språk**: C#
- **.NET versjon**: 10.0
- **Database**: SQLite (via Microsoft.Data.Sqlite)
- **Arkitektur**: MVC-mønster med Repository-pattern

## Kjøring

```bash
dotnet run
```

## Prosjektstruktur

- `Program.cs` - Startpunkt for applikasjonen
- `Ticket.cs` - Ticket-modell
- `TicketController.cs` - Kontroller for businesslogikk
- `TicketRepository.cs` - Databaselagring
- `ConsoleView.cs` - Brukergrensesnitt i konsoll
- `TicketStatus.cs` - Status-enumerasjon for tickets
