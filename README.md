# CST LePoint — Système de Gestion de Transport & Pointage

> **Rapport de Projet de Fin d'Études (PFE)**  
> Plateforme full-stack de gestion de flotte de bus, de collecte du personnel et de suivi en temps réel, avec intelligence artificielle intégrée pour la prédiction du temps d'arrivée des bus (ETA).

---

## Table des Matières

1. [Vue d'ensemble du projet](#1-vue-densemble-du-projet)  
2. [Architecture technique](#2-architecture-technique)  
3. [Stack technologique — détail complet](#3-stack-technologique--détail-complet)  
4. [Structure du projet — fichier par fichier](#4-structure-du-projet--fichier-par-fichier)  
5. [Fonctionnalités & modules](#5-fonctionnalités--modules)  
6. [Base de données & couche de données](#6-base-de-données--couche-de-données)  
7. [API & logique backend](#7-api--logique-backend)  
8. [Frontend Angular](#8-frontend-angular)  
9. [Authentification & sécurité](#9-authentification--sécurité)  
10. [Configuration & environnement](#10-configuration--environnement)  
11. [Installation & démarrage](#11-installation--démarrage)  
12. [Tests](#12-tests)  
13. [Déploiement](#13-déploiement)  
14. [Défis & décisions de conception](#14-défis--décisions-de-conception)  
15. [Glossaire](#15-glossaire)  

---

## 1. Vue d'ensemble du projet

### Qu'est-ce que ce projet ?

CST LePoint est une application web full-stack développée pour la société **CST (Compagnie de Services de Transport)** en Tunisie. Elle couvre deux problématiques opérationnelles étroitement liées :

1. **La gestion de flotte de bus** : création et gestion des circuits de collecte, des points de collecte géolocalisés, des bus, des chauffeurs et du suivi de position en temps réel via des modems IoT embarqués.
2. **La gestion du personnel** : gestion des employés, de leurs rattachements aux circuits/bus/shifts, des équipes, des ordres de travail et des historiques de présence.

Le système résout un problème concret dans le secteur du transport industriel tunisien : les entreprises ont besoin de savoir en permanence où se trouvent leurs bus, combien d'employés sont à bord, si les conducteurs assignés sont bien présents, et si les véhicules respectent les itinéraires prédéfinis. Avant ce système, ce suivi était fait manuellement (feuilles de pointage papier, appels téléphoniques), ce qui engendrait des erreurs, des retards et un manque total de visibilité en temps réel.

### Qui l'utilise ?

- **Administrateurs** : gèrent l'ensemble des données maîtresses (sociétés, utilisateurs, rôles, circuits, points de collecte).
- **Opérateurs** : suivent les bus en temps réel sur une carte interactive, consultent les événements de runtime.
- **Analystes** : génèrent des rapports BI dynamiques sur les bus, les employés et les traces de position.
- **Modems IoT embarqués** : des appareils physiques installés dans les bus envoient automatiquement leur position GPS et le taux d'occupation via l'API REST.

### Motivation

Le projet est né de la nécessité de digitaliser et centraliser des processus dispersés. La solution apporte une **traçabilité complète** (qui conduit quel bus sur quel circuit, avec quels employés), une **détection automatique des anomalies de parcours** (géofencing), une **prédiction IA du temps d'arrivée des bus (ETA)** et une **gestion fine des autorisations** par navigation et par action.

---

## 2. Architecture technique

### Architecture globale

Le projet adopte une architecture **microservices légère** avec trois services principaux orchestrés par Docker Compose :

```
┌─────────────────────────────────────────────────────────────────┐
│                        CLIENT (Browser)                         │
│              Angular 18 SPA – Port 4200 (Nginx)                 │
└───────────────────────────┬─────────────────────────────────────┘
                            │ HTTP/REST + JWT Bearer
                            ▼
┌─────────────────────────────────────────────────────────────────┐
│                   .NET 8 Backend API – Port 6064                │
│          Clean Architecture + CQRS (MediatR) + Carter           │
│    ┌────────────┐  ┌──────────────┐  ┌──────────────────────┐  │
│    │ WebAPI     │  │ Application  │  │ Infrastructure       │  │
│    │ (Endpoints)│→ │ (Commands/   │→ │ (EF Core, JWT,       │  │
│    │            │  │  Queries)    │  │  Repos, Services)    │  │
│    └────────────┘  └──────────────┘  └──────────────────────┘  │
└────────────┬──────────────────────────────┬─────────────────────┘
             │ SQL (EF Core)                │ HTTP (HttpClient)
             ▼                              ▼
┌─────────────────────┐      ┌──────────────────────────────────┐
│  SQL Server 2019    │      │  FastAPI ML Service – Port 8001  │
│  Database:          │      │  eta_prediction microservice     │
│                     │      │  (Python, prédiction ETA bus)    │
└─────────────────────┘      └──────────────────────────────────┘
```

### Architecture du backend : Clean Architecture en couches

Le backend suit la **Clean Architecture** de Robert C. Martin (Uncle Bob) organisée en 4 projets C# distincts :

**Couche Domain** (`CollectManagement.Domain`) — le cœur pur. Contient uniquement les entités, les value objects et les règles métier fondamentales. Aucune dépendance externe.

**Couche Application** (`CollectManagement.Application`) — les cas d'utilisation. Définit les commandes CQRS, les interfaces de repositories et de services que l'infrastructure doit implémenter. Dépend uniquement du Domain.

**Couche Infrastructure** (`CollectManagement.Infrastructure`) — l'implémentation technique. EF Core, JWT, services de fichiers, PDF, prédiction ETA des bus, hachage de mots de passe. Dépend du Domain et de l'Application.

**Couche Présentation** (`CollectManagement.WebAPI`) — les endpoints HTTP. Utilise Carter pour définir les routes sous forme de modules. Dépend des trois couches inférieures via injection de dépendances.

Cette séparation garantit que le cœur métier (Domain + Application) est **totalement indépendant** du framework ASP.NET, de la base de données et de toute librairie tierce — une exigence clé pour la testabilité et la maintenabilité.

### Pattern CQRS avec MediatR

Chaque opération métier est représentée par soit une **Command** (mutation de données) soit une **Query** (lecture). MediatR sert de médiateur : l'endpoint envoie un message (`sender.Send(command)`) sans connaître son handler. Le pipeline MediatR ajoute automatiquement :
- La journalisation (`LoggingBehavior`)
- La validation des entrées (`ValidationBehavior` via FluentValidation)
- La gestion de la transaction (`UnitOfWorkBehavior` qui appelle `SaveChangesAsync` après chaque command)

---

## 3. Stack technologique — détail complet

### Backend

| Technologie | Version | Rôle dans le projet | Raison du choix |
|---|---|---|---|
| **.NET 8** | 8.0 | Plateforme runtime | LTS, performances élevées, cross-platform |
| **ASP.NET Core Minimal API** | 8.0 | Hôte HTTP | Plus léger que les controllers classiques |
| **Carter** | latest | Routing modulaire | Organise les endpoints en ICarterModule, séparation propre |
| **MediatR** | 12.x | Bus CQRS interne | Découple handlers des endpoints, pipeline behaviors |
| **Entity Framework Core 8** | 8.x | ORM | Migrations, LINQ, requêtes split, NoTracking par défaut |
| **SQL Server 2019** | — | SGBD relationnel | Robustesse enterprise, déjà utilisé chez CST |
| **FluentValidation** | 11.x | Validation des entrées | API fluente, intégration MediatR behavior |
| **Mapster** | 7.x | Object mapping | Plus rapide que AutoMapper, génération de code |
| **Serilog** | 3.x | Journalisation structurée | Sink Console + File JSON rolling, configurable |
| **Rin** | — | Inspecteur dev (HTTP timeline) | Debuggage des requêtes en développement |
| **JWT Bearer (HMAC-SHA256)** | — | Authentification stateless | Standard industrie, pas de session côté serveur |
| **Puppeteer Sharp** | — | Génération de PDF | Rendu HTML fidèle via Chrome headless |
| **ULID** | 1.3.3 | Identifiants distribués | Tri chronologique, URL-safe, meilleur que GUID pour SQL |
| **SonarAnalyzer** | 9.27 | Analyse statique | Qualité code, détection bugs potentiels |

La configuration partagée `Directory.Build.props` active `ImplicitUsings`, `Nullable` (C# 8 nullable reference types) et enforce les code styles sur tous les projets C# en une seule fois.

### Frontend

| Technologie | Version | Rôle dans le projet | Raison du choix |
|---|---|---|---|
| **Angular 18** | 18.0.1 | Framework SPA | Standalone components, signals-ready, enterprise-grade |
| **Angular Material** | 18.0.1 | Composants UI | Formulaires, dialogs, snackbars, paginateurs Material Design |
| **Fuse (UI Kit)** | custom | Thème & layout | Layout "modern" prêt à l'emploi avec menu latéral, thèmes |
| **Tailwind CSS** | 3.4.3 | Utilitaires CSS | Personnalisation rapide sans écrire de CSS custom |
| **DevExtreme** | 24.1.6 | Grilles de données | `dx-data-grid` avec tri/filtre/export Excel natif |
| **Leaflet + ngx-leaflet** | 1.9.4 | Cartographie | Cartes interactives open-source, markers colorés, routing |
| **ApexCharts + ng-apexcharts** | 3.49.1 | Graphiques analytiques | Charts réactifs SVG pour les dashboards BI |
| **Chart.js** | 3.9.1 | Graphiques secondaires | Complément pour certains types de graphiques |
| **Transloco** | 6.0.4 | Internationalisation | Chargement lazy des traductions, support 6 langues |
| **RxJS** | 7.8.1 | Programmation réactive | Observables, BehaviorSubject, opérateurs de transformation |
| **Luxon** | 3.4.4 | Manipulation de dates | Immutable, timezone-aware, adapté Angular Material |
| **XLSX** | 0.18.5 | Export Excel | Export des grilles DevExtreme vers .xlsx |
| **pdfjs-dist** | 4.6.82 | Visionneuse PDF | Affichage inline des documents PDF |
| **crypto-js** | 4.2.0 | Cryptographie client | Hachage/chiffrement côté client si nécessaire |
| **Prettier** | 3.3.0 | Formatage de code | Cohérence du style, organisation automatique des imports |

---

## 4. Structure du projet — fichier par fichier

### Racine du workspace

```
c:\Users\rayen\RiderProjects\
├── docker-compose.yml          # Orchestration des 4 services (db, backend, frontend, eta-prediction)
├── .gitignore                  # Exclusions Git globales
├── backend/                    # Solution .NET 8 complète
├── frontend/                   # Application Angular 18
├── docs/                       # Documentation technique et guides de débogage
└── *.md                        # Fichiers de documentation de résolution de problèmes
```

### Backend (`backend/`)

```
backend/
├── CST.LePoint.sln             # Solution Visual Studio / Rider (4 projets)
├── Directory.Build.props       # Propriétés MSBuild partagées (net8.0, Nullable, ULID, Sonar)
├── Dockerfile                  # Build multi-stage : sdk:8.0 → aspnet:8.0
└── src/CollectManagement.ms/
    ├── CollectManagement.Domain/
    ├── CollectManagement.Application/
    ├── CollectManagement.Infrastructure/
    └── CollectManagement.WebAPI/
```

#### `CollectManagement.Domain/`
Contient toutes les entités métier sans aucune dépendance externe. Chaque entité suit un pattern strict :
- Constructeur privé (force l'utilisation des factories)
- `Create()` pour les nouvelles instances
- `QueryCreate()` pour la reconstruction depuis la base (EF Core)
- `Update()` pour les mutations
- Properties en `private set` (immutabilité partielle)

```
Domain/
├── Common/
│   └── AuditableEntity.cs      # Classe de base : InsererPar, DateInsertion, ModifierPar, DateModification
├── Analyse/
│   └── ReportLayout.cs         # Entité de sauvegarde des layouts BI personnalisés
├── Bus/
│   ├── Bus.cs                  # Entité Bus (IMEI, NumeroIMM, CodeCircuit, occupancy, position)
│   └── BusRuntimeEvent.cs      # Événements temps-réel : PositionUpdated, OutOfRadiusScan, BusEmptied
├── Circuits/
│   ├── Circuit.cs              # Itinéraire de collecte (code, libellé, distance, durée, couleur)
│   └── CircuitPointCollecte.cs # Association ordonnée circuit → points de collecte
├── Employes/
│   └── Employe.cs              # Employé avec RFID, géolocalisation domicile, codes d'affectation
├── Equipes/
│   └── Equipe.cs               # Équipe de travail (interne ou sous-traitant)
├── OrdresTravail/
│   ├── OrdreTravail.cs         # Ordre de travail (numéro, chantier, client, état, montant)
│   └── OrdreTravailDetail.cs   # Lignes de détail d'un ordre de travail
├── PointsCollecte/
│   └── PointCollecte.cs        # Point géolocalisé de collecte d'employés
├── Rattachements/
│   ├── Rattachement.cs         # Opération de rattachement (dates, heures, coût, type, statut)
│   ├── RattachementEmploye.cs  # Association rattachement → employé
│   └── RattachementArticle.cs  # Association rattachement → article/fourniture
├── Societes/
│   └── Societe.cs              # Société (données légales : TVA, RC, matricule fiscal, logo)
├── Utilisateurs/
│   ├── Utilisateur.cs          # Utilisateur applicatif (login, password hashé, rôle, societe)
│   ├── Entities/
│   │   ├── RoleUtilisateur.cs  # Rôle avec liste de navigations + actions autorisées
│   │   └── UtilisateurSite.cs  # Sites autorisés pour un utilisateur
├── Chauffeurs/Chauffeur.cs     # Chauffeur avec RFID de validation d'identité
├── Chantiers/Chantier.cs       # Site de chantier (numéro, libellé, client)
├── Gouvernorats/Gouvernorat.cs # Division administrative (gouvernorat tunisien)
├── Regions/Region.cs           # Sous-division régionale avec CodeGouvernorat
├── Modems/Modem.cs             # Modem IoT embarqué (IMEI, SIM, modèle)
├── Shifts/Shift.cs             # Shift de travail (code, libellé, heureDebut, heureFin)
├── Sites/Site.cs               # Site géographique d'une société
└── Reseaux/Reseau.cs           # Réseau de transport
```

#### `CollectManagement.Application/`
```
Application/
├── DependencyInjection.cs      # Enregistrement : Mapster, FluentValidation, MediatR + behaviors
├── Common/
│   └── ApiResponse<T>.cs       # Enveloppe standard : success, statusCode, message, data, validationErrors
├── Behaviors/
│   ├── LoggingBehavior<,>.cs   # Logue chaque commande/requête avec son résultat
│   ├── ValidationBehavior<,>.cs# Exécute FluentValidation avant le handler
│   └── UnitOfWorkBehavior<,>.cs# Appelle SaveChangesAsync après les commands uniquement
├── Exceptions/
│   └── *.cs                    # NotFoundException, BadCredentialException, ValidationException, etc.
├── Handlers/
│   └── *ExceptionHandler.cs    # IExceptionHandler ASP.NET pour chaque type d'exception
├── Interfaces/
│   ├── Repositories/           # Interfaces de tous les repositories (ICircuitRepository, etc.)
│   └── Services/               # IPasswordService, IJwtTokenGenerator, IExternalPredictionService (ETA bus), etc.
├── Contracts/
│   └── Predictions/            # DTOs de requête/réponse pour la prédiction ETA bus
└── Features/
    └── {Entity}/               # Un dossier par entité métier :
        ├── Commands/Create{Entity}/   → {Entity}Command.cs + CommandHandler.cs
        ├── Commands/Update{Entity}/   → idem
        ├── Commands/Delete{Entity}/   → idem
        ├── Queries/GetPagedList{Entity}/ → Query.cs + Handler.cs + Response.cs
        ├── Queries/GetOne{Entity}/    → Query.cs + Handler.cs + DTO.cs
        └── Mapping/                  → TypeAdapterConfig (Mapster)
```

#### `CollectManagement.Infrastructure/`
```
Infrastructure/
├── DependencyInjection.cs          # AddInfrastructureServices : DB, Auth, Repos, Services
├── Authentification/
│   ├── JwtTokenGenerator.cs        # Génération JWT HS256 avec claims sub/email/role/jti
│   └── JwtOptions.cs               # POCO lié à appsettings JwtOptions
├── Interceptors/
│   └── AuditableInterceptor.cs     # SaveChanges interceptor : remplit InsererPar/ModifierPar/dates
├── Persistence/
│   ├── Context/
│   │   └── ApplicationDbContext.cs # DbContext EF Core, OnModelCreating avec assembly scanning
│   ├── Repositories/
│   │   ├── RepositoryBase<T>.cs    # Implémentation générique : CRUD + BulkUpdate + SqlQuery
│   │   ├── UnitOfWork.cs           # Wraps SaveChangesAsync, GetRepository<T>()
│   │   └── {Entity}Repositories/  # Repositories spécialisés par domaine
│   └── Migrations/                 # Fichiers de migration EF Core (dans le schéma F3SManagement)
├── Services/
│   ├── LoggedInUserService.cs      # Extrait UserId depuis les claims JWT (IHttpContextAccessor)
│   ├── DateTimeProvider.cs         # Abstraction de DateTime.Now
│   ├── PasswordService.cs          # Hachage BCrypt des mots de passe
│   ├── ImageService.cs             # Lecture/écriture d'images sur le filesystem
│   ├── PdfGeneratorService.cs      # Génération PDF via Puppeteer (Chrome headless)
│   ├── BrowserProvider.cs          # Singleton du navigateur Puppeteer
│   ├── DocumentService.cs          # Orchestration de la génération de documents
│   └── ExternalPredictionService.cs# HttpClient appelant le ML service (port 8001)
├── Common/
│   └── NullableUlidJsonConverter.cs# Converter JSON System.Text.Json pour Ulid?
└── PuppeteerConfig/
    └── PuppeteerOptions.cs         # POCO : ChromePath
```

#### `CollectManagement.WebAPI/`
```
WebAPI/
├── Program.cs                      # Point d'entrée : builder, DI, middleware pipeline, migrations auto
├── DependencyInjection.cs          # AddPresentation : Carter, Swagger, JSON converters
├── appsettings.json                # Configuration : JWT, DB, CORS, Serilog, Puppeteer
├── appsettings.Development.json    # Surcharges de développement
├── Authorization/
│   ├── NavigationPermissionHandler.cs  # IAuthorizationHandler : vérifie navigations/actions en DB
│   ├── NavigationPermissionRequirement.cs # Définit le policy "NavigationPermission"
│   └── NavigationEndpointExtensions.cs    # Mappe HTTP method → FuseNavigationAction
└── EndPoints/
    ├── AuthenticationEndpoints.cs  # POST login/login-check (v1 admin + v99 super-admin)
    ├── BusEndpoints.cs             # CRUD bus + runtime position IoT + stream + vider + events
    ├── CircuitEndpoints.cs         # CRUD circuits
    ├── EmployeEndpoints.cs         # CRUD employés
    ├── PredictionEndpoints.cs      # 7 endpoints ML (durée, absence, ETA bus)
    ├── AnalyseEndpoints.cs         # BI layouts + query runner (bus/employe/trace)
    └── ...                         # Un module Carter par entité métier
```

### Frontend (`frontend/`)

```
frontend/
├── Dockerfile                      # node:20-alpine (build) → nginx:1.27-alpine (serve)
├── nginx.conf                      # Config Nginx SPA : try_files $uri $uri/ /index.html
├── angular.json                    # Workspace Angular (projet "fuse")
├── package.json                    # Dépendances npm (Angular 18, DevExtreme 24, Leaflet, etc.)
├── .prettierrc                     # Règles de formatage
├── public/
│   ├── config/setting-config.json  # URL de l'API chargée dynamiquement au démarrage
│   ├── i18n/                       # Traductions : fr.json, en.json, ar.json, es.json, it.json, tr.json
│   ├── icons/                      # Sprites SVG : Feather, Heroicons, Material (outline/solid/twotone)
│   └── images/                     # Assets statiques (logos, avatars, marqueur carte)
└── src/
    ├── main.ts                     # bootstrapApplication(AppComponent, appConfig)
    ├── app/
    │   ├── app.component.ts        # Root component (RouterOutlet)
    │   ├── app.config.ts           # ApplicationConfig (providers, Transloco, Fuse, Auth)
    │   ├── app.routes.ts           # Définition complète des routes (AuthGuard, NavigationGuard)
    │   ├── app.resolvers.ts        # initialDataResolver : charge navigation + user au démarrage
    │   ├── core/
    │   │   ├── auth/               # AuthService, AuthGuard, NoAuthGuard, authInterceptor, AuthUtils
    │   │   ├── user/               # UserService, User interface
    │   │   ├── navigation/         # NavigationService, navigation.data.ts, NavigationGuard
    │   │   ├── common/             # ApiService (wrapper HTTP), ApiResponse interface
    │   │   ├── config/             # SettingConfigService (charge setting-config.json)
    │   │   ├── role-utilisateur/   # RoleNavigation model
    │   │   ├── circuit/            # CircuitService + Circuit model
    │   │   ├── bus/                # BusService + Bus model + BusRuntimeEvent model
    │   │   ├── employe/            # EmployeService + Employe model
    │   │   └── ...                 # Un service + modèle par domaine
    │   ├── modules/
    │   │   ├── auth/               # sign-in, sign-out, sign-up, forgot-password, reset-password
    │   │   ├── collectmanagement/
    │   │   │   ├── accueil/        # Dashboard / page d'accueil
    │   │   │   ├── monitoring/
    │   │   │   │   └── bus-tracking/  # Carte temps-réel + liste + panneau événements
    │   │   │   └── analyse/        # BI bus, BI employé, trace GPS
    │   │   └── cst/                # Un module CRUD par entité : circuit, bus, employe, etc.
    │   ├── layout/                 # LayoutComponent (Fuse modern layout)
    │   └── mock-api/               # Mocks Fuse pour la navigation initiale
    └── environments/
        ├── environment.ts          # Prod : BaseApi, mapGeocodingApi
        └── environment.development.ts  # Dev : http://localhost:6064/cm/
```

## 5. Fonctionnalités & modules

### 5.1 Authentification & gestion des utilisateurs

L'utilisateur accède à `/sign-in` et saisit son login, son mot de passe, son identifiant de société (`societeId`) et optionnellement un numéro de chantier. Ces informations sont envoyées au endpoint `POST cm/authentication/v99/login`. Le backend vérifie les credentials, génère un JWT et retourne le token ainsi que toutes les informations du profil utilisateur y compris ses navigations autorisées. Le frontend stocke le token dans `localStorage` et redirige vers le tableau de bord.

Au rechargement de la page, `signInUsingToken()` appelle `POST cm/authentication/v99/login-check` pour revalider le token existant et récupérer un token fraîs. Un intercepteur HTTP attache automatiquement le `Bearer` à toutes les requêtes. Si un 401 arrive, l'utilisateur est déconnecté et redirigé vers la page de connexion.

Il existe deux versions de login : `v1` (admin standard) et `v99` (super-admin), actuellement identiques dans leur comportement mais séparées pour une future différenciation des droits.

### 5.2 Gestion des rôles & permissions (RBAC Navigation)

C'est la fonctionnalité de sécurité la plus sophistiquée du projet. Chaque `RoleUtilisateur` possède une liste de `Navigations` : chaque navigation correspond à un module de l'application (ex: `fichier.circuit`, `monitoring.bus-tracking`, `analyse.bi-bus`) et définit les **actions** autorisées (`Read`, `Add`, `Edit`, `Delete`).

Au login, les navigations de l'utilisateur sont retournées dans le JWT payload et stockées dans le `UserService`. Le `NavigationService` filtre dynamiquement l'arbre de menu pour n'afficher que les rubriques accessibles. Côté backend, chaque groupe de routes Carter déclare `.RequireNavigationPermission("navigation.id")` qui déclenche le `NavigationPermissionHandler` : ce handler interroge la base de données pour vérifier que l'utilisateur possède bien l'action correspondant à la méthode HTTP de la requête.

Les utilisateurs sans rôle (super-admin) contournent cette vérification et ont accès à tout.

### 5.3 Gestion des données maîtresses (Master Data CRUD)

L'application expose des modules CRUD complets pour 17 entités métier, tous accessibles depuis le menu `/fichier/` :

- **Sociétés** : données légales (TVA, RC, matricule fiscal, RNE, capital, logo)
- **Circuits** : itinéraires de bus (code, libellé, distance en km, durée estimée, couleur de visualisation, coordonnées départ/arrivée)
- **Points de collecte** : emplacements GPS où les employés montent dans le bus (code, libellé, lat/lng, gouvernorat, région)
- **Employés** : registre complet (matricule, RFID, nom, prénom, adresse, affectations bus/circuit/shift)
- **Bus** : flotte (numéro d'immatriculation, IMEI modem, modèle, capacité, chauffeur assigné, circuit)
- **Chauffeurs** : conducteurs avec carte RFID pour validation d'identité
- **Équipes** : groupes de travail (internes ou sous-traitants, client, entrepôt, tarif)
- **Ordres de travail** : missions avec détails (chantier, client, bon de commande, état, montant)
- **Rattachements** : opérations de pointage (date, heure début/fin, type, nature, coût, statut)
- **Shifts** : créneaux horaires de travail (code, libellé, heures)
- **Chantiers** : sites de chantier
- **Modems** : appareils IoT (IMEI, SIM, modèle) à associer aux bus
- **Régions / Gouvernorats** : découpage administratif tunisien
- **Sites & Réseaux** : organisations géographiques de la société

Chaque module suit le même pattern UX : une grille DevExtreme avec tri/filtrage/pagination côté serveur, un panneau latéral de saisie (formulaire Angular Material réactif), et des confirmations de suppression via les dialogues Fuse.

### 5.4 Suivi des bus en temps réel (`/monitoring/bus-tracking`)

C'est la fonctionnalité la plus visuellement riche. Elle est composée de trois sous-composants :

**BusTrackingListComponent** : liste latérale des bus avec leur numéro d'immatriculation, leur circuit, leur taux d'occupation et leur statut (actif/inactif). Un champ de recherche filtre la liste côté client.

**BusTrackingMapComponent** : carte Leaflet interactive centrant les bus sur leurs coordonnées GPS courantes. Chaque bus est représenté par un marqueur coloré. La sélection d'un bus dans la liste centre la carte sur ce bus.

**BusTrackingPanelComponent** : panneau d'historique affichant la timeline des événements de runtime du bus sélectionné (`PositionUpdated`, `OutOfRadiusScan`, `AutoPointCollecteGenerated`, `BusEmptied`), triés du plus récent au plus ancien.

La **mise à jour automatique** fonctionne par polling toutes les 10 secondes via `timer(0, 10000)` + `switchMap`. Un toggle permet d'activer/désactiver le rafraîchissement automatique. Le bouton "Vider le Bus" ouvre un dialogue de confirmation puis appelle `POST cm/bus/{id}/vider` qui remet l'occupation à zéro et crée un événement `BusEmptied`.

### 5.5 IoT : Mise à jour de position en temps réel

L'endpoint `POST cm/bus/runtime/position` est appelé par les modems physiques installés dans les bus. Il reçoit l'IMEI du modem, les coordonnées GPS, le taux d'occupation et optionnellement l'horodatage UTC. Le backend :

1. Valide que le timestamp n'est pas décalé de plus de 15 minutes (protection anti-replay)
2. Retrouve le bus par IMEI
3. Vérifie l'RFID du chauffeur si un chauffeur est assigné au bus
4. Met à jour les champs de position et d'occupation du bus
5. Calcule la distance Haversine entre la position du bus et le point de collecte le plus proche du circuit (géofencing 250m)
6. Si le bus est hors périmètre, crée automatiquement un `PointCollecte` avec le code `PC-AUTO-{timestamp}` et logue un événement `OutOfRadiusScan`
7. Logue systématiquement un événement `PositionUpdated`

### 5.6 Prédiction IA — Estimation du temps d'arrivée des bus (ETA)

Le service ML FastAPI (port 8001) fournit un modèle de prédiction du **temps d'arrivée estimé (ETA)** des bus à leur prochain point de collecte. Cette fonctionnalité est accessible via le groupe d'endpoints `cm/prediction`.

**Prédiction ETA d'un bus individuel** : l'endpoint `POST cm/prediction/bus-eta` reçoit les caractéristiques courantes d'un bus (distance estimée au prochain arrêt, coordonnées GPS actuelles, code circuit, modèle du bus, capacité, taux d'occupation actuel, horodatage de la dernière position) et retourne l'ETA estimé en minutes, en secondes, ainsi qu'un score de confiance du modèle.

**ETA de tous les bus disponibles** : l'endpoint `GET cm/prediction/bus-eta/available` orchestre automatiquement la prédiction pour l'ensemble des bus actifs de la société connectée. Le backend :
1. Résout la société de l'utilisateur via ses claims JWT
2. Charge tous les bus actifs de cette société
3. Charge tous les circuits et leurs points de collecte associés
4. Pour chaque bus, calcule la **distance Haversine** entre sa position GPS courante et son point de destination — en cherchant d'abord le point dont le code correspond à `CodePCArrivee` du circuit, en se repliant sur le point au `Ordre` le plus élevé, puis sur les coordonnées brutes du circuit, et en dernier recours sur une distance déterministe pseudo-aléatoire (graine basée sur le code du circuit et l'immatriculation, entre 300 et 2000 mètres)
5. Appelle `PredictBusEtaAsync` en parallèle pour tous les bus (`Task.WhenAll`)
6. Retourne la liste ordonnée par numéro d'immatriculation avec ETA et confiance

**Intégration dans l'interface** : les résultats d'ETA sont affichés dans le panneau de suivi temps réel (`/monitoring/bus-tracking`), permettant aux opérateurs de voir en un coup d'œil combien de minutes chaque bus mettra pour arriver à destination.

### 5.7 Analyse & Business Intelligence (`/analyse`)

Trois onglets de BI dynamique : **Bus**, **Employés** et **Trace**. Les utilisateurs peuvent concevoir des layouts de rapport personnalisés (sauvegardés en base via `UpsertReportLayout`) et exécuter des requêtes d'analyse ad-hoc via `POST cm/analyse/{type}/query`. Les résultats sont visualisés avec ApexCharts.

---

## 6. Base de données & couche de données

### Identifiants ULID

Toutes les clés primaires utilisent le type **ULID** (Universally Unique Lexicographically Sortable Identifier) encapsulé dans des value objects fortement typés (ex : `CircuitId`, `BusId`, `SocieteId`). L'avantage sur les GUID classiques est double : les ULIDs sont triables chronologiquement (les 10 premiers bits encodent le timestamp), ce qui améliore les performances d'insertion dans les index SQL Server, et ils sont lisibles dans les URL (Base32 Crockford).

### Multi-tenancy par SocieteId

Toutes les entités métier (Circuit, Bus, Employe, PointCollecte, etc.) portent un champ `SocieteId` qui joue le rôle de discriminant multi-tenant. Chaque requête filtre implicitement par la société de l'utilisateur connecté (résolue via `ILoggedInUserService`). Il n'existe pas de schéma de base séparé par tenant : c'est une approche **shared-schema multi-tenant**.

### Entités de la base de données

**Societe** — Table des sociétés clientes. Champs principaux : `SocieteId` (ULID PK), `Nom`, `CodeSociete`, `Tva`, `Rc`, `MatriculeFiscal`, `Rne`, `Capital`, `DateOverture`, `Telephone1/2`, `Fax1/2`, `Email`, `Adresse`, `CodePostal`, `Ville`, `Pays`, `LogoPath`. Plus les 4 champs d'audit hérités de `AuditableEntity`.

**Utilisateur** — Table des comptes applicatifs. `UtilisateurId`, `NomUtilisateur` (login), `Nom`, `Prenom`, `Email`, `Password` (hashé BCrypt), `RoleUtilisateurId` (FK nullable — null = super-admin), `SocieteId` (FK), `IsActive`. Relation many-to-many avec `UtilisateurSite`.

**RoleUtilisateur** — Rôles avec leur matrice de permissions stockée sous forme de JSON dans la colonne `Navigations` (liste de `{ NavigationId, Actions[] }`).

**Employe** — `EmployeId`, `Matricule` (identifiant RH), `RFID` (tag de badge), `Nom`, `Prenom`, `CodeCircuit`, `CodePointCollecte`, `CodeBus`, `CodeShift`, `Adresse`, `CodeGouvernorat`, `CodeRegion`, `Latitude`, `Longitude`, `SocieteId`.

**Circuit** — `CircuitId`, `CodeCircuit` (identifiant métier), `LibelleCircuit`, `Description`, `IsActive`, `Latitude/Longitude` (centre du circuit), `CodePCDepart/Arrivee` (codes des points extrêmes), `DistanceKm`, `DureeMinutes`, `Couleur` (hex pour la carte), `SocieteId`.

**CircuitPointCollecte** — Table de liaison ordonnée entre Circuit et PointCollecte. Contient `CircuitId`, `CodePointCollecte`, `Latitude`, `Longitude`, `Ordre` (entier déterminant la séquence sur l'itinéraire).

**PointCollecte** — `PointCollecteId`, `CodePointCollecte`, `LibellePointCollecte`, `Latitude` (decimal), `Longitude` (decimal), `CodeGouvernorat`, `CodeRegion`, `IsActive`, `SocieteId`, `CircuitId` (FK optionnelle — un point peut être générique ou spécifique à un circuit).

**Bus** — `BusId`, `NumeroIMM` (numéro d'immatriculation), `IMEI` (identifiant unique du modem IoT), `CodeCircuit`, `CodeChauffeur`, `ModelBus`, `Capacite` (nombre de passagers max), `CurrentOccupancy` (nombre actuel), `Latitude/Longitude`, `LastPositionAt`, `LastOccupancyUpdateAt`, `IsActive`, `SocieteId`.

**BusRuntimeEvent** — Journal des événements IoT. `BusRuntimeEventId`, `BusId` (FK), `EventType` (string : `PositionUpdated`, `OutOfRadiusScan`, `AutoPointCollecteGenerated`, `BusEmptied`), `Description`, `IMEI`, `Latitude/Longitude`, `Occupancy`, `OccurredAtUtc`.

**Chauffeur** — `ChauffeurId`, `CodeChauffeur`, `Nom`, `Prenom`, `RFIDChauffeur` (tag RFID de la carte conducteur), `IsActive`, `SocieteId`.

**Equipe** — `EquipeId`, `CodeEquipe`, `LibelleEquipe`, `CodeClient`, `CodeEntrepot`, `CodeTarif`, `CodeFournisseur`, `Responsable`, `IsInternal` (bool), `CodeVehicule`, `IsActive`, `SocieteId`.

**OrdreTravail** — `OrdreTravailId`, `NumeroOrdreTravail`, `NumeroChantier`, `CodeClient`, `NumeroBonCommande`, `CodeEquipe`, `EtatOT`, `Montant`, `DateCreation`, `NumeroConvention`, `CodeVehicule`, `Libelle`, `IsActive`, `SocieteId`. Relation one-to-many avec `OrdreTravailDetail`.

**Rattachement** — `RattachementId`, `NumeroRattachement`, `Exercice`, `DateRattachement`, `NumeroChantier`, `CodeClient`, `IsInternal`, `Cout`, `Type`, `Nature`, `Responsable`, `HeureDebut/Fin` (TimeSpan), `Emplacement`, `Reference`, `Status`, `DateCloture`, `Remarque`, `IsActive`, `SocieteId`. Relations one-to-many avec `RattachementEmploye` et `RattachementArticle`.

**Shift** — `ShiftId`, `CodeShift`, `LibelleShift`, `HeureDebut/Fin`, `SocieteId`.

**ReportLayout** — Layouts BI sauvegardés. `ReportLayoutId`, `Nom`, `ReportType` (enum Bus/Employe/Trace), `LayoutJson` (configuration sérialisée en JSON), `SocieteId`.

**Chantier, Gouvernorat, Region, Modem, Site, Reseau** — Tables de référentiel avec des champs code/libellé et `IsActive`.

### Migrations EF Core

Les migrations sont stockées dans le schéma `F3SManagement` (`MigrationsHistoryTable`). La méthode `Database.Migrate()` est appelée au démarrage de l'application, ce qui applique automatiquement toutes les migrations en attente. En complément, des scripts SQL idempotents ajoutent manuellement les colonnes manquantes (`IsActive`, `ModelModem`, `NumeroSim`, `CodeGouvernorat`) sur les tables Region, Modem, Chauffeur et Gouvernorat — une stratégie de migration en deux phases typique des projets évolutifs.

### Pattern Repository

La classe `RepositoryBase<T>` implémente `IRepositoryBase<T>` et fournit les opérations CRUD universelles :
- `AddAsync`, `AddRangeAsync` — ajout simple ou en masse
- `Update`, `UpdateState` — mise à jour avec tracking EF
- `BulkUpdateAsync<TExpression>` — `ExecuteUpdate` pour les mises à jour sans chargement en mémoire
- `DeleteAsync` — `ExecuteDelete` pour les suppressions sans chargement
- `GetById`, `GetAsync`, `GetMany`, `GetManyAsync` — lectures avec prédicats
- `GetPagedReponseAsync` — pagination (Skip/Take + AsNoTracking)
- `ListSelect`, `ListSelectMany` — projections typées
- `SqlQueryFirstAsync`, `SqlQueryListAsync` — SQL brut via `Database.SqlQuery<T>` pour les requêtes complexes d'analyse

---

## 7. API & logique backend

Tous les endpoints utilisent le préfixe `cm/` et retournent systématiquement une `ApiResponse<T>` enveloppant la donnée avec `success`, `statusCode`, `message`, `data` et `validationErrors`.

### Authentification (`cm/authentication`) — public

| Méthode | Route | Corps | Retour | Description |
|---|---|---|---|---|
| POST | `v99/login` | `{ login, password, societeId, numeroChantier }` | `ApiResponse<AuthenticationResponse>` | Login super-admin |
| POST | `v1/login` | idem | idem | Login admin standard |
| POST | `v99/login-check` | `{ Token }` | `ApiResponse<AuthenticationResponse>` | Revalide token existant |
| POST | `v1/login-check` | idem | idem | idem |

### Circuits (`cm/circuit`) — `RequireNavigationPermission("fichier.circuit")`

| Méthode | Route | Paramètres | Retour |
|---|---|---|---|
| GET | `list` | `?search&sort&order&page&size` | `ApiResponse<GetPagedListCircuitResponse>` |
| POST | `add` | body: `CreateCircuitCommand` | `ApiResponse<CreateCircuitResponse>` |
| PATCH | `update` | body: `UpdateCircuitCommand` | `ApiResponse<bool>` |
| POST | `{id}/delete` | route: `Ulid id` | `ApiResponse<bool>` |
| GET | `{id}/one` | route: `Ulid id` | `ApiResponse<GetOneCircuitDto>` |

Le pattern est strictement identique pour les endpoints : Employe, PointCollecte, Equipe, OrdreTravail, Rattachement, Shift, Chantier, Gouvernorat, Region, Modem, Chauffeur, Societe, Utilisateur, RoleUtilisateur, Site, Reseau.

### Bus (`cm/bus`) — `RequireNavigationPermission("fichier.bus")`

En plus des 5 opérations CRUD standard, le module Bus expose :

| Méthode | Route | Description |
|---|---|---|
| POST | `runtime/position` | Appelé par les modems IoT — met à jour GPS + occupancy, géofencing, événements |
| GET | `runtime/positions/stream` | Retourne un snapshot de tous les bus actifs avec leur dernière position connue |
| POST | `{id}/vider` | Remet l'occupation à 0 et logue `BusEmptied` |
| GET | `{id}/events` | Retourne l'historique des `BusRuntimeEvent` du bus, trié par date décroissante |

**Logique détaillée de `POST runtime/position`** :

Le corps est `{ IMEI, Latitude, Longitude, Occupancy, TimestampUtc, RFIDChauffeur }`. L'IMEI sert de clé d'identification (les modems ne connaissent pas leur BusId interne). La tolérance de timestamp évite les replays tardifs. La validation RFID est optionnelle : elle ne s'active que si le bus a un `CodeChauffeur` assigné ET que ce chauffeur a un `RFIDChauffeur` renseigné. Le géofencing calcule la distance Haversine entre la position reçue et chaque point du circuit du bus ; si tous sont à plus de 250m, le système génère automatiquement un `PointCollecte` nommé `PC-AUTO-{yyyyMMddHHmmss}`.

### Prédiction ETA des bus (`cm/prediction`) — `RequireNavigationPermission("fichier.bus")`

| Méthode | Route | Description |
|---|---|---|
| POST | `bus-eta` | Prédit l'ETA d'un bus individuel (distance au prochain arrêt, coords GPS, circuit, modèle, capacité, occupation) |
| GET | `bus-eta/available` | ETA pour tous les bus actifs de la société — orchestration complète avec résolution Haversine |
| GET | `metadata` | Métadonnées du modèle de prédiction (version, features utilisées, indicateurs de performance) |

L'endpoint `bus-eta/available` est particulièrement complet : il résout la société de l'utilisateur connecté, charge tous les bus actifs, tous les circuits et leurs points de collecte, calcule la distance Haversine entre chaque bus et son point de destination (en préférant `CodePCArrivee`, puis le point au plus grand `Ordre`, puis les coordonnées du circuit, puis une distance déterministe de repli), puis appelle `PredictBusEtaAsync` en parallèle pour tous les bus via `Task.WhenAll`.

### Analyse (`cm/analyse`) — permissions par sous-module

Trois sous-groupes : `bus` (`analyse.bi-bus`), `employe` (`analyse.bi-employe`), `trace` (`analyse.trace`).

Chacun expose :
- `GET layouts` — récupère les layouts sauvegardés pour ce type de rapport
- `POST layouts` — crée ou met à jour un layout
- `POST layouts/{id}/delete` — supprime un layout
- `POST query` — exécute une requête analytique dynamique et retourne les résultats

### Gestion des exceptions

La chaîne d'exception handlers transforme les exceptions en réponses HTTP structurées :
- `ValidationException` → 400 avec `validationErrors[]`
- `NotFoundException` → 404
- `BadCredentialException` → 401
- `UnAuthorizedException` → 401
- `ForbiddenException` → 403
- Toute autre exception → 500 via `GlobalExceptionHandling`

La première étape de la chaîne est `ExceptionLoggingHandler` qui logue toute exception via Serilog avant de la passer au handler suivant.

---

## 8. Frontend Angular

### Bootstrap de l'application

Le fichier `main.ts` appelle `bootstrapApplication(AppComponent, appConfig)`. Le fichier `app.config.ts` définit tous les providers :

- **`APP_INITIALIZER` (SettingConfigService)** : charge `public/config/setting-config.json` avant toute requête API. Ce fichier JSON contient l'URL de base de l'API (`baseApi`), ce qui permet de reconfigurer l'URL sans recompiler l'application.
- **`APP_INITIALIZER` (TranslocoService)** : charge la langue par défaut (Français) avant le rendu, évitant le flash de contenu non traduit.
- **`provideRouter`** avec `PreloadAllModules` : précharge tous les lazy modules en arrière-plan après le premier rendu.
- **`LuxonDateAdapter`** + `MAT_DATE_LOCALE: 'fr-FR'` : adapte les datepickers Material au format français.
- **`provideTransloco`** : 6 langues disponibles (fr, en, ar, es, it, tr), rechargement à chaud au changement de langue.
- **`provideFuse`** : configure le thème visuel (layout `modern`, scheme `light`, 6 thèmes de couleur disponibles).

### Gestion de l'état

L'application n'utilise pas de store global (pas de NgRx ni Akita). Chaque service injecte des `BehaviorSubject` qui agissent comme des mini-stores locaux. Par exemple, `CircuitService` maintient `_circuits$` (liste courante), `_circuit$` (entité sélectionnée) et `_circuitLength$` (total pour la pagination). Les composants s'abonnent à ces observables dans leur template avec `async pipe` ou dans `ngOnInit`.

### API Service — wrapper HTTP central

`ApiService` est le seul point d'accès HTTP de toute l'application. Il encapsule `HttpClient` et fournit des méthodes typées : `Get<T>`, `Post<T>`, `Patch<T>`, `Delete<T>`, `SilentPost<T>` (sans feedback), `GetFile`, `GetBlob`, `GetPdf`. Toutes les méthodes non-silencieuses affichent automatiquement :
- Un snackbar de succès vert "Enregistré avec succès." après un POST/PATCH/DELETE réussi
- Un snackbar d'erreur rouge avec le message de l'API en cas d'échec

### Intercepteur HTTP (`authInterceptor`)

Injecté dans le provider `HttpClient`, cet intercepteur fonctionnel (pas de classe) :
1. Lit le token depuis `AuthService.accessToken`
2. Vérifie qu'il n'est pas expiré via `AuthUtils.isTokenExpired` (décodage du JWT côté client)
3. Si valide, clone la requête en ajoutant l'en-tête `Authorization: Bearer {token}`
4. Intercepte les erreurs 401 pour déclencher la déconnexion et la redirection vers `/sign-in`

### Routing & Guards

**`AuthGuard`** : utilise `authService.check()` qui appelle `signInUsingToken()` si l'utilisateur n'est pas encore authentifié en mémoire. Si le token localStorage existe mais est expiré, le guard retourne `false` et déclenche la redirection.

**`NoAuthGuard`** : l'inverse — empêche un utilisateur déjà connecté d'accéder aux pages d'auth.

**`navigationGuard`** : utilisé en `canActivateChild` sur toutes les routes protégées. Lit le `navigationId` depuis les données de la route (`route.data.navigationId`) et vérifie que l'utilisateur courant possède cette navigation dans son profil. Si non, redirige vers la page d'accueil.

**`initialDataResolver`** : résolveur sur la route racine authentifiée. Appelle en parallèle `NavigationService.get()` et `UserService.get()` pour s'assurer que le menu et le profil utilisateur sont prêts avant le premier rendu.

### Structure des modules CRUD (`/modules/cst/{entity}/`)

Chaque module de données maîtresses suit une structure identique :

- **`{entity}.component.ts`** : composant shell avec `<router-outlet>` qui charge la liste ou le formulaire de détail.
- **`{entity}.routes.ts`** : définit deux routes (`''` → ListComponent, `':id'` → DetailsComponent). Le résolveur de la route détails charge soit l'entité existante depuis l'API (`GetById`), soit crée une entité vide (`CreateNew`). Il charge aussi les permissions de navigation pour le composant.
- **`list/`** : composant de liste utilisant `dx-data-grid` de DevExtreme. La grille est configurée avec pagination côté serveur, tri/filtrage synchronisés avec les paramètres d'URL, et export Excel intégré. Les actions d'ajout/modification naviguent vers `DetailsComponent` ; la suppression ouvre un dialogue de confirmation Fuse.
- **`details/`** : formulaire Angular Material réactif avec `FormGroup` et `FormControl`. Les champs utilisent `mat-form-field` avec validation inline. Les listes déroulantes (ex: sélection de circuit, de gouvernorat) sont alimentées par des services dédiés.

### Module Monitoring (`bus-tracking`)

`BusTrackingComponent` est le composant principal avec `ChangeDetectionStrategy.OnPush` pour des performances optimales. Il gère :
- **Le polling** : `autoRefreshControl` est un `FormControl(true)`. `startWith(value)` + `switchMap` crée/détruit automatiquement un `timer(0, 10000)` selon l'état du toggle.
- **La sélection** : `_selectedBusId$` est un `BehaviorSubject<string | null>`. Un `switchMap` sur ce subject annule automatiquement la requête d'événements précédente et charge les événements du nouveau bus sélectionné.
- **La carte** : `BusTrackingMapComponent` reçoit les `MapLocation[]` calculés par `BusTrackingAdapterService.buildMapLocations()`. L'adapter fusionne les données statiques des bus (nom, circuit) avec le snapshot de position live pour créer des `BusTrackingItem`.
- **Le "Vider"** : action d'urgence qui confirme puis appelle `EmptyBus`, met à jour l'état local optimistement, puis déclenche un `refreshOnce()`.

### Internationalisation

Transloco charge les fichiers JSON de traduction depuis `public/i18n/`. La langue par défaut est le Français. Les traductions sont utilisées via le pipe `transloco` dans les templates : `{{ 'cle.traduction' | transloco }}`. Le service supporte le rechargement à chaud : changer la langue active recharge les composants sans navigation.

---

## 9. Authentification & sécurité

### JWT (JSON Web Token)

Le backend génère des tokens JWT signés avec l'algorithme **HMAC-SHA256** (HS256). La clé secrète est stockée dans `appsettings.json` sous `JwtOptions.Secret`. Le token contient les claims suivants :
- `sub` : `UtilisateurId` (ULID) — identifiant unique de l'utilisateur
- `unique_name` : `NomUtilisateur` — login
- `email` : adresse email
- `jti` : ULID aléatoire — identifiant unique du token (anti-replay)
- `role` : `RoleUtilisateurId` (ULID) — rôle de l'utilisateur

La durée de vie du token est de 99999 minutes (≈70 jours) dans la configuration actuelle. Cette valeur est volontairement longue pour le développement et devrait être réduite en production. Il n'y a pas de mécanisme de refresh token : l'endpoint `login-check` revalide les credentials via le token existant et génère un nouveau token.

### Hachage des mots de passe

Les mots de passe sont hashés avec **BCrypt** via `PasswordService`. BCrypt est un algorithme de hachage adaptatif avec sel intégré, résistant aux attaques par force brute et par table arc-en-ciel. Le sel est différent pour chaque utilisateur, généré automatiquement.

### RBAC granulaire (Navigation-based RBAC)

Le système de contrôle d'accès est basé sur des **navigations** et des **actions**, pas sur des rôles simples. Cela permet une granularité extrêmement fine : un utilisateur peut avoir accès à la lecture des circuits mais pas à leur modification, ou à la modification des bus mais pas à leur suppression.

Le `NavigationPermissionHandler` s'exécute à chaque requête HTTP authentifiée sur une route protégée. Il charge le profil complet de l'utilisateur depuis la base de données (avec ses navigations), puis :
1. Retrouve la navigation correspondant au `navigationId` de l'endpoint
2. Identifie l'action requise selon la méthode HTTP : `GET` → `Read`, `POST /add` → `Add`, `PATCH` → `Edit`, `POST /delete` → `Delete`
3. Vérifie que `navigation.Actions.Contains(requiredAction)`
4. Succède ou échoue l'autorisation

Ce système est double : le backend le contrôle côté API (inviolable), et le frontend le reflète dans le menu (en filtrant les items de navigation) et dans les composants (en désactivant les boutons non autorisés via `hasActionPermission(action)`).

### CORS

La configuration CORS autorise explicitement les origines `localhost:4200` (dev), `localhost:6067/6070` et deux adresses IP publiques tunisiennes. Les méthodes autorisées sont `GET, POST, PUT, PATCH, DELETE` et les en-têtes `Authorization` et `Content-Type`.

### Validation des entrées

FluentValidation est exécuté via le `ValidationBehavior` MediatR avant tout handler de command. Les erreurs sont agrégées et retournées dans `ApiResponse.validationErrors[]` avec un statut 400.

### Validation timestamp IoT

L'endpoint de position des bus valide que le timestamp envoyé par le modem ne dépasse pas 15 minutes de décalage avec `DateTime.UtcNow`. Cette vérification empêche le traitement de messages IoT trop anciens (buffering réseau prolongé, replay d'anciens messages).

---

## 10. Configuration & environnement

### Backend — `appsettings.json`

| Clé | Description |
|---|---|
| `ConnectionStrings.SqlServerConnection` | Chaîne de connexion SQL Server. En Docker, surchargée par la variable d'environnement `ConnectionStrings__SqlServerConnection` |
| `JwtOptions.Secret` | Clé secrète HMAC-SHA256 (doit faire ≥32 caractères en production) |
| `JwtOptions.ExpiryMinutes` | Durée de vie du token en minutes (actuellement 99999) |
| `JwtOptions.Issuer` | Identifiant de l'émetteur JWT (AJ.CST) |
| `JwtOptions.Audience` | Audience JWT (Dispatching) |
| `Puppeteer.ChromePath` | Chemin absolu vers Chrome pour la génération PDF |
| `Cors.AllowedOrigins[]` | Liste des origines autorisées pour CORS |
| `Serilog.*` | Configuration des sinks (Console + File JSON rolling daily) |

### Frontend — `public/config/setting-config.json`

Ce fichier est chargé **au runtime** (pas à la compilation) par `SettingConfigService`. Il permet de déployer le même build Angular dans différents environnements en changeant uniquement ce fichier JSON :

```json
{
  "baseApi": "http://your-api-server:6064/cm/"
}
```

### Frontend — `environments/`

| Fichier | BaseApi |
|---|---|
| `environment.development.ts` | `http://localhost:6064/cm/` |
| `environment.ts` (production) | Défini au build time via `--configuration production` |

### Variables d'environnement Docker

Dans `docker-compose.yml`, le service `backend` surcharge la ConnectionString via :
```
ConnectionStrings__SqlServerConnection: Server=db,1433;Database=PointageBus;...
```
ASP.NET Core transforme automatiquement les doubles underscores `__` en séparateur de section de configuration.

---

## 11. Installation & démarrage

### Prérequis

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (recommandé) — ou :
  - .NET SDK 8.0
  - Node.js 20.x + npm
  - SQL Server 2019 (ou Azure SQL Edge)
  - Google Chrome (pour la génération PDF)

### Option A — Démarrage complet avec Docker Compose (recommandé)

```bash
# Cloner le repository
git clone <url-du-repo>
cd RiderProjects

# Démarrer tous les services (db, backend, frontend, ml)
docker-compose up --build

# L'application est disponible sur :
# Frontend : http://localhost:4200
# API : http://localhost:6064/cm/
# ML Service : http://localhost:8001
# Swagger : http://localhost:6064/swagger (si env=Development)
```

Docker Compose gère :
1. Le démarrage de SQL Server 2019 avec un health-check (attente que SQL soit prêt avant le backend)
2. Le build .NET et l'exécution des migrations automatiques au démarrage
3. Le build Angular et le service Nginx
4. Le service ML FastAPI avec les artefacts de modèle montés en volume

### Option B — Démarrage en développement (sans Docker)

#### ✨ Option B1 — Un seul script pour tout démarrer (Recommandé !)

Juste exécutez UNE commande et tout démarre automatiquement :

**Pour Windows :**
```bash
start-all.bat
```

**Pour Linux ou macOS :**
```bash
chmod +x start-all.sh  # Une seule fois : rendre le script exécutable
./start-all.sh
```

Ces scripts lancent :
1. Le backend .NET
2. Le frontend Angular
3. Le service ML ETA Prediction (si le répertoire existe)

---

#### Option B2 — Démarrage manuel, service par service

**Backend :**
```bash
cd backend

# Configurer la connexion SQL Server dans appsettings.json ou appsettings.Development.json
# ConnectionStrings.SqlServerConnection: "Server=localhost;Database=PointageBus;..."

# Restaurer les packages
dotnet restore CST.LePoint.sln

# Démarrer l'API (les migrations s'appliquent automatiquement)
dotnet run --project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj
# API disponible sur http://localhost:6064
```

**Frontend :**
```bash
cd frontend

# Installer les dépendances
npm install

# Vérifier que public/config/setting-config.json pointe sur l'API locale
# { "baseApi": "http://localhost:6064/cm/" }

# Démarrer le serveur de développement Angular
npm start
# Frontend disponible sur http://localhost:4200
```

**ML Service :**
```bash
cd ml-services/eta_prediction

# Installer les dépendances Python (si pas déjà fait)
pip install -r requirements.txt

# Démarrer FastAPI
uvicorn main:app --port 8001 --host 0.0.0.0 --reload
```

### Migrations de base de données

Les migrations EF Core s'appliquent automatiquement au démarrage de l'API. Pour créer une nouvelle migration manuellement :

```bash
cd backend
dotnet ef migrations add NomDeLaMigration \
  --project src/CollectManagement.ms/CollectManagement.Infrastructure \
  --startup-project src/CollectManagement.ms/CollectManagement.WebAPI
```

---

## 12. Tests

### État actuel

Le projet ne contient **pas de tests automatisés**. Le fichier `angular.json` configure Karma/Jasmine pour le frontend, et les projets .NET ont une structure compatible avec xUnit, mais aucune suite de tests n'a été implémentée. Cela représente une dette technique identifiée.

### Stratégie recommandée pour un projet en production

**Backend — tests unitaires** avec xUnit + Moq :
- Tester les handlers MediatR en mockant les repositories et services
- Tester les behaviors (LoggingBehavior, ValidationBehavior) avec des commandes factices
- Tester les entités du domaine (logique des factories, règles de mutation)

**Backend — tests d'intégration** avec `WebApplicationFactory<Program>` :
- Utiliser une base de données in-memory (SQLite ou SQL Server LocalDB)
- Tester les endpoints Carter de bout en bout avec authentification JWT de test

**Frontend — tests unitaires** avec Karma/Jasmine :
- Tester les services (ApiService, AuthService, CircuitService) avec `HttpClientTestingModule`
- Tester les composants avec `ComponentFixture` et `TestBed`

**Frontend — tests e2e** avec Cypress ou Playwright :
- Scénarios complets : login, navigation, CRUD, déconnexion

---

## 13. Déploiement

### Architecture Docker Compose (production simplifiée)

Le fichier `docker-compose.yml` à la racine orchestre 4 services en réseau interne Docker :

**Service `db`** : image officielle `mcr.microsoft.com/mssql/server:2019-latest`. Les données sont persistées dans le volume Docker `sqldata`. Un health-check toutes les 10 secondes avec `sqlcmd -Q "SELECT 1"` garantit que SQL Server est opérationnel avant de démarrer le backend.

**Service `backend`** : build multi-stage en deux phases.
- Phase 1 (`sdk:8.0`) : copie la solution, restaure NuGet, installe Node.js/npm pour la compilation Tailwind CSS du WebAPI (styles admin), puis publie en mode Release (`dotnet publish -c Release`).
- Phase 2 (`aspnet:8.0`) : image runtime légère, copie uniquement les binaires publiés. Point d'entrée : `dotnet CollectManagement.WebAPI.dll`. La ConnectionString est injectée via variable d'environnement pour pointer vers le service `db` interne.

**Service `frontend`** : build en deux phases.
- Phase 1 (`node:20-alpine`) : `npm install` puis `ng build --configuration development`.
- Phase 2 (`nginx:1.27-alpine`) : copie le dossier `dist/collectmanagement/browser` dans `/usr/share/nginx/html`. Nginx est configuré pour router toutes les requêtes vers `index.html` (SPA routing : `try_files $uri $uri/ /index.html`).

**Service `eta-prediction`** : microservice Python FastAPI dédié à la prédiction du temps d'arrivée des bus. Le dossier `artifacts/` (contenant les fichiers de modèle sérialisés) est monté en volume pour permettre une mise à jour des modèles sans reconstruire l'image Docker.

### Déploiement sur serveur dédié ou VPS

```bash
# Sur le serveur (après installation de Docker + Docker Compose)
git clone <url-du-repo>
cd RiderProjects

# Adapter les variables sensibles (mot de passe DB, secret JWT, origines CORS)
# dans docker-compose.yml et backend/src/.../appsettings.json

docker-compose up -d --build

# Voir les logs
docker-compose logs -f backend
docker-compose logs -f frontend
```

### Considérations de production

Les éléments suivants devraient être adressés avant un déploiement en production réelle :

- **Secrets management** : la clé JWT et le mot de passe SQL ne doivent pas être dans les fichiers de configuration versionnés. Utiliser des variables d'environnement injectées par le système d'orchestration (Docker Swarm secrets, Kubernetes secrets, Azure Key Vault).
- **HTTPS** : configurer un certificat TLS (Let's Encrypt via Nginx Certbot) devant tous les services.
- **Durée JWT** : réduire `ExpiryMinutes` à 60 ou moins et implémenter un mécanisme de refresh token.
- **Mode debug Serilog** : s'assurer que `EnableSensitiveDataLogging()` est désactivé en production.
- **Rin** : l'inspecteur de développement Rin doit être désactivé en production.

---

## 14. Défis & décisions de conception

### Défi 1 : Identification des bus par IMEI sans authentification IoT

Les modems embarqués dans les bus ne peuvent pas gérer une authentification JWT classique. L'IMEI du modem sert d'identifiant natif. Le défi était de sécuriser cet endpoint sans complexifier le firmware des modems. La solution adoptée est une validation RFID côté conducteur (le modem connaît le tag RFID du badge du conducteur) combinée à une tolérance de timestamp strict (±15 minutes). En production, une API key dédiée ou un certificat client TLS serait recommandé pour protéger l'endpoint IoT.

### Défi 2 : Géofencing temps réel sans surcharge de la base

Le calcul de la distance entre un bus et des dizaines de points de collecte à chaque scan IoT devait être performant. La décision a été de calculer la distance Haversine directement en C# (sans extension géospatiale SQL) car les points de collecte d'un circuit sont peu nombreux (généralement < 50). Pour les circuits très denses, l'optimisation serait de pré-calculer les `BoundingBox` ou d'utiliser les types géographiques SQL Server (`geography.STDistance()`).

### Défi 3 : Navigation RBAC doublement vérifiée

Faire correspondre le système de permissions backend avec le filtrage de menu Angular imposait un référentiel commun. La solution est la chaîne de `NavigationId` (ex: `fichier.circuit`) qui est déclarée dans les routes Angular (`.data.navigationId`) et dans les endpoints Carter (`.RequireNavigationPermission("fichier.circuit")`). Si un utilisateur tente d'accéder directement à une URL sans passer par le menu, le guard côté frontend bloque, et le backend bloque également de façon indépendante.

### Défi 4 : Génération automatique de points de collecte

Quand un bus scanne hors périmètre, le système doit décider quoi faire. Deux approches ont été considérées :
- Simplement ignorer le scan hors périmètre
- Créer automatiquement un point de collecte pour audit

La décision a été de créer un `PointCollecte` automatique avec un code horodaté (`PC-AUTO-{timestamp}`) pour garder une trace auditée de tous les scans, même hors circuit. Les opérateurs peuvent ensuite décider de valider ou supprimer ces points auto-générés.

### Défi 5 : Intégration de la prédiction ETA sans dépendance forte

Le service de prédiction ETA est un microservice Python complètement indépendant du backend .NET. Il est appelé via `HttpClient` dans `ExternalPredictionService`. Cette architecture découplée permet de mettre à jour le modèle de prédiction (ré-entraînement, nouvelle version) sans toucher au code C#, et d'utiliser n'importe quel framework Python pour le modèle sans contrainte d'interopérabilité. Le défi principal était la résolution de la distance d'entrée : le modèle attend une distance en mètres au prochain arrêt, mais les bus ne fournissent que leurs coordonnées GPS. La logique de résolution Haversine côté backend (avec ses quatre niveaux de repli) a été conçue pour garantir qu'une valeur de distance est toujours disponible, même si les données de circuit sont incomplètes.

### Défi 6 : Multi-tenant sans schéma séparé

Toutes les sociétés partagent le même schéma. Cela simplifie les migrations mais nécessite une discipline stricte pour que chaque requête filtre par `SocieteId`. L'approche actuelle repose sur la bonne pratique des développeurs. Une amélioration serait d'implémenter un `GlobalQueryFilter` EF Core sur toutes les entités qui applique automatiquement le filtre `SocieteId`.

### Ce qui serait amélioré

- **Tests automatisés** : l'absence de tests est la dette technique principale. Ajouter des tests unitaires sur les handlers et les entités, et des tests d'intégration sur les endpoints critiques.
- **Refresh token** : implémenter un endpoint dédié et raccourcir la durée du token d'accès.
- **WebSocket pour le tracking** : remplacer le polling 10s par une connexion SignalR pour un suivi temps réel plus réactif.
- **Global query filter EF** : automatiser le filtrage `SocieteId` pour éviter les oublis.
- **Séparation lecture/écriture** : certains handlers de query font des projections SQL complexes directement dans le repository via `SqlQueryListAsync` ; extraire ces projections dans des Read Models dédiés améliorerait la lisibilité.

---

## 15. Glossaire

**AuditableEntity** — Classe de base C# qui ajoute quatre champs d'audit à toutes les entités : `InsererPar` (login de l'auteur), `DateInsertion`, `ModifierPar` et `DateModification`. Ces champs sont remplis automatiquement par l'`AuditableInterceptor` EF Core à chaque opération de sauvegarde.

**Carter** — Bibliothèque .NET qui permet de définir des endpoints ASP.NET Core Minimal API sous forme de modules organisés (classes `ICarterModule`), séparés des fichiers de configuration de l'application.

**Chauffeur** — Conducteur de bus dans le système. Chaque chauffeur possède un `RFIDChauffeur` (numéro de sa carte RFID) servant à valider sa présence lors des scans IoT.

**Circuit** — Itinéraire de collecte d'une ligne de bus. Un circuit relie un point de départ (`CodePCDepart`) à un point d'arrivée (`CodePCArrivee`) en passant par une liste ordonnée de `PointsCollecte`.

**Clean Architecture** — Style d'architecture logicielle dans lequel le code est organisé en couches concentriques (Domain → Application → Infrastructure → Présentation) avec une règle de dépendance stricte : les couches internes ne dépendent jamais des couches externes.

**CQRS (Command Query Responsibility Segregation)** — Pattern architectural séparant les opérations de lecture (Query) des opérations d'écriture (Command). Chaque opération est représentée par une classe distincte avec son handler dédié.

**DevExtreme** — Suite de composants UI JavaScript de la société DevExpress. Dans ce projet, `dx-data-grid` est utilisé pour toutes les grilles de données avec tri, filtrage et pagination.

**EF Core (Entity Framework Core)** — ORM (Object-Relational Mapper) Microsoft pour .NET. Traduit les opérations LINQ en SQL et gère les migrations de schéma de base de données.

**RFID** — Radio-Frequency Identification. Dans ce projet, utilisé à deux niveaux : le badge de l'employé (sur l'entité `Employe`) pour le pointage, et la carte du chauffeur (`RFIDChauffeur` sur l'entité `Chauffeur`) pour la validation de présence lors du scan modem.

**Géofencing** — Technique de délimitation d'une zone géographique virtuelle. Dans ce projet, une tolérance de 250 mètres est appliquée autour de chaque point de collecte pour déterminer si un scan de bus est "dans le périmètre" ou non.

**Haversine** — Formule mathématique de calcul de la distance entre deux points sur une sphère (la Terre) à partir de leurs coordonnées géographiques (latitude/longitude). Utilisée pour le géofencing et le calcul de distance aux prédictions ML.

**IMEI** — International Mobile Equipment Identity. Identifiant unique de 15 chiffres propre à chaque modem GSM embarqué dans un bus. Utilisé comme clé de lookup pour l'endpoint IoT.

**MediatR** — Bibliothèque .NET implémentant le pattern Mediateur. Permet d'envoyer un message (Command ou Query) sans couplage direct à son handler.

**Multi-tenant** — Architecture applicative permettant à plusieurs clients (ici : plusieurs sociétés) de partager la même instance d'application et la même base de données, tout en ayant leurs données isolées les unes des autres.

**Ordre de Travail (OT)** — Document opérationnel assignant une mission de transport à une équipe. Contient le chantier, le client, le bon de commande, l'état et le montant.

**Point de Collecte** — Emplacement géographique où les employés attendent le bus pour être transportés vers leur site de travail.

**Rattachement** — Opération comptable et logistique liant des ressources (employés, articles) à un chantier ou une mission sur une période donnée.

**RBAC (Role-Based Access Control)** — Modèle de contrôle d'accès basé sur des rôles. Dans ce projet, étendu en "Navigation-based RBAC" où les droits sont définis par navigation applicative et par type d'action.

**Serilog** — Bibliothèque de journalisation structurée pour .NET. Les logs sont écrits en JSON compact dans des fichiers à rotation quotidienne et dans la console.

**Shift** — Créneau horaire de travail définissant les heures de début et de fin d'une vacation d'employés.

**SocieteId** — Identifiant de la société cliente. Champ présent sur toutes les entités métier, servant de discriminant pour la séparation des données en mode multi-tenant.

**ULID (Universally Unique Lexicographically Sortable Identifier)** — Variante des GUID avec tri chronologique intégré. Génère des identifiants de 128 bits encodés en Base32 Crockford (26 caractères). Les 48 premiers bits contiennent le timestamp en millisecondes, les 80 suivants sont aléatoires.

**Value Object** — Concept de Domain-Driven Design. Un Value Object encapsule une valeur primitive (ici : les IDs de type ULID) dans un type fortement typé (ex: `CircuitId(Ulid)`), évitant les erreurs de passage de mauvais IDs aux mauvaises méthodes.

---

*Document généré à partir de l'analyse complète du code source du projet CST LePoint — Juin 2026*
