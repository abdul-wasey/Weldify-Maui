# Weldify

[![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/apps/maui)
[![Platform](https://img.shields.io/badge/platform-Android%20%7C%20iOS%20%7C%20Windows%20%7C%20macOS-informational)]()
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](CONTRIBUTING.md)
[![Status](https://img.shields.io/badge/status-active%20development-orange.svg)]()

A cross-platform **.NET MAUI application for welding and metal fabrication shops** to manage customers, jobs, fabrication specifications, and order progress.

<!-- Add a screenshot or GIF of the app here once available -->
<!-- ![Weldify screenshot](docs/screenshots/home.png) -->

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Screenshots](#screenshots)
- [Tech Stack](#tech-stack)
- [Supported Platforms](#supported-platforms)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Running the App](#running-the-app)
- [Project Structure](#project-structure)
- [Data Storage](#data-storage)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [Code of Conduct](#code-of-conduct)
- [License](#license)
- [Acknowledgements](#acknowledgements)

## Overview

Small welding and fabrication shops often manage customer information and orders using notebooks, paper records, WhatsApp messages, or scattered files. This makes it difficult to keep track of customer requirements, job specifications, measurements, designs, and production status.

**Weldify** provides a simple, centralized solution for managing this workflow. The application connects **customers with their jobs/orders** and keeps important fabrication details — materials, dimensions, finishing, installation requirements, notes, and reference images — together in one place.

## Features

- Customer management
- Job/order management
- Job status tracking
- Fabrication specifications
- Material and iron gauge selection
- Dimensions and quantities
- Measurement units
- Finishing and color information
- Installation requirements and site address
- Job/reference images
- Local SQLite database
- Light and dark themes
- Cross-platform .NET MAUI application

### Job Statuses

Jobs move through the following stages:

`Draft` → `Quotation` → `Approved` → `In Production` → `Ready for Installation` → `Completed`
(or `Cancelled` at any point)

### Supported Products

- Safety Grill
- Main Door
- Iron Window
- Balcony Grill
- Main Gate
- Stair Railing
- Balcony Railing
- Other

### Supported Materials

- Square Pipe
- Rectangular Pipe
- Round Pipe
- Angle Iron
- Flat Bar
- Round Bar
- Sheet
- Channel
- Other

## Screenshots

> Screenshots and demo GIFs will be added here as UI stabilizes.

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | [.NET MAUI](https://dotnet.microsoft.com/apps/maui) |
| Language | C#, XAML |
| ORM | Entity Framework Core |
| Database | SQLite (local) |
| MVVM helpers | [CommunityToolkit.Mvvm](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/) |
| UI helpers | [CommunityToolkit.Maui](https://learn.microsoft.com/dotnet/communitytoolkit/maui/) |
| Icons | Material MAUI Icons |
| UI components | [Syncfusion MAUI Toolkit](https://www.syncfusion.com/maui-controls) |

## Supported Platforms

- Android
- iOS
- Windows
- macOS / Mac Catalyst

## Getting Started

### Prerequisites

Make sure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or later)
- [.NET MAUI workload](https://learn.microsoft.com/dotnet/maui/get-started/installation)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) with the .NET MAUI workload, **or** Visual Studio Code with the .NET MAUI extension

For platform-specific setup (Android SDK, iOS pairing to a Mac, etc.), see the official [Microsoft .NET MAUI documentation](https://learn.microsoft.com/dotnet/maui/).

### Installation

```bash
# Clone the repository
git clone https://github.com/<your-username>/Weldify.git
cd Weldify

# Install the MAUI workload (if not already installed)
dotnet workload install maui

# Restore dependencies
dotnet restore
```

### Running the App

Build the project:

```bash
dotnet build
```

Run on a specific target framework, for example Android:

```bash
dotnet build -t:Run -f net8.0-android
```

Or open `Weldify.sln` in Visual Studio and select your target platform/device from the run configuration dropdown.

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

## Data Storage

Weldify currently uses **SQLite** for local data storage via **Entity Framework Core**. The database is created locally on the user's device and stores:

- Customers
- Jobs
- Job items
- Job images

No external database server is currently required. Cloud sync and backup/restore are on the [roadmap](#roadmap).

## Roadmap

- [ ] Customer search and filtering
- [ ] Job search and filtering
- [ ] Customer job history
- [ ] Detailed job management
- [ ] Quotations and pricing
- [ ] Labour and material costing
- [ ] Payments and outstanding balances
- [ ] Production tracking
- [ ] Job due dates and priorities
- [ ] Reports and dashboard statistics
- [ ] Database backup and restore
- [ ] Export and sharing of quotations/job information

See the [open issues](https://github.com/<your-username>/Weldify/issues) for a full list of proposed features and known issues.

## Contributing

Contributions, suggestions, and improvements are welcome! This project follows a standard fork-and-pull-request workflow:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

Please open an issue first to discuss significant changes. See [CONTRIBUTING.md](CONTRIBUTING.md) for detailed guidelines (coding conventions, commit style, etc.) once available.

## Code of Conduct

This project adheres to a [Code of Conduct](CODE_OF_CONDUCT.md) to foster an open and welcoming community. By participating, you are expected to uphold it.

## License

Will be available once finalized.

## Acknowledgements

- [.NET MAUI](https://dotnet.microsoft.com/apps/maui)
- [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet)
- [CommunityToolkit.Maui](https://github.com/CommunityToolkit/Maui)
- [Syncfusion MAUI Toolkit](https://www.syncfusion.com/maui-controls)
