# Frontend Module Replication Guide

## Overview
The Circuit module is now complete and serves as the reference template for the remaining 4 entities.
This guide provides step-by-step instructions to replicate it for PointCollecte, Equipe, OrdreTravail, and Rattachement.

## Quick Reference: Circuit Module Structure

```
frontend/src/app/
├── core/circuit/
│   ├── circuit.model.ts         # Interfaces: Circuit, PagedCircuit
│   └── circuit.service.ts       # CRUD service with BehaviorSubjects
└── modules/cst/circuit/
    ├── circuit.component.ts     # Parent component (router-outlet)
    ├── circuit.component.html   # <router-outlet></router-outlet>
    ├── circuit.component.scss   # Styles
    ├── circuit.routes.ts        # Routing configuration
    ├── list/
    │   ├── list.component.ts    # List logic (7,119 lines)
    │   ├── list.component.html  # Responsive table (11,623 lines)
    │   └── list.component.scss  # Grid styles (307 lines)
    └── details/
        ├── details.component.ts    # Form logic (5,326 lines)
        ├── details.component.html  # Form template (5,954 lines)
        └── details.component.scss  # Form styles (28 lines)
```

## Step-by-Step Replication Process

### Step 1: Copy Directory Structure
```bash
cd /home/runner/work/PointagePfe/PointagePfe/frontend/src/app

# For each entity:
cp -r core/circuit core/{entity-name}
cp -r modules/cst/circuit modules/cst/{entity-name}
```

### Step 2: Rename Files
Within the copied directory, rename all files:
```bash
# Example for point-collecte:
cd core/point-collecte
mv circuit.model.ts point-collecte.model.ts
mv circuit.service.ts point-collecte.service.ts

cd ../../modules/cst/point-collecte
mv circuit.component.ts point-collecte.component.ts
mv circuit.component.html point-collecte.component.html
mv circuit.component.scss point-collecte.component.scss
mv circuit.routes.ts point-collecte.routes.ts
```

### Step 3: Find and Replace Text

For each file in the entity directory, perform these replacements:

#### Case Sensitive Replacements:
```
Circuit → PointCollecte (or Equipe, OrdreTravail, Rattachement)
circuit → pointCollecte (or equipe, ordreTravail, rattachement)
circuits → pointsCollecte (or equipes, ordresTravail, rattachements)
circuitId → pointCollecteId (or equipeId, ordreTravailId, rattachementId)
```

#### Import Path Replacements:
```
from './circuit.model' → from './point-collecte.model'
from './circuit.service' → from './point-collecte.service'
from '../../../core/circuit/ → from '../../../core/point-collecte/
```

#### API Endpoint Replacements:
In the service file, update all API paths:
```
"circuit/list" → "pointcollecte/list"
"circuit/add" → "pointcollecte/add"
"circuit/update" → "pointcollecte/update"
`circuit/${id}/one` → `pointcollecte/${id}/one`
`circuit/${id}/delete` → `pointcollecte/${id}/delete`
```

### Step 4: Update Model Interfaces

Edit the `.model.ts` file for each entity:

#### PointCollecte Model
```typescript
export interface PointCollecte {
    pointCollecteId: string;
    codePointCollecte: string;
    libellePointCollecte: string;
    latitude?: number;
    longitude?: number;
    codeGouvernorat?: string;
    codeRegion?: string;
    isActive: boolean;
    societeId: string;
}

export interface PagedPointCollecte {
    pointsCollecte: PointCollecte[];
    totalCount: number;
}
```

#### Equipe Model
```typescript
export interface Equipe {
    equipeId: string;
    codeEquipe: string;
    libelleEquipe?: string;
    codeClient?: string;
    codeEntrepot?: string;
    codeTarif?: string;
    codeFournisseur?: string;
    responsable?: string;
    isInternal: boolean;
    codeVehicule?: string;
    isActive: boolean;
    societeId: string;
}

export interface PagedEquipe {
    equipes: Equipe[];
    totalCount: number;
}
```

