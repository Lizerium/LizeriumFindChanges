<h1 align="center">🧩 Lizerium Find Changes 🧩</h1>

<p align="center">
  <img src="https://shields.dvurechensky.pro/badge/Platform-Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white" />
  <img src="https://shields.dvurechensky.pro/badge/.NET-Console%20Tool-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://shields.dvurechensky.pro/badge/Type-Update%20Builder-2E7D32?style=for-the-badge" />
  <img src="https://shields.dvurechensky.pro/badge/Mode-File%20Diff-1565C0?style=for-the-badge" />
  <img src="https://shields.dvurechensky.pro/badge/Status-Release-00C853?style=for-the-badge" />
  <img src="https://shields.dvurechensky.pro/badge/License-MIT-yellow?style=for-the-badge" />
</p>

<p align="center">
  <img src="https://shields.dvurechensky.pro/badge/Product-Lizerium-E53935?style=flat-square" />
  <img src="https://shields.dvurechensky.pro/badge/Use%20Case-Versioned%20Deployments-3949AB?style=flat-square" />
  <img src="https://shields.dvurechensky.pro/badge/Output-manifest.json-00897B?style=flat-square" />
  <img src="https://shields.dvurechensky.pro/badge/Output-Update%20Folder-5E35B1?style=flat-square" />
  <img src="https://shields.dvurechensky.pro/badge/Supports-Missing%20Files%20Mode-6D4C41?style=flat-square" />
</p>

<p align="center">
  <b>Lizerium Find Changes</b> — a console tool for comparing two versions of a file structure,
  generating a change list, and preparing a ready-to-deploy update folder.
</p>

<div align="center" style="margin: 20px 0; padding: 10px; background: #1c1917; border-radius: 10px;">
  <strong>🌐 Language: </strong>
  
  <a href="./README.ru.md" style="color: #F5F752; margin: 0 10px;">
    🇷🇺 Russian
  </a>
  | 
  <span style="color: #0891b2; margin: 0 10px;">
    ✅ 🇺🇸 English (current)
  </span>
</div>

---

> [!NOTE]
> This project is part of the **Lizerium** ecosystem and belongs to the following direction:
>
> - [`Lizerium.Tools.Structs`](https://github.com/Lizerium/Lizerium.Tools.Structs)
>
> If you are looking for related engineering and supporting tools, start there.

# Table of Contents

- [Table of Contents](#table-of-contents)
  - [Overview](#overview)
  - [Features](#features)
  - [Technologies](#technologies)
  - [How It Works](#how-it-works)
  - [Configuration Structure](#configuration-structure)
    - [Example `config.json`](#example-configjson)
    - [Parameters](#parameters)
  - [Modes](#modes)
    - [1. Update Generation Mode](#1-update-generation-mode)
      - [Output:](#output)
    - [2. Missing Files Extraction Mode](#2-missing-files-extraction-mode)
  - [Output Structure](#output-structure)
    - [`manifest.json`](#manifestjson)
    - [Update Folder](#update-folder)
    - [Missing Files Folder](#missing-files-folder)
  - [Example Scenario](#example-scenario)
    - [Input](#input)
    - [Config](#config)
    - [Result](#result)
  - [Other](#other)
  - [Author](#author)

---

## Overview

> [!IMPORTANT]
> The goal of this project is to automatically detect differences between two versions of a file structure and prepare update content.

The tool compares:

- directories
- files
- new items
- deleted items
- modified files
- moved / reused files

After analysis, it can:

- generate a `manifest.json`
- build an update folder with only required files
- extract files missing from the new version

---

## Features

- 📁 Directory comparison
- 📄 Detection of new, deleted, and modified files
- 🔁 Identification of moved or reused files
- 🏗 Generation of ready-to-use update folder
- 📜 `manifest.json` generation
- 🧩 Extraction of files missing in the new version
- ⚡ Simple configuration via `config.json`

---

## Technologies

- C#
- .NET
- Console Application
- System.Text.Json

---

## How It Works

The program uses two directories:

- `Before` — previous version
- `After` — new version

It determines:

- added folders
- removed folders
- changed files
- newly created files
- deleted files
- files with identical content but different paths (`Retranslate`)

Then it generates:

- a `manifest.json` with all changes
- an update folder with the correct structure

---

## Configuration Structure

The program uses:

```text
config.json
```

If it does not exist, it will be created automatically.

### Example `config.json`

```json
{
	"Before": "C:\\Builds\\OldVersion",
	"After": "C:\\Builds\\NewVersion",
	"Version": "99.3.12",
	"IsMissingFilesMode": false
}
```

- [Example](Configs/default_config.json)

### Parameters

- `Before` — path to previous version
- `After` — path to new version
- `Version` — output update folder name
- `IsMissingFilesMode` — mode switch

---

## Modes

### 1. Update Generation Mode

If:

```json
"IsMissingFilesMode": false
```

The program:

1. Compares `Before` and `After`
2. Generates `manifest.json`
3. Creates an update folder using `Version`
4. Copies only required changed / new / reused files

#### Output:

- `manifest.json`
- update folder, e.g.:

```text
99.3.12/
```

---

### 2. Missing Files Extraction Mode

If:

```json
"IsMissingFilesMode": true
```

The program:

1. Compares `Before` and `After`
2. Finds files present in old version but missing in new
3. Copies them into:

```text
MISSING/
```

Useful for:

- file loss analysis
- incomplete build detection
- recovery
- manual inspection

---

## Output Structure

### `manifest.json`

Contains a list of changes.

Change types:

- `Added`
- `Deleted`
- `Changed`
- `Unchanged`
- `Retranslate`

---

### Update Folder

Example:

```text
99.3.12/
```

Contains only files required for deployment.

---

### Missing Files Folder

If enabled:

```text
MISSING/
```

---

## Example Scenario

### Input

```text
C:\Builds\Freelancer_99.3.11
C:\Builds\Freelancer_99.3.12
```

### Config

```json
{
	"Before": "C:\\Builds\\Freelancer_99.3.11",
	"After": "C:\\Builds\\Freelancer_99.3.12",
	"Version": "99.3.12",
	"IsMissingFilesMode": false
}
```

### Result

```text
manifest.json
99.3.12/
```

These can be used for packaging and deployment (e.g., in **Lizerium Launcher**).

---

## Other

> [!TIP]
> Used as part of the update preparation pipeline in the **Lizerium** ecosystem.

> [!IMPORTANT]
> For accurate results, compare finalized builds, not intermediate working directories.

---

## Author

Developed and maintained by **Dvurechensky**
Part of the **Lizerium** ecosystem

- Website: [https://dvurechensky.pro](https://dvurechensky.pro)
- GitHub: [https://github.com/Dvurechensky](https://github.com/Dvurechensky)
