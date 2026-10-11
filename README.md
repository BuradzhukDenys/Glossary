# Glossary

## Description
A console-based glossary application built with C#. This project demonstrates the practical use of C# collections and basic data persistence.

## Features
* Translate words from English to Ukrainian
* Add new words to the dictionary
* Delete existing words from the dictionary
* View and check all available words
* Quiz Mode: The program displays a random English word from the glossary, and you must translate it to Ukrainian

## Technologies
* C# Collections (`Dictionary`)
* LINQ
* IEnumerable
* File I/O (Saving and loading vocabulary from a text file)
* CRUD operations

## Data Storage
* The file is created at `Glossary/glossary.txt` as for work dictionary;
* Format: One word per line `english - ukrainian`;
* On first launch glossary is empty;

## Requirements
.NET 8 SDK

## How to run
```
git clone https://github.com/BuradzhukDenys/Glossary.git
cd Glossary
dotnet run
```
