![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/41059f9a-1009-46d4-9c7a-61536890cf16)# BidIt_Saitynai

## Sistemos paskirtis

Projekto tikslas – sukurti svetainę skirtą knygų intuziastams, kurioje jie galėtų įdėti turimą knygą į varžytines bei jose dalyvauti, norėdami konkuruoti dėl norimos knygos. 

Veikimo principas – pačią kuriamą platformą sudaro dvi dalys: internetinė aplikacija, kuria naudojasi pirkėjai, ir administratorius bei aplikacijų programavimo sąsaja (angl. trump. API).

Svečias norėdamas naudotis šia sistema turi prisiregistruoti. Naudotojas gali naršyti po aukcionų katalogą, išsirinkus tinkamą aukcioną, norimame mieste arba pasirinkus norimą kompaniją, gali įdėti turimą knygą į varžytines, užpildžius reikiamą informaciją ir įkeliant nuotrauką. Taipogi naudotojas gali pats dalyvauti varžytinėse konkuruojant dėl norimos knygos. Aukcionas atsiranda saraše tik tuomet, kada administratorius jį patvirtina. Administratorius gali tą patį ką ir naudotojas bei pridėti kompaniją, ištrinti naudotojo paskyrą. 

Hierarchija: knyga < aukcionas < miestas.

## Funkciniai reikalavimai

Svečias gali:

1.	Prisiregistruoti.
2.	Prisijungti.
3.	Naršyti po svetainę.

Registruotas sistemos naudotojas gali:

1.	Prisijungti.
2.	Atsijungti.
3.	Pridėti knygą į varžytines pasirinktame aukcione, mieste.
4.	Naršyti po svetainę.
5.	Redaguoti dedamą knygą į aukcioną, kol jis nepatvirtintas.
6.	Statyti už norimą prekę.
7.	Peržiūrėti pasirinktos kompanijos aukcionų knygas ir pnš.

Administratorius gali :
1.	Pašalinti naudotojus.
2.	Patvirtinti tinkamą prekę varžytinėms.
3.	Pašalinti netinkamą prekę iš varžytinių.
4.	Statyti už norimą prekę.
5.	Pridėti kompaniją.
6.	Pašalinti kompaniją.
7.	Sukurti naują aukcioną.
8.	Pašalinti aukcioną.

## Sistemos architektūra
Sistemos sudedamosios dalys:

•	Kliento pusė (ang. Front-End) – Blazor;
•	Serverio pusė (ang. Back-End) –.NET;
•	Duomenų bazė – MySQL.

pav. 1 pavaizduota kuriamos sistemos diegimo diagrama. Sistemos talpinimui yra naudojamas Azure serveris. Kiekviena sistemos dalis yra diegiama tame pačiame serveryje. Internetinė aplikacija yra pasiekiama per HTTPS protokolą.  Šios sistemos veikimui yra reikalingas BidIt API, kuris pasiekiamas per aplikacijų programavimo sąsają. Pats BidIt API vykdo duomenų mainus su duomenų baze – tam naudojama TCP/IP sąsaja.
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/b21e4044-e1e3-4d03-aab2-19f2768334d7)

## Wireframe
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/e7afab40-af74-43b7-99e9-95cafec31dc1)
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/1eb858e1-2f77-4a5f-b835-b5b03785c117)
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/b4d37bb8-cf76-478e-b38b-090c2d252615)
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/60cdf896-c706-4133-a450-e9eeb7e17ea1)
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/01c7aa64-5281-429d-b9a3-cb5b11eb11e9)
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/4ce6b4ed-503a-4552-882f-a9e780dfe63e)
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/aad9fa34-a20d-4869-a3b1-8ef88cf9da85)

