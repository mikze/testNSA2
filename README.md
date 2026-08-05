🌳 Strategia branchowania – model hybrydowy z szybką ścieżką wydawniczą
Poniższy diagram przedstawia przyjęty w projekcie model pracy z gałęziami. Dzięki rozdzieleniu środowisk testowych oraz zastosowaniu gałęzi buforowych (Rozwoj i Release_1) uzyskujemy pełną kontrolę nad wersjonowaniem i możliwość błyskawicznego wypuszczania pojedynczych, przetestowanych funkcji.
 
________________________________________
📋 Opis gałęzi i przepływu pracy
1. Gałęzie robocze (feature branches) – np. users/mz/*, users/kk/*
•	Źródło: Każdy branch developerski wychodzi bezpośrednio z gałęzi Rozwoj.
•	Cel: Miejsce do wprowadzania nowych funkcjonalności, poprawek i eksperymentów.
•	Zasada działania:
1.	Developer tworzy gałąź roboczą z Rozwoj.
2.	Po zakończeniu prac, wykonuje merga najpierw do Develop – to pierwszy krok, który uruchamia testy wewnętrzne (buildy deweloperskie z odpowiednim postfixem).
3.	Jeśli testy wypadną pomyślnie, dopiero wtedy wykonuje merga do Rozwoj (akceptacja i przygotowanie do dalszego etapu).
🧪 Kluczowa zasada: Zmiana trafia do Rozwoj dopiero po zatwierdzeniu jej na Develop. Dzięki temu Rozwoj zawsze zawiera tylko sprawdzony i stabilny kod.
________________________________________
2. Gałąź Develop – środowisko testów wewnętrznych
•	Rola: Główne środowisko integracyjne dla programistów. Służy do budowania wersji deweloperskich (zazwyczaj z postfixem -dev).
•	Charakter: Gałąź robocza, do której w pierwszej kolejności trafiają wszystkie feature branche (niezależnie od tego, czy są gotowe do wydania).
•	Przeznaczenie: Szybkie testowanie, walidacja poprawności zmian w izolowanym środowisku przed wpuszczeniem ich do głównego nurtu prac nad kolejnym wydaniem.
________________________________________
3. Gałąź Rozwoj – bufor przedprodukcyjny (przejściowy)
•	Rola: Jest to gałąź przejściowa, która pełni wyłącznie funkcję bufora przed środowiskiem Preprod. Jej głównym zadaniem jest zgromadzenie zatwierdzonych funkcji oraz zmiana postfiksu wersji z -dev na -pre.
•	Charakter: Nie służy do bezpośredniego rozwoju (nie piszemy w niej kodu). To punkt kontrolny, który decyduje, co trafi na środowisko testowe klienta.
•	Przepływ: Przyjmuje mergę z branchy roboczych dopiero po ich wcześniejszym sprawdzeniu na Develop.
________________________________________
4. Gałąź Preprod – środowisko przedprodukcyjne (UAT)
•	Rola: Środowisko zbliżone do produkcyjnego, na którym testy wykonuje klient lub zespół QA.
•	Źródło: Przyjmuje zawartość z Rozwoj (która ma już ustawiony postfix -pre).
•	Cel: Ostateczna weryfikacja aplikacji przed wdrożeniem na produkcję.
________________________________________
5. Gałąź Release_1 – bufor wydaniowy (przejściowy)
•	Rola: Kolejna gałąź przejściowa, której jedynym zadaniem jest usunięcie postfiksu -pre z numeru wersji, przygotowując czysty numer dla środowiska produkcyjnego.
•	Charakter: Tak jak Rozwoj, nie jest miejscem do pisania kodu źródłowego funkcji. Służy wyłącznie do operacji na wersjonowaniu (np. zmiana 2606.0.1.1-pre na 2606.0.1.1).
•	Źródło: Przyjmuje zawartość z Preprod.
________________________________________
6. Gałąź main – produkcja
•	Rola: Główna, stabilna gałąź produkcyjna. Zawiera kod aktualnie działający na środowisku produkcyjnym.
•	Źródło: Przyjmuje zawartość z Release_1 (już pozbawioną postfiksu -pre).
________________________________________
⚡ Szybkie wydawanie wybranej funkcji (Hotfix / Priority Release)
Jedną z największych zalet tego modelu jest możliwość błyskawicznego wypuszczenia do klienta tylko jednej, wybranej funkcji, nawet gdy równolegle trwają prace nad kilkoma innymi.
Jak to działa w praktyce?
1.	Załóżmy, że mamy trzy równoległe gałęzie robocze: users/mz/NowaFunkcja, users/kk/DodanieRaportu oraz users/ab/Eksperyment.
2.	Wszystkie trzy zostały zmergowane do Develop (przeszły testy wewnętrzne, ale nie wszystkie są jeszcze gotowe do wydania dla klienta).
3.	Tylko users/mz/NowaFunkcja została w pełni zaakceptowana i chcemy ją jak najszybciej wdrożyć na produkcję.
Co robimy?
•	Ponieważ Rozwoj jest ręcznie zarządzanym buforem (a nie automatyczną kopią Develop), wybieramy świadomie, co do niego wrzucamy.
•	Wykonujemy merga TYLKO users/mz/NowaFunkcja do Rozwoj (pomijając pozostałe dwie).
•	Następnie kontynuujemy standardowy flow: Rozwoj → zmiana postfixa na -pre → Preprod → Release_1 (usunięcie -pre) → main.
Efekt:
•	Klient dostaje tylko nową funkcję mz w kilka minut.
•	Pozostałe dwie funkcje (kk i ab) pozostają bezpieczne na Develop (lub na swoich gałęziach) i czekają na swój własny cykl wydawniczy, nie blokując przy tym pilnego wdrożenia.
🚀 Dzięki temu modelowi nie musimy czekać z wydaniem jednej poprawki na pozostałe, co jest ułatwieniem w sytuacjach awaryjnych lub gdy klient oczekuje konkretnej funkcji "na już".
________________________________________
🔄 Podsumowanie przepływu (krok po kroku)
1.	Prace developerskie → Developer tworzy gałąź z Rozwoj i pracuje nad funkcją.
2.	Testy wewnętrzne → Kod jest mergowany najpierw do Develop (testy automatyczne, build deweloperski).
3.	Akceptacja i selekcja → Po pomyślnych testach, kod jest mergowany do Rozwoj (TYLKO jeśli ma trafić do najbliższego wydania; jeśli nie – czeka na Develop).
4.	Zmiana postfiksu → Na Rozwoj ustawiany jest postfix -pre (przygotowanie do środowiska testowego).
5.	Środowisko testowe → Rozwoj jest mergowany do Preprod (klient/QA testują wersję -pre).
6.	Przygotowanie do produkcji → Preprod jest mergowany do Release_1.
7.	Usunięcie postfiksu → Na Release_X usuwany jest -pre, pozostaje czysty numer wersji.
8.	Wdrożenie → Release_X jest mergowany do main (produkcja).

