# Pathfinder: Zintegrowany System Zarządzania Odwiedziami i Wyznaczania Tras

## 1. Wprowadzenie
System Pathfinder stanowi rozbudowane rozwiązanie programistyczne napisane w technologii .NET 9.0, którego celem jest optymalizacja ścieżek zwiedzania, kompleksowe przetwarzanie punktów użyteczności publicznej (POI) oraz parametryzowanie wskaźników gamifikacyjnych. Architektura systemu opiera się o paradygmaty projektowania sterowanego dziedziną (Domain-Driven Design). Ze względów wydajnościowych oraz spójności strukturalnej, środowisko zostało zrealizowane jako Modularny Monolit (Modular Monolith).

## 2. Architektura Systemu
Rozszerzalność oraz hermetyzacja logiki biznesowej zostały uzyskane poprzez podział na izolowane moduły. Każdy moduł definiuje własny kontekst ograniczony (Bounded Context) zawierając unikatowe encje, agregaty, obiekty wartości oraz dedykowaną warstwę aplikacyjną obsługującą przypadki użycia. System w pełni wspiera kontener odwrócenia kontroli (IoC), gdzie zarządzanie cyklem życia zależności (DI) regulowane jest na poziomie punktu wejściowego aplikacji pod postacią rozszerzeń strumienia głównego. 

Główne wektory komunikacyjne opierają się na funkcjonalności wbudowanych komponentów serwerowych dostarczanych za pośrednictwem Minimal API. Wdrożono zarazem natywny model kaskadowego rozwiązywania błędów na bazie standardu `ProblemDetails` pod postacią globalnego przechwytywacza wyjątków (`GlobalExceptionHandler`).

---

## 3. Specyfikacja Modułowa

### 3.1. Moduł: Attractions (Zarządzanie Obiektami)
Moduł ten służy do operacjonalizacji danych wejściowych o cechach geograficzno-logicznych. Hermetyzuje on logikę agregatu `Attraction`.
- **Kategoryzacja Sezonowa:** Implementacja klasyfikacji na przestrzeni enumeracji `Season`, która definiuje ramy okna dostępności danego węzła.
- **Kryteria Dostępności:** Wprowadzenie strukturalnego wskaźnika boolowskiego, pozwalającego filtorować obiekty spełniające obostrzenia infrastrukturalne dla podmiotów ze szczególnymi potrzebami (Dostępność dla wózków inwalidzkich).
- **Protokół Rezerwacyjny:** Zaawansowana logika oparta o agregat `Reservation` regulująca przepustowość miejsc (`MaxConcurrentReservations`). Analiza w czasie rzeczywistym interwałów czasowych metodami selekcji na nakładające się okna zapytań zapobiega wyśnigom oraz błędom przekroczenia dopuszczalnych wskaźników alokacji.

### 3.2. Moduł: Routing (Wyznaczanie Ścieżek)
Segment odpowiedzialny za ewaluację i syntezę danych geograficznych w celu deterministycznego wytwarzania planów.
- **Algorytmika Tras:** Jednostka `RouteGeneratorService` oblicza iteracyjnie wagi dróg oraz punktów przesiadkowych na bazie dostarczonych parametrów środowiskowych.
- **Ocena Preferencji:** Walidacja reguł heurystycznych realizowana za pośrednictwem struktur zdefiniowanych w bibliotece `FluentValidation`. Walidatory te nadzorują spójność metadanych wejściowych (`UserPreferences`) weryfikując korelacje pomiędzy oczekiwanym wysiłkiem fizycznym a relaksacyjnym celem eksploracji.

### 3.3. Moduł: Gamification (Ocena Aktywności)
Subsytem dedykowany do obliczania wartości dodanej w procesie angażowania użytkowników końcowych.
- Algorytmy oceniające zintegrowane we wzorcu `ActivityScoreCalculator` rzutują skwantyfikowany wysiłek poniesiony w zrealizowanej trasie na bazowe wskaźniki systemu grywalizacyjnego (punkty eksploracji oraz koła ratunkowe balansu relaksacyjnego). 

---

## 4. Wykorzystany Stos Technologiczny oraz Wzorce Projektowe

1. **Język i Środowisko uruchomieniowe:**
   - C# 13, docelowo kompilowany w ekosystemie .NET 9.0 SDK
