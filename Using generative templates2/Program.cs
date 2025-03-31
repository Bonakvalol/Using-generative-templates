using System;

public abstract class Document
{
    public abstract void Create();
}

public class TextDocument : Document
{
    public override void Create()
    {
        Console.WriteLine("Создание текстового документа");
    }
}

public class GraphicDocument : Document
{
    public override void Create()
    {
        Console.WriteLine("Создание графического документа");
    }
}

public class SpreadsheetDocument : Document
{
    public override void Create()
    {
        Console.WriteLine("Создание табличного документа");
    }
}

public abstract class DocumentFactory
{
    public abstract Document CreateDocument();
}

public class TextDocumentFactory : DocumentFactory
{
    public override Document CreateDocument()
    {
        return new TextDocument();
    }
}

public class GraphicDocumentFactory : DocumentFactory
{
    public override Document CreateDocument()
    {
        return new GraphicDocument();
    }
}

public class SpreadsheetDocumentFactory : DocumentFactory
{
    public override Document CreateDocument()
    {
        return new SpreadsheetDocument();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Выберите тип документа для создания:");
        Console.WriteLine("1 - Текстовый документ");
        Console.WriteLine("2 - Графический документ");
        Console.WriteLine("3 - Табличный документ");
        string choice = Console.ReadLine();

        DocumentFactory factory = null;

        switch (choice)
        {
            case "1":
                factory = new TextDocumentFactory();
                break;
            case "2":
                factory = new GraphicDocumentFactory();
                break;
            case "3":
                factory = new SpreadsheetDocumentFactory();
                break;
            default:
                Console.WriteLine("Некорректный выбор");
                return;
        }

        Document document = factory.CreateDocument();
        document.Create();

        Console.WriteLine("Документ создан");
    }
}
