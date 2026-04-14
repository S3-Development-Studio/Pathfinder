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

## 5. Podsumowanie
Powyższa implementacja w pełni odzwierciedla rygorystyczne wymagania postawione przed architekturą logiki sterowania procesami przestrzennymi w oparciu o czystą formę modularną, zabezpieczając system przed powstawaniem rozmytych kontekstów ograniczonych i długu technologicznego na płaszczyznach styku wielowarstwowych operacji. 
