using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

internal class Program
{
    private static Random _rnd = new();
    private static readonly StringBuilder trace = new();

    private static void Main(string[] args)
    {
        CompareByteArrays();
        FindByteArrayInAList();

        Trace.WriteLine(trace.ToString());
    }

    private static void CompareByteArrays()
    {
        int sizeOfArray = 1000;
        byte[] a1 = CreateByteArray(sizeOfArray);
        byte[] a2 = (byte[])a1.Clone();

        MeasureExecution(() => Compare(a1, a2, Comparer.CompareByteArrays_Utf8ToString));
        MeasureExecution(() => Compare(a1, a2, Comparer.CompareByteArrays_SpanSequenceEquals));
        MeasureExecution(() => Compare(a1, a2, Comparer.CompareByteArrays_SequenceEquals));
        MeasureExecution(() => Compare(a1, a2, Comparer.CompareByteArrays_Operator));
        MeasureExecution(() => Compare(a1, a2, Comparer.CompareByteArrays_Equals));
    }

    private static void FindByteArrayInAList()
    {
        int sizeOfList = 40*1000000;
        int sizeOfArray = 10;
        List<byte[]> list = CreateList(sizeOfList, sizeOfArray);
        byte[] lastElement = (byte[])list.Last().Clone();

        MeasureExecution(() => SearchInList(lastElement, list, Comparer.CompareByteArrays_Utf8ToString));
        MeasureExecution(() => SearchInList(lastElement, list, Comparer.CompareByteArrays_SpanSequenceEquals));
        MeasureExecution(() => SearchInList(lastElement, list, Comparer.CompareByteArrays_SequenceEquals));
        MeasureExecution(() => SearchInList(lastElement, list, Comparer.CompareByteArrays_Operator));
        MeasureExecution(() => SearchInList(lastElement, list, Comparer.CompareByteArrays_Equals));
        
    }

    private static List<byte[]> CreateList(int sizeOfList, int sizeOfElements)
    {
        List<byte[]> list = [];
        for(int i = 0; i < sizeOfList; i++)
            list.Add(CreateByteArray(sizeOfElements));

        return list;
    }

    private static byte[] CreateByteArray(int size)
    {
        var array = new byte[size];
        _rnd.NextBytes(array);

        return array;
    }

    private static void SearchInList(byte[] elementToSearch, List<byte[]> list, Func<byte[]?, byte[]?, bool> comparer)
    {
        trace.Append($"{"SEARCH Method:",-16}{comparer.GetMethodInfo().Name,-40}");        
        var areEqual = list.Contains(elementToSearch, new ByteArrayEqualityComparer {Comparer = comparer});        
        PrintResult(areEqual);
    }

    private static void Compare(byte[] a1, byte[] a2, Func<byte[]?, byte[]?, bool> comparer)
    {
        trace.Append($"{"Method:",-16}{comparer.GetMethodInfo().Name,-40}");
        bool areEqual = comparer(a1, a2);
        PrintResult(areEqual);
    }

    private static void PrintResult(bool areEqual)
    {
        if (areEqual)   trace.Append(" Found     ");
        else            trace.Append(" Not found ");
    }

    private static void MeasureExecution(Action action)
    {
        var sw = Stopwatch.StartNew();
        action();
        sw.Stop();
        trace.AppendLine($" - Execution took: {sw.Elapsed}");
    }
}

internal class ByteArrayEqualityComparer : IEqualityComparer<byte[]>
{
    public Func<byte[]?, byte[]?, bool>? Comparer {get; set;}

    public bool Equals(byte[]? x, byte[]? y) => Comparer!(x, y);

    public int GetHashCode([DisallowNull] byte[] obj) => obj.GetHashCode();
}

internal class Comparer
{
    public static bool CompareByteArrays_Operator(byte[]? a1, byte[]? a2) => a1 == a2;
    public static bool CompareByteArrays_Equals(byte[]? a1, byte[]? a2) => a1!.Equals(a2);
    internal static bool CompareByteArrays_SequenceEquals(byte[]? a1, byte[]? a2) => a1!.SequenceEqual(a2!);
    internal static bool CompareByteArrays_SpanSequenceEquals(byte[]? a1, byte[]? a2) => a1.AsSpan().SequenceEqual(a2);
    internal static bool CompareByteArrays_Utf8ToString(byte[]? arg1, byte[]? arg2) => Encoding.UTF8.GetString(arg1!) == Encoding.UTF8.GetString(arg2!);
}
