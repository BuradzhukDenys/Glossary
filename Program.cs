using GlossaryTrainer;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

string _projectDirectory = Directory.GetCurrentDirectory();
string _glossaryPath = Path.Combine(_projectDirectory, "Glossary");

FileManager filemanager = new(_glossaryPath, "glossary.txt");
GlossaryMenu program = new();
program.Run(filemanager);