#### OrdreTravail Model
```typescript
export interface OrdreTravail {
    ordreTravailId: string;
    numeroOrdreTravail: string;
    numeroChantier?: string;
    codeClient?: string;
    numeroBonCommande?: string;
    codeEquipe?: string;
    etatOT?: string;
    montant?: number;
    dateCreation?: Date;
    numeroConvention?: string;
    codeVehicule?: string;
    libelle?: string;
    isActive: boolean;
    societeId: string;
}

export interface PagedOrdreTravail {
    ordresTravail: OrdreTravail[];
    totalCount: number;
}
```

#### Rattachement Model
```typescript
export interface Rattachement {
    rattachementId: string;
    numeroRattachement: string;
    exercice?: string;
    dateRattachement: Date;
    numeroChantier?: string;
    codeClient?: string;
    isInternal: boolean;
    cout?: number;
    type?: string;
    nature?: string;
    responsable?: string;
    heureDebut?: string;
    heureFin?: string;
    emplacement?: string;
    reference?: string;
    status?: string;
    dateCloture?: Date;
    remarque?: string;
    isActive: boolean;
    societeId: string;
}

export interface PagedRattachement {
    rattachements: Rattachement[];
    totalCount: number;
}
```

### Step 5: Update Form Fields

In `details.component.ts`, update the form initialization:

#### PointCollecte Form
```typescript
this.pointCollecteForm = this.formBuilder.group({
    pointCollecteId: [null],
    codePointCollecte: ['', Validators.required],
    libellePointCollecte: ['', Validators.required],
    latitude: [''],
    longitude: [''],
    codeGouvernorat: [''],
    codeRegion: [''],
    isActive: [true],
    societeId: ['', Validators.required],
});
```

#### Equipe Form
```typescript
this.equipeForm = this.formBuilder.group({
    equipeId: [null],
    codeEquipe: ['', Validators.required],
    libelleEquipe: [''],
    codeClient: [''],
    codeEntrepot: [''],
    codeTarif: [''],
    codeFournisseur: [''],
    responsable: [''],
    isInternal: [false],
    codeVehicule: [''],
    isActive: [true],
    societeId: ['', Validators.required],
});
```

### Step 6: Update List Table Columns

In `list.component.html`, update the grid header and data columns:

#### PointCollecte Columns Example
```html
<!-- Header -->
<div [mat-sort-header]="'codePointCollecte'">{{ t('Code') }}</div>
<div [mat-sort-header]="'libellePointCollecte'">{{ t('Label') }}</div>
<div>{{ t('Latitude') }}</div>
<div>{{ t('Longitude') }}</div>
<div>{{ t('Governorate') }}</div>
<div>{{ t('Region') }}</div>
<div class="text-center">{{ t('Status') }}</div>
<div class="hidden sm:block text-center">{{ t('Details') }}</div>

<!-- Data Row -->
<div class="hidden sm:block truncate font-medium">{{pointCollecte.codePointCollecte}}</div>
<div class="hidden sm:block truncate">{{pointCollecte.libellePointCollecte}}</div>
<div class="hidden sm:block">{{pointCollecte.latitude || '-'}}</div>
<div class="hidden sm:block">{{pointCollecte.longitude || '-'}}</div>
<div class="hidden sm:block">{{pointCollecte.codeGouvernorat || '-'}}</div>
<div class="hidden sm:block">{{pointCollecte.codeRegion || '-'}}</div>
```

### Step 7: Update Details Form Fields

In `details.component.html`, update form fields:

#### PointCollecte Form Fields Example
```html
<!-- Code -->
<mat-form-field class="w-full">
    <mat-label>{{ t('Point-Collecte-Code') }}</mat-label>
    <input matInput formControlName="codePointCollecte">
</mat-form-field>

<!-- Label -->
<mat-form-field class="w-full">
    <mat-label>{{ t('Point-Collecte-Label') }}</mat-label>
    <input matInput formControlName="libellePointCollecte">
</mat-form-field>

<!-- Latitude -->
<mat-form-field class="w-full">
    <mat-label>{{ t('Latitude') }}</mat-label>
    <input matInput type="number" formControlName="latitude">
</mat-form-field>

<!-- Longitude -->
<mat-form-field class="w-full">
    <mat-label>{{ t('Longitude') }}</mat-label>
    <input matInput type="number" formControlName="longitude">
</mat-form-field>
```

