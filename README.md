# Fudbalski Turnir - Aplikacija za Evidenciju Utakmica

Višeslojna veb aplikacija za praćenje fudbalskih utakmica, vođenje evidencije o klubovima i beleženje rezultata sa detaljnim pregledom postignutih golova.

Ovaj projekat je razvijen kao pokazni primer višeslojne arhitekture (N-Tier) korišćenjem ASP.NET Core tehnologija, sa jasnim razdvajanjem odgovornosti, primenom repozitorijum šablona (Repository Pattern) i RESTful web servisa.

## Funkcionalnosti

- **Autentifikacija korisnika**: Siguran pristup sistemu samo za ovlašćena lica uz kriptovanje lozinki (hash funkcije i salt) za povećanu bezbednost podataka.
- **Upravljanje klubovima (Šifarnik)**: Evidencija fudbalskih klubova, njihovih stadiona i osnovnih informacija.
- **Evidencija utakmica**: Unos i pregled zapisnika sa utakmica na interfejsu koji na jednoj formi obrađuje i utakmicu i njene detalje (kroz sistemske transakcije baze).
- **Praćenje golova**: Detaljan unos svakog gola (minut, strelac, tim) u okviru jedne utakmice.
- **Automatska kalkulacija**: Sistem automatski sabira i prikazuje konačan rezultat na osnovu unetih golova ekipa.
- **Filtriranje i pregled**: Jednostavan tabelarni prikaz svih utakmica sa mogućnošću filtriranja.
- **Parametarska štampa**: Prikaz i štampanje pojedinačnih zapisnika u jasno definisanom i preglednom formatu.

## Tehnologije

- **Backend**: C#, ASP.NET Core MVC, ASP.NET Core Web API (REST)
- **Baza podataka**: MS SQL Server, Entity Framework Core (Code-First pristup) uz rad sa sirovim ADO.NET mehanizmima (DBUtils) i uskladištenim procedurama (Stored Procedures)
- **Frontend**: HTML5, CSS3, Bootstrap, JavaScript (klijentska validacija uz regularne izraze, asinhroni pozivi)
- **Arhitektura**: Solid struktura organizovana kroz 4 odvojena sloja

## Arhitektura Projekta

Aplikacija strogo poštuje princip odvajanja odgovornosti i podeljena je u četiri logička sloja realizovana kao zasebni Class Library projekti:

1. **Prezentacioni Sloj (`PrezentacioniSloj`)** 
   Naš korisnički interfejs realizovan kroz ASP.NET Core MVC. Sadrži kontrolere (Controllers), poglede (Views) i strogo definisane ViewModele za svaku akciju. Zadužen je isključivo za interakciju sa korisnikom i osnovnu klijentsku validaciju, a za podatke konzumira eksterne servise.

2. **Sloj Servisa (`SlojServisa`)** 
   Implementira REST API endpoint-e za manipulaciju podacima. Vrši prevođenje entiteta iz baze u DTO (Data Transfer Objects) modele obezbeđujući na taj način siguran ugovor komunikacije.

3. **Sloj Poslovne Logike (`SlojPoslovneLogike`)** 
   Mozak aplikacije. Ovde se nalaze specifična poslovna pravila. Sloj je napravljen na način da su logička ograničenja parametrizovana tj. učitavaju se dinamički iz XML datoteka, što omogućava izmenu pravila na nivou turnira bez rekompajliranja koda.

4. **Sloj Podataka (`SlojPodataka`)** 
   Upravlja perzistencijom nad MS SQL bazom podataka, služeći se Entity Framework Core-om i repozitorijum (Repository) šablonom uz punu podršku unutrašnjih baza-transakcija kod ulančanih unosa. Pored ORM pristupa, za specifične i kompleksnije upite koristi se i ADO.NET posredstvom pomoćnih baza-klasa (`DBUtils`) i uskladištenih procedura.

## Model Podataka

Sistem se oslanja na relacionu bazu nad objektno-orijentisanom paradigmom (Entity entiteti sa nasleđivanjem od bazne nadklase `OsnovniEntitet`):

- **Korisnik**: Nezavisna sistemska tabela za pristup.
- **Klub**: Tabela šifarnik koja sadrži podatke o timovima (Naziv, Grad, Stadion, itd.).
- **Zapisnik**: Glavna tabela koja čuva podatke o utakmici. Vezana je dvostrukim stranim ključem za Klub (za Domaćina i Gosta).
- **Stavka Zapisnika**: Detaljna tabela u kojoj se nalazi pojedinačni gol, vreme i strelac, a povezana je relacijom za Zapisnik i za Tima strelca.

## Poslovna Logika i Pravila

Konzistentnost podataka obezbeđena je implementacijom posebnih, sistemskih validatora i logike:

- **Hronologija rezultata**: Sistem odbija snimanje utakmice gde minut postignutog gola iskače iz redosleda (Minut sledećeg unetog gola mora pratiti hronološki red i ne sme biti manji od trenutnog minuta prethodno evidentirane stavke tog zapisa).
- **Provera domena**: Golovi moraju biti vremenski uneti u logičnom okviru koji definišu spoljna XML pravila.
- **Automatizam rezultata**: Sistem na osnovu odabira strelca kluba tačno upoređuje identifikator domaćina i gosta na tom meču inkrementirajući konačan rezultat autonomno od korisničkog zadavanja vrednosti.

## Pokretanje aplikacije

1. Otvorite Visual Studio rešenje `FudbalskiTurnirRVS.slnx` koje se nalazi u root-u projekta.
2. Proverite pristup bazi u odgovarajućim podešavanjima konfiguracije (`appsettings.json`).
3. Pokrenite komandu `Update-Database -Project SlojPodataka -StartupProject SlojServisa` u Package Manager Console.
4. Pokrenite .bat skriptu koja se naziva `start-dev.bat` koja se nalazi u root-u projekta.
5. Pokrenite web browser sa adresom `https://localhost:5001`.
