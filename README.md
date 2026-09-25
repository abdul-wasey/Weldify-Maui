# Weldify

A cross-platform **.NET MAUI application for welding and metal fabrication shops** to manage customers, jobs, fabrication specifications, and order progress.

## Overview

Small welding and fabrication shops often manage customer information and orders using notebooks, paper records, WhatsApp messages, or scattered files. This makes it difficult to keep track of customer requirements, job specifications, measurements, designs, and production status.

**Weldify** aims to provide a simple, centralized solution for managing this workflow.

The application connects **customers with their jobs/orders** and keeps important fabrication details such as materials, dimensions, finishing, installation requirements, notes, and reference images together in one place.

## Features

* Customer management
* Job/order management
* Job status tracking
* Fabrication specifications
* Material and iron gauge selection
* Dimensions and quantities
* Measurement units
* Finishing and color information
* Installation requirements and site address
* Job/reference images
* Local SQLite database
* Light and dark themes
* Cross-platform .NET MAUI application

### Job Statuses

Jobs can move through the following stages:

* Draft
* Quotation
* Approved
* In Production
* Ready for Installation
* Completed
* Cancelled

### Supported Products

The application currently includes product types such as:

* Safety Grill
* Main Door
* Iron Window
* Balcony Grill
* Main Gate
* Stair Railing
* Balcony Railing
* Other

### Supported Materials

* Square Pipe
* Rectangular Pipe
* Round Pipe
* Angle Iron
* Flat Bar
* Round Bar
* Sheet
* Channel
* Other

## Tech Stack

* **.NET MAUI**
* **C#**
* **XAML**
* **Entity Framework Core**
* **SQLite**
* **CommunityToolkit.Maui**
* **CommunityToolkit.Mvvm**
* **Material MAUI Icons**
* **Syncfusion MAUI Toolkit**

## Supported Platforms

* Android
* iOS
* Windows
* macOS / Mac Catalyst

## Project Structure

```text
Weldify/
├── Data/
│   ├── Entities/
│   ├── AppDbContext.cs
│   └── DatabaseInitializer.cs
├── Enums/
├── Services/
├── ViewModels/
├── Views/
├── Platforms/
├── Resources/
├── App.xaml
├── AppShell.xaml
└── MauiProgram.cs
```

## Getting Started

### Prerequisites

Make sure you have the following installed:

* .NET SDK
* .NET MAUI workload
* Visual Studio 2022+ with .NET MAUI support

For more information about setting up .NET MAUI, see the official Microsoft documentation.

### Clone the repository

```bash
git clone https://github.com/<your-username>/Weldify.git
cd Weldify
```

### Restore dependencies

```bash
dotnet restore
```

### Build the project

```bash
dotnet build
```

You can then open the solution in Visual Studio and run Weldify on the platform of your choice.

## Data Storage

Weldify currently uses **SQLite** for local data storage with **Entity Framework Core**.

The application database is created locally on the user's device and stores information such as:

* Customers
* Jobs
* Job items
* Job images

No external database server is currently required.

## Current Status

Weldify is currently **under active development**.

The core application foundation, database models, customer functionality, job structure, and main UI are in place. Some business workflows and management features are still being developed.

## Roadmap

Planned improvements include:

* Customer search and filtering
* Job search and filtering
* Customer job history
* Detailed job management
* Quotations and pricing
* Labour and material costing
* Payments and outstanding balances
* Production tracking
* Job due dates and priorities
* Reports and dashboard statistics
* Database backup and restore
* Export and sharing of quotations/job information

## Contributing

Contributions, suggestions, and improvements are welcome.

If you find a bug or have an idea for improving Weldify, please open an issue or submit a pull request.

## License

License information will be added when the project license is finalized.