2. **Framework Webowy:**
   - ASP.NET Core w wariancie Minimal API optymalizującym proces interpretacji zapytań sieciowych.
3. **Persystencja (warstwa infrastruktury):**
   - Na etapie wczesnej adaptacji symulowana in-memory (wewnętrzna instancja pamięci RAM kontenera DI, przy wykorzystaniu cyklu życia Singleotn dla zachowania współbieżnego stanu danych).
4. **Wzorce Integracyjne:**
   - **Repository Pattern:** Odizolowanie logiki składowania danych od logiki operacyjnej.
   - **Dependency Injection:** Dedykowane instancje typu `Singleton` do zarządzania stanem rezerwacji oraz `Transient` do stateless przetwarzania zapytań domenowych.
5. **OpenAPI / Analiza statyczna dokumentacji:**
   - Przystosowanie do standardu koncepcyjnego REST realizowanego przez generatory mapowania Swagger na profilu developerskim. 

<<<<<<< HEAD
Potok dzieli sie na trzy narastajace fazy:

### Faza Pierwsza - Pre-filtering (Odsiew Twardy)
System dysponuje repozytorium (tzw. Mokowana Baza Danych) dla miast, w ktorych kazda atrakcja ma flage `IsOutdoor`.
Jesli uzytkownik zadeklarowal zla pogode ("Deszczowo"), silnik na wstepie brutalnie wyrzuca ze zbioru punktow mozliwych do wytypowania wszystkie parki, rynki i obiekty znajdujace sie na zewnatrz. Zapobiega to uwzglednieniu np. spaceru po plazy zima czy w trakcje burzy.

### Faza Druga - Silnik Ważenia i Punktacji (Scoring)
Pozostale na liscie atrakcje musza zostac ulozone pod nastroj uzytkownika. 
W bazie danych narzucone odgornie sa dwa wkazniki dla kazdego miejsca (w skali od 1 do 10): 
- **ExplorationScore** - wartosc historyczna, naukowa, stopien zmuszenia do wysilku umyslowego lub fizycznego.
- **RelaxationScore** - poziom wypoczynku, radosci, kontaktu z natura.

Biorac wartosci z suwaka z interfejsu klienta, algorytm serwera normalizuje je tworzac mnozniki wagowe. Do wyliczenia uzywa wzoru:
`Wynik (Calculated Score) = (ExplorationScore * ExploreWeight) + (RelaxationScore * RelaxWeight)`

**Przyklad logiczny scoringu:**
Uzytkownik ustawil suwak na mocne zwiedzanie, system policzyl wagi: `ExploreWeight = 0.8`, `RelaxWeight = 0.2`.
- Mamy w bazie "Muzeum Narodowe" (`Exploration: 9`, `Relaxation: 2`). 
  Wyliczenie dla Muzeum wynosi: `(9 * 0.8) + (2 * 0.2) = 7.2 + 0.4 = 7.6`
- Mamy w bazie "Park Miejski" (`Exploration: 2`, `Relaxation: 9`).
  Wyliczenie dla Parku wynosi: `(2 * 0.8) + (9 * 0.2) = 1.6 + 1.8 = 3.4`
Wynik decyzyjny: Pod tak dobrane parametry, ukladajac plan system potraktuje "Muzeum Narodowe" (7.6) jako wazniejszy i pewniejszy punkt wycieczki na dany dzien niz "Park Miejski" (3.4). Lista zostaje posortowana malejaco wedlug wykladu tych wyliczen.

### Faza Trzecia - Pathfinding (Laczenie Punktow i Najblizszy Sasiad)

Milosnicy nawigacji musza znac rzeczywisty dystans fizyczny, by ocenic czas trasy. Aplikacja aplikuje wzor matematyczny - **Formule Haversine'a**, sluzaca do precyzyjnego badana krzywizny sferycznej Ziemi dla dwoch podanych wspolrzednych (szerokosc i dlugosc). Wzor na odleglosc `d`:
`a = sin²(Δlat/2) + cos(lat1) * cos(lat2) * sin²(Δlon/2)`
`c = 2 * atan2(√a, √(1-a))`
`d = R * c` (gdzie R to rozpietosc rownikowa planety - powszechnie ustalone 6371 km).

