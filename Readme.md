![](RackMultipart20210322-4-1q0k4u5_html_2dd637ae2fa5ca04.png)

**WYDZIAŁ BUDOWY MASZYN I INFORMATYKI**

**INFORMATYKA**

**Dokumentacja projektu**

Ceneo Searcher

Wykonawcy:

Jakub Gawęda 54403

Grzegorz Waluś 54497

Sławomir Kędzior 54418

Bielsko-Biała 2021/2022

# Spis treści

[1. Charakterystyka projektu 2](#_Toc67302452)

[2.Specyfikacja wymagań funkcjonalnych 3](#_Toc67302453)

[3.Specyfikacja wymagań niefunkcjonalnych 4](#_Toc67302454)

[3.1 Główne informacje 4](#_Toc67302455)

[3.2 Biblioteki 4](#_Toc67302456)

[3.3 Specyfikacja niefunkcjonalna 4](#_Toc67302457)

[4. Projekt systemu 5](#_Toc67302458)

[4.1 UML 5](#_Toc67302459)

[4.2 Baza danych 6](#_Toc67302460)

[5. Przebieg techniczny projektu 6](#_Toc67302461)

[5.1 Shared 6](#_Toc67302462)

[5.2 WebEngine 6](#_Toc67302463)

[5.3 DekstopClient 9](#_Toc67302464)

[6. Organizacja pracy 10](#_Toc67302465)

1.
# Charakterystyka projektu

**Ceneo searcher** jest stosowany do wyszukiwania przedmiotów które mogą zainteresować użytkownika. Aplikacja wykorzystuję dane ze strony [www.ceneo.pl](http://www.ceneo.pl/) dzięki czemu jest w stanie wychwycić najniższe ceny produktów które mogą zainteresować potencjalnego konsumenta. Nasze oprogramowanie daje możliwość monitorowania zmiany cen zasubskrybowanych przedmiotów oraz pozwala na sprawdzenie pozycji tych pozycji nawet w trybie offline.

1.
# Specyfikacja wymagań funkcjonalnych

| ID | F1 |
| --- | --- |
| Nazwa | Pobieranie informacji ze strony internetowej |
| Opis | Aplikacja ma możliwość wyciągania danych które nas zainteresowały ze strony www.ceneo.pl. |

| ID | F2 |
| --- | --- |
| Nazwa | Prezentacja wyników (GUI) |
| Opis | Aplikacja wyświetla w przejrzysty sposób wyniki wyszukiwania oraz zasubskrybowane przedmioty. |

| ID | F3 |
| --- | --- |
| Nazwa | Wyszukiwanie produktów |
| Opis | Użytkownik ma możliwość wyszukiwania produktów po wprowadzonych przez siebie frazach. |

| ID | F4 |
| --- | --- |
| Nazwa | Subskrypcja produktu |
| Opis | Zapisanie informacji o przedmiocie, który zainteresował użytkownika aplikacji i wyświetlanie ich w osobnej zakładce. |

| ID | F5 |
| --- | --- |
| Nazwa | Aktualizowanie cen produktów |
| Opis | Możliwość sprawdzania aktualnych cen produktów. |

| ID | F6 |
| --- | --- |
| Nazwa | Przeglądanie zasubskrybowanych przedmiotów w trybie offline |
| Opis | Użytkownik ma możliwość przeglądania swoich zasubskrybowanych przedmiotów w razie braku łączności z Internetem. |

| ID | F7 |
| --- | --- |
| Nazwa | Płynne przełączanie się między zakładkami |
| Opis | Możliwość przełączania się między wynikami wyszukiwania i zasubskrybowanymi przedmiotami które będą pokazywane w czasie rzeczywistym. |

1.
# Specyfikacja wymagań niefunkcjonalnych

## 3.1 Główne informacje

| Język programowania | C# |
| --- | --- |
| Platformy wspomagająca współpracę | Github.com, trello.com |
| Silnik projektu | ASP.NET Core WebAPI |
| Aplikacja kliencka | WPF .NET 5.0 |
| Baza danych | SQLite |

_Tabela 1 Główne informacje_

## 3.2 Biblioteki

| Nazwa biblioteki | Cel użycia |
| --- | --- |
| Entity Framework Core | Obsługa bazy danych |
| Prism | Zorganizowanie czystego kodu |
| RestSharp | Zapytania do HTTP |
| XUnit | Testy jednostkowe |
| AutoFixture | Testy TDD |
| HtmlAgilityPack | Uzyskanie informacji ze strony ceneo.pl |
| Material Design | Utworzenie przejrzystego stylu |
| FontAwsome | Poprawienie szaty graficznej |

_Tabela 2 Biblioteki_

## 3.3 Specyfikacja niefunkcjonalna

| ID | N1 |
| --- | --- |
| Nazwa | System organizacji pracy przy pomocy kanban board&#39;u |
| Opis | Utworzenie tablicy w serwisie [www.trello.com](http://www.trello.com/) |

| ID | N2 |
| --- | --- |
| Nazwa | Repozytorium internetowe |
| Opis | Stworzenie repozytorium wspomagające wspólną prace w serwisie www.github.com |

| ID | N3 |
| --- | --- |
| Nazwa | Utworzenie projektu |
| Opis | Utworzenie projektu, w którym zawrą się wszystkie aplikacje oraz serwisy |

| ID | N4 |
| --- | --- |
| Nazwa | Utworzenie silniku web |
| Opis | Utworzenie silniku web który będzie zajmował się organizacją danych |

| ID | N5 |
| --- | --- |
| Nazwa | Utworzenie aplikacji klienckiej |
| Opis | Utworzenie aplikacji, dla klienta która będzie przejrzysta i intuicyjna |

| ID | N6 |
| --- | --- |
| Nazwa | Utworzenie bazy danych |
| Opis | Utworzenie bazy danych w której będą przechowywane elementy subskrypcji użytkownika |

1.
# Projekt systemu

## 4.1 UML

![](RackMultipart20210322-4-1q0k4u5_html_df82146edb5b95cb.png)

_Rysunek 1 Rysunek techniczny uml_

Rysunek przedstawiający zasadę działania całego projektu.

## 4.2 Baza danych

| Product |
| --- |
| Nazwa: | Rodzaj: |
| Name | string |
| Link | string |
| Image | string |
| Rate | string |
| Price | string |

_Tabela 3 Struktura bazy danych_

1.
# Przebieg techniczny projektu

## 5.1 Shared

Została utworzona biblioteka klas zawierająca modele by ograniczyć nadmiarowość klas oraz kodu. W tym miejscu zawarte są dwa foldery w każdy po jednej klasie:

- Dtos

- ProductDto

- Name,
- Link,
- Image,
- Rate,
- Price,
- IsSubscribed,

- Model

- Product

- Name,
- Link,
- Image,
- Rate,
- Price,

Są to modele, na których oparty jest cały projekt. Dzięki temu każdy element projektu, który wymaga odwołania się do tych struktur, zawszę otrzyma taki sam i dzięki temu pozbyliśmy się nadmiarowych tworzeń owych klas/modeli w każdej części projektu z osobna.

## 5.2 WebEngine

Silnik, który został postawiony by niezależnie od aplikacji klienckiej zbierał informacje o produktach i działał w tle by użytkownik po uruchomieniu klienta mógł swobodnie wyszukiwać dane nie martwiąc się o brak połączenia. Silnik przy pomocy narzędzia o nazwie Swagger został naszym Web API. Głównym motywem wykorzystania tego sposobu była możliwość niezależnie od klienta sprawdzanie funkcjonalności co w późniejszym etapie pomogło przy komunikacji z klientem.

Struktura silnika:

- Klasa Program

Miejsce, w którym znajduje się nasz **main** i od niego zaczyna się całe działanie programu

- Klasa Startup

W tej klasie zawarte są zawarte konfiguracje startowe wraz z zadeklarowaniem i ustawieniem naszych serwisów.

- ConfigureServices

Jako parametr wejściowy przyjmuje kolekcje serwisów które w późniejszym etapie wdraża do programu i konfiguruje.

- Configure

W tym miejscu konfiguracyjnym aplikacja sprawdza czy jest w stanie developerskim, jeśli tak to aplikacja ustawiana jest by działała zgodnie z możliwościami Swaggeru. Po przeglądnięciu tego warunku jest również ustawienie naszej aplikacji by używała odpowiednie ustawienia jakie zostały dla niej przydzielone.

- Klasa WebScraper

Ta klasa jest odpowiedzialna za zdobywanie informacji ze strony. Jako iż nie udało się uzyskać oficjalnego klucza do usług API dla [www.ceneo.pl](http://www.ceneo.pl/), aplikacja musiała dostać w inny sposób dane które potrzebuje. W tym celu została wykorzystana technika web scrapingu dzięki której po wyodrębnieniu informacji jakich potrzebujemy, przetwarzamy je na model, którym jest Product. Ta klasa zawiera metody takie jak:

- Konstruktor,

W tym miejscu inicjujemy zmienną przechowującą nową stronę internetową oraz listę typu produkt.

- GetListOfProducts,

Metoda, która przyjmuje zmienną typu string, a dokładniej to link, który zostanie wygenerowany po podaniu przez użytkownika wyszukiwanej frazy. Na tym etapie, metoda tworzy oraz zwraca listę z produktami które znajdą się na stronie. Przeszukuje wyznaczone miejsca w kodzie HTML strony i wyciąga informacje, które nas interesują takie jak nazwa, link do produktu, link do zdjęcia, ocenę oraz cenę.

- GetProductPrice,

Ta funkcjonalność również przyjmuje link tylko że już konkretnego elementu. Po sprawdzeniu czy link nie przekierowuje nas do strony zewnętrznej, wyciąga informacje o najniższej cenie proponowanej przez portal ceneo.pl.

- Klasa ProductRepository

Jest to repozytorium aplikacji dzięki któremu możemy wykonywać operacje z bazą danych. Metody w niej zawarte są asynchronicznymi taskami ponieważ nasza aplikacja musi czekać na otrzymanie informacji od bazy danych.

Funkcje w niej zawarte to:

| Nazwa funkcji | Opis |
| --- | --- |
| AddProduct | Dodanie produktu do bazy danych |
| DeleteProduct | Usunięcie produktu z bazy danych |
| GetSubscribedProductAsync | Pobranie z bazy zasubskrybowanych produktów |
| IfProductExists | Sprawdza czy istnieje produkt o danym linku(id) |
| UpdateProduct | Aktualizuje cenę produktu |

_Tabela 4 Funkcje zawarte w ProductRepository_

- Klasa ProductInteriorPrice

Znajduje się ona w folderze &quot;Model&quot;, jak wczesniej wspomniałem, posiadamy bibliotekę osobną, lecz w tym wypadku ten model znajduje się tylko w przestrzeni nazw silnika a nie całego projektu. Zawarte w niej są tak naprawdę dwie klasy, nadrzędna, czyli ProductInteriorPrice której właściwością jest Offers natomiast ona jest zadeklarowana również w tym pliku a zawiera ona lowPrice.

- Interfejs IProductRepository
- Interfejs IWebSCraper

Korzystanie z interfejsów jest aktualnie standardem pracy, do naszych klas postanowiliśmy użyć tego sposobu by pisać kod czyściej, czytelniej i zgodnie z panującymi standardami.

- Klasa AutoMapperProfiles

Jest to klasa typu Helpers, dzięki niej projekt program jest w stanie przemapować klase Product na ProductDto.

- Klasa ProductContext

Zawiera kontekst potrzebny do wytworzenia bazy danych.

- Klasa BaseApiController

Kontroler dla naszego API.

- Klasa CeneoController

Ta klasa jest głównym kontrolerem serwera. W niej znajdują się metody, które decydują co wysłać w odpowiedzi do klienta.

- GetProductsFromCeneo

Metoda pobierająca informacje z ceneo.pl oraz sprawdzająca czy odpowiedź jaka przyszła nie jest nullem. Jeśli odpowiedź od strony jest prawidłowa, zwraca zmapowaną listę produktów, jeśli jednak jest nullem, zwróci wiadomość o błędzie do klienta.

- GetProductsAsync

Ten element aplikacji jest odpowiedzialny za zwrócenie do klienta listy rekordów z zasubskrybowanymi produktami. W razie problemów, zwróci wiadomość o błędzie do klienta.

- SubscribeProduct

Funkcja subskrybująca produkt która po wykonaniu akcji zwróci wiadomość, że produkt został dodany do listy bądź odpowie, że ten produkt się już na niej znajduje.

- UnsubscribeProduct

Funkcja, która usuwa przedmiot z listy subskrybowanych, po wykonaniu operacji zwraca odpowiedź czy udało się usunąć ten przedmiot.

- CheckProductsPrice

Metoda sprawdzająca czy w zasubskrybowanych produktach nie zaszła zmiana wartości ceny na niższą bądź wyższą co może pomoc w wyszukiwaniu okazji klientowi.

## 5.3 DekstopClient

Nasza aplikacja kliencka jest stworzona standardem MVVM, dzięki temu praca nad projektem była płynna, przejrzysta i ułożona w odpowiedni sposób. Aplikacja prezentuje się w następujący sposób

- Konwerter BooleanToVisibilityButtonConverter

Jest to konwerter wspomagający elementy widoku, jest w stanie przekonwertować wartości true/false na Visible/Collapsed i odwrotnie, co pomaga przy wizualizacji elementów.

- Interfejs IProductRepository
- Klasa ProductRepository

Zawiera wszystkie serwisy które wysyłają zapytania do serwera w sposób asynchroniczny jako Task.

- Klasa MainWindowViewModel

W tej klasie znajdziemy obsłużoną funkcjonalność klienta, wyszukiwanie, sprawdzanie poprawność wpisywanych fraz, subskrybowanie bądź usuwanie subskrypcji z przedmiotu.

- Widok

Jest to folder, który zawiera definicje przygotowanej szaty graficznej dla elementów w aplikacji klienckiej.

![](RackMultipart20210322-4-1q0k4u5_html_739b80bebd0cea4.png)

_Rysunek 3 Projekt Shared_

![](RackMultipart20210322-4-1q0k4u5_html_d41e498b8bd136a9.png)

_Rysunek 4 Projekt WebEngine_

![](RackMultipart20210322-4-1q0k4u5_html_c78b5571e5896bdf.png)

_Rysunek 5 Projekt DesktopClient_

1.
# Organizacja pracy

Podział zadań

**Grzegorz Waluś** - Utworzenie funkcjonalności silnika oraz połączenie z bazą danych.

**Jakub Gawęda** - Utworzenie funkcjonalności do pozyskania informacji ze strony internetowej, utworzenie dokumentacji.

**Sławomir Kędzior** - Utworzenie oraz opracowanie aplikacji klienckiej.

Praca jaka została wykonana nad projektem przebiegała w sposób płynny, cele były określane raz w tygodniu (poniedziałek), zebrania dotyczące dyskusji nad pracą odbywały się 2-4 razy w tygodniu. Łącznie na projekt zostało poświęcone 4 tygodnie pracy. Zadania jakie miały być realizowane, zostawały zapisane w serwisie Trello.com

| Trello.com | https://trello.com/b/6ctaNJU3/ceneo |
| --- | --- |
| Github.com | https://github.com/gwalus/CeneoSearcher |

_Linki do wykorzystania_

![](RackMultipart20210322-4-1q0k4u5_html_7ddfa857d57958bd.png)

_Rysunek 6 Trello_

Możliwość pisania wspólnego kodu została zrealizowana za pomocą serwisu github.com gdzie zostało utworzone repozytorium, w którym realizowaliśmy projekt.

![](RackMultipart20210322-4-1q0k4u5_html_a01a7272f5caf719.png)

_Rysunek 7 GitHub_

![](RackMultipart20210322-4-1q0k4u5_html_6205992337bf301d.png)

_Rysunek 8 Statystyki github_
