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
  <b>Lizerium Find Changes</b> — консольный инструмент для сравнения двух версий файловой структуры,
  формирования списка изменений и подготовки готовой папки обновления.
</p>

---

> [!NOTE]
> Этот проект является частью экосистемы **Lizerium** и относится к направлению:
>
> * [`Lizerium.Tools.Structs`](https://github.com/Lizerium/Lizerium.Tools.Structs)
>
> Если вы ищете связанные инженерные и вспомогательные инструменты, начните оттуда.

# Оглавление

- [Оглавление](#оглавление)
  - [Общее](#общее)
  - [Возможности](#возможности)
  - [Технологии](#технологии)
  - [Как это работает](#как-это-работает)
  - [Структура конфигурации](#структура-конфигурации)
    - [Пример `config.json`](#пример-configjson)
    - [Параметры](#параметры)
  - [Режимы работы](#режимы-работы)
  - [1. Режим генерации обновления](#1-режим-генерации-обновления)
    - [Выход:](#выход)
  - [2. Режим извлечения отсутствующих файлов](#2-режим-извлечения-отсутствующих-файлов)
  - [Структура выходных данных](#структура-выходных-данных)
    - [`manifest.json`](#manifestjson)
    - [Папка обновления](#папка-обновления)
    - [Папка отсутствующих файлов](#папка-отсутствующих-файлов)
  - [Пример сценария использования](#пример-сценария-использования)
    - [Исходные данные](#исходные-данные)
    - [Конфиг](#конфиг)
    - [Результат](#результат)
  - [Другое](#другое)
  - [Author](#author)

---

## Общее

> [!IMPORTANT]
> Цель проекта — автоматически определить разницу между двумя версиями файловой структуры и подготовить содержимое обновления.

Инструмент сравнивает:

- папки
- файлы
- новые элементы
- удалённые элементы
- изменённые файлы
- перемещённые / переиспользованные файлы

После анализа он может:

- сформировать `manifest.json`
- собрать папку обновления только из нужных файлов
- отдельно извлечь файлы, отсутствующие в новой версии

---

## Возможности

- 📁 Сравнение двух директорий
- 📄 Поиск новых, удалённых и изменённых файлов
- 🔁 Определение файлов, которые были перенесены или повторно использованы
- 🏗 Формирование готовой папки обновления
- 📜 Генерация `manifest.json`
- 🧩 Извлечение файлов, которые отсутствуют в новой версии
- ⚡ Простая конфигурация через `config.json`

---

## Технологии

- C#
- .NET
- Console Application
- System.Text.Json

---

## Как это работает

Программа использует две директории:

- `Before` — предыдущая версия
- `After` — новая версия

На основе сравнения она определяет:

- какие папки были добавлены
- какие папки были удалены
- какие файлы были изменены
- какие файлы появились впервые
- какие файлы были удалены
- какие файлы совпадают по содержимому, но имеют другой путь (`Retranslate`)

После этого формируется:

- `manifest.json` со списком изменений
- папка обновления с нужной структурой файлов

---

## Структура конфигурации

Программа использует файл:

```text
config.json
```

Если его нет — он будет создан автоматически.

### Пример `config.json`

```json
{
	"Before": "C:\\Builds\\OldVersion",
	"After": "C:\\Builds\\NewVersion",
	"Version": "99.3.12",
	"IsMissingFilesMode": false
}
```

- [Пример](Configs/default_config.json)

### Параметры

- `Before` — путь до предыдущей версии
- `After` — путь до новой версии
- `Version` — имя выходной папки обновления
- `IsMissingFilesMode` — переключение режима работы

---

## Режимы работы

## 1. Режим генерации обновления

Если:

```json
"IsMissingFilesMode": false
```

то программа:

1. Сравнивает `Before` и `After`
2. Формирует `manifest.json`
3. Создаёт папку обновления с именем из поля `Version`
4. Копирует в неё только необходимые изменённые / новые / переиспользуемые файлы

### Выход:

- `manifest.json`
- папка обновления, например:

```text
99.3.12/
```

---

## 2. Режим извлечения отсутствующих файлов

Если:

```json
"IsMissingFilesMode": true
```

то программа:

1. Сравнивает `Before` и `After`
2. Ищет файлы, которые были в старой версии, но отсутствуют в новой
3. Копирует их в отдельную папку:

```text
MISSING/
```

Этот режим полезен для:

- анализа потерь файлов
- проверки неполных сборок
- восстановления содержимого
- ручной инспекции изменений

---

## Структура выходных данных

После запуска могут быть сформированы следующие данные:

### `manifest.json`

Содержит список изменений между двумя версиями.

Примеры типов изменений:

- `Added`
- `Deleted`
- `Changed`
- `Unchanged`
- `Retranslate`

### Папка обновления

Например:

```text
99.3.12/
```

Внутри неё будет лежать только то, что действительно должно быть доставлено как обновление.

### Папка отсутствующих файлов

Если активирован режим извлечения отсутствующих файлов:

```text
MISSING/
```

---

## Пример сценария использования

### Исходные данные

```text
C:\Builds\Freelancer_99.3.11
C:\Builds\Freelancer_99.3.12
```

### Конфиг

```json
{
	"Before": "C:\\Builds\\Freelancer_99.3.11",
	"After": "C:\\Builds\\Freelancer_99.3.12",
	"Version": "99.3.12",
	"IsMissingFilesMode": false
}
```

### Результат

После запуска будут получены:

```text
manifest.json
99.3.12/
```

Эти данные можно использовать для дальнейшей упаковки и публикации обновления, например в экосистеме **Lizerium Launcher**.

---

## Другое

> [!TIP]
> Проект используется как часть пайплайна подготовки обновлений в экосистеме **Lizerium**.

> [!IMPORTANT]
> Для корректного результата рекомендуется сравнивать уже подготовленные и финальные сборки, а не промежуточные рабочие директории.

---

## Author

Developed and maintained by **Dvurechensky**
Part of the **Lizerium** ecosystem

- Website: [https://dvurechensky.pro](https://dvurechensky.pro)
- GitHub: [https://github.com/Dvurechensky](https://github.com/Dvurechensky)

---