Skuteczny plan jest nastepnie budowany z zastosowaniem uproszczonego algorytmu **Najblizszego Sasiada (Nearest Neighbor)**:
1. Algorytm bierze najlepiej pasujaca punktowo (wynik z Fazy Drugiej) atrakcje w calym wybranym miescie i ustawia ja jako "Punkt Startowy" numer 1.
2. Z wezla poczatkowego mierzy odleglosci sferyczne geometrii Haversine'a do _wszystkich_ pozostalych, jeszcze niewykorzystanych wezlow dla danego miasta w buforze. 
3. Po wyszukaniu absolutnie najkrotszego, fizycznego dystansu - algorytm dobiera te atrakcje jako Punkt numer 2.
4. Nastepuje weryfikacja zasobow do the punktu:
    - *Budzet czasu:* Oceniany jest szacowany czas wyrolowany pod deklaracje lokomocji (np. dlaieszego 5km/h, Komunikacja 15km/h). Wycieczka nie moze trwac wg wyliczen lacznie wiecej niz domyslny limit 8 bezlitosnych godzin dziennie.
    - *Budzet kilometrow:* Suma drogi miedzy wszystkimi dodanymi punktami nie moze przekroczyc suwaka odleglosci podanego przez uzytkownika w opcjach wywiadu (Jesli opcja lokomocyjna zakladala uzytek wylacznie wlasnych nog).
5. Cykl potarzalny jest sukcesywnie (O(N^2)) poki algorytm nie odrzuci dokoptowania kolejnego wezla np. powodujacego zlamanie regul z powyzszego weryfikatora (brak czasu/za daleko).

Dzieki takiemu wielofazowemu obiegowi danych, aplikacja dobiera idealnie spersonalizowany rygor wyjazdu dzialajac calkowicie deterministycznie. Zastosowanie Najblizszego Sasiada zamiast wpelni zoptymalizowanego TSP (Problem Komiwojazera), sprawia, ze wyliczenie generacji na serwerze i odeslanie calkowitego rezultatu wraz ze spisem JSON dla narzedzi programistycznych to zazwyczaj ulamki nieodczuwalnej mili-sekundy.

---

### 2.1. Wykorzystane Standardy i Parametry
Dla zapewnienia najwyższej precyzji, system operuje na stałych wartościach fizycznych:

| Parametr | Opis | Wartość |
| :--- | :--- | :--- |
| **R (Radius)** | Średni promień Ziemi przyjęty w Formule Haversine'a | `6371 km` |
| **V (Walking)** | Średnia prędkość poruszania się pieszego | `5 km/h` |
| **V (Public)** | Estymowana średnia prędkość komunikacji miejskiej | `15 km/h` |
| **T (Buffer)** | Domyślny margines czasowy na jedną atrakcję | `45 - 120 min` |
| **Complexity** | Złożoność obliczeniowa wyznaczania trasy | `O(N²)` |

## 3. Uruchamianie Systemu

1. Zainstaluj srodowisko uruchomieniowe SDK platformy .NET 9 na swoim serwerze bądz komputerze.
2. Wejdz do glownego korzenia pobranego repozytorium przy pomocy termianala.
3. Wpisz komende:
```bash
dotnet run
```
4. Narzedzie samodzielnie skompiluje klasy, narzuci schematy interfejsow, udostepni warstwe Middleware dla zasobow stalych i wypusci zywego hosta Kestrel, otwierajac okno logowania, podajac adres (typowe porty lokalne to np. `http://localhost:5233`), po wklejeniu ktorego do wyszukiwarki bezposrednio korzystamy ze skonfigurowanej witryny asystenta.


## 4. Struktura Projektu
```bash
Pathfinder/
├── Data/               # Repozytoria i ziarna danych (JSON)
├── Models/             # Klasy obiektów (Attraction, RoutePlan)
├── Services/           # Logika biznesowa i algorytmika
├── wwwroot/            # Frontend (HTML, CSS, JS)
├── Program.cs          # Punkt wejścia aplikacji .NET 9
└── Pathfinder.csproj   # Konfiguracja projektu i zależności
=======
## 5. Podsumowanie
Powyższa implementacja w pełni odzwierciedla rygorystyczne wymagania postawione przed architekturą logiki sterowania procesami przestrzennymi w oparciu o czystą formę modularną, zabezpieczając system przed powstawaniem rozmytych kontekstów ograniczonych i długu technologicznego na płaszczyznach styku wielowarstwowych operacji. 
>>>>>>> aefc97a0de9f1667d9ea8e096cbc5206dbb6217a
