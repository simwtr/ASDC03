using System;
using System.IO;

class Node<T>
{
    public T Data { get; set; }
    public Node<T> Next { get; set; }
}

class LinkedList<T> where T : Record
{
    private Node<T> head;

    public void TraverseAndPrint()
    {
        Node<T> current = head;
        while (current != null)
        {
            T data = current.Data;
            Console.WriteLine(
                $"ID: {data.Id}, Name: {data.Name}, Age: {data.Age}, IsActive: {data.IsActive}, Salary: {data.Salary}"
            );
            current = current.Next;
        }
    }

    public void Insert(T data)
    {
        Node<T> newNode = new Node<T> { Data = data };

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node<T> current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
    }

    public bool Search(T data)
    {
        Node<T> current = head;
        while (current != null)
        {
            if (current.Data.Id == data.Id)
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    public void Delete(T data)
    {
        if (head == null)
        {
            return;
        }

        if (head.Data.Id == data.Id)
        {
            head = head.Next;
            return;
        }

        Node<T> current = head;
        Node<T> previous = null;
        while (current != null)
        {
            if (current.Data.Id == data.Id)
            {
                previous.Next = current.Next;
                return;
            }
            previous = current;
            current = current.Next;
        }
    }
}

public class Record
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string IsActive { get; set; }
    public double Salary { get; set; }

    public Record()
    {
        Id = 0;
        Name = "";
        Age = 0;
        IsActive = "no";
        Salary = 0.0;
    }

    public Record(Record other)
    {
        Id = other.Id;
        Name = other.Name;
        Age = other.Age;
        IsActive = other.IsActive;
        Salary = other.Salary;
    }

    public static Record ReadFromStream(TextReader stream)
    {
        var record = new Record();
        var fields = stream.ReadLine().Split(',');
        record.Id = int.Parse(fields[0]);
        record.Name = fields[1];
        record.Age = int.Parse(fields[2]);
        record.IsActive = fields[3];
        record.Salary = double.Parse(fields[4]);
        return record;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var data = new Record[50];

        using (var file = new StreamReader("records.txt"))
        {
            for (int i = 0; i < 50; i++)
            {
                data[i] = Record.ReadFromStream(file);
            }
        }

        LinkedList<Record> linkedList = new LinkedList<Record>();

        // Вставка элементов в связанный список
        foreach (var record in data)
        {
            linkedList.Insert(record);
        }

        // Обход и вывод элементов связанного списка
        Console.WriteLine("Linked List:");
        linkedList.TraverseAndPrint();

        // Поиск элемента в связанном списке
        int searchId = 47;
        bool isFound = linkedList.Search(new Record { Id = searchId });
        Console.WriteLine($"Record with ID {searchId} found: {isFound}");

        // Удаление элемента из связанного списка
        int deleteId = 20;
        linkedList.Delete(new Record { Id = deleteId });
        Console.WriteLine($"Record with ID {deleteId} deleted.");

        // Повторный обход и вывод элементов связанного списка после удаления
        Console.WriteLine("Linked List after deletion:");
        linkedList.TraverseAndPrint();
    }

    static void PrintData(Record[] data)
    {
        foreach (var record in data)
        {
            Console.WriteLine(
                $"Id: {record.Id}, Name: {record.Name}, Age: {record.Age}, IsActive: {record.IsActive}, Salary: {record.Salary}"
            );
        }
    }
}