### Step 8: Update Grid CSS

In `list.component.scss`, adjust grid template columns based on number of fields:

```scss
.{entity}-grid {
    grid-template-columns: 48px auto;

    @screen sm {
        // Adjust based on number of columns
        // PointCollecte: 8 columns
        grid-template-columns: 1fr 2fr 1fr 1fr 1fr 1fr 120px 100px;
    }
}
```

### Step 9: Update Routing

In `{entity}.routes.ts`, update the route paths and titles:

```typescript
title: 'PointsCollecte',  // or 'Equipes', 'Ordres de Travail', 'Rattachements'
```

## Automated Script (Optional)

Create a bash script to automate the replication:

```bash
#!/bin/bash
# replicate-entity.sh

ENTITY_LOWER=$1  # e.g., "point-collecte"
ENTITY_PASCAL=$2  # e.g., "PointCollecte"
ENTITY_CAMEL=$3   # e.g., "pointCollecte"
ENTITY_PLURAL=$4  # e.g., "pointsCollecte"

# Copy directories
cp -r core/circuit "core/$ENTITY_LOWER"
cp -r modules/cst/circuit "modules/cst/$ENTITY_LOWER"

# Rename files in core
cd "core/$ENTITY_LOWER"
rename 's/circuit/'$ENTITY_LOWER'/' *

# Rename files in modules
cd "../../modules/cst/$ENTITY_LOWER"
rename 's/circuit/'$ENTITY_LOWER'/' *
cd list && rename 's/circuit/'$ENTITY_LOWER'/' *
cd ../details && rename 's/circuit/'$ENTITY_LOWER'/' *

# Find and replace in all files
find "core/$ENTITY_LOWER" -type f -exec sed -i "s/Circuit/$ENTITY_PASCAL/g" {} +
find "core/$ENTITY_LOWER" -type f -exec sed -i "s/circuit/$ENTITY_CAMEL/g" {} +
find "core/$ENTITY_LOWER" -type f -exec sed -i "s/circuits/$ENTITY_PLURAL/g" {} +

# Repeat for modules directory...
```

## Testing Checklist

After replicating each entity, test:

1. ✅ Module compiles without errors
2. ✅ Route navigation works
3. ✅ List view displays data
4. ✅ Search functionality works
5. ✅ Sorting works
6. ✅ Pagination works
7. ✅ Create new entity works
8. ✅ Edit entity works
9. ✅ Delete entity works (with confirmation)
10. ✅ Form validation works
11. ✅ Success/error messages display
12. ✅ Responsive design works on mobile

## Time Estimates

Per entity (with Circuit as reference):
- Copy and rename: 5 minutes
- Find/replace text: 10 minutes
- Update model: 5 minutes
- Update form fields: 10 minutes
- Update list columns: 15 minutes
- Update details template: 15 minutes
- Testing: 10 minutes

**Total per entity: ~70 minutes**
**All 4 entities: ~4.5 hours**

## Entity-Specific Notes

### PointCollecte
- Add number input types for latitude/longitude
- Consider adding map visualization (optional enhancement)

### Equipe
- Add checkbox for isInternal
- May need dropdown for codeClient, codeEntrepot (if referential data exists)

### OrdreTravail
- Add date picker for dateCreation
- Add number input for montant
- May need status dropdown for etatOT

### Rattachement (Most Complex)
- Add date picker for dateRattachement and dateCloture
- Add time inputs for heureDebut and heureFin
- Add number input for cout
- Consider multi-step form or tabs due to many fields

## Conclusion

The Circuit module provides a complete, battle-tested template. By following this guide, you can replicate it for all remaining entities in approximately 4-5 hours of focused work.

The key is systematic find/replace followed by field-specific customization.
