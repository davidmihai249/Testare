La deschiderea aplicatie, va aparea un meniu, cu urmatoarele functionalitati:
		1.Afisarea produselor existente in baza de date.
		2.Afisarea shipping fee-urilor existente in baza de date.
		3.Adaugare unui nou produs in cart. Trebuie sa fie introdus numele exact al obiectului dorit.
		4.Afisarea platii fara shipping fee, shipping fee-ul si totalul(suma celor doua).
		5.Afisarea platii cu discounts.
		6.Afisarea platii cu TVA-ul inclus.
		0.Iesirea din aplicatie.

Am utilizat o baza de date, SQLite. ConnectionFactory, DBUtils, si SqliteConnectionFactory sunt clase prin care
se face conectiunea cu baza de date.
Am utilizat o interfata pentru repository, implementand functiile CRUD pentru obiecte. IRepositoryProduct si IRepositoryShippingRate extind interfata,
fiind si ele la randul lor interfete, astfel de pot adauga mai multe functii unui repository pe obiect, nu doar cele din CRUD.
ProductRepository implementeaza interfata IRepositoryProduct, iar ShippingRateRepository implementeaza
interfata IRepositoryShippingRate.
Am respectat intr-o oarecare masura arhitectura stratificata.(Model/Domain, Repository,Service,UI).

Pentru a putea rula proiectul, va trebui sa adaugati baza de date. O veti gasi in folderul proiectului sub denumirea "ShoppingCartDB".
Apoi din fisierul App.config, trebuie sa inlocuiti path-ul din connectionString. Path-ul bazei de date, dupa adaugare acesteia in proiect, o veti gasi 
apasand click dreapta pe baza de data si properties, acolo veti avea URL-ul. Va trebuie sa inlocuiti din path "\" cu "/", iar inlocuirea din App.config se
va face in amble proiecte(ShoppingCart si ShoppingCart.UnitTests).
ConnectionString-ul va trebui sa arate ceva de genul:
	 <connectionStrings>
    		<clear />
   		<add name="ShoppingCartDB" providerName="" connectionString="Data Source=C:/Users/david/Desktop/Testare/ShoppingCart/ShoppingCartDB" />
  	 </connectionStrings>

Daca exista nelamuriri, sau apar probleme la rularea proiectului, va rog sa ma contactati!