## Realizacija
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/68fce805-4e9c-4cfc-985e-9867cb5fe630)
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/38df21f7-a34d-459b-9f8f-4773c9f5bd17)
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/7ee7218c-9026-4b8a-a713-a253a9f1e0d0)
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/40215ca8-93a6-4af8-8462-2913c2fe893f)
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/da8428ff-65bf-422f-9d19-968ccb3c2c42)
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/184eb5c5-f7eb-461e-8b2c-92fa278302a0)
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/d03a9885-3282-4ba0-a846-22c3925c286b)
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/a6422266-1e99-4c33-baba-05dd1218e28b)
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/3eab6776-4a4e-4396-99e9-b2a3ac45b5d4)
![image](https://github.com/Zazigalke/BidIt_v1/assets/133056297/800f0c17-20f8-46d1-bd61-6210a4ef0c07)

## Api specifikacija
Projekte yra 25 API endpointai.
|                 | Get visas knygas                             |
| ----------------| ---------------------------------------------|
| Metodas         | Get                                          |
| Autentifikacija | Nėra                                         |
| Parametrai      | Nėra                                         | 
| URL             | https://biditclient.azurewebsites.net/books  |
| Aprašymas       | Gražinamos visos knygos                      |
| Response kodai  | 200, 400                                     |
| Užklausa        | https://biditclient.azurewebsites.net/books  |

Response
```json
[
    {
        "id": 1,
        "title": "asdass",
        "condition": 1,
        "pageCount": 100,
        "startingPrice": 1.1,
        "user": 1
    },
    {
        "id": 2,
        "title": "asdass",
        "condition": 1,
        "pageCount": 100,
        "startingPrice": 1.1,
        "user": 1
    }
]
```
|                 | Get viena knygas                                       |
| ----------------| -------------------------------------------------------|
| Metodas         | Get                                                    |
| Autentifikacija | Nėra                                                   |
| Parametrai      | Id - knygos numeris                                    | 
| URL             | https://biditclient.azurewebsites.net/api/books/:id    |
| Aprašymas       | Gražinama viana knyga                                  |
| Response kodai  | 200, 404                                               |
| Užklausa        | https://biditclient.azurewebsites.net/api/books/1      |

Response
```json
    {
        "id": 1,
        "title": "asdass",
        "condition": 1,
        "pageCount": 100,
        "startingPrice": 1.1,
        "user": 1
    },
```
|                 | Post  knygas                                     |
| ----------------| -------------------------------------------------|
| Metodas         | Post                                             |
| Autentifikacija | User arba Admin                                  |
| Parametrai      | Nėra                                             | 
| URL             | https://biditclient.azurewebsites.net/api/books  |
| Aprašymas       | Pridedama nauja knyga                            |
| Response kodai  | 201, 404                                         |
| Užklausa        | https://biditclient.azurewebsites.net/api/books  |

Response
```json
{
    "id": 4,
    "title": "Knyga",
    "condition": 2,
    "pageCount": 123,
    "startingPrice": 123,
    "user": 1
}
```
|                 | Put knygas                                         |
| ----------------| ---------------------------------------------------|
| Metodas         | Put                                                |
| Autentifikacija | User arba Admin                                    |
| Parametrai      | Id                                                 | 
| URL             | https://biditclient.azurewebsites.net/api/books/:id|
| Aprašymas       | Redaguoti knyga                                    |
| Response kodai  | 200, 404                                           |
| Užklausa        | https://biditclient.azurewebsites.net/api/books/1  |

Response
```json
{
    "id": 1,
    "title": "asdasa",
    "condition": 10,
    "pageCount": 100,
    "startingPrice": 1.1,
    "user": 1
}
```
|                 | Delete knygas                                      |
| ----------------| ---------------------------------------------------|
| Metodas         | Delete                                             |
| Autentifikacija | User arba Admin                                    |
| Parametrai      | Id                                                 | 
| URL             | https://biditclient.azurewebsites.net/api/books/:id|
| Aprašymas       | Redaguoti knyga                                    |
| Response kodai  | 200, 404                                           |
| Užklausa        | https://biditclient.azurewebsites.net/api/books/1  |

Kompanijos 
|                 | Get kompanijas                                         |
| ----------------| -------------------------------------------------------|
| Metodas         | Get                                                    |
| Autentifikacija | Admin                                                  |
| Parametrai      | Nėra                                                   | 
| URL             | https://biditclient.azurewebsites.net/api/companies    |
| Aprašymas       | Get kompanijas                                         |
| Response kodai  | 200, 404                                               |
| Užklausa        | https://biditclient.azurewebsites.net/api/companies    |

Response
```json
[
    {
        "id": 1,
        "name": "Kompanija4"
    },
    {
        "id": 4,
        "name": "Komaaa"
    },
    {
        "id": 5,
        "name": "Kompanija"
    },
    {
        "id": 6,
        "name": "Kompanija Nauja"
    }
]
```
|                 | Get viena kompaniją                                    |
| ----------------| -------------------------------------------------------|
| Metodas         | Get                                                    |
| Autentifikacija | Admin                                                  |
| Parametrai      | Id                                                     |   
| URL             | https://biditclient.azurewebsites.net/api/companies/:id|
| Aprašymas       | Get kompanijas                                         |
| Response kodai  | 201, 404                                               |
| Užklausa        | https://biditclient.azurewebsites.net/api/companies/1  |

Response
```json

    {
        "id": 1,
        "name": "Kompanija4"
    }
    

```
|                 | Post kompaniją                                         |
| ----------------| -------------------------------------------------------|
| Metodas         | Post                                                   |
| Autentifikacija | Admin                                                  |
| Parametrai      | Nėra                                                   |   
| URL             | https://biditclient.azurewebsites.net/api/companies    |
| Aprašymas       | Post kompanijas                                        |
| Response kodai  | 201, 404                                               |
| Užklausa        | https://biditclient.azurewebsites.net/api/companies    |

Response
```json
    {
        "id": 1,
        "name": "Kompanija4"
    },
```
|                 | Put kompaniją                                          |
| ----------------| -------------------------------------------------------|
| Metodas         | Put                                                    |
| Autentifikacija | Admin                                                  |
| Parametrai      | Id                                                     |   
| URL             | https://biditclient.azurewebsites.net/api/companies/:id|
| Aprašymas       | Post kompanijas                                        |
| Response kodai  | 201, 400, 404                                          |
| Užklausa        | https://biditclient.azurewebsites.net/api/companies/1  |

Response
```json
    {
        "id": 1,
        "name": "Kompanija4"
    },
```
|                 | Delete kompaniją                                       |
| ----------------| -------------------------------------------------------|
| Metodas         | Delete                                                 |
| Autentifikacija | Admin                                                  |
| Parametrai      | Id                                                     |   
| URL             | https://biditclient.azurewebsites.net/api/companies/:id|
| Aprašymas       | Delete kompanijas                                      |
| Response kodai  | 200, 404                                               |
| Užklausa        | https://biditclient.azurewebsites.net/api/companies/1  |

Aukcionai

|                 | Get aukcionai                                          |
| ----------------| -------------------------------------------------------|
| Metodas         | Get                                                    |
| Autentifikacija | User arba Admin                                        |
| Parametrai      | Nėra                                                   |   
| URL             | https://biditclient.azurewebsites.net/api/auctions     |
| Aprašymas       | Post kompanijas                                        |
| Response kodai  | 201, 400, 404                                          |
| Užklausa        | https://biditclient.azurewebsites.net/api/auctions     |

Response
```json
[
    {
        "id": 3,
        "name": "Aukcionas1",
        "city": "Vilnius",
        "creationDate": "2023-12-21T03:57:47.866161",
        "startingTime": "2023-12-21T03:57:47.866161",
        "endingTime": "2023-12-21T03:57:47.866162",
        "company": "Kompanija4"
    },
    {
        "id": 19,
        "name": "Aukcionas2",
        "city": "Kaunas",
        "creationDate": "2023-12-21T23:18:33.689572",
        "startingTime": "2023-12-21T23:18:33.689572",
        "endingTime": "2023-12-21T23:18:33.689573",
        "company": "Komaaa"
    },
    {
        "id": 20,
        "name": "Aukcionas3",
        "city": "Kaunas",
        "creationDate": "2023-12-22T01:00:45.143278",
        "startingTime": "2023-12-22T01:00:45.143304",
        "endingTime": "2023-12-22T01:00:45.143322",
        "company": "Komaaa"
    },
    {
        "id": 21,
        "name": "Aukcionas5",
        "city": "Marijampole",
        "creationDate": "2023-12-22T01:52:09.58909",
        "startingTime": "2023-12-22T01:52:09.589132",
        "endingTime": "2023-12-22T01:52:09.589166",
        "company": "Komaaa"
    }
]
```
|                 | Get vieną aukcioną                                     |
| ----------------| -------------------------------------------------------|
| Metodas         | Get                                                    |
| Autentifikacija | Admin ir User                                          |
| Parametrai      | Id                                                     |   
| URL             | https://biditclient.azurewebsites.net/api/auctions/:id |
| Aprašymas       | Get aukcioną                                           |
| Response kodai  | 200, 404                                               |
| Užklausa        | https://biditclient.azurewebsites.net/api/auctions/3   |

Response
```json
{
    "id": 3,
    "name": "Aukcionas1",
    "city": "Vilnius",
    "creationDate": "2023-12-21T03:57:47.866161",
    "startingTime": "2023-12-21T03:57:47.866161",
    "endingTime": "2023-12-21T03:57:47.866162",
    "company": "Kompanija4"
}

```
|                 | Post aukcioną                                          |
| ----------------| -------------------------------------------------------|
| Metodas         | Post                                                   |
| Autentifikacija | Admin                                                  |
| Parametrai      | Nėra                                                   |   
| URL             | https://biditclient.azurewebsites.net/api/auctions     |
| Aprašymas       | Post aukcioną                                          |
| Response kodai  | 200, 404                                               |
| Užklausa        | https://biditclient.azurewebsites.net/api/auctions     |

Response
```json
{
    "id": 22,
    "name": "ad",
    "city": "DASD",
    "creationDate": "2023-12-22T03:46:59.4684671+00:00",
    "startingTime": "2023-12-22T03:46:59.4684677+00:00",
    "endingTime": "2023-12-22T03:46:59.468468+00:00",
    "company": "Kompanija4"
}
```
|                 | Put aukcioną                                           |
| ----------------| -------------------------------------------------------|
| Metodas         | Put                                                    |
| Autentifikacija | Admin                                                  |
| Parametrai      | Id                                                     |   
| URL             | https://biditclient.azurewebsites.net/api/auctions/:id |
| Aprašymas       | Put kompanija                                          |
| Response kodai  | 200, 404, 403                                          |
| Užklausa        | https://biditclient.azurewebsites.net/api/auctions/3   |

Response
```json
{
    "id": 3,
    "name": "Aukcionas2",
    "city": "Aukcionas2",
    "creationDate": "2023-10-19T23:51:56.742312",
    "startingTime": "2023-10-19T23:51:56.742312",
    "endingTime": "2023-10-19T23:51:56.742312",
    "company": "Komaaa"
}
```
|                 | Delete aukcioną                                        |
| ----------------| -------------------------------------------------------|
| Metodas         | Delete                                                 |
| Autentifikacija | Admin                                                  |
| Parametrai      | Id                                                     |   
| URL             | https://biditclient.azurewebsites.net/api/auctions/:id |
| Aprašymas       | Delete kompanija                                       |
| Response kodai  | 200, 404,                                              |
| Užklausa        | https://biditclient.azurewebsites.net/api/auctions/3   |


# Išvados
Naudojantis „.NET Core“ karkasą (serverio pusei) ir „Blazor" sukurta knygų aukcionų svetainė. Šioje svetainėje galima kurti, redaguoti, ištrinti knygas, aukcionus, kompanijas. Vartotojų yra 3 rūšys: Admin, User ir Guest. Serverio dalis įdiegta "Azure" debesyje.
